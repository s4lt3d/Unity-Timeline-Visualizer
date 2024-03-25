using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

public class WaveformXMLSerializer : IWaveformSerializer
{
    public string Serialize(WaveformDataSet waveformDataSet)
    {
        var result = "";

        // Set the culture to avoid serialization issues in different countries
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        var serializer = new XmlSerializer(typeof(WaveformDataSetXMLDTO));
        
        WaveformDataSetXMLDTO wxml = DataSetToXMLDTO(waveformDataSet);
        
        using (var writer = new StringWriter())
        {
            serializer.Serialize(writer, wxml);
            result = writer.ToString();
        }

        Thread.CurrentThread.CurrentCulture = currentCulture;
        return result;
    }

    public WaveformDataSet Deserialize(string xml)
    {
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        var serializer = new XmlSerializer(typeof(WaveformDataSetXMLDTO));
        WaveformDataSetXMLDTO wxml = null;
        using (var reader = new StringReader(xml))
        {
            wxml = (WaveformDataSetXMLDTO)serializer.Deserialize(reader);
        }

        var wds = DataSetToDTO(wxml);

        Thread.CurrentThread.CurrentCulture = currentCulture;
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

    private static WaveformDataSet DataSetToDTO(WaveformDataSetXMLDTO wxml)
    {
        var wds = new WaveformDataSet();

        if (wxml == null)
            return wds;

        wds = new WaveformDataSet
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
                Colour = SerializeUtil.ParseColor(t.Colour),
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
                    Colour = SerializeUtil.ParseColor(ac.Colour),
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

    private static WaveformDataSetXMLDTO DataSetToXMLDTO(WaveformDataSet wds)
    {
        if (wds == null) return new WaveformDataSetXMLDTO();

        var wxml = new WaveformDataSetXMLDTO
        {
            AppVersion = wds.AppVersion,
            CreationTime = wds.CreationTime,
            LastSignificantChange = wds.LastSignificantChange,
            ModifiedBy = wds.ModifiedBy,
            ProjectID = wds.ProjectID,
            Transport = wds.Transport.Select(t => new TransportXMLDTO
            {
                EndToEnd = t.EndToEnd.ToString(),
                ScrubInterval = t.ScrubInterval.ToString(),
                Position = t.Position.ToString(),
                LoopPoint1 = t.LoopPoint1.ToString(),
                LoopPoint2 = t.LoopPoint2.ToString()
            }).ToList(),
            Tracks = wds.Tracks.Select(t => new TrackXMLDTO
            {
                Id = t.Id.ToString(),
                MidiVProp = t.MidiVProp.ToString(),
                MidiVOffset = t.MidiVOffset.ToString(),
                Colour = SerializeUtil.SerializeColor(t.Colour),
                Height = t.Height.ToString(),
                Modifiers = t.Modifiers,
                MacroParameters = t.MacroParameters.Select(mp => new MacroParametersXMLDTO { Id = mp.Id }).ToList(),
                AudioClips = t.AudioClips.Select(ac => new WaveformAudioClipXMLDTO
                {
                    Id = ac.Id.ToString(),
                    Name = ac.Name,
                    Start = ac.Start.ToString(),
                    Length = ac.Length.ToString(),
                    Offset = ac.Offset.ToString(),
                    Source = ac.Source,
                    Sync = ac.Sync.ToString(),
                    ElastiqueMode = ac.ElastiqueMode.ToString(),
                    Pan = ac.Pan.ToString(),
                    Colour = SerializeUtil.SerializeColor(ac.Colour),
                    ProxyAllowed = ac.ProxyAllowed,
                    ResamplingQuality = ac.ResamplingQuality,
                    FadeIn = ac.FadeIn.ToString(),
                    FadeOut = ac.FadeOut.ToString(),
                    LoopInfo = ac.LoopInfo.Select(li => new LoopInfoXMLDTO
                    {
                        RootNote = li.RootNote.ToString(),
                        NumBeats = li.NumBeats.ToString(),
                        OneShot = li.OneShot.ToString(),
                        Denominator = li.Denominator.ToString(),
                        Numerator = li.Numerator.ToString(),
                        Bpm = li.Bpm.ToString(),
                        InMarker = li.InMarker.ToString(),
                        OutMarker = li.OutMarker.ToString()
                    }).ToList()
                }).ToList(),
                Plugin = t.Plugin.Select(p => new PluginXMLDTO
                {
                    Id = p.Id,
                    Type = p.Type,
                    Enabled = p.Enabled,
                    ModifierAssignments = p.ModifierAssignments,
                    MacroParameters = p.MacroParameters.Select(mp => new MacroParametersXMLDTO { Id = mp.Id }).ToList()
                }).ToList(),
                OutputDevices = t.OutputDevices.Select(od => new OutputDevicesXMLDTO
                {
                    Devices = od.Devices.Select(d => new DeviceXMLDTO { Name = d.Name }).ToList()
                }).ToList()
            }).ToList()
        };

        return wxml;
    }
}