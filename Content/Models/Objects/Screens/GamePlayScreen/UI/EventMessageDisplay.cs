using Autofac.Core;
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
        v.001b (10/24) Added CardEvent message timer.
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

        public float MessageTimer { get; set; }

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public EventMessageDisplay(UiManager ui, GameSession session, SpriteFont font)
        {
            _ui = ui;

            _session = session;

            _font = font;

            MessageTimer = 2f;
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
            If a RoomCard is pulled, the RoomTile message will be replaced after [MessageTimer]x seconds.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            _eventMessage = ReturnTileMessage(_session.CurrentRoom.RoomType);

            if (_session.CurrentRoomCard != null && MessageTimer <= 0)
            {
                _eventMessage = ReturnCardMessage(_session.CurrentRoomCard);
                MessageTimer = 0;
            }
            else
                MessageTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

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
        //private string ReturnTileMessage(RoomType tileEvent, RoomCard currentCard)
        private string ReturnTileMessage(RoomType tileEvent)
        {
            //if (currentCard == null || currentCard.CardEvent == RoomCardEvent.Empty)
            //if (currentCard.CardEvent == RoomCardEvent.Empty)
            {
                switch (tileEvent)
                {
                    case RoomType.Empty: return "  Empty room\n\nYou have come upon an\n empty room\nStill and dark.\n...For now.";

                    case RoomType.Corridor: return "  Corridor\n\nA long corridor opens\nbefore you.\nYou may move again.";

                    case RoomType.DarkRoom: return "  Dark room\n\nThe darkness in this room\nis so thick one can almost\ntouch it.\nThe light of your torch\n manage not to penetrate\n it.";

                    case RoomType.TreasureRoom: return "  Treasure Room!\n\nYou have found your way\nto the hoard.\nWealth beyond measure is\n yours for the taking!";

                    case RoomType.Rubble: return "  Rubble\n\nThe room is full of rubble.\nTraversing it will be\nchallenging.";

                    case RoomType.BottomLessPit: return "  Bottomless Pit!\n\nA pit of darkness opens\nat your feet!\nWill you avoid falling\ninto it?";

                    case RoomType.BridgeRoom: return "  Narrow Bridge\n\nA narrow bridge stretches\nacross a dark, ominous\n pit.\nCrossing it will be a\nperilous challenge.";

                    //case RoomType.TrapRoom: return "ILL FORTUNE!";  // Not used since TrapCard should have the message.

                    case RoomType.WebbedRoom: return "  Webbed Room\n\nLayers upon layers of thick\nspiderweb covers this room.\nTraversing it will be\ndifficult.";

                    case RoomType.Exit: return "  Castle Entrance\n\nA stair leads up into the\ndarkness.\nYou may leave the castle\n this way.";

                    case RoomType.SpinRoom: return "  Rotating room!\n\nYou have stepped into a\n small room.\nYou can feel how it\nsuddenly begin to rotate!\nYou may not leave the\nsame direction you\nentered.";

                    default: return string.Empty;
                }
            }
            //else
            //    return ReturnCardMessage(currentCard);
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                       Return Card Message				   
        ------------------------------------------------------------------------------------------------------------
          - Class support method. Returns an event message based on the current RoomCard [CardEvent] type.
        ----------------------------------------------------------------------------------------------------------*/
        private string ReturnCardMessage(RoomCard currentCard)
        {
            switch (currentCard.CardEvent)
            {
                case RoomCardEvent.Empty:
                    return "Empty room\n\nYou have come upon an\n empty room\nStill and dark.\n...For now.";

                // Containers
                case RoomCardEvent.Chest:
                    return "A Chest\n\nYou have found a chest.\nDare you open it?";

                case RoomCardEvent.Sarcophagus:
                    return "A Sarcophagus\n\nYou have come upon\nthe stone tomb of\nsome long dead hero.\nDare you push the\nlid aside?";

                case RoomCardEvent.FallenWarrior:
                    return "Fallen warrior\n\nThe remains of an ill\nfated warrior lies\non the floor.";


                // Item
                case RoomCardEvent.Item:
                    return "Item\n\nYou have found an item";


                // Traps
                case RoomCardEvent.TrapDoor:
                    return "TRAP!\n\nA trapdoor suddenly swing\nopen beneath your feet!\nOnly swift action\nmay save you";

                case RoomCardEvent.SpearTrap:
                    return "TRAP!\n\nWalls littered with spears\nrapidly close in on you!\nThree pressure plates\nare on the floor.\nYou have time to\nstep on two of\nthem.";

                case RoomCardEvent.CaveIn:
                    return "TRAP!\n\nThe ceiling suddenly\ncollapses on you!\nWill you avoid certain deathh?";

                case RoomCardEvent.WallArrows:
                    return "TRAP!\n\nArrows shoot out\nof the walls.\nCan you avoid taking\ndamage?";


                // Damage
                case RoomCardEvent.VampireBats:
                    return "Vampire bats!\n\nA swarm of bloodsucking\nvermin assail you!\nWill you manage\nto avoid their sharp\nfangs?";


                // Ambush
                case RoomCardEvent.AmbushSkeleton:
                    return "AMBUSH!\n\nYou have been ambushed\nby a skeleton.\nWill you survive the\nattack?";

                case RoomCardEvent.AmbushOgre:
                    return "AMBUSH!\n\nYou have been ambushed\nby an ogre.\nWill you survive the\nattack?";

                case RoomCardEvent.AmbushOrc:
                    return "AMBUSH!\n\nYou have been ambushed\nby an orc.\nWill you survive the\nattack?";


                // Monsters (static)
                case RoomCardEvent.GiantSpider:
                    return "Giant spider!\n\nA large, hideous arachnid\nblocks your path.\nYou will have to\n fight or flee";

                case RoomCardEvent.PosionCaterpillar:
                    return "Poisonous Caterpillar!\n\nYou have stumbled upon\na deadly freak.\nCan you avoid its\npoisonous sting?";

                case RoomCardEvent.CorpseEaters:
                    return "Corpse eaters!\n\nA nightmarish army\nof undead devourers\ncrawl out from under\nthe stones of the\nflooring!\nYou must defend\nyourself or perish";

                case RoomCardEvent.Darkelf:
                    return "Dark elf!\n\nA sinister goblionid\nlurks in the shadows.\nWill you challenge it\nor flee?";

                case RoomCardEvent.Ogre:
                    return "An Ogre!\n\nA towering figure\nlooms before you.\nDare you challenge\nthis brute?";

                case RoomCardEvent.Skeleton:
                    return "A Skeleton!\n\nThe animated remains\nof a dead warrior\nblocks your path.\nYou must fight or flee!";

                case RoomCardEvent.Orc:
                    return "An Orc!\n\nThe growling figure\nof a brutish orc\nblocks your path.\nYou must fight it\nor flee!";

                    case RoomCardEvent.TwoOrcs:
                    return "Two Orcs!\n\nYou have stumbled\nupon a duo of orc\nguards!\nYou must fight them\nor flee!";


                // Special
                case RoomCardEvent.TorchOut:
                    return "Torch out!\n\nYour torch suddenly\nfades!\nYou must spend time\ntrying to ignite it\n again before you may\nproceed.";

                case RoomCardEvent.CurseOfMadMage:
                    return "Curse of the mad mage!\n\nLaughter of madness and\na cascade of blinding\nlights assail your\n senses!\n You can feel the\ncastle shifting";

                case RoomCardEvent.PursuingShadow:
                    return "A pursuing shadow!\n\nYou are being stalked\n by a sinister shadow.\nOnly time will tell\nwhen it moves in\nfor the kill!";

                //case RoomCardEvent.Reshuffle:

                //case RoomCardEvent.WayDown:

                default: return string.Empty;

            }
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
