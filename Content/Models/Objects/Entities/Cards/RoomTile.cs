using dragon_hoard.Content.Models.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace dragon_hoard.Content.Models.Objects.Entities.Cards
{
    //////////////////////////////////////////// ROOM TILE CLASS /////////////////////////////////////////////////////
    /*
      
        Data container class to represent a room tile drawn from the RoomTile-stack.
        Assigns certain values to a RoomTile object such as number of exits and type of room.

        :: TODO ---------------------------
            
            
            

        :: DONE ---------------------------
            (2026)
            v.001e (10/30) Extensive rework on Exits and rotation functionality.
            v.001d (10/29) Additions to support RoomTile rotation on GameMap.
            v.001c (10/26) Code rework to support database implementation.
            v.001b (10/23) Removing superfluos code related to minimap presentation.
            v.001a (10/08) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public sealed class RoomTile
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        public int Id { get; init; }                                // Unique ID

        public RoomType RoomType { get; set; }                      // Determine the type of room
        
        public DoorType DoorUp { get; set; }                        // Type of doors at each exit (0 = None, 1 = Closed, 2 = Portcullis).
        public DoorType DoorRight { get; set; }
        public DoorType DoorDown { get; set; }
        public DoorType DoorLeft { get; set; }

        public RoomExits Exits { get; set; }                        // Canonical exits

        public RoomExits RotatedExits { get; set; }                 // Rotated versions of canonical exits.

        public Direction EnteredFrom {  get; set; }                 // Direction Player entered from initially

        public int DoorValue { get; set; }                         // Value that determines which directions have exits

        public int RoomSearched { get; private set; }               // Flag for the RoomActionsBar UI class of the GamePlayScreen

        public string TileImageReference { get; set; } = "";       // Database reference for what Texture2D used as the [TileImage] on the GameMap.

        public string TileRoomImageReference { get; set; } = "";   // Database reference for what Texture2D used as the [TileRoomImage] with the RoomImage-class.

        public string EventName { get; set; }                      // A headline of any event related to the type of RoomTile.

        public string EventText { get; set; }                      // Text describing any event related to the type of RoomTile.

        

        // FUNCTIONAL
        public int Row { get; set; }                                // Row-position on gamemap grid

        public int Col { get; set; }                                // Column-position on gamemap grid

        private readonly RoomTile[,] _mapGrid;

        private int _quarterTurns;                                  // The amount of times to rotate the RoomTile (for GameMap).

        private float _rotation => _quarterTurns * MathHelper.PiOver2;  // Rotation in radians

        

        // GRAPHICS
        public Texture2D TileImage { get; set; }                    // Tile visual representation on gamemap      

        public Rectangle LargeSquare { get; set; }                  // Tile representation on gamemap

        public Texture2D TileRoomImage { get; set; }                // Image displayed via the RoomImage-class on the GamePlayScreen.

        private int _size = 256;                                    // Size of TileImage and LargeSquare

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        #region Constructors
        // For RoomTile-placement
        public RoomTile() { }

        // For gamemap
        public RoomTile(RoomTile[,] mapGrid, RoomType roomType, int row, int col)
        {
            _mapGrid = mapGrid;

            RoomType = roomType;
            
            Row = row;
            
            Col = col;

            _quarterTurns = 0;
        }
        #endregion

        //============================================ METHODS ===================================================//
        #region methods




        /*----------------------------------------------------------------------------------------------------------
                                                       Draw					   
        ------------------------------------------------------------------------------------------------------------
          - Class method. Draws the rotated TileImage texture onto the GameMapScreen.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            // origin: The point around which the texture rotates. Using the center makes the room rotate around its middle.
            Vector2 origin = new Vector2(TileImage.Width / 2f, TileImage.Height / 2f);

            Vector2 position = new Vector2(LargeSquare.X, LargeSquare.Y);

            Vector2 center = position + new Vector2(_size / 2f, _size / 2f);

            spriteBatch.Draw(
                TileImage,
                center,
                null,
                Color.White,
                _rotation,
                origin,
                1.0f,
                SpriteEffects.None,
                0f
            );
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Place Tile					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method representing the room tile being pulled from the stack and placed on the 
            game board.
        ----------------------------------------------------------------------------------------------------------*/
        public void PlaceTile(Direction moveDirection, RoomTile roomTile)
        {
            RoomType = roomTile.RoomType;

            DoorValue = roomTile.DoorValue;

            DoorUp = roomTile.DoorUp;

            DoorDown = roomTile.DoorDown;

            DoorRight = roomTile.DoorRight;

            DoorLeft = roomTile.DoorLeft;

            // Only check entrance direction and number of available exits of a room, if it isn't a Castle Exit.
            if (RoomType != RoomType.Exit)
            {
                EnteredFrom = DetermineEnteredFrom(moveDirection);
                CalculateQuarterTurns(EnteredFrom);
                Exits = (RoomExits)DoorValue;
                RotatedExits = GetExits(EnteredFrom);
            }


            //DoorErrorCheck();

            RoomTypeSearchCheck(RoomType);

            EventName = roomTile.EventName;

            EventText = roomTile.EventText;

            TileImageReference = roomTile.TileImageReference;

            TileImage = roomTile.TileImage;

            TileRoomImageReference = roomTile.TileRoomImageReference;

            TileRoomImage = roomTile.TileRoomImage;

            LargeSquare = new Rectangle(800, 400, _size, _size);

        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                  Set Entrance					   
        ------------------------------------------------------------------------------------------------------------
          - Class support method. Calculates the amount of times the RoomTile should be rotated based on
            the direction the player enters the RoomTile IN, compared with a default 'anchor' direction of 'Down'.
        ----------------------------------------------------------------------------------------------------------*/
        private void CalculateQuarterTurns(Direction enteredFrom)
        {
            _quarterTurns = ((int)enteredFrom - (int)Direction.Down + 4) % 4;
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Get Exits					   
        ------------------------------------------------------------------------------------------------------------
          - Class support method. Rotates the exits of the RoomTile by the value of [_quarterTurns] and returns 
            the new exit values.
        ----------------------------------------------------------------------------------------------------------*/
        private RoomExits GetExits(Direction enteredFrom)
        {
            RoomExits exits = Exits;
            //RoomExits exits = CheckConnectedExits(_mapGrid, Row, Col, Exits);

            for (int i = 0; i < _quarterTurns; i++)
                exits = RotateClockWise(exits);

            //exits = CheckConnectedExits(_mapGrid, Row, Col, exits);


            return CheckConnectedExits(_mapGrid, Row, Col, exits);
            //return exits;
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Rotate ClockWise					   
        ------------------------------------------------------------------------------------------------------------
          - Class support method. Rotates every exit clockwise by 90 degrees.
        ----------------------------------------------------------------------------------------------------------*/
        private RoomExits RotateClockWise(RoomExits exits)
        {
            RoomExits rotated = RoomExits.None;

            if (exits.HasFlag(RoomExits.Up))
                rotated |= RoomExits.Right;

            if (exits.HasFlag(RoomExits.Right))
                rotated |= RoomExits.Down;

            if (exits.HasFlag(RoomExits.Down))
                rotated |= RoomExits.Left;

            if (exits.HasFlag(RoomExits.Left))
                rotated |= RoomExits.Up;

            return rotated;
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
                                                      Check Connected Exits					   
        ------------------------------------------------------------------------------------------------------------
         - Class support method. Checks the four neighboring tiles and adjusts and returns available exits 
            of the current RoomTile, preventing the player to walk through walls.
        ----------------------------------------------------------------------------------------------------------*/
        private RoomExits CheckConnectedExits(RoomTile[,] mapGrid, int row, int col, RoomExits exits)
        {
            RoomExits adjustedExits = exits;

            // Up: neighbor's Down exit
            if (row > 0)
            {
                RoomTile neighbor = mapGrid[row - 1, col];

                // If the neighboring tile is a castle Exit room, we simply abort since those have fixed (non-rotated) exits.
                if (neighbor.RoomType == RoomType.Exit)
                    return adjustedExits;

                else if (neighbor.RoomType != RoomType.Undiscovered && !neighbor.RotatedExits.HasFlag(RoomExits.Down))
                {
                    var mask = ~RoomExits.Up;
                    adjustedExits = adjustedExits & mask;
                }
            }

            // Right: neighbor's Left exit
            if (col < mapGrid.GetLength(1))
            {
                RoomTile neighbor = mapGrid[row, col + 1];

                if (neighbor.RoomType == RoomType.Exit)
                    return adjustedExits;

                if (neighbor.RoomType != RoomType.Undiscovered && !neighbor.RotatedExits.HasFlag(RoomExits.Left))
                {
                    var mask = ~RoomExits.Right;
                    adjustedExits = adjustedExits & mask;
                }  
            }

            // Down: neighbor's Up exit
            if (row < mapGrid.GetLength(0))
            {
                RoomTile neighbor = mapGrid[row + 1, col];

                if (neighbor.RoomType == RoomType.Exit)
                    return adjustedExits;

                if (neighbor.RoomType != RoomType.Undiscovered && !neighbor.RotatedExits.HasFlag(RoomExits.Up))
                {
                    var mask = ~RoomExits.Down;
                    adjustedExits = adjustedExits & mask;
                }
            }

            // Left: neighbor's Right exit
            if (col > 1)
            {
                RoomTile neighbor = mapGrid[row, col - 1];

                if (neighbor.RoomType == RoomType.Exit)
                    return adjustedExits;

                if (neighbor.RoomType != RoomType.Undiscovered && !neighbor.RotatedExits.HasFlag(RoomExits.Right))
                {
                    var mask = ~RoomExits.Left;
                    adjustedExits = adjustedExits & mask;
                }  
            }

            return adjustedExits;
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Room Type Check					   
        ------------------------------------------------------------------------------------------------------------
         - Class specific method. Sets the flag to allow a room to be searched depending on the type of room.
            Only a RoomTile that is empty, has rubble or is a spinroom can be searched.
        ----------------------------------------------------------------------------------------------------------*/
        private void RoomTypeSearchCheck(RoomType roomType)
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

            //spriteBatch.DrawString(font, $"Exits: Up: {ExitUp}\n Right: {ExitRight}\n Down: {ExitDown}\n " +
            //    $"Left: {ExitLeft}", new Vector2(position.X + 5, position.Y + 115), Color.White);

            spriteBatch.DrawString(font, $"Exits: {RotatedExits.ToString()}", new Vector2(position.X + 5, position.Y + 115), Color.White);

            spriteBatch.DrawString(font, $"Tile texture: {TileImageReference}", new Vector2(position.X + 5, position.Y + 205), Color.White);

            spriteBatch.DrawString(font, $"Search Left: {RoomSearched.ToString()}", new Vector2(position.X + 5, position.Y + 220), Color.White);
        }
    }
}