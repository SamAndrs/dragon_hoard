using dragon_hoard.Content.Models.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics.Contracts;
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

        public bool ExitUp { get; set; }
        public bool ExitRight { get; set; }
        public bool ExitDown { get; set; }
        public bool ExitLeft { get; set; }

        //public bool[] Doors { get; private set; }           // Determine what exits have a door.

        public Direction EnteredFrom {  get; set; }   // Direction Player entered from initially

        public int DoorValue { get; private set; }          // Value that determines which directions have exits

        // FUNCTIONAL
        public int Row { get; set; }                        // Row-position on gamemap grid

        public int Col { get; set; }                        // Column-position on gamemap grid

        // GRAPHICS
        public Texture2D TileImage { get; private set; }    // Tile visual representation on gamemap

        public Rectangle LargeSquare { get; private set; }  // Tile representation on gamemap

        public Rectangle MiniSquare { get; private set; }   // Tile representation on minimap

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

            _miniSquareColor = SetMiniMapColor(RoomType);
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


        //============================================ METHODS ===================================================//
        #region methods



        public void Update(GameTime gameTime)
        {
            // Only update if the RoomType is of a category that need to change.
            if(RoomType != RoomType.Undiscovered && RoomType != RoomType.Exit && RoomType != RoomType.OuterWall)
                _miniSquareColor = SetMiniMapColor(RoomType);
        }

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

            _miniSquareColor = SetMiniMapColor(RoomType);

        }// End -----------------------------------------------------------------------------------------------------



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
                                                       Draw On Mini Map					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. Draws the RoomTile's representation square on the GamePlayScreen minimap.
            Different kinds of rooms are displayed in different colors.
        ----------------------------------------------------------------------------------------------------------*/
        public void DrawOnMiniMap(SpriteBatch spriteBatch, Texture2D squareTexture)
        {
            spriteBatch.Draw(squareTexture, MiniSquare, _miniSquareColor);
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Set Mini Map Color			   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. Assigns the RoomTile's minimap square a color denoting the type of
            room represented.
        ----------------------------------------------------------------------------------------------------------*/
        private Color SetMiniMapColor(RoomType roomType)
        {
            Color darkGray = new Color(28, 30, 33);
            Color pit = new Color(10, 50, 255);
            Color bridge = new Color(50, 50, 200);
            Color dark = new Color(25, 25, 25);

            switch (RoomType)
            {
                case RoomType.Undiscovered: return Color.Black;
                case RoomType.Exit:         return Color.Yellow;
                case RoomType.OuterWall:    return darkGray;
                case RoomType.Empty:        return Color.DarkBlue;
                case RoomType.BottomLessPit: return pit;
                case RoomType.BridgeRoom:   return bridge;
                case RoomType.DarkRoom:     return dark;
                case RoomType.Corridor:     return Color.Orange;
                case RoomType.Rubble:       return Color.Brown;
                case RoomType.SpinRoom:     return Color.LightGray;
                case RoomType.TreasureRoom: return Color.Gold;
                case RoomType.WebbedRoom:   return Color.LightSalmon;
                case RoomType.TrapRoom:     return Color.Red;
                default: return Color.IndianRed;

                    //case RoomType.Exit: setColor = Color.Yellow; break;
                    //case RoomType.OuterWall: setColor = darkGray; break;
                    //case RoomType.Empty: setColor = Color.DarkBlue; break;
                    //case RoomType.BottomLessPit: setColor = pit; break;
                    //case RoomType.BridgeRoom: setColor = pit; break;
                    //case RoomType.DarkRoom: setColor = dark; break;
                    //case RoomType.Corridor: setColor = Color.Orange; break;
                    //case RoomType.Rubble: setColor = Color.Brown; break;
                    //case RoomType.SpinRoom: setColor = Color.LightGray; break;
                    //case RoomType.TreasureRoom: setColor = Color.Gold; break;
                    //case RoomType.WebbedRoom: setColor = Color.LightSalmon; break;
                    //case RoomType.TrapRoom: setColor = Color.Red; break;
            }

            //return setColor;
        }// End -----------------------------------------------------------------------------------------------------


        #endregion
    }
}
