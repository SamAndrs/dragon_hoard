using dragon_hoard.Content.Models.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace dragon_hoard.Content.Models.Objects.Entities.Cards
{
    //////////////////////////////////////////// ROOM TILE CLASS /////////////////////////////////////////////////////
    /*
      
        Small data container class to represent a room tile being drawn from the RoomTile-stack.
        Assigns certain values to a Room object such as number of exits and type of room.

        :: TODO ---------------------------

            



        :: DONE ---------------------------
            (2026)
            v.001a (10/08) Setup of basic core properties and methods.

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

        public Texture2D RoomImage { get; private set; }    // GamePlayScreen visual representation

        private Color _miniSquareColor;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        // For RoomTile-placement
        public RoomTile() { }
        
        // For gamemap
        public RoomTile(RoomType roomType, int row, int col, int squareSize, Vector2 roomPosition)
        {
            
            RoomType = roomType;
            
            Row = row;
            
            Col = col;

            MiniSquare = new Rectangle((int)roomPosition.X, (int)roomPosition.Y, squareSize, squareSize);
        }

        // For RoomTile Stack
        public RoomTile(int tileId, RoomType roomType, int doorValue, bool[] doors)
        {
            TileId = tileId;

            RoomType = roomType;

            DoorValue = doorValue;

            Doors = doors;
        }


        //============================================ METHODS ===================================================//
        #region methods


        public void Update(GameTime gameTime)
        {

        }

        /*----------------------------------------------------------------------------------------------------------
                                                       Place Tile					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method representing the room tile being pulled from the stack and placed on the 
            game board.
        ----------------------------------------------------------------------------------------------------------*/
        public void PlaceTile(Direction fromDirection, RoomTile roomTile)
        {
            //TileImage = tileImage;

            TileId = roomTile.TileId;

            RoomType = roomTile.RoomType;

            EnteredFrom = fromDirection;
           
            Exits = DetermineRoomExits(EnteredFrom, roomTile.DoorValue);

            //DoorErrorCheck();

            _miniSquareColor = SetMiniMapColor(RoomType);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Determine Room Exits				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method determining available exits of a room based on direction of entrance and a 
            selection of static values: (90, 82, 74, 26, 10, 18 and 66).
            1 = RoomTile has 4 exits
            2 = RoomTile has 3 exits (entrance, up, opposite end)
            3 = RoomTile has 3 exits (entrance, down, opposite end)
            4 = RoomTile has 3 exits (entrance, up, down)
            5 = RoomTile has 2 exits (entrance, up)
            6 = RoomTile has 2 exits (entrance, down)
            7 = RoomTile has 2 exits (entrance, opposite end)
            8 = RoomTile is a dead end (only entrance)

            
        ----------------------------------------------------------------------------------------------------------*/
        private bool[] DetermineRoomExits(Direction fromDirection, int doorValue)
        {
            bool[] exits = new bool[4];

            if (fromDirection == Direction.Up)
            {
                switch (doorValue)
                {
                    case 1: exits = [true, true, true, true]; break;
                    case 2: exits = [true, true, true, false]; break;
                    case 3: exits = [true, false, true, true]; break;
                    case 4: exits = [true, true, false, true]; break;
                    case 5: exits = [true, true, false, false]; break;
                    case 6: exits = [true, false, false, true]; break;
                    case 7: exits = [true, false, true, false]; break;
                    case 8: exits = [true, false, false, false]; break;
                }
            }
            else if (fromDirection == Direction.Right)
            {
                switch (doorValue)
                {
                    case 1: exits = [true, true, true, true]; break;
                    case 2: exits = [true, true, false, true]; break;
                    case 3: exits = [false, true, true, true]; break;
                    case 4: exits = [true, true, true, false]; break;
                    case 5: exits = [true, true, false, false]; break;
                    case 6: exits = [false, true, true, false]; break;
                    case 7: exits = [false, true, false, true]; break;
                    case 8: exits = [false, true, false, false]; break;
                }
            }
            else if (fromDirection == Direction.Down)
            {
                switch (doorValue)
                {
                    case 1: exits = [true, true, true, true]; break;
                    case 2: exits = [true, false, true, true]; break;
                    case 3: exits = [true, true, true, false]; break;
                    case 4: exits = [false, true, true, true]; break;
                    case 5: exits = [false, false, true, true]; break;
                    case 6: exits = [false, true, true, false]; break;
                    case 7: exits = [true, false, true, false]; break;
                    case 8: exits = [false, false, true, false]; break;
                }
            }
            else if (fromDirection == Direction.Left)
            {
                switch (doorValue)
                {
                    case 1: exits = [true, true, true, true]; break;
                    case 2: exits = [true, true, false, true]; break;
                    case 3: exits = [false, true, true, true]; break;
                    case 4: exits = [true, false, true, true]; break;
                    case 5: exits = [true, false, false, true]; break;
                    case 6: exits = [false, false, true, true]; break;
                    case 7: exits = [false, true, false, true]; break;
                    case 8: exits = [false, false, false, true]; break;

                }
            }
        

                #region old code ===============================================
                //if (fromDirection == Direction.Up)
                //{
                //    //if (Exits[0] == true) { value += 2; }
                //    //if (Exits[1] == true) { value += 16; }
                //    //if (Exits[2] == true) { value += 64; }
                //    //if (Exits[3] == true) { value += 8; }

                //    switch (doorValue)
                //    {
                //        case 90:
                //            exits = [true, true, true, true]; break;

                //        case 82:
                //            exits = [true, true, true, false]; break;

                //        case 74:
                //            exits = [true, false, true, true]; break;

                //        case 26:
                //            exits = [true, true, false, true]; break;

                //        case 10:
                //            exits = [true, false, false, true]; break;

                //        case 18:
                //            exits = [true, true, false, false]; break;

                //        case 66:
                //            exits = [true, false, true, false]; break;

                //    }

                //}
                //else if (fromDirection == Direction.Right)
                //{
                //    //if (Exits[0] == true) { value += 8; }
                //    //if (Exits[1] == true) { value += 2; }
                //    //if (Exits[2] == true) { value += 16; }
                //    //if (Exits[3] == true) { value += 64; }

                //    switch (doorValue)
                //    {
                //        case 90:
                //            exits = [true, true, true, true]; break;

                //        case 82:
                //            exits = [false, true, true, true]; break;

                //        case 74:
                //            exits = [true, true, false, true]; break;

                //        case 26:
                //            exits = [true, true, true, false]; break;

                //        case 10:
                //            exits = [true, true, false, false]; break;

                //        case 18:
                //            exits = [false, true, true, false]; break;

                //        case 66:
                //            exits = [false, true, false, true]; break;
                //    }
                //}
                //else if (fromDirection == Direction.Down)
                //{
                //    //if (Exits[0] == true) { value += 64; }
                //    //if (Exits[1] == true) { value += 8; }
                //    //if (Exits[2] == true) { value += 2; }
                //    //if (Exits[3] == true) { value += 16; }

                //    switch (doorValue)
                //    {
                //        case 90:
                //            exits = [true, true, true, true]; break;

                //        case 82:
                //            exits = [true, false, true, true]; break;

                //        case 74:
                //            exits = [true, true, true, false]; break;

                //        case 26:
                //            exits = [false, true, true, true]; break;

                //        case 10:
                //            exits = [false, true, true, false]; break;

                //        case 18:
                //            exits = [false, false, true, true]; break;

                //        case 66:
                //            exits = [true, false, true, false]; break;
                //    }
                //}
                //else
                //{
                //    //if (Exits[0] == true) { value += 16; }
                //    //if (Exits[1] == true) { value += 64; }
                //    //if (Exits[2] == true) { value += 2; }
                //    //if (Exits[3] == true) { value += 8; }
                //    switch (doorValue)
                //    {
                //        case 90:
                //            exits = [true, true, true, true]; break;

                //        case 82:
                //            exits = [true, true, false, true]; break;

                //        case 74:
                //            exits = [false, true, true, true]; break;

                //        case 26:
                //            exits = [true, false, true, true]; break;

                //        case 10:
                //            exits = [false, false, true, true]; break;

                //        case 18:
                //            exits = [true, false, false, true]; break;

                //        case 66:
                //            exits = [false, true, false, true]; break;

                //    }
                //}
                #endregion
                return exits;
        }// End -----------------------------------------------------------------------------------------------------


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
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Determine Castle Exits				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. Checks whether an Exit room is on the top or bottom row, and depending on
            result; creates an exit so player can progress into the GameMap proper.
        ----------------------------------------------------------------------------------------------------------*/
        public void DetermineCastleExits(int maxRow, int maxCol)
        {
            if (RoomType != RoomType.Exit)
                return;

            else
            {
                Exits = new bool[4];
                if(Row == 1) // Check if exit is on 2nd row from top...
                {
                    if(Col == 1) // ..and is top left corner
                        Exits = [false, true, true, false]; // exits are right and down

                    else // ..is top right corner
                        Exits = [false, false, true, true]; // exits are left and down 
                }

                if(Row == maxRow) // If exit is at bottom row...
                {
                    if (Col == 1) // ..and is bottom left corner
                        Exits = [true, true, false, false]; // exits are up and right

                    else // ..is bottom right corner
                        Exits = [true, false, false, true]; // exits are up and left 
                }
            }
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Draw On Mini Map					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. Draws the RoomTile's representation square on the GamePlayScreen minimap.
            Different kinds of rooms are displayed in different colors.
        ----------------------------------------------------------------------------------------------------------*/
        public void DrawOnMiniMap(SpriteBatch spriteBatch, Texture2D squareTexture)
        {
            Color darkGray = new Color(28, 30, 33); // Dark gray color for outer wall room squares

            //Draw the room square on the map
            if (RoomType == RoomType.Exit)
                spriteBatch.Draw(squareTexture, MiniSquare, Color.Yellow);

            else if (RoomType == RoomType.OuterWall)
                spriteBatch.Draw(squareTexture, MiniSquare, darkGray);
            else
            {
                spriteBatch.Draw(squareTexture, MiniSquare, _miniSquareColor);
            }
        }// End -----------------------------------------------------------------------------------------------------
        
        private Color SetMiniMapColor(RoomType roomType)
        {
            Color setColor = new Color();

            Color pit = new Color(10, 50, 255);
            Color bridge = new Color(50, 50, 200);
            Color dark = new Color(25, 25, 25);

            switch (RoomType)
            {
                //case RoomType.Exit: setColor = Color.Yellow; break;
                //case RoomType.OuterWall: setColor = darkGray; break;
                case RoomType.Empty: setColor = Color.DarkBlue; break;
                case RoomType.BottomLessPit: setColor = pit; break;
                case RoomType.BridgeRoom: setColor = pit; break;
                case RoomType.DarkRoom: setColor = dark; break;
                case RoomType.Corridor: setColor = Color.Orange; break;
                case RoomType.Rubble: setColor = Color.Brown; break;
                case RoomType.SpinRoom: setColor = Color.LightGray; break;
                case RoomType.TreasureRoom: setColor = Color.Gold; break;
                case RoomType.WebbedRoom: setColor = Color.LightSalmon; break;
                case RoomType.TrapRoom: setColor = Color.Red; break;
            }

            return setColor;
        }
        
        #endregion
    }
}
