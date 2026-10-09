using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragon_hoard.Content.Models.Objects.Entities
{
    ///////////////////////////////////////// GAME SESSION CLASS ///////////////////////////////////////////////
    /*
        Data container class for a game session. This class will hold all the relevant information about the 
        current game state, including player data, game progress, and any other necessary details. 
      
       :: TODO ---------------------------

            v.001a (10/07) Setup of basic core properties and methods.



        :: DONE ---------------------------
            (2026)

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class GameSession
    {
        //========================================== PROPERTIES ==================================================//

        // public Player Player { get; private set; }

        public PlayerCharacter PlayerCharacter { get; private set; }

        public IEnumerable<Room> ExploredRooms { get; private set; }

        public Room CurrentPlayerRoom { get; private set; }

        public int CurrentPCHealth { get; private set; }


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public GameSession()
        {
            // Initialize the game session with default values or load from a saved state.

            // DEBUG: Create a default player character for testing purposes.
            PlayerCharacter = new PlayerCharacter("Svend",10, 6, 7,8,10);
            CurrentPCHealth = 8; // DEBUG: Set default health for testing purposes.
        }

        public void SetCurrentRoom(Room room) => CurrentPlayerRoom = room;
    }
}
