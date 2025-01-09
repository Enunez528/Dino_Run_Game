Author: Eric Nunez Rodriguez
Date: 12/16/2024
Unity Game Title: Dino Run Game

Game Overview
This game challenges players to run through an obstacle course and shoot balloons 
at the same time. The goal is to first eliminate all balloons, which then gives 
you access to a door to win the game. This door is at the end of the obstacle course,
and another feature that happens when the game runs is that it tracks the player's time, 
and the best time is saved for each level.

Key Features:
- Third person shooter type of game.
- Time tracking.
- Collision interactions.
- Reaching the door is the end goal of the game.
- UI feedback.

Setup Instructions
Prerequisites:
	- Unity Editor (version 2021.3 or later recommended)
	- Basic understanding of Unity (e.g., how to open a project and run scenes).

Project Files:

Ensure you have the following project files:

- GameMaster.cs script
- PlayerController.cs script
- CameraControl.cs script
- MovingPlatform.cs script
- SceneSwitcher.cs script
- Models,Prefabs, and audios
- Scene files

Steps to Run:

Open the Project:
	- Launch Unity and open the project folder.
Set Up the Scene:
	- Ensure the GameMaster script is attached to a GameObject in the scene.
	- Also any platform you want moving attach the MovingPlatform script.
	- To ensure buttons switch to correct scene make sure each button is attached
	  to SceneSwitcher script and that the correct functions are attached to each to
	  when clicked.
	- Also PLayerController script is attached to player.
	- Assign references to correct object in inspector such as balloon, door, gun, audios, etc.
	- Lastly, configure the spawn area dimensions (e.g., spawnAreaWidth, spawnAreaHeight, spawnAreaY).

Test the Game:

Press Play in Unity to start the game.
Use the WASD keys to move, Space to jump, and F to shoot.
Eliminate all balloons to unlock the door and proceed.

Controls

Movement: WASD or Arrow keys.
Jump: Space.
Shoot: F.
Return to Main Menu: Escape.

Game Flow

Balloon Spawning:
Balloons are spawned randomly within the defined area at the start of the game.

Popping Balloons:
Shoot balloons to eliminate them. A message will display indicating how many balloons remain.

Door Interaction:
Once all balloons are eliminated, a message will inform you that the door is unlocked. Interact with the door to proceed.

Win Condition:
Interacting with the door after clearing all balloons ends the level, with a chance to play again or exit to main menu.

Lose Condition: 
Falling off obstacle course platforms and hitting the underlayer called the net resulting in game over, 
with a chance to play again or exit to main menu. 

Credit:

Here I give credit for art work and audios from free websites. The audios I got from a website called Pixabay. The Images for you win screen and game over screen from the website freepik. The other images I created for controls, and start. The pistol blender model and bullet model I got from website Free 3D.
