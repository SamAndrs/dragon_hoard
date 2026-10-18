using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace dragon_hoard.Content.Models.Objects.Entities.Cards
{
    ///////////////////////////////////////////// BASE CARD CLASS /////////////////////////////////////////////////
    /*
        Parent class for all Card classes such as RoomCard, TrapCard, SearchCard etc.
        Contains inheritable properties and methods common across the board of subclasses.

    :: TODO ---------------------------

            v.001a (10/16) Setup of basic core properties and methods.

    :: DONE ---------------------------

        (2026)
        

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public abstract class BaseCard
    {
        //========================================== PROPERTIES ==================================================//

        internal virtual Texture2D CardImage { get; set; }

        internal virtual Rectangle BoundingBox { get; set; }

        internal virtual Vector2 CardPosition { get; set; }

        internal int _width = 300;

        internal int _height = 370;


        //============================================ METHODS ===================================================//

    }
}
