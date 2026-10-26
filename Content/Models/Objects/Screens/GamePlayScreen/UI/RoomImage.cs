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

        public int Width { get; private set; }
        
        public int Height { get; private set; }

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


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public RoomImage(MainGame game, Random randomizer)
        {
            _game = game;

            Width = 307;

            Height = 461;

            _random = randomizer;
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
        //public void Update(GameTime gameTime, float timer, RoomType roomType, RoomCard roomCard)
        public void Update(GameTime gameTime, float timer, RoomTile currentTile, RoomCard roomCard)
        {
            //SetTileEventImage(roomType);
            CurrentImage = currentTile.TileRoomImage;

            if (roomCard != null && timer <= 0)
                CurrentImage = roomCard.CardImage;

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



        #endregion

    }

}
