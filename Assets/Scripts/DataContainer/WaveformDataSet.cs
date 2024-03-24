using System.Collections.Generic;
using UnityEngine;

public class WaveformDataSet
{
    public List<Transport> Transport { get; set; }
    public List<Track> Tracks { get; set; }
    public string AppVersion { get; set; }
    public string ProjectID { get; set; }
    public string CreationTime { get; set; }
    public string LastSignificantChange { get; set; }
    public string ModifiedBy { get; set; }
}
public class MacroParameters
{
    public string Id { get; set; }
}

public class Transport
{
    public float EndToEnd { get; set; }
    public float ScrubInterval { get; set; }
    public float Position { get; set; }
    public float LoopPoint1 { get; set; }
    public float LoopPoint2 { get; set; }
}

public class Track
{
    public string Modifiers { get; set; }
    public List<MacroParameters> MacroParameters { get; set; }
    public List<WaveformAudioClip> AudioClips { get; set; }
    public List<Plugin> Plugin { get; set; }
    public List<OutputDevices> OutputDevices { get; set; }
    public int Id { get; set; }
    public float MidiVProp { get; set; }
    public float MidiVOffset { get; set; }
    public Color Colour { get; set; }
    public float Height { get; set; }
}

public class WaveformAudioClip
{
    public List<LoopInfo> LoopInfo { get; set; }
    public string Name { get; set; }
    public float Start { get; set; }
    public float Length { get; set; }
    public float Offset { get; set; }
    public int Id { get; set; }
    public string Source { get; set; }
    public int Sync { get; set; }
    public int ElastiqueMode { get; set; }
    public float Pan { get; set; }
    public Color Colour { get; set; }
    public string ProxyAllowed { get; set; }
    public string ResamplingQuality { get; set; }
    public float FadeIn { get; set; }
    public float FadeOut { get; set; }
}

public class LoopInfo
{
    public float RootNote { get; set; }
    public float NumBeats { get; set; }
    public int OneShot { get; set; }
    public int Denominator { get; set; }
    public int Numerator { get; set; }
    public float Bpm { get; set; }
    public int InMarker { get; set; }
    public int OutMarker { get; set; }
}

public class Plugin
{
    public string ModifierAssignments { get; set; }
    public List<MacroParameters> MacroParameters { get; set; }
    public string Type { get; set; }
    public string Id { get; set; }
    public string Enabled { get; set; }
}

public class OutputDevices
{
    private List<Device> devices;
    public List<Device> Devices { get; set; }
}

public class Device
{
    private string name;
    public string Name { get; set; }
}