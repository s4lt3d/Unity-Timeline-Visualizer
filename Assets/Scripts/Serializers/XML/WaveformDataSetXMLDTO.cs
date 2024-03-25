using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

[XmlRoot(ElementName = "EDIT")]
public class WaveformDataSetXMLDTO
{
    [XmlElement("TRANSPORT")]
    public List<TransportXMLDTO> Transport { get; set; } = new();

    [XmlElement("TRACK")]
    public List<TrackXMLDTO> Tracks { get; set; } = new();

    [XmlAttribute("appVersion")]
    public string AppVersion { get; set; } = "";

    [XmlAttribute("projectID")]
    public string ProjectID { get; set; } = "";

    [XmlAttribute("creationTime")]
    public string CreationTime { get; set; } = "";

    [XmlAttribute("lastSignificantChange")]
    public string LastSignificantChange { get; set; } = "";

    [XmlAttribute("modifiedBy")]
    public string ModifiedBy { get; set; } = "";
}
public class MacroParametersXMLDTO
{
    [XmlAttribute("id")]
    public string Id { get; set; } = "";
}
public class TransportXMLDTO
{
    private float endToEnd;
    private float loopPoint1;
    private float loopPoint2;
    private float position;
    private float scrubInterval;

    [XmlAttribute("endToEnd")]
    public string EndToEnd
    {
        get => endToEnd.ToString();
        set => endToEnd = ValidationUtil.ParseFloat(value, "endToEnd");
    }

    [XmlAttribute("scrubInterval")]
    public string ScrubInterval
    {
        get => scrubInterval.ToString();
        set => scrubInterval = ValidationUtil.ParseFloat(value, "scrubInterval");
    }

    [XmlAttribute("position")]
    public string Position
    {
        get => position.ToString();
        set => position = ValidationUtil.ParseFloat(value, "position");
    }

    [XmlAttribute("loopPoint1")]
    public string LoopPoint1
    {
        get => loopPoint1.ToString();
        set => loopPoint1 = ValidationUtil.ParseFloat(value, "loopPoint1");
    }

    [XmlAttribute("loopPoint2")]
    public string LoopPoint2
    {
        get => loopPoint2.ToString();
        set => loopPoint2 = ValidationUtil.ParseFloat(value, "loopPoint2");
    }
}

public class TrackXMLDTO
{
    private Color colour = Color.red;
    private float height;
    private int id;
    private float midiVOffset;
    private float midiVProp;

    [XmlElement("MODIFIERS")]
    public string Modifiers { get; set; }

    [XmlElement("MACROPARAMETERS")]
    public List<MacroParametersXMLDTO> MacroParameters { get; set; }

    [XmlElement("AUDIOCLIP")]
    public List<WaveformAudioClipXMLDTO> AudioClips { get; set; }

    [XmlElement("PLUGIN")]
    public List<PluginXMLDTO> Plugin { get; set; }

    [XmlElement("OUTPUTDEVICES")]
    public List<OutputDevicesXMLDTO> OutputDevices { get; set; }

    [XmlAttribute("id")]
    public string Id
    {
        get => id.ToString();
        set => id = ValidationUtil.ParseInt(value, "id");
    }

    [XmlAttribute("midiVProp")]
    public string MidiVProp
    {
        get => midiVProp.ToString();
        set => midiVProp = ValidationUtil.ParseFloat(value, "midiVProp");
    }

    [XmlAttribute("midiVOffset")]
    public string MidiVOffset
    {
        get => midiVOffset.ToString();
        set => midiVOffset = ValidationUtil.ParseFloat(value, "midiVOffset");
    }

    [XmlAttribute("colour")]
    public string Colour
    {
        get => colour.ToHexString().Replace("#", "");
        set => colour = ValidationUtil.ParseColor(value);
    }

    [XmlAttribute("height")]
    public string Height
    {
        get => height.ToString();
        set => height = ValidationUtil.ParseFloat(value, "height");
    }
}

public class WaveformAudioClipXMLDTO
{
    private Color colour;
    private int elastiqueMode;
    private float fadeIn;
    private float fadeOut;
    private int id;
    private float length;
    private float offset;
    private float pan;
    private float start;
    private int sync;

    [XmlElement("LOOPINFO")]
    public List<LoopInfoXMLDTO> LoopInfo { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("start")]
    public string Start
    {
        get => start.ToString();
        set => start = ValidationUtil.ParseFloat(value, "start");
    }

