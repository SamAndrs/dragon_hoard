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
            Consists of a (default) 12x15 grid of RoomTile objects. Two rows and two columns make up the 
            untraversable outer wall.
            The 4 corners of the GameMap grid proper acts as starting- and exit squares.
             
    :: TODO ---------------------------
        
        


    :: DONE ---------------------------
        (2026)
        v.001b (10/14) Rework of CreateRoom method. Additional methods (ReturnRoomTile, PickRandomStartPosition) added.
        v.001a (10/01) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class GameMap
    {
        // DATA
        private RoomTile[,] _mapGrid;
               
        public int Rows { get; private set; } = 10;     // Height size property of the map. Default is 10 squares.
        
        public int Columns { get; private set; } = 13;  // Width size property of the map. Default is 13 squares.

        public RoomTile[,] MiniMap { get { return _mapGrid; } }

        // FUNCTIONALITY


        // GRAPHICS
        //public Vector2 MapPosition { get; set; }                // Position of the map on the screen
        public Point MapPosition { get; set; }


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public GameMap(int rows, int columns)
        {
            // Add 2 rows and columns for outer walls & exit rooms.
            Rows = rows +2;
            Columns = columns +2;

            _mapGrid = new RoomTile[Rows, Columns];
        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                       Initialize				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method to initialize the map grid with RoomTile objects. 
            Each RoomTile is assigned a position on the map is calculated 
            based on its row and column on the grid.	
        ----------------------------------------------------------------------------------------------------------*/
        public void Initialize()
        {
            // Loop through the rows and columns of the map grid to create Room objects
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    _mapGrid[row, col] = CreateRoom(row, col, 15);
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
            
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                               Draw Game Map			   
        ------------------------------------------------------------------------------------------------------------
        - Main public method to draw game map on the screen.
        ----------------------------------------------------------------------------------------------------------*/
        public void DrawGameMap(SpriteBatch spriteBatch)
        {
          
          

        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                               Create Room					   
        ------------------------------------------------------------------------------------------------------------
        - Class specific method to create a 'blank' RoomTile object and assign it to the map grid.
            The RoomTile object will be changed to either an outer wall or a Castle Exit tile depending on
            certain conditions.
            Sets initial properties such as the position of the room's 'texture square' on the minimap and its type.
        ----------------------------------------------------------------------------------------------------------*/
        private RoomTile CreateRoom(int row, int col, int squareSize)
        {
            // Default room for every position.
            RoomTile newRoom = new RoomTile(RoomType.Undiscovered, row, col, squareSize);

            // Outer walls.
            if (row == 0 || row == Rows - 1 || col == 0 || col == Columns - 1)
                newRoom = new RoomTile(RoomType.OuterWall, row, col, squareSize);

            // Exits - If room is a corner, set it as an exit;
            else if ((row == 1 || row == Rows - 2) && (col == 1 || col == Columns -2 ) )
            {
                // If room is a corner, set it as an exit;
                newRoom = new RoomTile(RoomType.Exit, row, col, squareSize);
                CreateExitDoors(newRoom);
            }
            
            return newRoom;
        }// End -----------------------------------------------------------------------------------------------------

     
        /*----------------------------------------------------------------------------------------------------------
                                               Return Room Tile					   
        ------------------------------------------------------------------------------------------------------------
        - Class specific method returning a given GameMap RoomTile upon request.
        ----------------------------------------------------------------------------------------------------------*/
        public RoomTile ReturnRoomTile(Direction direction, int currentRow, int currentCol)
        {
           int newRow = currentRow;
           int newCol = currentCol;

            switch (direction)
            {
                // Set placeholder tile to corresponding RoomTile on Minimap, depending on direction moved.
                case Direction.Up: newRow --; break;
                case Direction.Right: newCol ++; break;
                case Direction.Down: newRow ++; break;
                case Direction.Left: newCol --; break;
            }

             return _mapGrid[newRow, newCol];
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                              Create Exit Doors					   
       ------------------------------------------------------------------------------------------------------------
       - Class specific method assigning doors in Exit rooms, by which player can progress into the castle.
       ----------------------------------------------------------------------------------------------------------*/
        private void CreateExitDoors(RoomTile roomTile)
        {
            if (roomTile.Row == 1)
            {
                if (roomTile.Col == 1)
                    roomTile.ExitRight = true;
                else
                    roomTile.ExitLeft = true;

                roomTile.ExitDown = true;
                roomTile.EnteredFrom = Direction.Up;
            }
            else
            {
                if (roomTile.Col == 1)
                    roomTile.ExitRight = true;
                else
                    roomTile.ExitLeft = true;

                roomTile.ExitUp = true;
                roomTile.EnteredFrom = Direction.Down;
            }
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                               Create Random Start Position					   
        ------------------------------------------------------------------------------------------------------------
        - Class specific method returning one of the four corner Exit/ Start -rooms.
        ----------------------------------------------------------------------------------------------------------*/
        public RoomTile PickRandomStartPosition()
        {
            Random rand = new Random();

            int result = rand.Next(3);

            int maxRows = Rows - 2;
            int maxCols = Columns - 2;

            switch(result)
            {
                case 0: // Upper left
                    return _mapGrid[1, 1];

                case 1: // Upper right
                    return _mapGrid[1, maxCols];

                case 2: // Bottom left
                    return _mapGrid[maxRows, 1];

                case 3: // Bottom right
                    return _mapGrid[maxRows, maxCols];

                default: // Top left as default.
                    return _mapGrid[1, 1];
            }
        }// End -----------------------------------------------------------------------------------------------------

        #endregion
    }
}
