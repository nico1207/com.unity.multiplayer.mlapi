**The Global Game State and Scene Transitioning Sample**

![](images\image21.png)![](images\image11.png)

Within the MLAPI repository, there is a test project that contains several MLAPI samples.  Within the test project, there is one folder named “GlobalGameState” that contains all of the prefabs, scenes, and scripts required to export a custom package *With two exceptions:*
1. *The SyncTransform component is located in the root scripts folder*.
2. *The SceneToStateOptionsEditor is located in the root editor folder.*

![](images\image14.png)


**Traditional MLAPI Scene Registration:**

![](images\image17.png)

The traditional MLAPI scene registration still applies, but has some minor guidelines that should be taken into consideration:
1. Always include the MLAPIBootStrap.
*This is easy to remember because the NetworkingManager lives here.*
2. Always include any scenes that require MLAPI and any scene that might not require MLAPI, but it is loaded just before or just after a scene that does require MLAPI.  In the above screenshot, we include the MainMenu because it transitions to the GameLobby and it is what is loaded when a player leaves the InGame game session.
3. If possible, it is advised to try and keep the same original spawn settings that is the default for the global game state sample:

![](images\image4.png)

**The Global Game State:**

![](images\image10.png)

*(notes on MLAPI State, Scene to Link, and “to game state”)*
**MLAPI States:**
* None: MLAPI is not needed for the scene
* Connecting: MLAPI needs to be initialized and connect or listen for connections
* InSession: MLAPI is running, clients and server are all connected
* ExitSession: MLAPI needs to be shutdown
**Global Game States:
	***The three global game state changes below are handles by the local player*
* None: no game state, game will transition to the next valid state (typically init)
* Init: Splash screens, notes, videos can all be loaded and played here.
* Menu: Player is at the main menu of the game (or in some subset of the menu)

*From this point forward, all clients (including host) will synch to these states*
* GameLobby: Players are in the game lobby.
* InGame: Players are playing within a game session.
* ExitGame: All players will be disconnected and returned to the assigned scene.
**The Associated Script Files:**

![](images\image5.png)


* **GlobalGameState: **Handles keeping the players synchronized throughout the life cycle of a game application instance.  It handles starting up and shutting down MLAPI (previous page).
* **InGameManager:  **This is a bare-bones game management component that includes an  in-game state machine to help with the synchronization of players joining and disconnecting.  It also provides an example of how to pause players, and change a player’s visibility.
* **IntroSceneControl:  **The simplest of all controls.  This shows the simplest transition of the global game state (from Init to MainMenu).
* **MenuControl:  **The second simplest of all controls.  This allows a player to select whether they will be hosting or joining (host or client) a game session.
* **LobbyControl:  **This provides the user with a bare-bones multiplayer lobby.  It can be easily expanded upon to create a more rich UI experience.
* *PlayerControl:  (not used as of yet)*
* **RandomPlayerMovement:  **While one might think this component only moves the player randomly (which it does), this component also demonstrates how to pause as well as hide the player when it is automatically spawned by MLAPI.
* **StateToSceneTransitionLinks:  **This contains the classes required for the custom global game state property field that allows the user to link MLAPI state to a scene and the scene to a global game state.

**MLAPI States, Scenes, and Game States:**
*(Code snippet is from MenuControl.cs)*![](images\image6.png)

Above is the most fundamental aspect of the GlobalGameState class and how it can be used to expedite what can be a daunting task in most netcode driven games.  StartLocalGame is invoked by the “Start Host” button and JoinLocalGame is invoked by the “Start Client” button.

![](images\image19.png)

**There are three things that should be specified to start and connect:**
1. Whether we are hosting or not ( IsHostingGame )
2. What GameState we want to transition to (SetGameState)
3. The connection address (if it changed)
That’s it!  The rest is handled by the GlobalGameState class!

**The BareBones Lobby:**

![](images\image2.png)

While this doesn’t look like much, it provides a very good foundation to building a more rich and interactive lobby experience.  As opposed to spending time figuring out how to make a lobby using MLAPI and Unity, this scene transition and management example provides the user with all of the working elements required to build a lobby for their game (MS-1 relative).
**But what about games in progress?  How is this handled?**
![](images\image7.png)
*The GlobalGameState class is a networked state machine.*  As such, the server (host) controls the GlobalGameState and when a client joins from the main menu, as opposed to transitioning directly into the Lobby and then into the In-Game session, GlobalGameState will redirect the late joining player to the In-Game session directly!
![](images\image9.png)
This sample provides the user with a “solid starting point template” so they can focus more on building their game as opposed to “re-inventing the wheel”.

**Additional Features and Examples:**

**In-Game Pausing:**
Only the server will see the “server commands” (which there is only one currently).
When pressed, all players are paused.![](images\image1.png)

**An Exit Game Timer Example:**
The default rules for this sample’s “game scene” are:
* If a client exits (by clicking the ‘X’ exit button) they will disconnect immediately
* If a server exits, then all clients will be notified and the timer will begin to count down.  Upon the timer reaching zero, all clients will disconnect from the game.  The host-server waits until all clients have disconnected before it disconnects itself.
![](images\image13.png)

**In-Editor MLAPI Boot Strapping:**
![](images\image22.png)
One common issue with the Singleton approach and MLAPI is that users have to have a version of the NetworkingManager within a scene in order to be able to test directly from the scene in question.  By using the “MLAPI BootStrap” philosophy (MS-1 relative) and with a little help from the GlobalGameState class, users can launch directly from any scene and the GlobalGameState will assure that the MLAPIBootStrap scene is loaded first and, if needed, that a new session is created or joined (depending upon if you select to be the host or not).

Both the LobbyControl and InGameManager classes provide an additional field called “Launch As Host In Editor” (when set the in-editor will launch MLAPI as a host, otherwise it will launch as a client).
![](images\image15.png)

Clicking the play button will kick off a series of events that will land you directly in the scene you are editing while keeping the GlobalGameState synchronized as if you had launched the game from the MLAPIBootStrap scene, progressed through the various scenes, created a lobby, clicked ready, and then final landing into the scene the user was editing.  If you want to join another client…![](images\image18.png)
Just launch a build version of the test project, navigate to the main menu, and click “start client”.  This will connect directly to the InGame scene.

![](images\image12.png)
Transitioning into a game in progress is simplified!

**How to transition from one In Game scene to another (while In Session):**

![](images\image16.png)

This sample handles the transitioning between scenes, while in the “In Session” MLAPI state, in a linear fashion.  The order in which the scenes will load are based on the order they are presented within the Global Game State.  
The InGame and InGame2 scenes have a “server only” visible “Next Level” button that allows the host to transition to the next scene based on the Global Game State ordering.
Within the InGameManager.cs file you will find the following code that is invoked upon clicking the “Next Level” button:![](images\image3.png)

By invoking the same “In Game” state, the Global Game State will automatically increment to the next MLAPI “In Session” scene that is linked to the “In Game” state.
That is all that is needed to transition everyone into the next scene!


**Customizing the GlobalGameState Custom Property:**
Since most of the “magic” happens with the linking of an MLAPI State to a specific Scene that is linked to a specific global game state:

![](images\image20.png)

With the scenes, you can just drag and drop scenes into the “Scene to Link” field column.  How this processes that into a “build version friendly” scene name is all done within the SceneToStateOptionsEditor class:

![](images\image8.png)
