using System.IO;
using System.Xml.Serialization;

public class WaveformFileSerializer
{
    public string Serialize(WaveformDataSet waveformDataSetInstance)
    {
        var serializer = new XmlSerializer(typeof(WaveformDataSet));
        using (var writer = new StringWriter())
        {
            serializer.Serialize(writer, waveformDataSetInstance);
            return writer.ToString();
        }
    }
    
    public WaveformDataSet Deserialize(string xml)
    {
        var serializer = new XmlSerializer(typeof(WaveformDataSet));
        using (var reader = new StringReader(xml))
        {
            var result = (WaveformDataSet)serializer.Deserialize(reader);
            return result;
        }
    }

    public WaveformDataSet LoadFromFile(string path)
    {
        
        using (var tr = new StreamReader(path))
        {
            var serializer = new XmlSerializer(typeof(WaveformDataSet));
            var result = (WaveformDataSet)serializer.Deserialize(tr);
            return result;
        }
    }
    
    public void SaveToFile(string path, WaveformDataSet waveformDataSetInstance)
    {
        TextWriter tw = new StreamWriter(path);
        var serializer = new XmlSerializer(typeof(WaveformDataSet));
        serializer.Serialize(tw, waveformDataSetInstance);
    }
}