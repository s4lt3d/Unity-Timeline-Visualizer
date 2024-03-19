using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class MacroParameters
{
    private string id = "";

    [XmlAttribute("id")]
    public string Id
    {
        get => id;
        set => id = value;
    }
}
[XmlRoot(ElementName="EDIT")]
public class WaveformDataSet
{
    private List<Transport> transport = new();
    private List<Track> tracks = new();
    private string appVersion = "";
    private string projectID = "";
    private string creationTime = "";
    private string lastSignificantChange = "";
    private string modifiedBy = "";

    [XmlElement("TRANSPORT")]
    public List<Transport> Transport
    {
        get => transport;
        set => transport = value;
    }

    [XmlElement("TRACK")]
    public List<Track> Tracks
    {
        get => tracks;
        set => tracks = value;
    }

    [XmlAttribute("appVersion")]
    public string AppVersion
    {
        get => appVersion;
        set => appVersion = value;
    }

    [XmlAttribute("projectID")]
    public string ProjectID
    {
        get => projectID;
        set => projectID = value;
    }

    [XmlAttribute("creationTime")]
    public string CreationTime
    {
        get => creationTime;
        set => creationTime = value;
    }

    [XmlAttribute("lastSignificantChange")]
    public string LastSignificantChange
    {
        get => lastSignificantChange;
        set => lastSignificantChange = value;
    }

    [XmlAttribute("modifiedBy")]
    public string ModifiedBy
    {
        get => modifiedBy;
        set => modifiedBy = value;
    }
}

public class Transport
{
    private float endToEnd;
    private float scrubInterval;
    private float position;
    private float loopPoint1;
    private float loopPoint2;

    [XmlAttribute("endToEnd")]
    public string EndToEnd
    {
        get => endToEnd.ToString();
        set { if(float.TryParse(value, out float result)) endToEnd = result;
            else
            {
                Debug.Log($"endToEnd is invalid {value}");
            }
        }
    }

    [XmlAttribute("scrubInterval")]
    public string ScrubInterval
    {
        get => scrubInterval.ToString();
        set { if(float.TryParse(value, out float result)) scrubInterval = result;
            else
            {
                Debug.Log($"scrubInterval is invalid {value}");
            }
        }
    }

    [XmlAttribute("position")]
    public string Position
    {
        get => position.ToString();
        set { if(float.TryParse(value, out float result)) position = result;
            else
            {
                Debug.Log($"position is invalid {value}");
            }
        }
    }

    [XmlAttribute("loopPoint1")]
    public string LoopPoint1
    {
        get => loopPoint1.ToString();
        set { if(float.TryParse(value, out float result)) loopPoint1 = result;
            else
            {
                Debug.Log($"loopPoint1 is invalid {value}");
            }
        }
    }

    [XmlAttribute("loopPoint2")]
    public string LoopPoint2
    {
        get => loopPoint2.ToString();
        set { if(float.TryParse(value, out float result)) loopPoint2 = result;
            else
            {
                Debug.Log($"loopPoint2 is invalid {value}");
            }
        }
    }
}

public static class Util
{
    public static Color ParseColor(string value)
    {
        var val = value.StartsWith("#") ? value : "#" + value;
        if(UnityEngine.ColorUtility.TryParseHtmlString(val, out Color result))
            return result;
         
        Debug.Log($"colour is invalid: {value}");
        return Color.red;
    }
    
}

public class Track
{
    private string modifiers;
    private List<MacroParameters> macroParameters;
    private List<WaveformAudioClip> audioClips;
    private List<Plugin> plugin;
    private List<OutputDevices> outputDevices;
    private int id;
    private float midiVProp;
    private float midiVOffset;
    private Color colour = Color.red;
    private float height;

    [XmlElement("MODIFIERS")]
    public string Modifiers
    {
        get => modifiers;
        set => modifiers = value;
    }

    [XmlElement("MACROPARAMETERS")]
    public List<MacroParameters> MacroParameters
    {
        get => macroParameters;
        set => macroParameters = value;
    }

    [XmlElement("AUDIOCLIP")]
    public List<WaveformAudioClip> AudioClips
    {
        get => audioClips;
        set => audioClips = value;
    }

