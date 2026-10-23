using dragon_hoard.Content.Models.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragon_hoard.Content.Models.Objects.Entities.Cards
{
    //////////////////////////////////////////// ROOM TILE CLASS /////////////////////////////////////////////////////
    /*
      
        Small data container class to represent a room tile being drawn from the RoomTile-stack.
        Assigns certain values to a RoomTile object such as number of exits and type of room.

        :: TODO ---------------------------

            



        :: DONE ---------------------------
            (2026)
            v.001b (10/23) Removing superfluos code related to minimap presentation.
            v.001a (10/08) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RoomTile
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        public int TileId { get; private set; }             // Unique ID

        public RoomType RoomType { get; private set; }      // Determine the type of room

        public bool ExitUp { get; set; }
        public bool ExitRight { get; set; }
        public bool ExitDown { get; set; }
        public bool ExitLeft { get; set; }

        //public bool[] Doors { get; private set; }           // Determine what exits have a door.

        public Direction EnteredFrom {  get; set; }         // Direction Player entered from initially

        public int DoorValue { get; private set; }          // Value that determines which directions have exits

        public int RoomSearched { get; private set; }       // Flag for the RoomActionsBar UI class of the GamePlayScreen


        // FUNCTIONAL
        public int Row { get; set; }                        // Row-position on gamemap grid

        public int Col { get; set; }                        // Column-position on gamemap grid


        // GRAPHICS
        public Texture2D TileImage { get; private set; }    // Tile visual representation on gamemap

        public Rectangle LargeSquare { get; private set; }  // Tile representation on gamemap



        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        #region Constructors
        // For RoomTile-placement
        public RoomTile() { }
        
        // For gamemap
        public RoomTile(RoomType roomType, int row, int col, int squareSize)
        {
            RoomType = roomType;
            
            Row = row;
            
            Col = col;
        }

        // For RoomTile Stack
        public RoomTile(int tileId, RoomType roomType, int doorValue)
        //public RoomTile(int tileId, Texture2D roomImage, RoomType roomType, int doorValue)
        {
            TileId = tileId;

            RoomType = roomType;

            DoorValue = doorValue;

            //Doors = doors;
        }
        #endregion

        //============================================ METHODS ===================================================//
        #region methods


        
        /*----------------------------------------------------------------------------------------------------------
                                                       Place Tile					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method representing the room tile being pulled from the stack and placed on the 
            game board.
        ----------------------------------------------------------------------------------------------------------*/
        public void PlaceTile(Direction moveDirection, RoomTile roomTile, int doorValue)
        {
            //TileImage = tileImage;

            TileId = roomTile.TileId;

            RoomType = roomTile.RoomType;

            DoorValue = doorValue;

            // Only check entrance direction and number of available exits of a room, if it isn't a Castle Exit.
            if (RoomType != RoomType.Exit)
            {
                EnteredFrom = DetermineEnteredFrom(moveDirection);
                DetermineRoomExits(doorValue);
            }

            //DoorErrorCheck();

            RoomTypeCheck(RoomType);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                              Player Searches Room					   
        ------------------------------------------------------------------------------------------------------------
          - Class support method to decrease the available amount of times a player can perform another 
            room search.
        ----------------------------------------------------------------------------------------------------------*/
        public void PlayerSearchesRoom()
        {
            if (RoomSearched > 0)
                RoomSearched--;
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                              Determine Entered From					   
        ------------------------------------------------------------------------------------------------------------
          - Class support method determine the direction a RoomTile is entered from, based on the
            direction the player took to enter it (i.e. the opposite one:  Going 'Up' means entering from 'Down').
        ----------------------------------------------------------------------------------------------------------*/
        private Direction DetermineEnteredFrom(Direction moveDirection)
        {
            Direction setEnteredFrom = Direction.Up;
            switch (moveDirection)
            {
                case Direction.Up: setEnteredFrom = Direction.Down; break;
                case Direction.Right: setEnteredFrom = Direction.Left; break;
                case Direction.Down: setEnteredFrom = Direction.Up; break;
                case Direction.Left: setEnteredFrom = Direction.Right; break;
            }

            return setEnteredFrom;
        }


        /*----------------------------------------------------------------------------------------------------------
                                                       Determine Room Exits				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method determining available exits of a room based on direction of entrance and a 
            selection of static values: 1 to 8. 
            1 = RoomTile has 4 exits
            2 = RoomTile has 3 exits (entrance, up, opposite end)
            3 = RoomTile has 3 exits (entrance, down, opposite end)
            4 = RoomTile has 3 exits (entrance, up, down)
            5 = RoomTile has 2 exits (entrance, up)
            6 = RoomTile has 2 exits (entrance, down)
            7 = RoomTile has 2 exits (entrance, opposite end)
            8 = RoomTile is a dead end (only entrance)

            When assigning available doors, always 'rotate' the RoomTile in the 'direction' point-of-view.
            As an example: compare two RoomTiles both with a value of 2 (3 doors; entrance, up and opposite end).
            If entered from the [Left]; doors will be left (entrance), up (up) and right (opposite) side of RoomTile.
            If entered from [Down]; doors will be down (entrance), left (up) and up (opposite) side of RoomTile.
        ----------------------------------------------------------------------------------------------------------*/
        private void DetermineRoomExits(int doorValue)
        {
            if (EnteredFrom == Direction.Up)
            {
                switch (doorValue)
                {
                    case 1: ExitUp = true; ExitRight = true; ExitDown = true; ExitLeft = true; break;
                    case 2: ExitUp = true; ExitRight = true; ExitDown = true; ExitLeft = false; break;
                    case 3: ExitUp = true; ExitRight = false; ExitDown = true; ExitLeft = true; break;
                    case 4: ExitUp = true; ExitRight = true; ExitDown = false; ExitLeft = true; break;
                    case 5: ExitUp = true; ExitRight = true; ExitDown = false; ExitLeft = false; break;
                    case 6: ExitUp = true; ExitRight = false; ExitDown = false; ExitLeft = true; break;
                    case 7: ExitUp = true; ExitRight = false; ExitDown = true; ExitLeft = false; break;
                    case 8: ExitUp = true; ExitRight = false; ExitDown = false; ExitLeft = false; break;
                }
            }
            if (EnteredFrom == Direction.Right)
            {
                switch (doorValue)
                {
                    case 1: ExitUp = true; ExitRight = true; ExitDown = true; ExitLeft = true; break;
                    case 2: ExitUp = true; ExitRight = true; ExitDown = false; ExitLeft = true; break;
                    case 3: ExitUp = false; ExitRight = true; ExitDown = true; ExitLeft = true; break;
                    case 4: ExitUp = true; ExitRight = true; ExitDown = true; ExitLeft = false; break;
                    case 5: ExitUp = true; ExitRight = true; ExitDown = false; ExitLeft = false; break;
                    case 6: ExitUp = false; ExitRight = true; ExitDown= true; ExitLeft = false; break;
                    case 7: ExitUp = false; ExitRight = true; ExitDown = false; ExitLeft = true; break;
                    case 8: ExitUp = false; ExitRight = true; ExitDown = false; ExitLeft = false; break;
                }
            }
            if (EnteredFrom == Direction.Down)
            {
                switch (doorValue)
                {
                    case 1: ExitUp = true; ExitRight = true; ExitDown = true; ExitLeft = true; break;
                    case 2: ExitUp = true; ExitRight = true; ExitDown = true; ExitLeft = false; break;
                    case 3: ExitUp = true; ExitRight = false; ExitDown = true; ExitLeft = true; break;
                    case 4: ExitUp = false; ExitRight = true; ExitDown = true; ExitLeft = true; break;
                    case 5: ExitUp = false; ExitRight = true; ExitDown = true; ExitLeft = false; break;
                    case 6: ExitUp = false; ExitRight = false; ExitDown = true; ExitLeft = true; break;
                    case 7: ExitUp = true; ExitRight = false; ExitDown = true; ExitLeft = false; break;
                    case 8: ExitUp = false; ExitRight = false; ExitDown = true; ExitLeft = false; break;
                }

            }
            if (EnteredFrom == Direction.Left)
            {
                switch (doorValue)
                {
                    case 1: ExitUp = true; ExitRight = true; ExitDown = true; ExitLeft = true; break;
                    case 2: ExitUp = true; ExitRight = true; ExitDown = false; ExitLeft = true; break;
                    case 3: ExitUp = false; ExitRight = true; ExitDown = true; ExitLeft = true; break;
                    case 4: ExitUp = true; ExitRight = false; ExitDown = true; ExitLeft = true; break;
                    case 5: ExitUp = true; ExitRight = false; ExitDown = false; ExitLeft = true; break;
                    case 6: ExitUp = false; ExitRight = false; ExitDown = true; ExitLeft = true; break;
                    case 7: ExitUp = false; ExitRight = true; ExitDown = false; ExitLeft = true; break;
                    case 8: ExitUp = false; ExitRight = false; ExitDown = false; ExitLeft = true; break;

                }
            }

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Door Error Check					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. Compares Exits and Doors to prevent a door being assigned to a non-existent exit.
        ----------------------------------------------------------------------------------------------------------*/
        private void DoorErrorCheck()
        {
            //for (int i = 0; i < Exits.Length; i++)
            //{
            //    if (Exits[i] == false)
            //        Doors[i] = false;
            //}
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Room Type Check					   
        ------------------------------------------------------------------------------------------------------------
         - Class specific method. Sets the flag to allow a room to be searched depending on the type of room.
            Only a RoomTile that is empty, has rubble or is a spinroom can be searched.
        ----------------------------------------------------------------------------------------------------------*/
        private void RoomTypeCheck(RoomType roomType)
        {
            if (roomType == RoomType.Empty || roomType == RoomType.Rubble || roomType == RoomType.SpinRoom)
                RoomSearched = 2;

            else
                RoomSearched = 0;
        }// End -----------------------------------------------------------------------------------------------------

        #endregion


        public void DisplayRoomTileData(SpriteBatch spriteBatch, Point position, Texture2D squareTexture, SpriteFont font)
        {
            Rectangle background = new Rectangle(position.X, position.Y, 200, 250);

            spriteBatch.Draw(squareTexture, background, Color.DarkGreen);

            spriteBatch.DrawString(font, "RoomTile data", new Vector2(position.X + 55, position.Y + 5), Color.White);

            spriteBatch.DrawString(font, $"Grid (row,col): [{Row},{Col}]", new Vector2(position.X + 5, position.Y + 25), Color.White);

            spriteBatch.DrawString(font, $"RoomType: {RoomType.ToString()}", new Vector2(position.X + 5, position.Y + 45), Color.White);

            spriteBatch.DrawString(font, $"EnteredFrom: {EnteredFrom}", new Vector2(position.X + 5, position.Y + 65), Color.White);

            spriteBatch.DrawString(font, $"DoorValue: {DoorValue.ToString()}", new Vector2(position.X + 5, position.Y + 85), Color.White);

            spriteBatch.DrawString(font, $"Exits: Up: {ExitUp}\n Right: {ExitRight}\n Down: {ExitDown}\n " +
                $"Left: {ExitLeft}", new Vector2(position.X + 5, position.Y + 115), Color.White);

            spriteBatch.DrawString(font, $"Search Left: {RoomSearched.ToString()}", new Vector2(position.X + 5, position.Y + 205), Color.White);
        }
    }
}
