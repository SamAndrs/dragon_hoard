using dragon_hoard.Content.Models.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragon_hoard.Content.Models.Objects.Entities.Cards
{
    ///////////////////////////////////////////// ROOM CARD CLASS /////////////////////////////////////////////////
    /*
        Child of the Base Card class.
        Provides data- and visual representation of game events.

    :: TODO ---------------------------

        v.001a (v.001a (10/16) Setup of basic core properties and methods.


    :: DONE ---------------------------

        (2026)
        

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RoomCard: BaseCard
    {
        //========================================== PROPERTIES ==================================================//

        /* INHERITED
            Texture2D      CardImage
            Rectangle      BoundingBox
        
        */

        public int Id { get; set; }

        public int CardNumber { get; private set; }

        public RoomCardEvent CardEvent { get; private set; }

        private int _width;

        private int _height;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public RoomCard(int cNr, Texture2D cardImage, RoomCardEvent cardEvent): base()
        {
            CardNumber = cNr; // DEBUG

            CardImage = cardImage;

            CardEvent = cardEvent;

            _width = 300;

            _height = 370;
        }

        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                  Update				   
        ------------------------------------------------------------------------------------------------------------
           - Class specific method.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(Vector2 position)
        {
            BoundingBox = new Rectangle((int)position.X, (int)position.Y, _width, _height);
        }// End -----------------------------------------------------------------------------------------------------

        /*----------------------------------------------------------------------------------------------------------
                                                  Draw				   
        ------------------------------------------------------------------------------------------------------------
           - Class specific method.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(CardImage, BoundingBox, Color.White);
            //spriteBatch.DrawString(font, $"CardNumber: {CardNumber}", new Vector2(BoundingBox.Width/2, BoundingBox.Height/2), Color.Black);
            spriteBatch.DrawString(font, $"CardNumber: {CardNumber}", new Vector2(BoundingBox.X + 10, BoundingBox.Y + (_height/2)), Color.Black);

        }// End -----------------------------------------------------------------------------------------------------
        #endregion
    }
}
