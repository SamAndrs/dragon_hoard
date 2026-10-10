using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragon_hoard.Content.Models.Objects.Entities
{
    ///////////////////////////////////////// PLAYER CHARACTER CLASS ///////////////////////////////////////////////
    /*
        - Data container class for player character information.
        
            
    :: TODO ---------------------------
        
        v.001a (09/27) Setup of basic core properties and methods.



    :: DONE ---------------------------
        (2026)

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class PlayerCharacter
    {
        public string Name { get; private set; }

        // STATS
        public int MaxHealth { get; private set; }

        public int CurrentHealth { get; set; } // TODO Change to private set later

        public int Strength { get; private set; }

        public int Dexterity { get; private set; }

        //public int Resolve { get; private set; }

        public int Luck { get; private set; }

        public int Armor { get; private set; }

        

        // INVENTORY
        // public Dictionary<Item, int> Equipment { get; private set; }

        // public Item Ring { get; private set; }

        // public Item Amulet { get; private set; }

        // public Dictionary<Item, int> Treasure { get; private set; }

        // MISC
        public bool TorchIsOn { get; set; }

        public Room CurrentRoom { get; private set; }

        // public List<Room> ExploredRooms {get; private set; }


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public PlayerCharacter(string name, int health, int str, int dex, int arm, int lck)
        {
            Name = name;
            
            MaxHealth = health;

            Strength = str;

            Dexterity = dex;

            //Resolve = res;

            Armor = arm;

            Luck = lck;
        }


        //============================================ METHODS ===================================================//
        #region methods

        /*----------------------------------------------------------------------------------------------------------
                                                       Method					   
        ------------------------------------------------------------------------------------------------------------
          - Description
        ----------------------------------------------------------------------------------------------------------*/



        // End -----------------------------------------------------------------------------------------------------


        //public void AddRoomExplored(int roomNr) => ExploredRooms.Add(roomNr);

        //public void AddTreasure(Item treasure, int amount) => Treasure.Add(treasure, amount);

        #endregion

    }
}
