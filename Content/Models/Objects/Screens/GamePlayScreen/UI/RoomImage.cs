using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI
{
    //////////////////////////////////////////// ROOM IMAGE CLASS /////////////////////////////////////////////////////
    /*
      
        Class for handling the visual representation of the RoomTiles and the RoomCard events on the
        GamePlayScreen.
        Depending on what RoomType is assigned a particular RoomTile, this class will present a
        corresponding image.
        Any such image will be overriden once a RoomCard is being pulled, as this will replace the
        image displayed.

        :: TODO ---------------------------

            * rework once database is implemented.

        :: DONE ---------------------------
            (2026)
            v.001a (10/18) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RoomImage
    {
        //========================================== PROPERTIES ==================================================//

        private MainGame _game;

        //private int _width;

        public int Width { get; private set; }
        
        public int Height { get; private set; }

        //private int _height;

        private Rectangle _imageBox;

        private Random _random;

        private Point _position;

        public Texture2D CurrentImage { get; private set; }

        // ROOMTILE EVENT IMAGES
        private Texture2D _empty;
        private Texture2D _exit;
        private Texture2D _corridor;
        private Texture2D _corridor2;
        private Texture2D _darkRoom;
        private Texture2D _treasureRoom;
        private Texture2D _rubble;
        private Texture2D _bottomlessPit;
        private Texture2D _bridgeRoom;
        private Texture2D _webbedRoom;
        private Texture2D _spinRoom;

        // ROOMCARD EVENT IMAGES
        private Texture2D _empty2;
        private Texture2D _empty3;

        // Containers
        private Texture2D _chest;
        private Texture2D _sarcophagus;
        private Texture2D _fallenWarrior;

        // Traps
        private Texture2D _trapDoor;
        private Texture2D _spearTrap;
        private Texture2D _caveIn;
        private Texture2D _wallArrows;

        // Damage
        private Texture2D _vampirBats;

        // Ambushes
        private Texture2D _ambushSkeleton;
        private Texture2D _ambushOgre;
        private Texture2D _ambushOrc;


        // Monsters (static)
        private Texture2D _giantSpider;
        private Texture2D _poisonCaterpillar;
        private Texture2D _corpseEaters;

        // Monsters (dynamic)
        private Texture2D _darkElf;
        private Texture2D _darkElf2;
        private Texture2D _darkElf3;
        private Texture2D _ogre;
        private Texture2D _ogre2;
        private Texture2D _ogre3;
        private Texture2D _skeleton;
        private Texture2D _skeleton2;
        private Texture2D _skeleton3;
        private Texture2D _orc;
        private Texture2D _orc2;
        private Texture2D _orc3;
        private Texture2D _twoOrcs;
        private Texture2D _twoOrcs2;
        private Texture2D _twoOrcs3;

        // Special
        private Texture2D _torchOut;
        private Texture2D _curseMadMage;
        private Texture2D _pursuingShadow;
        private Texture2D _pursuingShadow2;
        // private Texture2D _wayDown;
        // private Texture2D _reshuffle;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public RoomImage(MainGame game, Random randomizer)
        {
            _game = game;

            Width = 307;

            Height = 461;

            _random = randomizer;

            CurrentImage = _exit;

            LoadRoomTileEventTextures();

            LoadRoomCardEventTextures();
        }


        //============================================ METHODS ===================================================//
        #region methods

        /*----------------------------------------------------------------------------------------------------------
                                                 Initialize				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. Loads all the necessary textures and delegates the creation of the stack of
            RoomTiles to sub-method.
        ----------------------------------------------------------------------------------------------------------*/
        public void Initialize(Point position)
        {
            _position = position;
            _imageBox = new Rectangle(position.X, position.Y, Width, Height);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                 Update				   
        ------------------------------------------------------------------------------------------------------------
          - Class method. Sets and updates the image representing the current RoomTile type event and/ or 
            card event type. If both images are used; the Tile event image will be replaced by the 
            Card event image, once the referenced (class)EventMessageDisplay.[MessageTimer] reaches zero.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime, float timer, int randomResult, RoomType roomType, RoomCard roomCard)
        {
            SetTileEventImage(roomType);

            if (roomCard != null && timer <= 0)
                SetRoomCardEventImage(randomResult, roomCard);

        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                 Draw				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. Draws the currently active image onto the GamePlayScreen.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            if(CurrentImage != null)
                spriteBatch.Draw(CurrentImage, _imageBox, Color.White);
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                 Set Tile Event Image				   
        ------------------------------------------------------------------------------------------------------------
          - Class support method. Sets the current image to be displayed based on a RoomTile's
            [RoomType] property.
        ----------------------------------------------------------------------------------------------------------*/
        private void SetTileEventImage(RoomType roomType)
        {
            switch (roomType)
            {
                //case RoomType.Empty:
                //    CurrentImage = _empty; break;

                case RoomType.Exit:
                    CurrentImage = _exit; break;

                case RoomType.Corridor:
                    int result = _random.Next(1);
                    if (result == 0)
                        CurrentImage = _corridor;
                    else
                        CurrentImage = _corridor2;
                    break;

                case RoomType.DarkRoom:
                    CurrentImage = _darkRoom; break;

                case RoomType.Rubble:
                    CurrentImage = _rubble; break;

                case RoomType.TrapRoom:
                    CurrentImage = _empty; break;

                case RoomType.BottomLessPit:
                    CurrentImage = _bottomlessPit; break;

                case RoomType.BridgeRoom:
                    CurrentImage = _bridgeRoom; break;

                case RoomType.WebbedRoom:
                    CurrentImage = _webbedRoom; break;

                case RoomType.SpinRoom:
                    CurrentImage = _spinRoom; break;

                case RoomType.TreasureRoom:
                    CurrentImage = _treasureRoom; break;

                default:
                    CurrentImage = _empty; break;
            }
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                 Set Room Card Event Image				   
        ------------------------------------------------------------------------------------------------------------
          - Class support method. Sets the current image to be displayed based on a RoomCard's
            [CardEvent] property.
        ----------------------------------------------------------------------------------------------------------*/
        //private void SetRoomCardEventImage(Random randomizer, RoomCard roomCard)
        private void SetRoomCardEventImage(int result, RoomCard roomCard)
        {
            switch (roomCard.CardEvent)
            {
                case RoomCardEvent.Empty:
                    //int result = randomizer.Next(2);
                    if (result == 0) CurrentImage = _empty;
                    else if (result == 1) CurrentImage = _empty2;
                    else CurrentImage = _empty3;
                    break;

                // Containers
                case RoomCardEvent.Chest: CurrentImage = _chest; break;
                case RoomCardEvent.Sarcophagus: CurrentImage = _sarcophagus; break;
                case RoomCardEvent.FallenWarrior: CurrentImage = _fallenWarrior; break;
                
                // Traps
                case RoomCardEvent.TrapDoor: CurrentImage = _trapDoor; break;
                case RoomCardEvent.SpearTrap: CurrentImage = _spearTrap; break;
                case RoomCardEvent.CaveIn: CurrentImage = _caveIn; break;
                case RoomCardEvent.WallArrows: CurrentImage = _wallArrows; break;
                
                // Damage
                case RoomCardEvent.VampireBats: CurrentImage = _vampirBats; break;

                // Ambushes
                case RoomCardEvent.AmbushSkeleton: CurrentImage = _ambushSkeleton; break;
                case RoomCardEvent.AmbushOgre: CurrentImage = _ambushOgre; break;
                case RoomCardEvent.AmbushOrc: CurrentImage = _ambushOrc; break;

                // Monsters (static)
                case RoomCardEvent.GiantSpider: CurrentImage = _giantSpider; break;
                case RoomCardEvent.PosionCaterpillar: CurrentImage = _poisonCaterpillar; break;
                case RoomCardEvent.CorpseEaters: CurrentImage = _corpseEaters; break;

                // Monsters (dynamic)
                case RoomCardEvent.Darkelf:
                    //int dElf = randomizer.Next(2);
                    if (result == 0) CurrentImage = _darkElf;
                    if (result == 1) CurrentImage = _darkElf2;
                    else CurrentImage = _darkElf3;
                    break;

                case RoomCardEvent.Ogre:
                    //int ogre = randomizer.Next(2);
                    if (result == 0) CurrentImage = _ogre;
                    if (result == 1) CurrentImage = _ogre2;
                    else CurrentImage = _ogre3;
                    break;

                case RoomCardEvent.Skeleton:
                    //int skel = randomizer.Next(2);
                    if (result == 0) CurrentImage = _skeleton;
                    if (result == 1) CurrentImage = _skeleton2;
                    else CurrentImage =  _skeleton3;
                    break;

                case RoomCardEvent.Orc:
                    //int orc = randomizer.Next(2);
                    if (result == 0) CurrentImage = _orc;
                    if (result == 1) CurrentImage = _orc2;
                    else CurrentImage = _orc3;
                    break;
                
                case RoomCardEvent.TwoOrcs:
                    //int tOrcs = randomizer.Next(2);
                    if (result == 0) CurrentImage = _twoOrcs;
                    if (result == 1) CurrentImage = _twoOrcs2;
                    else CurrentImage = _twoOrcs3;
                    break;

                // Special
                case RoomCardEvent.TorchOut: CurrentImage = _torchOut; break;
                case RoomCardEvent.CurseOfMadMage: CurrentImage = _curseMadMage; break;
                
                case RoomCardEvent.PursuingShadow:
                    //int shadow = randomizer.Next(1);
                    if (result <= 1) CurrentImage = _pursuingShadow;
                    else CurrentImage = _pursuingShadow2;
                    break;
              
                default:
                    CurrentImage = _empty; break;

            }
        }// End -----------------------------------------------------------------------------------------------------

        private void LoadRoomTileEventTextures()
        {
            _empty = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomEmpty");
            _exit = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomExit");
            _corridor = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomCorridor");
            _corridor2 = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomCorridor2");
            _darkRoom = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomDark");
            _treasureRoom = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomTreasure");
            _rubble = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomRubble");
            _bottomlessPit = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomBottomlessPit");
            _bridgeRoom = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomBridge");
            _webbedRoom = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomWebbed");
            _spinRoom = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomSpin");
        }// End -----------------------------------------------------------------------------------------------------


        private void LoadRoomCardEventTextures()
        {
            _empty2 = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomEmpty2");
            _empty3 = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomEmpty3");

            // Containers
            _chest = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomChest");
            _sarcophagus = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomSarcophagus");
            _fallenWarrior = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomFallenWarrior");

            // Traps
            _trapDoor = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomTrapDoor");
            _spearTrap = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomSpearTrap");
            _caveIn = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomCaveIn");
            _wallArrows = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomWallArrows");

            // Damage
            _vampirBats = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomVampireBats");

            // Ambushes
            _ambushSkeleton = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomAmbushSkeleton");
            _ambushOgre = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomAmbushOgre");
            _ambushOrc = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomAmbushOrc");

            // Monsters (static)
            _giantSpider = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomGiantSpider");
            _poisonCaterpillar = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomPoisonCaterpillar");
            _corpseEaters = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomCorpseEaters");

            // Monster (dynamic)
            _darkElf = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomDarkElf");
            _darkElf2 = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomDarkElf2");
            _darkElf3 = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomDarkElf3");

            _ogre = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomOgre");
            _ogre2 = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomOgre2");
            _ogre3 = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomOgre3");

            _skeleton = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomSkeleton");
            _skeleton2 = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomSkeleton2");
            _skeleton3 = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomSkeleton3");

            _orc = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomOrc");
            _orc2 = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomOrc2");
            _orc2 = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomOrc3");

            _twoOrcs = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomTwoOrcs");
            _twoOrcs2 = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomTwoOrcs2");
            _twoOrcs3 = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomTwoOrcs3");

            // Special
            _torchOut = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomTorchOut");
            _curseMadMage = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomCurseMadMage");
            _pursuingShadow = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomPursuingShadow");
            _pursuingShadow2 = _game.Content.Load<Texture2D>("Images/RoomCardImages/img_roomPursuingShadow2");

        }// End -----------------------------------------------------------------------------------------------------

        #endregion

    }

}
