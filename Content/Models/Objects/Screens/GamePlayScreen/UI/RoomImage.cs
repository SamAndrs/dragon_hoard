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
            v.001b (10/26) Altered to account for database implementation.
            v.001a (10/18) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RoomImage
    {
        //========================================== PROPERTIES ==================================================//

        private MainGame _game;

        public int Width { get; private set; }
        
        public int Height { get; private set; }

        private Rectangle _imageBox;


        private Point _position;

        public Texture2D CurrentImage { get; private set; }

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public RoomImage(MainGame game)
        {
            _game = game;

            Width = 307;

            Height = 461;
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
        public void Update(GameTime gameTime, float timer, RoomTile currentTile, RoomCard roomCard)
        {
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

        #endregion

    }

}
