# Game Basic Information #

## Summary ##

Complete platforming challenges and solve puzzles by using the grappling hook to swing on hooks and pull objects.

## Gameplay explanation ##

**Controls:** Use a mouse and keyboard
* A & D = Walk/Swing Left and Right
* W = Jump or Retract Rope
* S = Extend Rope
* Mouse = Aim Grapple Hook
* Left Click = Fire Grapple Hook

You need to complete platforming challenges and/or solve puzzles to reach the end of each level using your Grapple Hook. Your jump does not go very high, so it is necessary to use your Grapple Hook for traversal. Pulling objects can help you create platforms to jump on and trigger switches.

# Main Roles #

## User Interface

### Joel Boersma:

**Main Menu:** The first thing you see when you start the game. In front of a nice ocean background is a panel, on which lies the title of the game, a “New Game” button that starts the game’s intro (which then starts the first level), and five “Level” buttons, each of which starts a different level in the game.

**Intro:** This screen introduces the protagonist and story. A sprite for the player character is front and center. At the bottom, the “story sequence” appears and the story slowly appears letter by letter. There is a pause in-between each letter appearing, and an even longer pause after a sentence is completed. To skip the story sequence, the player can press space, and all the text for the story will appear on screen at once. When the story sequence is completed, either by force or by letting it continue to the end, a prompt to “Press ‘Space’ to Continue” will appear, and when the player does this, the first level begins.

**Level Complete:** When the player touches the exit portal of a level, a banner saying “Level Complete!” will appear on-screen for a few seconds before the next level begins. This banner is really just a disabled button.

**Aiming Arrow:** The aiming arrow (described further in the “input” section) will disappear while the grapple is attached to something, and reappear when the grapple disconnects.

### Frederick Tan:

**Camera:** As a 2D-platformer/adventure game, I decided to use a PushBox camera controller for our player.  This script is the same as Josh’s camera project on the Github repository with a few slight tweaks.  This is because the player can get a decent initial view of the landscape of the level they are in.  In addition, having a push box allows the player to control what they want to see, giving them more power in the game rather than the game controlling the player.  Having a relatively-sized box also allows the player to pause and do things such as pulling blocks or attaching the grapple to hook without the camera moving.
* Related scripts: PushBoxPlayer.cs


## Movement/Physics

### Gabriel Brusman: 
**ADSR:** I implemented the ADSR curve for the player movement. I did this by borrowing from Josh’s ADSR project on the Github repository. I had to change the input controls to correspond to A and D instead of Fire1 and Fire2, and I also had to make tweaks to allow for players to hold both A and D at the same time, and to handle what happens when players hold both keys and release one of them. The original ADSR code in the class repo did not handle these cases. My goal was to make the player feel controllable, and not too slippery, since the game relies on the player making precise movements, but I didn’t want the movements to feel robotic, or instant.
* Related scripts: Movement.cs

**Jump Logic:** I used a raycast to determine whether the player character is standing on solid ground, which determines whether or not they are able to jump. At first I only did one raycast from the center of the character, but this caused issues if the player was partially hanging off the edge of a cliff, since if the center of the character was over the edge it wouldn’t let them jump. I changed this check to 3 raycasts, one from the center of the character, one from the left edge, and one from the right edge. This fixed the issue.
* Related scripts: Movement.cs

**Grapple Aiming and Shooting:** I implemented the grappling hook aiming and shooting mechanic. I made the arrow that spins when the player moves the mouse to indicate the direction of the projectile. This was done by taking the tangent of the mouse Y position and the mouse X position. I also implemented the projectile itself and how it shoots from the player and connects to the objects in the game world. The grapple projectile spawns at the end of the arrow object, and is propelled by adding a force to it when it is spawned. I used“Grapplable” and “Pullable” tags to mark which objects could be connected to, and I check for these tags when a grapple projectile collides with an object. I also had to handle despawning the grapple projectiles, and allowing the player to detach the grappling hook by pressing Fire1 a second time. I made the grapple projectiles despawn if they haven’t collided with anything after a certain amount of time, or if they collide with an object that does not have either the “Grapplable” or “Pullable” tag. 
* Related scripts: GrapplingHook.cs, GrappleProjectileController.cs, Aimer.cs

**Grapple Swinging and Pulling:** I implemented the swinging and pulling mechanics for the grappling hook. The swinging back and forth just uses the same code as the walking left and right, but achieves the swinging motion due to the Joint2D connection with the grappled object. The pulling mechanic was achieved by giving the pullable blocks dynamic RigidBody2D’s, and since they have mass, they alter the player’s movement speed when pulling automatically, which makes the player feel like they are pulling a heavy object. I also implemented the mechanic that allows the player to extend and retract the rope when they are connected to an object by pressing S and W respectively. I did this by decreasing/increasing the length of the Joint2D object that we use for the rope.
* Related scripts: Movement.cs, GrapplingHook.cs, GrappleProjectileController.cs,


