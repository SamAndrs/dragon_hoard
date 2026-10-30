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

       * Rework once database is implemented.


    :: DONE ---------------------------

        (2026)
         v.001a (v.001a (10/16) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RoomCard: BaseCard
    {
        //========================================== PROPERTIES ==================================================//

        /* INHERITED
            Texture2D      CardImage
            Rectangle      BoundingBox
        
            int            _width
            int            _height
        */

        // DATA
        public int Id { get; set; }

        public int CardNumber { get; private set; } // DEBUG

        public RoomCardEvent CardEvent { get; private set; }

        //public string CardMessage { get; private set; }   // TODO Property via database. Replaces the need for EventMessage to look up message based on CardEvent. To Replace CardEvent

        // FUNCTIONAL
        public bool ChestOpened { get; private set; }       // Flag for the RoomActionsBar UI class of the GamePlayScreen

        public bool CorpseSearched { get; private set; }    // Flag for the RoomActionsBar UI class of the GamePlayScreen

        public bool ItemPickedUp { get; private set; }      // Flag for the RoomActionsBar UI class of the GamePlayScreen



        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public RoomCard(int cNr, RoomCardEvent cardEvent) : base()
        {
            CardNumber = cNr; // DEBUG

            CardEvent = cardEvent;

            ChestOpened = false;

            CorpseSearched = false;

            ItemPickedUp = false;

        }

        //============================================ METHODS ===================================================//
        #region methods


        public void PlayerOpensChest() => ChestOpened = true;
        

        public void PlayerSearchCorpse() => CorpseSearched = true;

        public void ItemIsPickedUp()
        {
            if (!ItemPickedUp)
                ItemPickedUp = true;
        }

        public void ResetStats()
        {
            ChestOpened = false;
            CorpseSearched = false;
            ItemPickedUp = false;
        }

        #endregion
    
        public void DisplayCardData(SpriteBatch spriteBatch, Point position, Texture2D squareTexture, SpriteFont font)
        {
            Rectangle background = new Rectangle(position.X, position.Y, 200, 250);

            spriteBatch.Draw(squareTexture, background, Color.DarkBlue);

            spriteBatch.DrawString(font, "RoomCard data", new Vector2(position.X + 55, position.Y + 5), Color.White);

            spriteBatch.DrawString(font, $"CardEvent: {CardEvent.ToString()}", new Vector2(position.X + 5, position.Y + 25), Color.White);

            spriteBatch.DrawString(font, $"Corpse searched: {CorpseSearched}", new Vector2(position.X + 5, position.Y + 45), Color.White);

            spriteBatch.DrawString(font, $"Chest Opened: {ChestOpened}", new Vector2(position.X + 5, position.Y + 65), Color.White);

            // spriteBatch.DrawString(font, $"Item: {Item.ToString()}", new Vector2(position.X + 5, position.Y + 85), Color.White);

            spriteBatch.DrawString(font, $"Item Picked Up: {ItemPickedUp}", new Vector2(position.X + 5, position.Y + 105), Color.White);
        }
    }
}
