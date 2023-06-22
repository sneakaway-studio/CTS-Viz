
# CTS-Viz





## Instructions

### Install & Open Project

1. Install git, Github Desktop, git-lts, Unity Hub, Unity (see version)
1. Install Submodule (see [instructions](https://github.com/sneakaway-studio/SneakawayUtilities))
1. Open project: Unity Hub > click once on project name
1. Open scene: Unity Project > Scenes > Visualization
1. Change your Unity UI to look like the screenshot below.
1. Click Play button


### Select a timezone

1. Open project (see above)
1. Select the VizManager game object in Hierarchy
1. Expand the Scriptables > VizSettings folder in Project
1. Drag the specific visualization file to the VizSettings field in the VizManager
1. Click Play button


### Export Video 

#### Setup
1. Open project (see above)
1. Open Unity Recorder: Window > General > Recorder > Recorder Window
1. Dock the Recorder window below the Game view

#### For each timezone

1. Change settings on Recorder as needed
2. Select a timezone (see above)
3. Update the file name settings. For example, these `<wild cards>`: 

```
00-00-01-<Date>-<Time>-<Resolution>-<Take>
``` 

will be replaced in the filename of the exported video: 

```
00-00-01-spain-canary-islands-lotus-berthelotii-20220817-095839-4K-24H.mp4
```

4. Click the red play button in the Recorder. It will record until you (or the Recording Mode settings) exit play mode.


### Unity Recorder FAQ

[Unity Recorder](https://docs.unity3d.com/Packages/com.unity.recorder@2.0/manual/index.html) can capture and save video from Unity, including game play and other user interactions. A good place to start is the [Unity Recorder Tutorial](https://learn.unity.com/tutorial/working-with-unity-recorder#) and [advanced usage with timeline to move camera around scene](https://www.youtube.com/watch?v=AIlDJoCuJ1E&ab_channel=TheTrueDuck)

![UnityRecorderDialogue.png](Assets/Sprites/UnityRecorderDialogue.png)









## Owen's Time

Time | Date | Description
--- | --- | ---
40? | ... | Building SVG Randomizer


Time | Date | Description
--- | --- | ---
2 | Oct 16 | SVG Research
2 | Oct 18 | Write SVG > PNG exporter, create basic viz
4.5 | Oct 19 | Set up Git, Utiliities submodule, Visualization and Animate classes
6 | Oct 20 | Day 2 of all nighter
3 | Oct 23 | Working on fixing tweens
9 | Oct 24 | Added Unity Record, switch to DOTween, move all props to Scriptables
9.5 | Oct 25 | Working on clock
2.5 | Oct 26 | Finish basic clock
1 | Nov 1 | Fix issue with file paths
8 | Nov 6 | Add gradients, fix animations, fix math behind scenes, move camera around object
4.5 | Nov 10 | Tweaking animation, adding automatic directory / asset collection method
6.5 | Nov 11 | Recording video, finishing automatic directory / asset collection
---
58.5


Time | Date | Description
--- | --- | ---
2.5 | Nov 12 | Clean up and document code, add widescreen example
1 | Nov 13 | Add resolution manager, clean up instantiation
3 | Nov 28 | Upload videos, Update lighting / shaders / materials so objects are different than background, Finish updating positioning to fill the container bounding box (instead of current position which is meters from center radius), Create transition where objects go to silhouette
5.5 | Nov 29 | Adding color lerp functions, merging SneakawayUtilities
2 | Nov 30 | Moving color lerp to project
6 | Dec 1 | Finally got the color lerp, time (kind of) working.
7 | Dec 2 | Refactoring color lerp code to control intensity, etc.
4 | Dec 3 | Refactoring color lerp to control gradient
3 | Dec 4 | Creating TimeLerp_Gradient, Ugh
3 | Dec 5 | Learning basic ShaderGraph, creating gradient animation
8 | Dec 6 | Finishing ShaderGraph work, cleaning up project, adding timeProps
2 | Dec 7 | Adding new assets
5 | Dec 8 | Fixing vizFileList
9.5 | Dec 9 | Refactoring / writing new rotation for visualization, adding Cinemachine, dolly, recordings
11 | Dec 10 | Creating FrameClock to fix timing of background gradient/foreground lights
5 | Dec 11 | Re-exporting all renders
7.5 | Dec 19 | Update code to export PNGs to Unity project
2 | Dec 20 | Rendering videos
1 | Jan 8 | Updates to animation colors and quantity, rendering videos
---
88

Time | Date | Description
--- | --- | ---
4.5 | Jun 20 | Trying to fix errors in SVG randomizer (that were caused by Google Drive files offline and the CTS-data sheet being made private)
1 | Jun 22 | Updates to project to do and instructions






## To do

- [X] Randomizer
- [X] Set up project, Git, LFS
- [X] SVG Export
	- [X] Create basic test for viz
	- [X] Copy all SVG to `svg-randomizer` (on Mac Pro)
	- [X] Then convert to PNGs
	- [X] Then copy PNGs to Unity folder
- [X] Create new submodule for utilities
 	- [X] Import into this project
	- [ ] Copy all `Scripts/_Common/` from previous (Graverobbers)
- [X] Video Export
	- [X] Add Unity Recorder
	- [X] Create FAQ ([see below](#unity-recorder-faq))
	- [X] Make tests, give to Joelle
	- [X] Make videos for 6 different locations, give to Joelle
		- [X] ...she edits into 3 or 6 div
- [X] Time Class
	- [X] 24 hour clock (controllable starttime, endtime, and timeScale)
	- [X] Display clock
	- [X] Move basic "sun"
	- [X] Function to update things if game pauses
	- [X] Control basic light
- [X] Visualization
 	- [X] Display basic randomized, with controls
 	- [X] Change to savable configuration method to repeat visuals w/o losing settings
- [X] Animation
	- [X] Remove glitch from parent emotion (gimbal lock)
	- [X] Change from LeanTween to DOTween
	- [X] Config files save edits to animation settings
	- [X] Make Save Config button
	- [X] Make workflow like this:
		- [X] 1. Create new config file button
		- [X] 2. Drag config file to visualizer game object
		- [X] 3. Make animations, change properties, etc
		- [X] 4. Just change VizSettings file to update values in config file with current values of animation
- [ ] UI
	- [X] Add control viz buttons
	- [ ] Add overlay with instructions?
- [X] Sky
	- [X] Use gradient for background / lighting
	- [X] Use actual sunset? https://github.com/Mursaat/SunriseSunset
- [ ] Brainstorming for conversations later
	- [ ] Exhibition
		- [X] New doc: Compare Raspberry Pi vs. Micca vs. BrightSign
		- [ ] New doc: Spec out / budget multiple bezel free monitors
		- [ ] New doc: Encapsulate research / solutions for removing gradient banding
		- [ ] Link from "High Res Files..."
	- [ ] Potential Mobile App
		- [ ] Notification points you to app when sunsets
		- [ ] Keep things modular to make mobile version performant
		- [ ] Build mobile app welcome screen in a new scene
		- [X] In SVG exporters, make sure there is an option to produce differenct size PNGs for app
		- [X] SVG export should mimic same structure as original project (sunset data)
- [ ] Potential performance improvements
	- [ ] Add Object Pool
    - [ ] Others?
- [ ] Visual improvements
	- [ ] Fix muddy color transition 
    - [ ] Improve leaves 
        - [ ] Add ripple effect with shaders or mesh deformation to the leaves
        - [ ] Show shadows falling on leaves
        - [ ] Show light through semi-transparent leaves 
        - [ ] Experiment with lighting cookie on leaves (to fake shadows)  
- [ ] Video improvements
    - [ ] Logistics: It would be great if we could click record once and Unity rendered all 24 videos automatically.
    - [ ] Render quality: Need a truth table comparing all the different codecs and resolutions across monitors. Right now, the only acceptable quality version is HUGE.

