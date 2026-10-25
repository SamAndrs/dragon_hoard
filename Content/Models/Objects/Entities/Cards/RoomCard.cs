using dragon_hoard.Content.Models.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragon_hoard.Content.Models.Objects.Entities.Cards
{
    ///////////////////////////////////////////// ROOM CARD CLASS /////////////////////////////////////////////////
    /*
        The RoomCard represents and event taking place whenever a player steps into certain RoomTiles (i.e. 
        empty and rubble tiles).
        A card event ranges from anything between coming across a chest that can be opened, triggering a trap
        or facing an enemy such as an Ogre.
        A RoomCard is normally discarded at the end of a gameturn/ the player retreating from a battle etc.
        However certain RoomCards are [Persistent] and will remain in play based on various conditions.

        Certain flags also determines if different actions have been performed or not (chest opened, 
        item picked up etc.)

    :: TODO ---------------------------

       * Rework once database is implemented.


    :: DONE ---------------------------

        (2026)
         v.001a (v.001a (10/16) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RoomCard
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        public int Id { get; set; }

        public string EventName { get; private set; }           // A headline for the card of the type of event taking place.

        public RoomCardEvent CardEvent { get; private set; }    // The actual type of event taking place.

        public string CardDescription { get; private set; }     // Text describing the event of the card. TODO Property via database. Replaces the need for EventMessage to look up message based on CardEvent.

        // FUNCTIONAL
        public bool ChestOpened { get; private set; }           // Flag for the RoomActionsBar UI class of the GamePlayScreen

        public bool CorpseSearched { get; private set; }        // Flag for the RoomActionsBar UI class of the GamePlayScreen

        public bool ItemPickedUp { get; private set; }          // Flag for the RoomActionsBar UI class of the GamePlayScreen

        public bool Item { get; private set; }                  // Assigns an Item for: [CardEvent.Item]

        public bool Persistent { get; private set; }            // Flag denoting if a card event is to remain in play due to special conditions.

        // GRAPHICS
        public Texture2D CardImage { get; private set; }

        private int _width;

        private int _height;


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public RoomCard(RoomCardEvent cardEvent) : base()
        {
            CardEvent = cardEvent;

            ChestOpened = false;

            CorpseSearched = false;

            ItemPickedUp = false;

            _width = 307;

            _height = 461;

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
