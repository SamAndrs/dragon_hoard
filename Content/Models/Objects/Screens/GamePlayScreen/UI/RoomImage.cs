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
                                                 Set Current Image				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. Checks wether a RoomCard is actively picked or not. 
            If no RoomCard then the class will display a default RoomTile image coresponding to the RoomType.
            If there IS an active RoomCard, the class will set the active image to display to the texture 
            assosciated with the RoomCard's [CardEvent].
        ----------------------------------------------------------------------------------------------------------*/
        public void SetCurrentImage(RoomType roomType, RoomCard roomCard)
        {
            if (roomCard == null)
            {
                switch (roomType)
                {
                    //case RoomType.Empty:
                    //    CurrentImage = _empty; break;

                    case RoomType.Exit:
                        CurrentImage = _exit; break;

                    case RoomType.Corridor:
                        int result = _random.Next(1);
                        if(result == 0)
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
            }

            else
            {
                //CurrentImage = roomCard.CardImage;
                CurrentImage = SetRoomCardEventImage(_random, roomCard);
            }
        }// End -----------------------------------------------------------------------------------------------------


        private Texture2D SetRoomCardEventImage(Random randomizer, RoomCard roomCard)
        {
            switch (roomCard.CardEvent)
            {
                case RoomCardEvent.Empty:
                    int result = randomizer.Next(2);
                    if (result == 0)
                        return _empty;
                    else if (result == 1)
                        return _empty2;
                    else
                        return _empty3;

                // Containers
                case RoomCardEvent.Chest: return _chest;
                case RoomCardEvent.Sarcophagus: return _sarcophagus;
                case RoomCardEvent.FallenWarrior: return _fallenWarrior;
                
                // Traps
                case RoomCardEvent.TrapDoor: return _trapDoor;
                case RoomCardEvent.SpearTrap: return _spearTrap;
                case RoomCardEvent.CaveIn: return _caveIn;
                case RoomCardEvent.WallArrows: return _wallArrows;
                
                // Damage
                case RoomCardEvent.VampireBats: return _vampirBats;

                // Ambushes
                case RoomCardEvent.AmbushSkeleton: return _ambushSkeleton;
                case RoomCardEvent.AmbushOgre: return _ambushOgre;
                case RoomCardEvent.AmbushOrc: return _ambushOrc;

                // Monsters (static)
                case RoomCardEvent.GiantSpider: return _giantSpider;
                case RoomCardEvent.PosionCaterpillar: return _poisonCaterpillar;
                case RoomCardEvent.CorpseEaters: return _corpseEaters;

                // Monsters (dynamic)
                case RoomCardEvent.Darkelf:
                    int dElf = randomizer.Next(2);
                    if (dElf == 0) return _darkElf;
                    if (dElf == 1) return _darkElf2;
                    else return _darkElf3;

                case RoomCardEvent.Ogre:
                    int ogre = randomizer.Next(2);
                    if (ogre == 0) return _ogre;
                    if (ogre == 1) return _ogre2;
                    else return _ogre3;


                case RoomCardEvent.Skeleton:
                    int skel = randomizer.Next(2);
                    if (skel == 0) return _skeleton;
                    if (skel == 1) return _skeleton2;
                    else return _skeleton3;

                case RoomCardEvent.Orc:
                    int orc = randomizer.Next(2);
                    if (orc == 0) return _orc;
                    if (orc == 1) return _orc2;
                    else return _orc3;
                
                case RoomCardEvent.TwoOrcs:
                    int tOrcs = randomizer.Next(2);
                    if (tOrcs == 0) return _twoOrcs;
                    if (tOrcs == 1) return _twoOrcs2;
                    else return _twoOrcs3;

                // Special
                case RoomCardEvent.TorchOut: return _torchOut;
                case RoomCardEvent.CurseOfMadMage: return _curseMadMage;
                
                case RoomCardEvent.PursuingShadow:
                    int shadow = randomizer.Next(1);
                    if (shadow == 0) return _pursuingShadow;
                    else return _pursuingShadow2;

                default: return null;

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
