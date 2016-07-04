Projecting Sprites in your scenes
=================================

This shader allows you to project a sprite (with transparent background) on your scene.

Steps:

- create a new material and assign it the Aiv/SpriteProjector shader
- import your sprite asset (ensure it has transparent background) and assign it to the previously created material
- create an empty gameobject
- add the 'projector' component to the empty gameobject and set it as 'orthographic'
- assign the previously created material to the projector
- enjoy
