
# CTS-Viz


## To do

- [X] Randomizer
- [X] Set up project, Git, LFS
- [ ] SVG Export
	- [X] Create basic test for viz
	- [ ] Copy all SVG to `svg-randomizer` (on Mac Pro)
	- [ ] Then convert to PNGs
	- [ ] Then copy PNGs to Unity folder
- [X] Create new submodule for utilities
 	- [X] Import into this project
	- [ ] Copy all `Scripts/_Common/` from previous (Graverobbers)
- [X] Video Export
	- [X] Add Unity Recorder
	- [X] Create FAQ ([see below](#unity-recorder-faq))
	- [X] Make tests, give to Joelle
	- [ ] Make videos for 6 different locations, give to Joelle
		- [ ] ...she edits into 3 or 6 div
- [ ] Time Class
	- [ ] 24 hour clock (controllable starttime, endtime, and timeScale)
		- 1. 00:00:00–23:59:59 at 00:24:00, then speed up simulation *60
		- 2. Make variable named offset
	- [ ] Display clock
	- [ ] Move "sun"
- [ ] Potential Mobile App
	- [ ] Notification points you to app when sunsets
	- [ ] Keep things modular to make mobile version performant
	- [ ] Build mobile app welcome screen in a new scene
	- [ ] In SVG exporters, make sure there is an option to produce differenct size PNGs for app
	- [ ] SVG export should mimic same structure as original project (sunset data)
- [ ] Visualization
 	- [X] Display basic randomized, with controls
 	- [ ] Change to savable configuration method to repeat visuals w/o losing settings
- [ ] Animation
	- [X] Remove glitch from parent emotion (gimbal lock)
	- [ ] Change from LeanTween to DOTween
	- [ ] Config files save edits to animation settings
	- [ ] Make Save Config button
	- [ ] Make workflow like this:
		- 1. Create new config file button
		- 2. Drag config file to visualizer game object
		- 3. Make animations, change properties, etc
		- 4. Click Save button to update values in config file with current values of animation
- [ ] UI
	- [X] Add control viz buttons
	- [ ] Improve button style
	- [ ] Add overlay with instructions?
- [ ] Sky
	- [ ] Skybox image
	- [ ] Lighting / Rendering
		- [ ] Change color
		- [ ] Change value
- [ ] Brainstorming for conversations later
	- [ ] Add ripple effect with shaders or mesh deformation to the leaves
	- [ ] Leaves show shadow and light through them
	- [ ] Lighting cookie


## Unity Recorder FAQ

![UnityRecorderDialogue.png](Assets/Sprites/UnityRecorderDialogue.png)

- [Unity Recorder Tutorial](https://learn.unity.com/tutorial/working-with-unity-recorder#) also see [advanced usage with timeline to move camera around scene](https://www.youtube.com/watch?v=AIlDJoCuJ1E&ab_channel=TheTrueDuck)
- How to change file name settings (e.g. `00-00-01-spain-canary-islands-lotus-berthelotii-20220817-095839-4K-24H.mp4`)? **In dialogue box**
- Where does it save? **In dialogue box**
- How long does it run? **Until exit Unity play mode**
- Can we have grey default Unity background? **Yes, you can use a camera or the entire Game View**
- Can it record interaction? **Yes**
- What happens in the gallery? **???**





## Time

Time | Date | Description
--- | --- | ---
40? | ... | SVG Randomizer
2 | Oct 16 | SVG Research
2 | Oct 18 | Write SVG > PNG exporter, create basic viz
4.5 | Oct 19 | Set up Git, Utiliities submodule, Visualization and Animate classes
6 | Oct 20 | Day 2 of all nighter
3 | Oct 23 | Working on fixing tweens
3 | Oct 24 | Adds Unity Record