    [XmlAttribute("length")]
    public string Length
    {
        get => length.ToString();
        set => length = ValidationUtil.ParseFloat(value, "length");
    }

    [XmlAttribute("offset")]
    public string Offset
    {
        get => offset.ToString();
        set => offset = ValidationUtil.ParseFloat(value, "offset");
    }

    [XmlAttribute("id")]
    public string Id
    {
        get => id.ToString();
        set => id = ValidationUtil.ParseInt(value, "id");
    }

    [XmlAttribute("source")]
    public string Source { get; set; }

    [XmlAttribute("sync")]
    public string Sync
    {
        get => sync.ToString();
        set => sync = ValidationUtil.ParseInt(value, "sync");
    }

    [XmlAttribute("elastiqueMode")]
    public string ElastiqueMode
    {
        get => elastiqueMode.ToString();
        set => elastiqueMode = ValidationUtil.ParseInt(value, "elastiqueMode");
    }

    [XmlAttribute("pan")]
    public string Pan
    {
        get => pan.ToString();
        set
        {
            if (float.TryParse(value, out var result)) pan = result;
            else Debug.Log($"pan is invalid {value}");
        }
    }

    [XmlAttribute("colour")]
    public string Colour
    {
        get => colour.ToHexString().Replace("#", "");
        set => colour = ValidationUtil.ParseColor(value);
    }

    [XmlAttribute("proxyAllowed")]
    public string ProxyAllowed { get; set; }

    [XmlAttribute("resamplingQuality")]
    public string ResamplingQuality { get; set; }

    [XmlAttribute("fadeIn")]
    public string FadeIn
    {
        get => fadeIn.ToString();
        set => fadeIn = ValidationUtil.ParseFloat(value, "fadeIn");
    }

    [XmlAttribute("fadeOut")]
    public string FadeOut
    {
        get => fadeOut.ToString();
        set => fadeOut = ValidationUtil.ParseFloat(value, "fadeOut");
    }
}

public class LoopInfoXMLDTO
{
    private float bpm;
    private int denominator;
    private int inMarker;
    private float numBeats;
    private int numerator;
    private int oneShot;
    private int outMarker;
    private float rootNote;

    [XmlAttribute("rootNote")]
    public string RootNote
    {
        get => rootNote.ToString();
        set => rootNote = ValidationUtil.ParseFloat(value, "rootNote");
    }

    [XmlAttribute("numBeats")]
    public string NumBeats
    {
        get => numBeats.ToString();
        set => numBeats = ValidationUtil.ParseFloat(value, "numBeats");
    }

    [XmlAttribute("oneShot")]
    public string OneShot
    {
        get => oneShot.ToString();
        set => oneShot = ValidationUtil.ParseInt(value, "oneShot");
    }

    [XmlAttribute("denominator")]
    public string Denominator
    {
        get => denominator.ToString();
        set => denominator = ValidationUtil.ParseInt(value, "denominator");
    }

    [XmlAttribute("numerator")]
    public string Numerator
    {
        get => numerator.ToString();
        set => numerator = ValidationUtil.ParseInt(value, "numerator");
    }

    [XmlAttribute("bpm")]
    public string Bpm
    {
        get => bpm.ToString();
        set => bpm = ValidationUtil.ParseFloat(value, "bpm");
    }

    [XmlAttribute("inMarker")]
    public string InMarker
    {
        get => inMarker.ToString();
        set => inMarker = ValidationUtil.ParseInt(value, "inMarker");
    }

    [XmlAttribute("outMarker")]
    public string OutMarker
    {
        get => outMarker.ToString();
        set => outMarker = ValidationUtil.ParseInt(value, "outMarker");
    }
}

public class PluginXMLDTO
{
    [XmlElement("MODIFIERASSIGNMENTS")]
    public string ModifierAssignments { get; set; }

    [XmlElement("MACROPARAMETERS")]
    public List<MacroParametersXMLDTO> MacroParameters { get; set; }

    [XmlAttribute("type")]
    public string Type { get; set; }

    [XmlAttribute("id")]
    public string Id { get; set; }

    [XmlAttribute("enabled")]
    public string Enabled { get; set; }
}

public class OutputDevicesXMLDTO
{
    [XmlElement("DEVICE")]
    public List<DeviceXMLDTO> Devices { get; set; }
}

public class DeviceXMLDTO
{
    [XmlAttribute("name")]
    public string Name { get; set; }
}