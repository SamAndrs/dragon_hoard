using dragon_hoard.Content.Models.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragon_hoard.Content.Models.Objects.Entities.Cards
{
    //////////////////////////////////////////// ROOM TILE CLASS /////////////////////////////////////////////////////
    /*
      
        Small data container class to represent a room tile being drawn from the RoomTile-stack.
        Assigns certain values to a Room object such as number of exits and type of room.

        :: TODO ---------------------------

            v.001a (10/08) Setup of basic core properties and methods.



        :: DONE ---------------------------
            (2026)

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RoomTile
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        public int TileId { get; private set; }             // Unique ID

        public RoomType RoomType { get; private set; }      // Determine the type of room

        public bool[] Exits { get; private set; }           // Determine if Up, Right, Down, Left has exits

        public bool[] Doors { get; private set; }           // Determine what exits have a door.

        public Direction EnteredFrom {  get; private set; }   // Direction Player entered from initially

        public int DoorValue { get; private set; }          // Value that determines which directions have exits

        // FUNCTIONAL
        public int Row { get; set; }                        // Row-position on gamemap grid

        public int Col { get; set; }                        // Column-position on gamemap grid

        // GRAPHICS
        public Texture2D TileImage { get; private set; }    // Tile visual representation on gamemap

        public Rectangle LargeSquare { get; private set; }  // Tile representation on gamemap

        public Rectangle MiniSquare { get; private set; }   // Tile representation on minimap

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public RoomTile(int tileId, RoomType roomType, int doorValue, bool[] doors)
        {
            TileId = tileId;

            RoomType = roomType;

            DoorValue = doorValue;

            Doors = doors;
        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                       Place Tile					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method representing the room tile being pulled from the stack and placed on the 
            game board.
        ----------------------------------------------------------------------------------------------------------*/
        public void PlaceTile(Texture2D tileImage, Direction fromDirection)
        {
            TileImage = tileImage;

            EnteredFrom = fromDirection;
           
            Exits = CalculateDoors(fromDirection, DoorValue);

            DoorErrorCheck();

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Calculate Doors				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method determining available exits of a room based on direction of entrance and a 
            selection of static values (90, 82, 74, 26, 10, 18 and 66).
        ----------------------------------------------------------------------------------------------------------*/
        private bool[] CalculateDoors(Direction fromDirection, int doorValue)
        {
            bool[] exits = new bool[4];

            if (fromDirection == Direction.Up)
            {
                //if (Exits[0] == true) { value += 2; }
                //if (Exits[1] == true) { value += 16; }
                //if (Exits[2] == true) { value += 64; }
                //if (Exits[3] == true) { value += 8; }

                switch (doorValue)
                {
                    case 90:
                        exits = new bool[4] { true, true, true, true }; break;

                    case 82:
                        exits = new bool[4] { true, true, true, false }; break;

                    case 74:
                        exits = new bool[4] { true, false, true, true }; break;

                    case 26:
                        exits = new bool[4] { true, true, false, true }; break;

                    case 10:
                        exits = new bool[4] { true, false, false, true }; break;

                    case 18:
                        exits = new bool[4] { true, true, false, false }; break;

                    case 66:
                        exits = new bool[4] { true, false, true, false }; break;

                }
               
            }
            else if (fromDirection == Direction.Right)
            {
                //if (Exits[0] == true) { value += 8; }
                //if (Exits[1] == true) { value += 2; }
                //if (Exits[2] == true) { value += 16; }
                //if (Exits[3] == true) { value += 64; }

                switch (doorValue)
                {
                    case 90:
                        exits = new bool[4] { true, true, true, true }; break;

                    case 82:
                        exits = new bool[4] { false, true, true, true }; break;

                    case 74:
                        exits = new bool[4] { true, true, false, true }; break;

                    case 26:
                        exits = new bool[4] { true, true, true, false }; break;

                    case 10:
                        exits = new bool[4] { true, true, false, false }; break;

                    case 18:
                        exits = new bool[4] { false, true, true, false }; break;

                    case 66:
                        exits = new bool[4] { false, true, false, true }; break;
                }
            }
            else if (fromDirection == Direction.Down)
            {
                //if (Exits[0] == true) { value += 64; }
                //if (Exits[1] == true) { value += 8; }
                //if (Exits[2] == true) { value += 2; }
                //if (Exits[3] == true) { value += 16; }

                switch (doorValue)
                {
                    case 90:
                        exits = new bool[4] { true, true, true, true }; break;

                    case 82:
                        exits = new bool[4] { true, false, true, true }; break;

                    case 74:
                        exits = new bool[4] { true, true, true, false }; break;

                    case 26:
                        exits = new bool[4] { false, true, true, true }; break;

                    case 10:
                        exits = new bool[4] { false, true, true, false }; break;

                    case 18:
                        exits = new bool[4] { false, false, true, true }; break;

                    case 66:
                        exits = new bool[4] { true, false, true, false }; break;
                }
            }
            else
            {
                //if (Exits[0] == true) { value += 16; }
                //if (Exits[1] == true) { value += 64; }
                //if (Exits[2] == true) { value += 2; }
                //if (Exits[3] == true) { value += 8; }
                switch (doorValue)
                {
                    case 90:
                        exits = new bool[4] { true, true, true, true }; break;

                    case 82:
                        exits = new bool[4] { true, true, false, true }; break;

                    case 74:
                        exits = new bool[4] { false, true, true, true }; break;

                    case 26:
                        exits = new bool[4] { true, false, true, true }; break;

                    case 10:
                        exits = new bool[4] { false, false, true, true }; break;

                    case 18:
                        exits = new bool[4] { true, false, false, true }; break;

                    case 66:
                        exits = new bool[4] { false, true, false, true }; break;

                }
            }

            return exits;
        }



        /*----------------------------------------------------------------------------------------------------------
                                                       Door Error Check					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. Compares Exits and Doors to prevent a door being assigned to a non-existent exit.
        ----------------------------------------------------------------------------------------------------------*/
        private void DoorErrorCheck()
        {
            for (int i = 0; i < Exits.Length; i++)
            {
                if (Exits[i] == false)
                    Doors[i] = false;
            }
        }
        #endregion
    }
}
