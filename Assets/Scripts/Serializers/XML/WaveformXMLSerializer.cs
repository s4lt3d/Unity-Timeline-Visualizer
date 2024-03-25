using System.IO;
using System.Linq;
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

        var wds = new WaveformDataSet
        {
            AppVersion = wxml.AppVersion,
            CreationTime = wxml.CreationTime,
            LastSignificantChange = wxml.LastSignificantChange,
            ModifiedBy = wxml.ModifiedBy,
            ProjectID = wxml.ProjectID,
            Transport = wxml.Transport.Select(t => new Transport
            {
                EndToEnd = float.Parse(t.EndToEnd),
                ScrubInterval = float.Parse(t.ScrubInterval),
                Position = float.Parse(t.Position),
                LoopPoint1 = float.Parse(t.LoopPoint1),
                LoopPoint2 = float.Parse(t.LoopPoint2)
            }).ToList(),
            Tracks = wxml.Tracks.Select(t => new Track
            {
                Id = int.Parse(t.Id),
                MidiVProp = float.Parse(t.MidiVProp),
                MidiVOffset = float.Parse(t.MidiVOffset),
                Colour = ValidationUtil.ParseColor(t.Colour),
                Height = float.Parse(t.Height),
                Modifiers = t.Modifiers,
                MacroParameters = t.MacroParameters.Select(mp => new MacroParameters { Id = mp.Id }).ToList(),
                AudioClips = t.AudioClips.Select(ac => new WaveformAudioClip
                {
                    Id = int.Parse(ac.Id),
                    Name = ac.Name,
                    Start = float.Parse(ac.Start),
                    Length = float.Parse(ac.Length),
                    Offset = float.Parse(ac.Offset),
                    Source = ac.Source,
                    Sync = int.Parse(ac.Sync),
                    ElastiqueMode = int.Parse(ac.ElastiqueMode),
                    Pan = float.Parse(ac.Pan),
                    Colour = ValidationUtil.ParseColor(ac.Colour),
                    ProxyAllowed = ac.ProxyAllowed,
                    ResamplingQuality = ac.ResamplingQuality,
                    FadeIn = float.Parse(ac.FadeIn),
                    FadeOut = float.Parse(ac.FadeOut),
                    LoopInfo = ac.LoopInfo.Select(li => new LoopInfo
                    {
                        RootNote = float.Parse(li.RootNote),
                        NumBeats = float.Parse(li.NumBeats),
                        OneShot = int.Parse(li.OneShot),
                        Denominator = int.Parse(li.Denominator),
                        Numerator = int.Parse(li.Numerator),
                        Bpm = float.Parse(li.Bpm),
                        InMarker = int.Parse(li.InMarker),
                        OutMarker = int.Parse(li.OutMarker)
                    }).ToList()
                }).ToList(),
                Plugin = t.Plugin.Select(p => new Plugin
                {
                    Id = p.Id,
                    Type = p.Type,
                    Enabled = p.Enabled,
                    ModifierAssignments = p.ModifierAssignments,
                    MacroParameters = p.MacroParameters.Select(mp => new MacroParameters { Id = mp.Id }).ToList()
                }).ToList(),
                OutputDevices = t.OutputDevices.Select(od => new OutputDevices
                {
                    Devices = od.Devices.Select(d => new Device { Name = d.Name }).ToList()
                }).ToList()
            }).ToList()
        };

        return wds;
    }

    public WaveformDataSet LoadFromFile(string path)
    {
        if (!File.Exists(path))
            return null;

        var xml = File.ReadAllText(path);
        return Deserialize(xml);
    }

    public void SaveToFile(string path, WaveformDataSet waveformDataSetXMLDTOInstance)
    {
        TextWriter tw = new StreamWriter(path);
        tw.Write(Serialize(waveformDataSetXMLDTOInstance));
        tw.Close();
    }

    public void InitializeService()
    {
        // empty
    }

    public void CleanupService()
    {
        // empty
    }
}