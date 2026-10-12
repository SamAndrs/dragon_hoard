using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace dragon_hoard.Content.Models.Objects.Entities
{
    ///////////////////////////////////////// GAME MAP CLASS ///////////////////////////////////////////////
    /*
        - Data container class for game map information and functionality.
            Consists of a (default) 12x15 grid of RoomTile objects. Two rows and two columns make up the untraceable 
            outer wall (save for the four corners which acts as starting- and exit squares).
             
        
            
    :: TODO ---------------------------
        



    :: DONE ---------------------------
        (2026)
        v.001a (10/01) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class GameMap
    {
        // DATA
        private RoomTile[,] _mapGrid;
        
        //private UiManager _ui;
        
        public int Rows { get; private set; } = 10;     // Height size property of the map. Default is 10 squares.
        
        public int Columns { get; private set; } = 13;  // Width size property of the map. Default is 13 squares.

        public RoomTile[,] MiniMap { get { return _mapGrid; } }

        // FUNCTIONALITY


        // GRAPHICS
        public Vector2 MapPosition { get; set; }// Position of the map on the screen

        public Texture2D SquareTexture { get; private set; }


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public GameMap(int rows, int columns, Texture2D squareTexture)
        {
            // Add 2 rows and columns for outer walls & exit rooms.
            Rows = rows +2;
            Columns = columns +2;

            _mapGrid = new RoomTile[Rows, Columns];
            SquareTexture = squareTexture;
        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                       Initialize					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method to initialize the map grid with RoomTile objects. 
            Each RoomTile is assigned a numerical ID and its position on the map is calculated 
            based on its row and column on the grid.
        ----------------------------------------------------------------------------------------------------------*/
        public void Initialize()
        {
            int roomId = 1; // Initialize a variable to assign numerical IDs to each room

            // Loop through the rows and columns of the map grid to create Room objects
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    CreateRoom(roomId, row, col, 15);
                    roomId++;
                }
            }
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                 Update				   
        ------------------------------------------------------------------------------------------------------------
          - Updates the game map state based on the game time.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            foreach(var roomTile in _mapGrid)
            {
                roomTile.Update(gameTime);
                    
            }
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                               Draw					   
        ------------------------------------------------------------------------------------------------------------
        - Main public method to draw game map on the screen.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var room in _mapGrid)
                room.DrawOnMiniMap(spriteBatch, SquareTexture);

        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                               Create Room					   
        ------------------------------------------------------------------------------------------------------------
        - Class specific method to create a 'blank' RoomTile object and assign it to the map grid.
            Sets initial properties such as the position of the room's 'texture square' on the map and its type.
        ----------------------------------------------------------------------------------------------------------*/
        private void CreateRoom(int roomId, int row, int col, int squareSize)
        {
            RoomType roomType = RoomType.Undiscovered;
            int mapPosX = (int)MapPosition.X + col * (squareSize + 3);
            int mapPosY = (int)MapPosition.Y + row * (squareSize + 3);

            // Set the top-, bottom rows and the left- and rightmost columns of the map
            if (row == 0 || row == Rows - 1 || col == 0 || col == Columns - 1)
                roomType = RoomType.OuterWall;

            // Check 2nd row from top and bottom
            else if (row == 1 || row == Rows - 2)
            {
                // If room is a corner, set it as an exit; otherwise, as an undiscovered room
                if (col == 1 || col == Columns - 2)
                    roomType = RoomType.Exit;
            }
            else
                roomType = RoomType.Undiscovered;

            // Create the room and assign it to the map grid
            _mapGrid[row, col] = new RoomTile(roomType, row, col, squareSize, new Vector2(mapPosX, mapPosY));
            // Create doors to progress into castle, if room is an Exit.
            _mapGrid[row, col].DetermineCastleExits(Rows - 2, Columns - 2);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                               Create Random Start Position					   
        ------------------------------------------------------------------------------------------------------------
        - Class specific method returning one of the four corner Exit/ Start -rooms.
        ----------------------------------------------------------------------------------------------------------*/
        public RoomTile CreateRandomStartPosition(RoomTile roomTile)
        {
            Random rand = new Random();

            int result = rand.Next(3);

            int maxRows = _mapGrid.GetLength(0) - 2;
            int maxCols = _mapGrid.GetLength (1) - 2;

            switch(result)
            {
                case 0: // Upper left
                    roomTile = _mapGrid[1, 1];
                    break;
                case 1: // Upper right
                    roomTile = _mapGrid[1, maxCols];
                    break;
                case 2: // Bottom left
                    roomTile = _mapGrid[maxRows, 1];
                    break;

                case 3: // Bottom right
                    roomTile = _mapGrid[maxRows, maxCols];
                    break;
            }
            return roomTile;
        }// End -----------------------------------------------------------------------------------------------------

        #endregion
    }
}
