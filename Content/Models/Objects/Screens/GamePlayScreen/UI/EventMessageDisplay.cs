using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI
{
    /////////////////////////////////////// EVENT MESSAGE DISPLAY CLASS ///////////////////////////////////////////
    /*
            Child UI class to the GamePlayScreen. Handles everything related to displaying any message
            related to a RoomTile- or RoomCard event such as entering a Dark room, facing a giant spider etc.

    :: TODO ---------------------------

        * Add strings for RoomCard events.
        * Read strings from database?
        * Modify the length of textlines automatically based on size.
        * Add support for TrapCard events.

    :: DONE ---------------------------
        v.001a (10/23) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class EventMessageDisplay
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        private GameSession _session;

        private UiManager _ui;

        private SpriteFont _font;

        // FUNCTIONAL
        private Rectangle _background;

        // GRAPHICS 
        private Point _position;

        private int _width;

        private int _height;

        private string _eventMessage = string.Empty;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public EventMessageDisplay(UiManager ui, GameSession session, SpriteFont font)
        {
            _ui = ui;

            _session = session;

            _font = font;
        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                       Load					   
        ------------------------------------------------------------------------------------------------------------
          - Class method for setting up the properties of the message display. 
        ----------------------------------------------------------------------------------------------------------*/
        public void Load(Point position)
        {
            _position = position;

            _width = 200;

            _height = 300;

            _background = new Rectangle(position.X, position.Y, _width, _height);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Update					   
        ------------------------------------------------------------------------------------------------------------
          - Class method for updating the display message based on RoomTile type or RoomCarde event type.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            _eventMessage = ReturnTileMessage(_session.CurrentRoom.RoomType, _session.CurrentRoomCard);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Draw					   
        ------------------------------------------------------------------------------------------------------------
          - Class method for drawing the event message onto the screen, upon a background texture.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_ui.RectangleTexture, _background, Color.SaddleBrown);
            spriteBatch.DrawString(_font, _eventMessage, new Vector2(_position.X + 5, _position.Y + 5), Color.Gold);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Return Message					   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for returning an event message.
            If there is no RoomCard or the RoomCard is of the 'Empty' variety, then a message based on the
            [RoomType] of the current RoomTile is returned.
            Otherwise a message based on the current RoomCard [CardEvent] is returned.
        ----------------------------------------------------------------------------------------------------------*/
        private string ReturnTileMessage(RoomType tileEvent, RoomCard currentCard)
        {
            if (currentCard == null || currentCard.CardEvent == RoomCardEvent.Empty)
            {
                switch (tileEvent)
                {
                    case RoomType.Empty: return "  Empty room\nYou have come upon an\n empty room\nStill and dark.\n...For now.";

                    case RoomType.Corridor: return "  Corridor\nA long corridor opens\nbefore you.\nYou may move again.";

                    case RoomType.DarkRoom: return "  Dark room\nThe darkness in this room\nis so thick one can almost\ntouch it.\nThe light of your torch\n manage not to penetrate\n it.";

                    case RoomType.TreasureRoom: return "  Treasure Room\nThe heart of the castle opens\nbefore your eyes!\nYou have found your way\nto the hoard.\nWealth beyond measure is\n yours for the taking!";

                    case RoomType.Rubble: return "  Rubble\nThe room is full of rubble.\nTraversing it will be\nchallenging.";

                    case RoomType.BottomLessPit: return "  Bottomless Pit!\n\nA pit of darkness opens\nat your feet!\nWill you avoid falling\ninto it?";

                    case RoomType.BridgeRoom: return "  Narrow Bridge\nA narrow bridge stretches\nacross a dark, ominous\n pit.\nCrossing it will be a\nperilous challenge.";

                    //case RoomType.TrapRoom: return "ILL FORTUNE!";  // Not used since TrapCard should have the message.

                    case RoomType.WebbedRoom: return "  Webbed Room\nLayers upon layers of thick\nspiderweb covers this room.\nTraversing it will be\ndifficult.";

                    case RoomType.Exit: return "  Castle Entrance\nA stair leads up into the\ndarkness.\nYou may leave the castle\n this way.";

                    case RoomType.SpinRoom: return "  Rotating room!\nYou have stepped into a\n small room.\nYou can feel how it\nsuddenly begin to rotate!\nYou may not leave the\nsame direction you\nentered.";

                    default: return string.Empty;
                }
            }
            else
                return ReturnCardMessage(currentCard);
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                       Return Card Message				   
        ------------------------------------------------------------------------------------------------------------
          - Class support method. Returns an event message based on the current RoomCard [CardEvent] type.
        ----------------------------------------------------------------------------------------------------------*/
        private string ReturnCardMessage(RoomCard currentCard)
        {
            return string.Empty; //TODO

            /*
                 // Containers
        Chest,
        Sarcophagus,
        FallenWarrior,

        // Item
        Item,

        // Traps
        TrapDoor,
        SpearTrap,
        CaveIn,
        WallArrows,

        // Damage
        VampireBats,

        // Ambush
        AmbushSkeleton,
        AmbushOgre,
        AmbushOrc,

        // Monsters (static)
        GiantSpider,
        PosionCaterpillar,
        CorpseEaters,

        // Monsters (dynamic)
        Darkelf,
        Ogre,
        Skeleton,
        Orc,
        TwoOrcs,

        // Special events
        TorchOut,
        CurseOfMadMage,
        PursuingShadow,
        // Reshuffle,
        // WayDown,
             */
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Method					   
        ------------------------------------------------------------------------------------------------------------
          - Description
        ----------------------------------------------------------------------------------------------------------*/



        // End -----------------------------------------------------------------------------------------------------
        #endregion
    }
}