## Animation and Visuals


### Frederick Tan:
**Assets provided:**
https://assetstore.unity.com/packages/2d/environments/free-platform-game-assets-85838

### Jed Nugal: 
**Assets used:**
https://jesse-m.itch.io/jungle-pack
https://rvros.itch.io/animated-pixel-hero

**Sprites:**
I did the sprite animations for the character using the Animation editor within Unity. To do this I had to create an ‘Animator’ that included the sprite’s different states of animation. The different states included Idle, Run, Jump, and Holding_Rope. I used the sprites taken from the asset links above to create a frame by frame animation in the Animation window. To notify when to change/end animations I added parameters to the transitions between states, such as a float ‘Speed’ to determine when to be idle or run, a bool ‘IsJumping’ to determine when to jump, and a bool ‘IsHoldingRope’ to determine when to hold rope. I made it so that the transitions between stages did not have any transition time so that an animation would immediately begin once another ends. This proved to be particularly effective when doing the jump animation. Before getting rid of the transition times, the character would go into the air while still in either the idle or run animation before starting to jump. 

**Interaction with Movement and Physics:**
To manipulate these variables and control the animations, I first added an animator variable to the Movement Script. I did this rather than create a different script because the animations need the variables of movement in order to change properly. Certain conditions within the movement script such as if the player collided with an object or if he was in the air allowed me to manipulate the transition parameters of my Animator in order to play the animations when needed. One thing to note is that we had trouble getting the animations and physics to properly interact with one another. I felt that it was more important to fix this interaction because our game is a mainly mechanic-driven one. This led to investing in trying to fix the bugs in these interactions over adding more background or non-player visuals and animations. The reason for the bugs is that the animator uses the transform/rotation to animate while our movement and grappling hook classes also utilized the transform. This caused some mechanics to disappear and either the animations or the movement physics would overwrite one another. In the end, I fixed the issues within two stages and was able to put the full animations in said stages, but some bugs still remain in the other stages so we have simple player boxes for those stages.

**Environment/Background art:**
For this part everyone contributed to various stages. At a certain point in our game development, each person was essentially working on their own stages to test their mechanics or art that they were in charge of.
I chose the assets packs in the links above because I felt that fit the story that we chose. Our story is purposefully silly and playful, so I chose bright looking sprites and images to reflect that energetic feeling that we wanted our game to have. I did the overall environment art and design for some stages and contributed some to others. For ConnectStage I changed previously white boxes to ‘tiki’ looking boxes to foreshadow the darker look of the next stages. For PullStage I knew I wanted a more ‘jungle-y’ and at first glance a more daunting look than the previous stages. To do this I utilized a darker forest background and added different looking spikes than the previous stages. I played with the layers of each gameObject to determine if a gameObject was to be at the background or foreground. I added things such as trees, mushrooms, and skulls to make the game more lifelike.


## Input

### Frederick Tan: 
I laid the groundwork for the movement and grapple hook inputs.  I created the scripts called Movement.cs and implemented the first version of the grapple hook.  Initial movement input used the arrow keys to move left/right, jump, and up/down the grapple, before being changed to ADSR controls.  Initial grapple hook input used the space bar to launch the grapple hook by using a DistanceJoint2D and a Line Renderer to connect the player and the object, and show the rope.

### Gabriel Brusman:
I remapped the ADSR controls to A and D, and had to write code in Movement.cs to handle what would happen when both A and D were pressed at the same time, and then also what would happen if the player was holding both keys and then released one of them. I also wrote the input for the jump mechanic, which uses W, the grapple shooting and releasing, which uses left click, and the grapple extend and retract mechanics, which use S and W respectively. I also implemented the aiming arrow, whose rotation is controlled by the mouse position.

### Joel Boersma: 
Pressing the escape key at any time takes you back to the main menu.


## Game Logic

### Frederick Tan:
**Death:** I implemented a script that attaches to the spikes in the game, so when the player falls and hits the spikes, they “die” and respawn at their initial position when they started the level.  This was done with a BoxCollider2D with IsTrigger turned on, and a OnTriggerEnter function.  I also made it so game objects that were moved around in the level also reset to their initial position when the player dies.
Related: Death.cs

**Portal:** At the end of each level (on the far right side), I used a treasure chest asset to represent a portal to the next level.  Functionality-wise, I created a GameManager and a next level script.  The GameManger is used to move from one level to the next, while the next level script provided the function that calls the load next level function when the player touches (triggers) the chest.
Related: GameManager.cs, NextLevel.cs

