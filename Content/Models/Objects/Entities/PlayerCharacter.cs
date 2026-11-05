using Microsoft.Xna.Framework.Graphics;
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
        
        
        v.001b (11/05) Methods for loading graphics assets added..


    :: DONE ---------------------------
        (2026)
        v.001a (09/27) Setup of basic core properties and methods.

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

        public int Gold { get; private set; }

        // MISC
        public bool TorchIsOn { get; set; }

        // public List<Room> ExploredRooms {get; private set; }

        // GRAPHICS
        public Texture2D CharacterPortrait { get; private set; }                // Character select screen portrait image

        public Texture2D MapPin { get; private set; }                           // Game map pin image for the character

        public Texture2D CombatCardA { get; private set; }                      // Combat attack option (A) image for the character

        public Texture2D CombatCardB { get; private set; }

        public Texture2D CombatCardC { get; private set; }


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

            Gold = 0;
        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                       Set Character Portrait					   
        ------------------------------------------------------------------------------------------------------------
          - Description
        ----------------------------------------------------------------------------------------------------------*/
        public void SetCharacterPortrait(MainGame game)
        {
            CharacterPortrait = game.Content.Load<Texture2D>($"Images/Characters/{Name}_Portrait");
        }// End -----------------------------------------------------------------------------------------------------


        // End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Load CardImages					   
        ------------------------------------------------------------------------------------------------------------
          - Class method to load the combat card images for the player character. This method is called during 
            the game initialization phase to ensure that all necessary graphics assets are loaded and ready 
            for use in combat scenarios.
        ----------------------------------------------------------------------------------------------------------*/
        public void LoadCardImages(MainGame game)
        {
            CombatCardA = game.Content.Load<Texture2D>($"Images/Characters/CombatCardImages/{Name}_CardA".ToUpper());
            CombatCardB = game.Content.Load<Texture2D>($"Images/Characters/CombatCardImages/{Name}_CardB".ToUpper());
            CombatCardC = game.Content.Load<Texture2D>($"Images/Characters/CombatCardImages/{Name}_CardC".ToUpper());

        }// End -----------------------------------------------------------------------------------------------------


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
