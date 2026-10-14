using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            v.001a (10/07) Setup of basic core properties and methods.



        :: DONE ---------------------------
            (2026)

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class GameSession
    {
        //========================================== PROPERTIES ==================================================//

        // public Player Player { get; private set; }

        private MainGame _game;

        private UiManager _ui;

        public PlayerCharacter PlayerCharacter { get;  set; }

        public int PlayerMove { get; set; }                         // Determine how many rooms player can progress this turn.

        public Direction MoveDirection { get; private set; }

        public GameMap GameMap { get; private set; }

        public List<RoomTile> ExploredRooms { get; private set; }   // What rooms the player have visited.

        public RoomTile CurrentRoom { get; private set; }           // The current room in which the player resides.

        public RoomTile NextRoom { get; private set; }

        public int CurrentPCHealth { get; private set; }

        private bool _playerCanExit = false;                        // Determine if player can leave through an exit (i.e. end game).

        public int GameTurn { get; private set; } = 0;              // The amount of game turns since start.

        // CARD STACKS
        public Stack<RoomTile> RoomTileStack { get; private set; }

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public GameSession(MainGame game, UiManager ui)
        {
            _game = game;
            _ui = ui;

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
            PlayerMove = 1;

        }// End -----------------------------------------------------------------------------------------------------

        public void SetCurrentRoom(RoomTile roomTile) => CurrentRoom = roomTile;

        #endregion

        // DEBUG
        private void CreateTestInitialization()
        {
            CreateRoomTileStack(10, 13);

            GameMap = new GameMap(10, 13, _ui.RectangleTexture);
            GameMap.MapPosition = new Vector2((_game.GraphicsDevice.Viewport.Width - 300), 50);
            GameMap.Initialize();
            CurrentRoom = GameMap.PickRandomStartPosition();
        }

        public void CreateTestRound(Direction direction)
        {
            // Pop RoomTile from RoomTile-stack
            // Place RoomTile on GameMap
            // Add visited roomtile to ExploredRooms-list
            
            //RoomTile nextTile = TestFindRoom(direction);    // Return the roomtile of the gamemap in the direction moved
            RoomTile nextTile = GameMap.ReturnRoomTile(direction, CurrentRoom.Row, CurrentRoom.Col);
            RoomTile fromStack = RoomTileStack.Pop();


            if (!ExploredRooms.Contains(CurrentRoom)) // If not yet; add current room to list of explored ones.  
                ExploredRooms.Add(CurrentRoom);



            // If the next RoomTile is not discovered yet; Place current RoomTile in list of visited rooms
            // Place the next Tile down on the MiniMap grid.
            // Set the new CurrentRoom to the Next Tile
            if (nextTile.RoomType == RoomType.Undiscovered)
            {                
                nextTile.PlaceTile(MoveDirection, fromStack, fromStack.DoorValue); // Pull next RoomTile from stack and place it on gamemap grid

                GameMap.MiniMap[nextTile.Row, nextTile.Col] = nextTile;

                SetCurrentRoom(nextTile);
            }
            // If already visited, then just move.
            else
            {
                SetCurrentRoom(nextTile);
            }
        }

        private RoomTile TestFindRoom(Direction direction)
        {
            RoomTile foundTile = new RoomTile();
            switch (direction)
            {
                // Set placeholder tile to corresponding RoomTile on Minimap, depending on direction moved.
                case Direction.Up: foundTile = GameMap.MiniMap[CurrentRoom.Row - 1, CurrentRoom.Col]; break;
                case Direction.Right: foundTile = GameMap.MiniMap[CurrentRoom.Row, CurrentRoom.Col + 1]; break;
                case Direction.Down: foundTile = GameMap.MiniMap[CurrentRoom.Row + 1, CurrentRoom.Col]; break;
                case Direction.Left: foundTile = GameMap.MiniMap[CurrentRoom.Row, CurrentRoom.Col - 1]; break;
            }

            return foundTile;
        }

        private void CreateRoomTileStack(int rows, int columns)
        {
            int amount = rows * columns;
            RoomTileStack = new Stack<RoomTile>();
            //for (int i = 0; i < amount; i++)
            //{
            //    RoomTileStack.Push(new RoomTile(1, RoomType.Empty, 1));
            //    //    RoomTileStack.Push(new RoomTile(i, RoomType.Empty, 74, new bool[4] { true, false, false, true }));
            //}

            RoomTileStack.Push(new RoomTile(2, RoomType.Empty, 8)); // (only entrance)
            RoomTileStack.Push(new RoomTile(2, RoomType.Empty, 4)); // (entrance, up, down)
            RoomTileStack.Push(new RoomTile(2, RoomType.Empty, 7)); // (entrance, opposite end)
            RoomTileStack.Push(new RoomTile(2, RoomType.Empty, 3)); //(entrance, down, opposite end)

            //RoomTileStack.Push(new RoomTile(2, RoomType.Empty, 2, new bool[4] { false, false, false, false }));
            //RoomTileStack.Push(new RoomTile(3, RoomType.TrapRoom, 3, new bool[4] { false, false, false, false }));
            //RoomTileStack.Push(new RoomTile(4, RoomType.WebbedRoom, 4, new bool[4] { false, false, false, false }));
            //RoomTileStack.Push(new RoomTile(5, RoomType.Empty, 5, new bool[4] { false, false, false, false }));
            //RoomTileStack.Push(new RoomTile(6, RoomType.BridgeRoom, 6, new bool[4] { false, false, false, false }));
            //RoomTileStack.Push(new RoomTile(7, RoomType.Empty, 7, new bool[4] { false, false, false, false }));
            //RoomTileStack.Push(new RoomTile(8, RoomType.Rubble, 8, new bool[4] { false, false, false, false }));
            //RoomTileStack.Push(new RoomTile(9, RoomType.DarkRoom, 1, new bool[4] { false, false, false, false }));
        }

    }
}
