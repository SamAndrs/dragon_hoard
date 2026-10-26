using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI;
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
            - Progress through the game loop depending on the current state of events
      
       :: TODO ---------------------------

            * Extend selection of available flavor messages.
            * (optional) fetch messages from external file/ database.
            * Account for the player moving into an exit (should prompt "Want to leave?" message)
            * Account for events (RoomTile and RoomCards) preventing player from moving or cards from being removed.
            * Account for RoomTiles having an event preventing pulling of a RoomCard. (traps, exits ec.)

            
            


        :: DONE ---------------------------
            (2026)
            v.001d /10/17) Remade RoomTile stack into a class of its own. Added RoomCard event functionality.
            v.001c (10/15) Addition of properties for the Flavor message functionality.
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

        public Random Randomizer { get; private set; }

        public Player CurrentPlayer { get; private set; }           // TODO

        public PlayerCharacter PlayerCharacter { get; set; }

        // Game- / mini map
        public Direction MoveDirection { get; set; }

        public GameMap GameMap { get; private set; }

        public List<RoomTile> ExploredRooms { get; private set; }   // What rooms the player have visited.

        public RoomTile PreviousRoom { get; private set; }

        public RoomTile CurrentRoom { get; private set; }           // The current room in which the player resides.

        public RoomTile NextRoom { get; private set; }

        

        // Player actions
        private bool _playerCanExit;                               // Determine if player can leave through an exit (i.e. end game).

        public bool PlayerCanMove { get; private set; }            // Determine if the player can progress to another room.

        public int PlayerCanSearch { get; private set; }           // Determine amount of times left the player can search a room.
        
        public bool PlayerInCombat { get; private set; }           // Flag for if the player character is fighting.

        public int CombatRound {  get; private set; }              // Current round of combat
        
        // Misc
        public int GameTurn { get; private set; }                   // The amount of game turns since start.

        public RoomImage ImageDisplay { get; private set; }

        // Messages
        public bool DisplayFlavorMessage { get; set; }              // Flag switch for displaying 'flavor' message


        // CARD STACKS
        private int _roomTileCount = 25;                            // The total amount of RoomTile objects in the database.

        private int _roomCardCount = 89;                            // The total amount of RoomCard objects in the database.

        private int _doorCardCount = 0;

        private int _searchCardCount = 0;

        private int _chestCardCount = 0;

        private int _corpseCardCount = 0;

        private int _trapCardCount = 0;

        private int _treasureCardCount = 0;

        private int _dragonCardCount = 0;

        public RoomTileStack RoomTileStack { get; private set; }

        public RoomCardStack RoomCardStack { get; private set; }


        public RoomCard CurrentRoomCard { get; private set; }



        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public GameSession(MainGame game, UiManager ui)
        {
            _game = game;
            _ui = ui;

            // CurrentPlayer = player;

            Randomizer = new Random();

            ExploredRooms = new List<RoomTile>();

            RoomTileStack = new RoomTileStack(_game, Randomizer, _roomTileCount);

            RoomCardStack = new RoomCardStack(_game.RoomCardService , Randomizer, _roomCardCount);

            ImageDisplay = new RoomImage(_game, Randomizer);
        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                       Initialize					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method called from the NewGameScreen upon Start game, to initialize the necessary 
            data and objects upon starting a brand new game session. 
            Sets the choosen playercharacter, initiates the first game turn and controls the game turn flow.
        ----------------------------------------------------------------------------------------------------------*/
        public void Initialize(PlayerCharacter playerCharacter)
        {
            GameTurn = 0;

            _playerCanExit = false;

            PlayerCanMove = true;

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

            // Set sun stage
            // Create and initialize gamemap
            // Set player initial room (randomized entrance)
            // 
      

            // DEBUG
            CreateTestInitialization();

            

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Set Current Room					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method to assign the CurrentRoom property the latest pulled RoomTile object.
        ----------------------------------------------------------------------------------------------------------*/
        public void SetCurrentRoom(RoomTile roomTile) => CurrentRoom = roomTile;
        // End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Set Previous Room					   
       ------------------------------------------------------------------------------------------------------------
         - Class specific method to assign the PreviousRoom property the current RoomTile object.
       ----------------------------------------------------------------------------------------------------------*/
        public void SetPreviousRoom(RoomTile roomTile) => PreviousRoom = roomTile;
        // End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                       Discard Current RoomCard					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method to discard the current active RoomCard if the player moves.
        ----------------------------------------------------------------------------------------------------------*/
        public void DiscardCurrentRoomCard() => CurrentRoomCard = null;
        // End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Set Player Can Move					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method to set the switch On/Off whether character movement is allowed or not.
        ----------------------------------------------------------------------------------------------------------*/
        public void SetPlayerCanMove(bool setFlag) => PlayerCanMove = setFlag;
        // End -----------------------------------------------------------------------------------------------------

        #endregion

        // DEBUG
        private void CreateTestInitialization()
        {
            RoomTileStack.Initialize();
            RoomCardStack.Initialize();

            GameMap = new GameMap(10, 13);
            GameMap.Initialize();

            DisplayFlavorMessage = false;
            CurrentRoom = GameMap.PickRandomStartPosition();
        }

        public void CreateTestRound()
        {
            // Pop RoomTile from RoomTile-stack
            // Place RoomTile on GameMap
            // Add visited roomtile to ExploredRooms-list

            // Progress game turn
            // Display flavor message
            // Handle any RoomTile events
            // Pull a RoomCard.
            // Handle any RoomCardEvent.

            // Return the roomtile of the gamemap in the direction moved
            RoomTile nextTile = GameMap.ReturnRoomTile(MoveDirection, CurrentRoom.Row, CurrentRoom.Col);

            // Place current RoomTile in list of visited rooms, if not yet added.
            if (!ExploredRooms.Contains(CurrentRoom))   
                ExploredRooms.Add(CurrentRoom);

            // If the player has moved from the castle entrance, they can now exit the castle.
            if (ExploredRooms.Count > 0)
                _playerCanExit = true;

            // If the next RoomTile has not yet been visited...
            if (nextTile.RoomType == RoomType.Undiscovered)
            {
                // Pull next RoomTile from the stack
                RoomTile fromStack = RoomTileStack.PullTile();

                // Update the new RoomTile's information.
                nextTile.PlaceTile(MoveDirection, fromStack, fromStack.DoorValue);

                // Set the current RoomTile as the previous room
                SetPreviousRoom(CurrentRoom);
                // Reset room stats
                ResetRoomStats();
                // Progress to the new RoomTile
                SetCurrentRoom(nextTile);
            }
            // If already visited, then just move.
            else
            {
                SetPreviousRoom(CurrentRoom);
                ResetRoomStats();
                SetCurrentRoom(nextTile);
            }

            GameTurn++;
            DisplayFlavorMessage = true;

            // If the RoomTile is empty, pull a room card. 
            if (CurrentRoom.RoomType == RoomType.Empty)
            {
                CurrentRoomCard = RoomCardStack.PullCard();
                DoRoomCardEvent(CurrentRoomCard);                
            }

            // If the RoomTile is a corridor, keep moving in an available direction.
            else if(CurrentRoom.RoomType == RoomType.Corridor)
            {
                //TODO: Allow extra movement (1 RoomTile)
            }
            // If the RoomTile has an event type of some kind, run the event.
            else
            {
                DoRoomTileEvent(CurrentRoom);
            }

            SetPlayerCanMove(true);
           
        }


        private void ResetRoomStats()
        {
            PlayerCanSearch = 2;
            PlayerInCombat = false;
        }


        private void DoRoomTileEvent(RoomTile roomTile)
        {
            switch (roomTile.RoomType)
            {
                case RoomType.DarkRoom:
                    // No RoomCard pulled.
                    // End turn
                    // Next turn roll to see which door to exit at
                    break;

                case RoomType.TreasureRoom:
                    // No RoomCard pulled.
                    // Pull treaurecards
                    // Pull dragon card
                    // End turn
                    break;

                case RoomType.Rubble:
                    // Pull RoomCard DoRoomCardEvent(CurrentRoom);
                    // Do Card Event
                    // End turn
                    // Next turn try move: Success move direction. Failure remain in place. End turn
                    break;

                case RoomType.TrapRoom:
                    // No RoomCard pulled.
                    // Pull TrapCard
                    // Do Card Event
                    // End turn

                    // Trap event triggers only once upon 1st enter. Can't search in room: No RoomCard pulled.
                    break;

                case RoomType.BottomLessPit:
                    // No RoomCard pulled
                    // Roll T12 vs DEX. Success = Survive. Turn ends.  Failure = instant death
                    // If success: can proceed next turn.
                    break;

                case RoomType.BridgeRoom:
                    // No RoomCard pulled
                    // Move back or try cross bridge. If crossing bridge: T10 vs DEX. 
                    // If success: Turn ends. Next turn proceed.
                    // If failure: Instant death.
                    break;

                case RoomType.WebbedRoom:
                    // No RoomCard pulled
                    // Turn ends
                    // Next turn either move back or try force way through webs. T12 vs. STR
                    // If success: Exit through door
                    // If failure: End turn. Try again next.
                    break;

                case RoomType.SpinRoom:
                    // No RoomCard pulled.
                    // Exit changes 2 steps clockwise.
                    // Turn ends.
                    // Next turn: Proceed or search room (can do 2 times max, then death).
                    break;
            }
        }// End -----------------------------------------------------------------------------------------------------


        private void DoRoomCardEvent(RoomCard roomCard)
        {
            switch (roomCard.CardEvent)
            {
                case RoomCardEvent.Empty: break;

                #region ITEMS -------------------------------------
                case RoomCardEvent.Item:
                    // DoItemEvent
                    // End Turn
                    break;
                #endregion

                #region CONTAINERS ----------------------------------------
                case RoomCardEvent.Chest:
                    // OpenContainerEvent:
                    // Pull ChestCard
                    // DoChestCardEvent
                    // End turn
                    break;

                case RoomCardEvent.Sarcophagus:
                    // OpenContainerEvent:
                    // Pull ChestCard
                    // DoChestCardEvent
                    // End turn
                    break;

                case RoomCardEvent.FallenWarrior:
                    // SearchCorspeEvent:
                    // Pull CorpseCard
                    // DoCorpseCardEvent
                    // End turn
                    break;
                #endregion

                #region TRAPS -------------------------------------
                case RoomCardEvent.TrapDoor:
                    // DoTrapEvent
                    // End Turn
                    break;

                case RoomCardEvent.SpearTrap:
                    // DoTrapEvent
                    // End Turn
                    break;

                case RoomCardEvent.CaveIn:
                    // DoTrapEvent
                    // End Turn
                    break;

                case RoomCardEvent.WallArrows:
                    // DoTrapEvent
                    // End Turn
                    break;
                #endregion

                #region DAMAGE --------------------------------
                case RoomCardEvent.VampireBats:
                    // DoDamageEvent
                    // End Turn
                    break;

                #endregion

                #region AMBUSH -------------------------
                case RoomCardEvent.AmbushSkeleton:
                    // DoAmbushEvent
                    // Combat
                    // End Turn
                    break;


                case RoomCardEvent.AmbushOgre:
                    // DoAmbushEvent
                    // Combat
                    // End Turn
                    break;

                case RoomCardEvent.AmbushOrc:
                    // DoAmbushEvent
                    // Combat
                    // End Turn
                    break;
                #endregion

                #region MONSTERS (STATIC) ------------------
                case RoomCardEvent.GiantSpider:
                    // DoSpiderEvent
                    // End Turn
                    break;

                case RoomCardEvent.PosionCaterpillar:
                    // DoPoisonCaterpillarEvent
                    // End Turn
                    break;

                case RoomCardEvent.CorpseEaters:
                    // DoCorpseEaterEvent
                    // End Turn
                    break;
                #endregion

                #region MONSTERS (DYNAMIC) -----------------------
                case RoomCardEvent.Darkelf:
                    // Pull MonsterCard(Darkelf)
                    // Resolve MonsterCard (monster combat or flee)
                    // If monster: flee = End Turn
                    // If Monster: combat = Player fight or flee.
                    // If Player: flee = resolve success.  (failure = dmg)
                    // If Player: combat = resolve combat
                    // End Turn
                    break;

                case RoomCardEvent.Ogre:
                    // Pull MonsterCard(Ogre)
                    // Same as above
                    break;

                case RoomCardEvent.Skeleton:
                    // Pull MonsterCard(Skeleton)
                    // Same as above
                    break;

                case RoomCardEvent.Orc:
                    // Pull MonsterCard(Orc)
                    // Same as above
                    break;

                case RoomCardEvent.TwoOrcs:
                    // Pull MonsterCard(TwoOrcs)
                    // Same as above
                    break;
                #endregion

                #region SPECIAL ----------------------
                case RoomCardEvent.TorchOut:
                    // End Turn
                    // Next turn: DoTorchOutEvent
                    break;

                case RoomCardEvent.CurseOfMadMage:
                    // DoCurseOfMadMageEvent
                    // End Turn (???)
                    break;

                case RoomCardEvent.PursuingShadow:
                    // DoPursuingShadowEvent
                    // End Turn
                    break;
                    #endregion
            }
        }// End -----------------------------------------------------------------------------------------------------

    }
}
