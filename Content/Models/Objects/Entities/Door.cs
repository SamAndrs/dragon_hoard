using dragon_hoard.Content.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragon_hoard.Content.Models.Objects.Entities
{
    //////////////////////////////////////////// DOOR CLASS /////////////////////////////////////////////////////
    /*
      
    
        :: TODO ---------------------------

            v.001a (10/08) Setup of basic core properties and methods.



        :: DONE ---------------------------
            (2026)

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class Door
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        public Direction Direction { get; set; }

        public DoorType DoorType { get; private set; }

        public bool IsLocked { get; set; }

        public bool IsTrapped { get; set; }

        // FUNCTIONAL

        // GRAPHICS

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public Door(Direction direction) 
        {
            Direction = direction;
        }


        //============================================ METHODS ===================================================//
        #region methods

        /*----------------------------------------------------------------------------------------------------------
                                                       Method					   
        ------------------------------------------------------------------------------------------------------------
          - Description
        ----------------------------------------------------------------------------------------------------------*/
        public void Load()
        {

        }// End -----------------------------------------------------------------------------------------------------

        #endregion
    }
}
