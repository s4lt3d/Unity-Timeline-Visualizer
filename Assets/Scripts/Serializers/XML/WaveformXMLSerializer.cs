using System.IO;
using System.Xml.Serialization;

public class WaveformXMLSerializer : IWaveformSerializer
{
    public string Serialize(WaveformDataSet waveformDataSetXMLDTOInstance)
    {
        var serializer = new XmlSerializer(typeof(WaveformDataSetXMLDTO));
        using (var writer = new StringWriter())
        {
            serializer.Serialize(writer, waveformDataSetXMLDTOInstance);
            return writer.ToString();
        }
    }
    
    public WaveformDataSet Deserialize(string xml)
    {
        var serializer = new XmlSerializer(typeof(WaveformDataSetXMLDTO));
        WaveformDataSetXMLDTO wxml = null;
        using (var reader = new StringReader(xml))
        {
            wxml = (WaveformDataSetXMLDTO)serializer.Deserialize(reader);
        }
        
        WaveformDataSet wds = new WaveformDataSet();
        wds.AppVersion = wxml.AppVersion;
        wds.CreationTime = wxml.CreationTime;
        wds.LastSignificantChange = wxml.LastSignificantChange;
        wds.ModifiedBy = wxml.ModifiedBy;
        wds.ProjectID = wxml.ProjectID;
        //wds.Transport = wxml.Transport;
        //wds.Tracks = wxml.Tracks;

        return wds;
    }

    public WaveformDataSet LoadFromFile(string path)
    {
        if (!File.Exists(path))
            return null;
        
        string xml = File.ReadAllText(path);        
        return Deserialize(xml);
    }
    
    public void SaveToFile(string path, WaveformDataSet waveformDataSetXMLDTOInstance)
    {
        TextWriter tw = new StreamWriter(path);
        tw.Write(Serialize(waveformDataSetXMLDTOInstance));
        tw.Close();
    }
}