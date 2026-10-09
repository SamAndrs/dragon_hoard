using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragon_hoard.Content.Models.Objects.Entities
{
    ///////////////////////////////////////// GAME MAP CLASS ///////////////////////////////////////////////
    /*
        - Data container class for game map information and functionality.
        
            
    :: TODO ---------------------------
        



    :: DONE ---------------------------
        (2026)
        v.001a (10/01) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class GameMap
    {
        // DATA
        private Room[,] _mapGrid;
        
        private UiManager _ui;
        
        public int Rows { get; private set; } = 10;     // Height size property of the map. Default is 10 squares.
        
        public int Columns { get; private set; } = 13;  // Width size property of the map. Default is 13 squares.

        public Room[,] MiniMap { get { return _mapGrid; } }

        // FUNCTIONALITY


        // GRAPHICS
        private Vector2 _mapPosition; // Position of the map on the screen


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public GameMap(UiManager ui, int rows, int columns, Vector2 mapPosition)
        {
            // Add 2 rows and columns for outer walls & exit rooms.
            Rows = rows +2;
            Columns = columns +2;

            _mapGrid = new Room[Rows, Columns];
            _ui = ui;
            _mapPosition = mapPosition;
        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                       Initialize					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method to initialize the map grid with Room objects. 
            Each room is assigned a numerical ID and its position on the map is calculated based on its row and column.
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
                                                       Load Map Data					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method to load visible map data from the game session or other source, 
            if applicable.
        ----------------------------------------------------------------------------------------------------------*/
        public void Load()
        {
            // Load visible map data from the game session or other source, if applicable.

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                 Update				   
        ------------------------------------------------------------------------------------------------------------
          - Updates the game map state based on the game time.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            foreach(var room in _mapGrid)
            {
                room.Update(gameTime);
                    
            }
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                               Draw					   
        ------------------------------------------------------------------------------------------------------------
        - Main public method to draw game map on the screen.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
           DrawMapRooms(spriteBatch);

        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                               Create Room					   
        ------------------------------------------------------------------------------------------------------------
        - Class specific method to create a room object and assign it to the map grid.
            Sets initial properties such as the position of the room's 'texture square' on the map and its type.
        ----------------------------------------------------------------------------------------------------------*/
        private void CreateRoom(int roomId, int row, int col, int squareSize)
        {
            RoomType roomType;
            int mapPosX = (int)_mapPosition.X + col * (squareSize + 3);
            int mapPosY = (int)_mapPosition.Y + row * (squareSize + 3);

            // Check the top row of the map
            if (row == 0)
            {
                // If room is either top left or top right corner, set it as an exit; otherwise, set it as an outer wall
                if (col == 0 || col == Columns - 1)
                    roomType = RoomType.Exit;
                
                else
                    roomType = RoomType.OuterWall;

            }
            // Check the bottom row of the map
            else if (row == Rows - 1)
            {
                // If room is either bottom left or bottom right corner, set it as an exit; otherwise, set it as an outer wall
                if (col == 0 || col == Columns - 1)
                    roomType = RoomType.Exit;

                else
                    roomType = RoomType.OuterWall;
            }
            // If the room isn't a corner room but is in first or last column, set it as an outer wall; 
            else if (col == 0 || col == Columns - 1)
                roomType = RoomType.OuterWall;

            // otherwise, set it as an empty room
            else
                roomType = RoomType.Empty;

            // Create the room and assign it to the map grid
            _mapGrid[row, col] = new Room(_ui, roomId, row, col, new Vector2(mapPosX, mapPosY), squareSize, roomType);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                               Draw Map Rooms					   
         ------------------------------------------------------------------------------------------------------------
        - Helper method to draw each room square on the map. 
        ----------------------------------------------------------------------------------------------------------*/
        private void DrawMapRooms(SpriteBatch spriteBatch)
        {
            foreach (var room in _mapGrid)
                room.Draw(spriteBatch);

        }// End -----------------------------------------------------------------------------------------------------

        #endregion
    }
}
