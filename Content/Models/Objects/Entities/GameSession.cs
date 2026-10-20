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

        public PlayerCharacter PlayerCharacter { get;  set; }

        // Game- / mini map
        public Direction MoveDirection { get; set; }

        public GameMap GameMap { get; private set; }

        public List<RoomTile> ExploredRooms { get; private set; }   // What rooms the player have visited.

        public RoomTile CurrentRoom { get; private set; }           // The current room in which the player resides.

        public RoomTile NextRoom { get; private set; }

        

        // Player actions
        private bool _playerCanExit;                               // Determine if player can leave through an exit (i.e. end game).

        public bool PlayerCanMove { get; private set; }            // Determine if the player can progress to another room.

        public int PlayerCanSearch { get; private set; }          // Determine amount of times left the player can search a room.
         
        
        // Misc
        public int GameTurn { get; private set; }                   // The amount of game turns since start.

        public RoomImage ImageDisplay { get; private set; }

        // Messages
        public bool DisplayFlavorMessage { get; set; }              // Flag switch for displaying 'flavor' message

        public string FlavorMessage { get; private set; }           // Flavor message text

        public bool DisplayEventMessage { get; set; }               // Flag switch for displaying event message

        public string EventMessage { get; private set; }            // Text explaining what event is transpiring.

        
        // CARD STACKS
        public RoomTileStack RoomTileStack { get; private set; }

        public RoomCardStack RoomCardStack { get; private set; }


        public RoomCard CurrentRoomCard { get; private set; }

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public GameSession(MainGame game, UiManager ui)
        {
            _game = game;
            _ui = ui;

            Randomizer = new Random();

            ExploredRooms = new List<RoomTile>();

            RoomTileStack = new RoomTileStack(_game, Randomizer, 25);

            RoomCardStack = new RoomCardStack(_ui, Randomizer, 25);

            ImageDisplay = new RoomImage(_game);
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

            GameMap = new GameMap(10, 13, _ui.RectangleTexture);
            GameMap.MapPosition = new Vector2((_game.GraphicsDevice.Viewport.Width - 300), 120);
            GameMap.Initialize();

            ImageDisplay.Initialize(Randomizer);

            DisplayFlavorMessage = false;
            CurrentRoom = GameMap.PickRandomStartPosition();
            ImageDisplay.SetCurrentImage(CurrentRoom.RoomType, CurrentRoomCard);
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
            DisplayFlavorMessage = true;

            // If the RoomTile is empty, pull a room card. 
            if (CurrentRoom.RoomType == RoomType.Empty)
            {
                CurrentRoomCard = RoomCardStack.PullCard();
                ImageDisplay.SetCurrentImage(CurrentRoom.RoomType, CurrentRoomCard);
                DoRoomCardEvent(CurrentRoomCard);
            }

            // If the RoomTile is a corridor, keep moving in an available direction.
            else if(CurrentRoom.RoomType == RoomType.Corridor)
            {
                //TODO: Allow extra movement (1 RoomTile)
                ImageDisplay.SetCurrentImage(CurrentRoom.RoomType, CurrentRoomCard);
            }
            // If the RoomTile has an event type of some kind, run the event.
            else
            {
                ImageDisplay.SetCurrentImage(CurrentRoom.RoomType, CurrentRoomCard);
                DoRoomTileEvent(CurrentRoom);
            }

            SetPlayerCanMove(true);
        }


        private void DoRoomTileEvent(RoomTile roomTile)
        {
            switch (roomTile.RoomType)
            {
                case RoomType.DarkRoom:
                    // No RoomCard pulled.
                    // End turn
                    // Next turn roll to see which door to exit at
                    EventMessage = "The room is pitch black";
                    DisplayEventMessage = true;
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
                    EventMessage = "The room is full of rubble";
                    DisplayEventMessage = true;
                    break;

                case RoomType.TrapRoom:
                    // No RoomCard pulled.
                    // Pull TrapCard
                    // Do Card Event
                    // End turn

                    // Trap event triggers only once upon 1st enter. Can't search in room: No RoomCard pulled.
                    EventMessage = "A trap has triggered!";
                    DisplayEventMessage = true;
                    break;

                case RoomType.BottomLessPit:
                    // No RoomCard pulled
                    // Roll T12 vs DEX. Success = Survive. Turn ends.  Failure = instant death
                    // If success: can proceed next turn.
                    EventMessage = "A bottomless pit!";
                    DisplayEventMessage = true;
                    break;

                case RoomType.BridgeRoom:
                    // No RoomCard pulled
                    // Move back or try cross bridge. If crossing bridge: T10 vs DEX. 
                    // If success: Turn ends. Next turn proceed.
                    // If failure: Instant death.
                    EventMessage = "A narrow bridge over a dark pit";
                    DisplayEventMessage = true;
                    break;

                case RoomType.WebbedRoom:
                    // No RoomCard pulled
                    // Turn ends
                    // Next turn either move back or try force way through webs. T12 vs. STR
                    // If success: Exit through door
                    // If failure: End turn. Try again next.
                    EventMessage = "The room is full of spider webs";
                    DisplayEventMessage = true;
                    break;

                case RoomType.SpinRoom:
                    // No RoomCard pulled.
                    // Exit changes 2 steps clockwise.
                    // Turn ends.
                    // Next turn: Proceed or search room (can do 2 times max, then death).
                    EventMessage = "The room begins to turn";
                    DisplayEventMessage = true;
                    break;
            }
        }// End -----------------------------------------------------------------------------------------------------


        private void DoRoomCardEvent(RoomCard roomCard)
        {
            switch (roomCard.CardEvent)
            {
                case RoomCardEvent.Empty:
                    EventMessage = "The room is empty";
                    DisplayEventMessage = true;
                    break;

                #region CONTAINERS ----------------------------------------
                case RoomCardEvent.Chest:
                    // OpenContainerEvent:
                    // Pull ChestCard
                    // DoChestCardEvent
                    // End turn
                    EventMessage = "A Chest";
                    DisplayEventMessage = true;
                    break;

                case RoomCardEvent.Sarcophagus:
                    // OpenContainerEvent:
                    // Pull ChestCard
                    // DoChestCardEvent
                    // End turn
                    EventMessage = "A Sarcophagus";
                    DisplayEventMessage = true;
                    break;

                case RoomCardEvent.FallenWarrior:
                    // SearchCorspeEvent:
                    // Pull CorpseCard
                    // DoCorpseCardEvent
                    // End turn
                    EventMessage = "A Fallen warrior";
                    DisplayEventMessage = true;
                    break;
                #endregion

                #region TRAPS -------------------------------------
                case RoomCardEvent.TrapDoor:
                    // DoTrapEvent
                    // End Turn
                    EventMessage = "TRAP!";
                    DisplayEventMessage = true;
                    break;

                case RoomCardEvent.SpearTrap:
                    // DoTrapEvent
                    // End Turn
                    EventMessage = "TRAP!";
                    DisplayEventMessage = true;
                    break;

                case RoomCardEvent.CaveIn:
                    // DoTrapEvent
                    // End Turn
                    EventMessage = "TRAP!";
                    DisplayEventMessage = true;
                    break;

                case RoomCardEvent.WallArrows:
                    // DoTrapEvent
                    // End Turn
                    EventMessage = "TRAP!";
                    DisplayEventMessage = true;
                    break;
                #endregion

                #region DAMAGE --------------------------------
                case RoomCardEvent.VampireBats:
                    // DoDamageEvent
                    // End Turn
                    EventMessage = "Vampire bats!";
                    DisplayEventMessage = true;
                    break;

                #endregion

                #region AMBUSH -------------------------
                case RoomCardEvent.AmbushSkeleton:
                    // DoAmbushEvent
                    // Combat
                    // End Turn
                    EventMessage = "AMBUSHED!";
                    DisplayEventMessage = true;
                    break;


                case RoomCardEvent.AmbushOgre:
                    // DoAmbushEvent
                    // Combat
                    // End Turn
                    EventMessage = "AMBUSHED!";
                    DisplayEventMessage = true;
                    break;

                case RoomCardEvent.AmbushOrc:
                    // DoAmbushEvent
                    // Combat
                    // End Turn
                    EventMessage = "AMBUSHED!";
                    DisplayEventMessage = true;
                    break;
                #endregion

                #region MONSTERS (STATIC) ------------------
                case RoomCardEvent.GiantSpider:
                    // DoSpiderEvent
                    // End Turn
                    EventMessage = "Giant spider!";
                    DisplayEventMessage = true;
                    break;

                case RoomCardEvent.PosionCaterpillar:
                    // DoPoisonCaterpillarEvent
                    // End Turn
                    EventMessage = "Poisonous Caterpillar";
                    DisplayEventMessage = true;
                    break;

                case RoomCardEvent.CorpseEaters:
                    // DoCorpseEaterEvent
                    // End Turn
                    EventMessage = "Corpse eaters!";
                    DisplayEventMessage = true;
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
                    EventMessage = "A Darkelf!";
                    DisplayEventMessage = true;
                    break;

                case RoomCardEvent.Ogre:
                    // Pull MonsterCard(Ogre)
                    // Same as above
                    EventMessage = "An Ogre!";
                    DisplayEventMessage = true;
                    break;

                case RoomCardEvent.Skeleton:
                    // Pull MonsterCard(Skeleton)
                    // Same as above
                    EventMessage = "A Skeleton!";
                    DisplayEventMessage = true;
                    break;

                case RoomCardEvent.Orc:
                    // Pull MonsterCard(Orc)
                    // Same as above
                    EventMessage = "An Orc!";
                    DisplayEventMessage = true;
                    break;

                case RoomCardEvent.TwoOrcs:
                    // Pull MonsterCard(TwoOrcs)
                    // Same as above
                    EventMessage = "Two Orcs!!";
                    DisplayEventMessage = true;
                    break;
                #endregion

                #region SPECIAL ----------------------
                case RoomCardEvent.TorchOut:
                    // End Turn
                    // Next turn: DoTorchOutEvent
                    EventMessage = "The Torch is out!";
                    DisplayEventMessage = true;
                    break;

                case RoomCardEvent.CurseOfMadMage:
                    // DoCurseOfMadMageEvent
                    // End Turn (???)
                    EventMessage = "Curse of the mad mage!";
                    DisplayEventMessage = true;
                    break;

                case RoomCardEvent.PursuingShadow:
                    // DoPursuingShadowEvent
                    // End Turn
                    EventMessage = "A shadow pursues you!";
                    DisplayEventMessage = true;
                    break;
                #endregion
            }
        }// End -----------------------------------------------------------------------------------------------------

        private string RandomFlavorMessage(Random randomizer)
        {
            int result = randomizer.Next(3);

            switch (result)
            {
                case 0: return "Mind the path of the sun. As night draws closer the denizens of the castle grows fiercer";

                case 1: return "The  brave,  the  foolhardy,  the  desperate.  The  promise  of  the  hoard  calls  to  them  each.";

                case 2: return "Take  heed  to  escape  the  castle  before  sunset,  for  none  have  survived  the  horrors  of  the  night.";

                case 3: return "The  promise  of  wealth  beckons.  Lurking  close  behind  is  the  promise  of  peril.";

                case 4: return "Night  draws  ever  closer,  as  another  hour  passes.";

                default: return "Bravery  or  madness;  to challenge  the  castle  is  both";
            }
        }// End -----------------------------------------------------------------------------------------------------
    }
}
