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
        
            int            _width
            int            _height
        */

        public int Id { get; set; }

        public int CardNumber { get; private set; } // DEBUG

        public RoomCardEvent CardEvent { get; private set; }

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public RoomCard(int cNr, RoomCardEvent cardEvent): base()
        {
            CardNumber = cNr; // DEBUG

            CardEvent = cardEvent;

        }

        //============================================ METHODS ===================================================//
        #region methods


        #endregion
    }
}
