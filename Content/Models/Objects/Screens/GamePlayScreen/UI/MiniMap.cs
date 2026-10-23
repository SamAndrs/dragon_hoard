using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI
{
    /////////////////////////////////////////// MINIMAP CLASS ////////////////////////////////////////////////////
    /*
        Child UI class to the GamePlayScreen. Represents the GameMap mapgrid and the assosciated information 
            and functionality.
            

    :: TODO ---------------------------


    :: DONE ---------------------------
        v.001a (10/23) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class MiniMap
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        private UiManager _ui;

        private GameSession _session;

        private GameMap _map;

        private Rectangle[,] _grid;

        // FUNCTIONAL
        private int _currentRow;

        private int _currentCol;


        // GRAPHICS 
        private Point _position;

        private int _squareSize;


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public MiniMap(UiManager ui, GameSession session, GameMap map)
        {
            _ui = ui;

            _session = session;
            
            _map = map;
        }


        //============================================ METHODS ===================================================//
        #region methods

        /*----------------------------------------------------------------------------------------------------------
                                                       Load					   
        ------------------------------------------------------------------------------------------------------------
          - Class method for loading property values, initiating and setting up the MiniMap square grid.
        ----------------------------------------------------------------------------------------------------------*/
        public void Load(Point position)
        {
            _position = position;

            _squareSize = 15;

            _grid = new Rectangle[_map.Rows, _map.Columns];

            _currentRow = _session.CurrentRoom.Row;
            _currentCol = _session.CurrentRoom.Col;

            for (int row = 0; row < _map.Rows; row++)
            {
                for (int col = 0; col < _map.Columns; col++)
                {
                    int mapPosX = _position.X + col * (_squareSize + 3);
                    int mapPosY = _position.Y + row * (_squareSize + 3);

                    _grid[row, col] = new Rectangle(mapPosX, mapPosY, _squareSize, _squareSize);
                }
            }
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                      Update					   
        ------------------------------------------------------------------------------------------------------------
         - Class method to keep track of the MiniMap square representing the current RoomTile 
            (i.e. player location)
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            _currentRow = _session.CurrentRoom.Row;
            _currentCol = _session.CurrentRoom.Col;

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Draw					   
        ------------------------------------------------------------------------------------------------------------
         - Class method for drawing a representation of the GameMap grid as a minimap onto the screen.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int row = 0; row < _grid.GetLength(0); row++)
            {
                for (int col = 0; col < _grid.GetLength(1); col++)
                {
                    // If the square isn't refering to the CurrentRoom, pick an appropriate color for the type of Room
                    if (_grid[row, col] != _grid[_currentRow, _currentCol])
                        spriteBatch.Draw(_ui.RectangleTexture, _grid[row, col], SetMiniMapColor(_map.MiniMap[row, col].RoomType));

                    // Otherwise give it a white color to represent the current player location.
                    else
                        spriteBatch.Draw(_ui.RectangleTexture, _grid[row, col], Color.White);

                }
            }
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Set Mini Map Color			   
       ------------------------------------------------------------------------------------------------------------
         - Class specific method. Assigns the RoomTile's minimap square a color denoting the type of
           room represented.
       ----------------------------------------------------------------------------------------------------------*/
        private Color SetMiniMapColor(RoomType roomType)
        {
            //TODO re-evaluate colors!
            Color darkGray = new Color(28, 30, 33);
            Color pit = new Color(10, 50, 255);
            Color bridge = new Color(50, 50, 200);
            Color dark = new Color(25, 25, 25);

            switch (roomType)
            {
                case RoomType.Undiscovered: return Color.Black;
                case RoomType.Exit: return Color.Yellow;
                case RoomType.OuterWall: return darkGray;
                case RoomType.Empty: return Color.DarkBlue;
                case RoomType.BottomLessPit: return pit;
                case RoomType.BridgeRoom: return bridge;
                case RoomType.DarkRoom: return dark;
                case RoomType.Corridor: return Color.Orange;
                case RoomType.Rubble: return Color.Brown;
                case RoomType.SpinRoom: return Color.LightGray;
                case RoomType.TreasureRoom: return Color.Gold;
                case RoomType.WebbedRoom: return Color.LightSalmon;
                case RoomType.TrapRoom: return Color.Red;
                default: return Color.IndianRed;
            }

            //return setColor;
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Method					   
        ------------------------------------------------------------------------------------------------------------
         - Description
        ----------------------------------------------------------------------------------------------------------*/



        // End -----------------------------------------------------------------------------------------------------
        #endregion
    }
}
