
# CTS-Viz


## To do

- [X] Randomizer
- [X] Set up project, Git, LFS
- [ ] SVG Export
	- [X] Create basic test for viz
	- [ ] Export all SVGs
	- [ ] Export all SVGs to the randomizer repo (on Mac Pro)
	- [ ] Then convert to PNGs
	- [ ] Then copy PNGs to Unity folder
- [X] Create new submodule for utilities
 	- [X] Import into this project
	- [ ] Copy all `Scripts/_Common/` from previous (Graverobbers)
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
	- [ ] Remove glitch from parent emotion (gimbal lock)
	- [ ] Config files save edits to animation settings
	- [ ] Make Save Config button 
	- [ ] Make workflow like this:
		- 1. Create new config file button
		- 2. Drag config file to visualizer game object
		- 3. Make animations, change properties, etc
		- 4. Click Save button to update values in config file with current values of animation
- [ ] Video export
	- [ ] Make a video export button
	- [ ] Change a file name and resolution `00-00-01-spain-canary-islands-lotus-berthelotii-20220817-095839-4K-24H.mp4`
	- [ ] Where does it save
	- [ ] How long does it run? 
	- [ ] Can we have grey default Unity background?
	- [ ] Do you want it to record interaction?
	- [ ] What happens in the gallery? 
	- [ ] Make tests, give to Joelle, she edits into 3 or 6 div
- [ ] UI
	- [X] Add control viz buttons
	- [ ] Improve button style
	- [ ] Add overlay with instructions?
- [ ] Time Class
	- [ ] 24 hour clock (controllable starttime, endtime, and timeScale) 
		- 1. 00:00:00–23:59:59 at 00:24:00, then speed up simulation *60
		- 2. Make variable named offset
	- [ ] Display clock
	- [ ] Move "sun"
- [ ] Sky
	- [ ] Skybox image
	- [ ] Lighting / Rendering
		- [ ] Change color
		- [ ] Change value
- [ ] Brainstorming for conversations later
	- [ ] Add ripple effect with shaders or mesh deformation to the leaves
	- [ ] Leaves show shadow and light through them
	- [ ] Lighting cookie



## Time

Time | Date | Description
--- | --- | ---
40? | ... | SVG Randomizer
2 | Oct 16 | SVG Research
2 | Oct 18 | Write SVG > PNG exporter, create basic viz
4.5 | Oct 19 | Set up Git, Utiliities submodule, Visualization and Animate classes
6 | Oct 20 | Day 2 of all nighter