**Level Challenges (Swinging across platforms and pulling blocks):** While my teammates designed the environment of each level, I took responsibility for designing the challenges of each level, except for the ExtremeStage.  The first level is a tutorial stage, so all the player had to do is pull the block and attach the grapple to the hook to swing over the spike.  Each subsequent level required an amount of swinging.  PullStage is where the pulling mechanic comes in, as I made it so the player must pull blocks from another platform to create a “bridge” to crossover.

### Jed Mandy Nugal: 
I added changes to our Death.cs script in order to be more generalized. Our Death script utilized an OntriggerEnter function to see if the player hit the spikes. This was hardcoded beforehand for user to input the player variable within the scene and only check if the GameObject’s name was ‘Player’. I later changed this to consider if it was the ‘Player’ box prefab or the ‘Henry’ prefab that was heading for death. When designing PullStage, I completely removed the grass floor from the previous stages and changed them to platforms that were farther apart and only able to be arrived at by effectively using the mechanics of our game rather than just walking or jumping there. Some designs that did not make it to more challenging stages is ‘unlocking’ previously disabled and invisible blocks to be able to swing from. The player would pull the block to a designated area to ‘unlock’ previously disabled and invisible floating blocks to be able to swing over to the far platform.
I also made a change to the Grappling Hook Projectile within GrapplingHook.cs  to destroy it when hitting a Pullable or Grapplable object rather than disabling.


# Sub-Roles

## Audio

### Michael Cordero
**From the asset store: **
https://assetstore.unity.com/packages/audio/music/electronic/8-bit-music-free-136967


## Gameplay Testing

### Gabriel Brusman:
I had my roommates test the game early on in development. One of their main complaints was that the player could not jump unless the center of the character was touching the ground. This caused issues when players made “last minute” jumps off of cliffs, because by the time the player pressed jump, the center of the character was already off the edge and so the game wouldn’t let them. This caused many frustrating deaths, so I changed how the jump check was made (see Movement/Physics). 
During the final time we had more people play our game. They were able to find bugs and edge cases that we didn’t. For example, in the PullStage, if a pullable block fell into the spikes, the game would act as if the player had died, which caused a confusing level reset. We have since fixed this issue. Also, everyone instinctively used left click to shoot the grapple, and originally we had this control set to spacebar. We changed the control to left click for the final version, and this also makes more sense since the projectile is aimed with the mouse, so it would be consistent for the projectile to be fired with the mouse as well.
People also enjoyed some of the chaos that the physics system brought to the game, like the way the player can grapple a block to them and then stand on top of it and use it to jump infinitely. That wasn’t an intended mechanic, but people seemed to really enjoy it, which was interesting.
Overall people seemed to enjoy the movement mechanics of the game, and the game was challenging enough to engage people. People seemed to be having fun with the game, so that was good to see.


## Narrative Design

### Joel Boersma: 
To quickly summarize the plot, Henry, our protagonist, is a delivery man who needs to deliver supplies to fix the land's roads, and since the roads are out he needs to use his grappling hook to get around.
Our team knew from the beginning that the story was not a priority and nowhere near as important as the core gameplay. So, I decided to craft a simple yet effective story, with the other team members contributing ideas for names and the like.
In the game, the story is exclusively told through an intro sequence that begins when the player starts a new game. It introduces the protagonist and the synopsis and sends the player on their way. Because I did the UI too, this intro sequence didn’t require much communication to implement.

## Press Kit and Trailer

### Frederick Tan:
[Press Kit](https://github.com/ftan95/ECS189Final/blob/master/Project%20Grapple%20Press%20Kit.pdf) 

[Trailer](https://youtu.be/MUZ-6QGcnM4) 

The trailer gives people a glimpse of the environment we have created and what the gameplay looks like.  The press kit contains screenshots of the mechanics in action, start menu and a couple of levels.  Each screenshot was chosen in order to show a core aspect of the game.

## Game Feel

### Jed Nugal:
For the most part everyone contributed to this part as we continuously tested and played the game throughout development. We changed variables including the speed of the player, the ratio of the rope, and distance changed when rappelling up and down the rope. We also played around the scalings for the various blocks such as the Grappleable or Pushable blocks. Before, the hitboxes for the blocks would be bigger than the actual picture of the block so we would hit an invisible wall if we got too close. Our swinging mechanics were a bit buggy throughout development so we all contributed to saying which parts needed adjustment in order to naturalize the movements. In addition, we naturally made the stages more and more challenging as the player progresses through the levels so that the game had a sense of achievement and engagement. In the early stages we have fewer blocks to swing from that are closer together in order to complete the stage. In subsequent stages the blocks are further apart and new mechanics are introduced to challenge the player in order to complete the level.

