public interface IWaveformSerializer: IService
{
    string Serialize(WaveformDataSet waveformDataSetXMLDTOInstance);
    WaveformDataSet Deserialize(string xml);
    WaveformDataSet LoadFromFile(string path);
    void SaveToFile(string path, WaveformDataSet waveformDataSetXMLDTOInstance);
}