    [XmlElement("PLUGIN")]
    public List<Plugin> Plugin
    {
        get => plugin;
        set => plugin = value;
    }

    [XmlElement("OUTPUTDEVICES")]
    public List<OutputDevices> OutputDevices
    {
        get => outputDevices;
        set => outputDevices = value;
    }

    [XmlAttribute("id")]
    public string Id
    {
        get => id.ToString();
        set {
            if(int.TryParse(value, out int result)) id = result;
            else
            {
                Debug.Log($"id is invalid: {id}");
            }
        }
    }

    [XmlAttribute("midiVProp")]
    public string MidiVProp
    {
        get => midiVProp.ToString();
        set { if(float.TryParse(value, out float result)) midiVProp = result;
            else
            {
                Debug.Log($"midiVProp is invalid {value}");
            }
        }
    }

    [XmlAttribute("midiVOffset")]
    public string MidiVOffset
    {
        get => midiVOffset.ToString();
        set { if(float.TryParse(value, out float result)) midiVOffset = result;
            else
            {
                Debug.Log($"midiVOffset is invalid {value}");
            }
        }
    }

    [XmlAttribute("colour")]
    public string Colour
    {
        get => colour.ToHexString().Replace("#", "");
        set { colour = Util.ParseColor(value); }
    }

    [XmlAttribute("height")]
    public string Height
    {
        get => height.ToString();
        set { if(float.TryParse(value, out float result)) height = result;
            else
            {
                Debug.Log($"height is invalid {value}");
            }
        }
    }
}

public class WaveformAudioClip
{
    private List<LoopInfo> loopInfo;
    private string name;
    private float start;
    private float length;
    private float offset;
    private int id;
    private string source;
    private int sync;
    private int elastiqueMode;
    private float pan;
    private Color colour;
    private string proxyAllowed;
    private string resamplingQuality;
    private float fadeIn;
    private float fadeOut;

    [XmlElement("LOOPINFO")]
    public List<LoopInfo> LoopInfo
    {
        get => loopInfo;
        set => loopInfo = value;
    }

    [XmlAttribute("name")]
    public string Name
    {
        get => name;
        set => name = value;
    }

    [XmlAttribute("start")]
    public string Start
    {
        get => start.ToString();
        set { if(float.TryParse(value, out float result)) start = result;
            else
            {
                Debug.Log($"start is invalid {value}");
            }
        }
    }

    [XmlAttribute("length")]
    public string Length
    {
        get => length.ToString();
        set { if(float.TryParse(value, out float result)) length = result;
            else
            {
                Debug.Log($"length is invalid {value}");
            }
        }
    }

    [XmlAttribute("offset")]
    public string Offset
    {
        get => offset.ToString();
        set { if(float.TryParse(value, out float result)) offset = result;
            else
            {
                Debug.Log($"offset is invalid {value}");
            }
        }
    }

    [XmlAttribute("id")]
    public string Id
    {
        get => id.ToString();
        set {
            if(int.TryParse(value, out int result)) id = result;
            else
            {
                Debug.Log($"id is invalid: {id}");
            }
        }
    }

    [XmlAttribute("source")]
    public string Source
    {
        get => source;
        set => source = value;
    }

    [XmlAttribute("sync")]
    public string Sync
    {
        get => sync.ToString();
        set {
            if(int.TryParse(value, out int result)) sync = result;
            else
            {
                Debug.Log($"sync is invalid: {sync}");
            }
        }
    }

    [XmlAttribute("elastiqueMode")]
    public string ElastiqueMode
    {
        get => elastiqueMode.ToString();
        set {
            if(int.TryParse(value, out int result)) elastiqueMode = result;
            else
            {
                Debug.Log($"elastiqueMode is invalid: {elastiqueMode}");
            }
        }
    }

    [XmlAttribute("pan")]
    public string Pan
    {
        get => pan.ToString();
        set { if(float.TryParse(value, out float result)) pan = result;
            else
            {
                Debug.Log($"pan is invalid {value}");
            }
        }
    }

    [XmlAttribute("colour")]
    public string Colour
    {
        get => colour.ToHexString().Replace("#", "");
        set { colour = Util.ParseColor(value); }
    }

    [XmlAttribute("proxyAllowed")]
    public string ProxyAllowed
    {
        get => proxyAllowed;
        set => proxyAllowed = value;
    }

