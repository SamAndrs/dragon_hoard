using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace dragon_hoard.Content.Models.Objects.Entities
{
    ///////////////////////////////////////// GAME SESSION CLASS ///////////////////////////////////////////////
    /*
        Game class for handling most functionality and data of the core game loop.
        The game session:
            - Prepares and initiates the various game assets required for the game loop.
            - Keeps track of the choosen player character
            - Keeps track of what parts of the gamemap have been discovered and visited
            - Keeps track of the current room in which the player character is
            - Progress through the game loop depending on the current state of events (see RunGameTurn())
      
       :: TODO ---------------------------

            * Extend selection of available flavor messages.
            * (optional) fetch messages from external file/ database.

            v.001c (10/15) Addition of properties for the Flavor message functionality.
            


        :: DONE ---------------------------
            (2026)
            v.001b (10/12) Additions to facilitate RoomTile system functionality.
            v.001a (10/07) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class GameSession
    {
        //========================================== PROPERTIES ==================================================//

        // public Player Player { get; private set; }

        // DATA
        private MainGame _game;

        private UiManager _ui;

        public PlayerCharacter PlayerCharacter { get;  set; }

        public Direction MoveDirection { get; set; }

        public GameMap GameMap { get; private set; }

        public List<RoomTile> ExploredRooms { get; private set; }   // What rooms the player have visited.

        public RoomTile CurrentRoom { get; private set; }           // The current room in which the player resides.

        public RoomTile NextRoom { get; private set; }

        public int CurrentPCHealth { get; private set; }

        private bool _playerCanExit = false;                        // Determine if player can leave through an exit (i.e. end game).

        public int GameTurn { get; private set; }                   // The amount of game turns since start.


        public bool DisplayMessage { get; set; }                    // Controls display of 'flavor' message

        public string FlavorMessage { get; private set; }           // Flavor message text

        public Random Randomizer { get; private set; }


        // CARD STACKS
        public Stack<RoomTile> RoomTileStack { get; private set; }

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public GameSession(MainGame game, UiManager ui)
        {
            _game = game;
            _ui = ui;

            Randomizer = new Random();

            ExploredRooms = new List<RoomTile>();
        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                       Initialize					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method called from the NewGameScreen upon Start game, to initialize the necessary 
            data and objects upon starting a brand new game session. 
            Sets the choosen playercharacter, creates-, fills- and shuffles the various game card stacks 
            and initiates the first game turn.
        ----------------------------------------------------------------------------------------------------------*/
        public void Initialize(PlayerCharacter playerCharacter)
        {
            GameTurn = 0;

            // Create a new character with choosen stats from NewGameScreen
            PlayerCharacter = playerCharacter;

            /* Create and fill the various card stacks
             * RoomTileStack
             * RoomCardStack
             * DoorCardStack
             * SearchCardStack
             * ChestCardStack
             * CorpseCardStack
             * TrapCardStack
             * TreasureCardStack
             * DragonCardStack
             * CatacombsCardStack
            */

            // Initialize ExploredRooms-list

            // DEBUG
            CreateTestInitialization();

            // Set sun stage
            // Create and initialize gamemap
            // Set player initial room (randomized entrance)
            // 
            // GameTurn++;

        }// End -----------------------------------------------------------------------------------------------------

        public void SetCurrentRoom(RoomTile roomTile) => CurrentRoom = roomTile;

        #endregion

        // DEBUG
        private void CreateTestInitialization()
        {
            CreateRoomTileStack(10, 13);

            GameMap = new GameMap(10, 13, _ui.RectangleTexture);
            GameMap.MapPosition = new Vector2((_game.GraphicsDevice.Viewport.Width - 300), 80);
            GameMap.Initialize();
            DisplayMessage = false;
            CurrentRoom = GameMap.PickRandomStartPosition();

        }

        public void CreateTestRound()
        {

            // Pop RoomTile from RoomTile-stack
            // Place RoomTile on GameMap
            // Add visited roomtile to ExploredRooms-list

            // Progress game turn
            // Display flavor message

            // Return the roomtile of the gamemap in the direction moved
            RoomTile nextTile = GameMap.ReturnRoomTile(MoveDirection, CurrentRoom.Row, CurrentRoom.Col);

            // Place current RoomTile in list of visited rooms, if not yet added.
            if (!ExploredRooms.Contains(CurrentRoom))   
                ExploredRooms.Add(CurrentRoom);

            // If the next RoomTile has not yet been visited...
            if (nextTile.RoomType == RoomType.Undiscovered)
            {
                // Pull next RoomTile from the stack
                RoomTile fromStack = RoomTileStack.Pop();

                // Update the new RoomTile's information.
                nextTile.PlaceTile(MoveDirection, fromStack, fromStack.DoorValue);

                // Progress to the new RoomTile
                SetCurrentRoom(nextTile);
            }
            // If already visited, then just move.
            else
            {
                SetCurrentRoom(nextTile);
            }

            GameTurn++;
            FlavorMessage = RandomFlavorMessage(Randomizer);
            DisplayMessage = true;
        }

       
        private void CreateRoomTileStack(int rows, int columns)
        {
            int amount = (rows * columns)/2;
            RoomTileStack = new Stack<RoomTile>();
            for (int i = 0; i < amount; i++)
            {
                RoomTileStack.Push(new RoomTile(1, RoomType.Empty, 1));
                //    RoomTileStack.Push(new RoomTile(i, RoomType.Empty, 74, new bool[4] { true, false, false, true }));
            }

            //RoomTileStack.Push(new RoomTile(2, RoomType.Empty, 8)); // (only entrance)
            //RoomTileStack.Push(new RoomTile(2, RoomType.Empty, 7)); // (entrance, opposite end)
            //RoomTileStack.Push(new RoomTile(2, RoomType.Empty, 4)); // (entrance, up, down)
            //RoomTileStack.Push(new RoomTile(2, RoomType.Empty, 7)); // (entrance, opposite end)
            //RoomTileStack.Push(new RoomTile(2, RoomType.Empty, 7)); //(entrance, down, opposite end)

            //RoomTileStack.Push(new RoomTile(2, RoomType.Empty, 2, new bool[4] { false, false, false, false }));
            //RoomTileStack.Push(new RoomTile(3, RoomType.TrapRoom, 3, new bool[4] { false, false, false, false }));
            //RoomTileStack.Push(new RoomTile(4, RoomType.WebbedRoom, 4, new bool[4] { false, false, false, false }));
            //RoomTileStack.Push(new RoomTile(5, RoomType.Empty, 5, new bool[4] { false, false, false, false }));
            //RoomTileStack.Push(new RoomTile(6, RoomType.BridgeRoom, 6, new bool[4] { false, false, false, false }));
            //RoomTileStack.Push(new RoomTile(7, RoomType.Empty, 7, new bool[4] { false, false, false, false }));
            //RoomTileStack.Push(new RoomTile(8, RoomType.Rubble, 8, new bool[4] { false, false, false, false }));
            //RoomTileStack.Push(new RoomTile(9, RoomType.DarkRoom, 1, new bool[4] { false, false, false, false }));
        }


        private string RandomFlavorMessage(Random randomizer)
        {
            int result = randomizer.Next(3);

            switch (result)
            {
                case 0: return "Mind the path of the sun. As night draws closer the denizens of the castle grows fiercer";

                case 1: return "The  brave,  the  foolhardy,  the  desperate.  The  promise  of  the  hoard  calls  to  them  each.";

                case 2: return "Take  heed  to  escape  the  castle  before  sunset,  for  none  have  survived  the  horrors  of  the  night.";

                case 3: return "The  promise  of  wealth  beckons.  Lurking  close  behind  is  the  promise  of  peril.";

                default: return "Bravery  or  madness;  challenging  the  castle  is  both";
            }
        }
    }
}
