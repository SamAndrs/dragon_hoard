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

        public GameMap GameMap { get; private set; }

        public List<RoomTile> ExploredRooms { get; private set; }   // What rooms the player have visited.

        public RoomTile CurrentRoom { get; private set; }           // The current room in which the player resides.

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
            CreateRoomTileStack(20);

            GameMap = new GameMap(10, 13, _ui.RectangleTexture);
            GameMap.MapPosition = new Vector2((_game.GraphicsDevice.Viewport.Width - 300), 50);
            GameMap.Initialize();
            CurrentRoom = GameMap.CreateRandomStartPosition(CurrentRoom);

        }

        private void CreateTestRound()
        {
            
            // Add visited roomtile to ExploredRooms-list
        }

        private void CreateRoomTileStack(int amount)
        {
            RoomTileStack = new Stack<RoomTile>();
            for (int i = 0; i < amount; i++)
            {

                RoomTileStack.Push(new RoomTile(i, RoomType.Empty, 74, new bool[4] { true, false, false, true }));
            }
        }

    }
}