    [XmlAttribute("resamplingQuality")]
    public string ResamplingQuality
    {
        get => resamplingQuality;
        set => resamplingQuality = value;
    }

    [XmlAttribute("fadeIn")]
    public string FadeIn
    {
        get => fadeIn.ToString();
        set { if(float.TryParse(value, out float result)) fadeIn = result;
            else
            {
                Debug.Log($"fadeIn is invalid {value}");
            }
        }
    }

    [XmlAttribute("fadeOut")]
    public string FadeOut
    {
        get => fadeOut.ToString();
        set { if(float.TryParse(value, out float result)) fadeOut = result;
            else
            {
                Debug.Log($"fadeOut is invalid {value}");
            }
        }
    }
}

public class LoopInfo
{
    private float rootNote;
    private float numBeats;
    private int oneShot;
    private int denominator;
    private int numerator;
    private float bpm;
    private int inMarker;
    private int outMarker;

    [XmlAttribute("rootNote")]
    public string RootNote
    {
        get => rootNote.ToString();
        set { if(float.TryParse(value, out float result)) rootNote = result;
            else
            {
                Debug.Log($"rootNote is invalid {value}");
            }
        }
    }

    [XmlAttribute("numBeats")]
    public string NumBeats
    {
        get => numBeats.ToString();
        set { if(float.TryParse(value, out float result)) numBeats = result;
            else
            {
                Debug.Log($"numBeats is invalid {value}");
            }
        }
    }

    [XmlAttribute("oneShot")]
    public string OneShot
    {
        get => oneShot.ToString();
        set {
            if(int.TryParse(value, out int result)) oneShot = result;
            else
            {
                Debug.Log($"oneShot is invalid: {oneShot}");
            }
        }
    }

    [XmlAttribute("denominator")]
    public string Denominator
    {
        get => denominator.ToString();
        set {
            if(int.TryParse(value, out int result)) denominator = result;
            else
            {
                Debug.Log($"denominator is invalid: {denominator}");
            }
        }
    }

    [XmlAttribute("numerator")]
    public string Numerator
    {
        get => numerator.ToString();
        set {
            if(int.TryParse(value, out int result)) numerator = result;
            else
            {
                Debug.Log($"numerator is invalid: {numerator}");
            }
        }
    }

    [XmlAttribute("bpm")]
    public string Bpm
    {
        get => bpm.ToString();
        set { if(float.TryParse(value, out float result)) bpm = result;
            else
            {
                Debug.Log($"bpm is invalid {value}");
            }
        }
    }

    [XmlAttribute("inMarker")]
    public string InMarker
    {
        get => inMarker.ToString();
        set {
            if(int.TryParse(value, out int result)) inMarker = result;
            else
            {
                Debug.Log($"inMarker is invalid: {inMarker}");
            }
        }
    }

    [XmlAttribute("outMarker")]
    public string OutMarker
    {
        get => outMarker.ToString();
        set {
            if(int.TryParse(value, out int result)) outMarker = result;
            else
            {
                Debug.Log($"outMarker is invalid: {outMarker}");
            }
        }
    }
}

public class Plugin
{
    private string modifierAssignments;
    private List<MacroParameters> macroParameters;
    private string type;
    private string id;
    private string enabled;

    [XmlElement("MODIFIERASSIGNMENTS")]
    public string ModifierAssignments
    {
        get => modifierAssignments;
        set => modifierAssignments = value;
    }

    [XmlElement("MACROPARAMETERS")]
    public List<MacroParameters> MacroParameters
    {
        get => macroParameters;
        set => macroParameters = value;
    }

    [XmlAttribute("type")]
    public string Type
    {
        get => type;
        set => type = value;
    }

    [XmlAttribute("id")]
    public string Id
    {
        get => id;
        set => id = value;
    }

    [XmlAttribute("enabled")]
    public string Enabled
    {
        get => enabled;
        set => enabled = value;
    }
}

public class OutputDevices
{
    private List<Device> devices;

    [XmlElement("DEVICE")]
    public List<Device> Devices
    {
        get => devices;
        set => devices = value;
    }
}

public class Device
{
    private string name;

    [XmlAttribute("name")]
    public string Name
    {
        get => name;
        set => name = value;
    }
}