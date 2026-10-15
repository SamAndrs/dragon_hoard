using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragon_hoard.Content.Models.Objects.Entities.Cards
{
    ///////////////////////////////////////////// BASE CARD CLASS /////////////////////////////////////////////////
    /*
        Parent class for all Card classes such as RoomCard, TrapCard, SearchCard etc.
        Contains inheritable properties and methods common across the board of subclasses.

    :: TODO ---------------------------


    :: DONE ---------------------------

        (2026)
        

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public abstract class BaseCard
    {
        //========================================== PROPERTIES ==================================================//

        internal Texture2D CardImage { get; set; }

        internal Rectangle BoundingBox { get; set; }


        //============================================ METHODS ===================================================//

    }
}
