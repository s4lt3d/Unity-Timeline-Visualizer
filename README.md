# Unity Timeline Visualizer 

## Project Brief
The aim of this project is to develop a Unity project that can read a provided XML file and visualize its contents within a Unity scene. This project focuses on using the `TRACK`, `TRANSPORT`, and `AUDIOCLIP` elements, along with their position information, to create an interactive visualization. The key is not to perfect the implementation but to lay the groundwork for the core functionality and document potential expansions or refinements.

![gifs/preview.gif](gifs/preview.gif)

### Project Goal
- Parse and visualize XML EDIT file data in a Unity scene.
- Utilize `TRACK` and `AUDIOCLIP` elements with their positional data effectively.
- Document additional architectural, planning, and consideration aspects for unimplemented features.
- Demonstrate Fragment and Vertex shaders
- Introduce basic system design for future expansion

### XML Parsing
- Implemented a robust XML parsing system within Unity to extract data from the provided XML file, focusing on key elements such as `TRACK`, `TRANSPORT`, and `AUDIOCLIP`.
-- Seperated responsibility from file parsing and data structure allowing for different file types or file versions to be handled correctly.
-- XML file importer handles simple validation of the data
- Developed error-handling mechanisms to manage inconsistencies in floats, colors, and ints. Further validation can be done. 

### Scene and Object Creation
- Created a loading screen which demonstrates basic 3D layout utilizing vertex shaders for nearly all animations. 
- Created a dynamic Unity scene that accurately represents the data parsed from the XML file. Each visual element corresponds to specific XML elements, providing a clear and interactive visualization.
- Utilized Unity's UI to lay out `AUDIOCLIP`, `TRANSPORT`, and `TRACK` elements in the scene.
-- Timeline supports drag and drop to arrange tracks. This drag and drop should work on mobile devices out of the box. 

### AudioClip + Track Visualization
- Visualized `AUDIOCLIP` elements with detailed positional information, placing them under their respective `TRACK` nodes at correct positions.
-- Waveform visualized through a fragment shader. The fragment shader has an extra ability to set the track on fire, but ran shot of time to implement this though it can easily be previewed. 
- Shaders mask correctly for the UI by using the stencil needed for UI masking. 

## Architecture
- The project uses a Service Locator pattern to register services, such as the waveform serializer, using interfaces so they may be swapped out without needed to changing dependencies. 
-- A blend of monobehaviors and regular objects can be registered as a service. This simplified service container provides most of the functionality needed within an application to remove concrete dependencies in core systems. 
- Async tasks are used to load data as if it were loaded from a url to demonstrate the use of async methodology in Unity. 

## Future Considerations
- The project could be completed to become a basic editor with a bit more work. 
### Audio Manager System
- A system should be developed for the changing in the UI to be registered with the current state of the waveform data. 
- Audio playback could easily be integrated as the audio data is currently in memory as an audio clip in order to generate the waveforms. 
### Sidebar
- The sidebar with the Clips, SFX, and Patterns could be expanded to provide a list of clips available as drag and drop, SFX would be plugins or effects which are commonly used, and patterns would be groups of clips and sfx commonly found together for ease of use. 
### Workspace History
- Expanding on the versioning of the data, a workspace history would work similar to git's version history with branching, authorship, history, and tags. This would allow multiple people to work on a project together and be able to share the results. They would also be able to go back and forth in edits to tune their project. 

## Discussions
- **Custom Shader Development:** A basic implementation of a fragment shader was written by hand. However, utilizing a visual shader tool is significantly easier and provides the same benefits and a hand written shader. 
-- The shader graph used was Amiplify Shader Editor which provides the raw shader code if performance tweaking becomes nessecary. 
