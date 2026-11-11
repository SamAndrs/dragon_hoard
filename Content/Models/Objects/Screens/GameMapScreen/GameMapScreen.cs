using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using dragon_hoard.Content.Models.Objects.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace dragon_hoard.Content.Models.Objects.Screens.GameMapScreen
{
    //////////////////////////////////////// GAME MAP SCREEN CLASS ////////////////////////////////////////////////
    /*

    :: TODO ---------------------------


    :: DONE ---------------------------
        (2026)

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public sealed class GameMapScreen : GameScreen
    {
        //========================================== PROPERTIES ==================================================//

        /* INHERITED
       MainGame            _game
       GameScreenManager   _screenManager
       UiManager           _ui
       bool                IsContentLoaded
       bool                IsActive    
       bool                RenderOnPause
       string              Description
       UiElement[]         ScreenUi
       */


        // DATA
        private GameSession _session;

        private GameMap _map;

        private Player _player;

        private PlayerCharacter _character;

        private RoomTile _currentRoom;

        private int _mapTotalWidth;

        private int _mapTotalHeight;


        // FUNCTIONAL
        // private ButtonsBar _buttonsBar;

        private Scrollbar _horizontalBar;

        private Scrollbar _verticalBar;

        private Vector2 _mapOffset;


        // GRAPHICS 
        private Vector2 _mapPosition;

        private Vector2 _adjPosition;

        private int _tileSize;

        private Rectangle _viewPort;

        private Point _hBarPos;

        private Point _vBarPos;

        private Vector2 _charPosition;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public GameMapScreen(MainGame game, GameScreenManager screenManager, UiManager ui) : base(game, screenManager, ui)
        {
            Description = "Game Map Screen";
            
            RenderOnPause = false;

            _tileSize = 256;
        }

        //============================================ METHODS ===================================================//
        #region methods
               


        /*----------------------------------------------------------------------------------------------------------
                                                       Load Content				   
        ------------------------------------------------------------------------------------------------------------
          - Class method for loading the screen specific content and set property values.
        ----------------------------------------------------------------------------------------------------------*/
        public override void LoadContent()
        {
            _session = _game.GameSession;

            _map = _session.GameMap;

            _mapTotalWidth = (_map.Columns - 1) * _tileSize + ((int)_mapPosition.X - 100);

            _mapTotalHeight = (_map.Rows - 1) * _tileSize + ((int)_mapPosition.Y - 100);

            _character = _session.PlayerCharacter;

            //_player = _session.Player;  // TODO

            _mapPosition = new Vector2(80, 80);

            _viewPort = new Rectangle(0,0, _game.VirtualView.Width, _game.VirtualView.Height);

            SetupSliders(_mapTotalWidth, _mapTotalHeight, _viewPort.Width, _viewPort.Height);

            IsContentLoaded = true;

        } // End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Update					   
        ------------------------------------------------------------------------------------------------------------
           - Override of derived method. Updates the screen specific content, including UI elements, provided the
              game play screen is set to 'Active'.
        ----------------------------------------------------------------------------------------------------------*/
        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                _currentRoom = _session.CurrentRoom;

                // DEBUG
                if (_game.PrevKeyboardState.IsKeyUp(Keys.E) && _game.CurrentKeyboardState.IsKeyDown(Keys.E))
                    ExitThisScreen();

                _horizontalBar.Update(gameTime);

                _verticalBar.Update(gameTime);

                _mapOffset.X = _horizontalBar.ScrollValue * (_mapTotalWidth - _viewPort.Width);

                _mapOffset.Y = _verticalBar.ScrollValue * (_mapTotalHeight - _viewPort.Height);

                // Adjust the position of the RoomTile map based on the values of the sliderbars.
                _adjPosition = new Vector2(_mapPosition.X - (int)_mapOffset.X, _mapPosition.Y - (int)_mapOffset.Y);

                // In similar fashion; adjust the player character's position on the game map.
                _charPosition = _character.MapPosition + _adjPosition;

                _character.MapPinRect = new Rectangle((int)_charPosition.X, (int)_charPosition.Y, 50, 50);
            }
        } // End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Draw					   
        ------------------------------------------------------------------------------------------------------------
         - Class method. Draws the visual representation of the Game Map on the screen.
        ----------------------------------------------------------------------------------------------------------*/
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                _game.GraphicsDevice.Clear(Color.Black);

                DrawGameMap(spriteBatch, _adjPosition);

                _horizontalBar.Draw(spriteBatch);

                _verticalBar.Draw(spriteBatch);

                // TODO replace with Character MapPin
                spriteBatch.Draw(_ui.RectangleTexture, _character.MapPinRect, Color.White);


                DebugScreen(spriteBatch);
            }
        } // End -----------------------------------------------------------------------------------------------------




        /*----------------------------------------------------------------------------------------------------------
                                               Draw Game Map			   
        ------------------------------------------------------------------------------------------------------------
        - Class support method. Draws the explored RoomTile Texture squares on the screen in accordance with
            their position on the Game map grid.
        ----------------------------------------------------------------------------------------------------------*/
       private void DrawGameMap(SpriteBatch spriteBatch, Vector2 mapPosition)
        {
            for (int row = 1; row < _map.Rows - 1; row++)
            {
                for (int col = 1; col < _map.Columns - 1; col++)
                {
                    var tile = _map.MiniMap[row, col];
                    // Only explored-, exit- and treasure room tiles will be drawn
                    if (tile != null && tile.RoomType != RoomType.OuterWall && tile.RoomType != RoomType.Undiscovered)
                    {
                        // Adjust the position of the tile with the position of the map on the screen.
                        tile.Draw(spriteBatch, mapPosition);
                    }
                }
            }
        }// End -----------------------------------------------------------------------------------------------------



        private void SetupSliders(int totalWidth, int totalHeight, int viewWidth, int viewHeight)
        {
            _hBarPos = new Point((int)_game.CenterX - (720 / 2), _viewPort.Height - 50);

            _vBarPos = new Point(_viewPort.Width - 50, _viewPort.Y + 50);

            _horizontalBar = new Scrollbar(_ui, new Rectangle(_hBarPos.X, _hBarPos.Y, 720, 15), true, ScrollOrientation.Horizontal);

            _verticalBar = new Scrollbar(_ui, new Rectangle(_vBarPos.X, _vBarPos.Y, 15, 600), true, ScrollOrientation.Vertical);

            _horizontalBar.ContentSize = totalWidth;

            _horizontalBar.ViewSize = viewWidth;

            _verticalBar.ContentSize = totalHeight;

            _verticalBar.ViewSize = viewHeight;
        }

        /*----------------------------------------------------------------------------------------------------------
                                                       Debug Screen					   
        ------------------------------------------------------------------------------------------------------------
          - Override of derived method. Draws the screen specific debugging information on the game screen.
        ----------------------------------------------------------------------------------------------------------*/
        public override void DebugScreen(SpriteBatch spriteBatch)
        {
            bool showTileData = false;

            bool showSliderData = false;

            if(showTileData)
            {
                Vector2 adjustedPos = new Vector2(_mapPosition.X - (int)_mapOffset.X, _mapPosition.Y - (int)_mapOffset.Y);

                for (int row = 1; row < _map.Rows - 1; row++)
                {
                    for (int col = 1; col < _map.Columns - 1; col++)
                    {
                        var tile = _map.MiniMap[row, col];
                        if (tile != null && tile.RoomType != RoomType.OuterWall && tile.RoomType != RoomType.Undiscovered)
                        {
                            spriteBatch.DrawString(_ui.DebugText, $"R: {tile.Row}| C: {tile.Col}", new Vector2((tile.LargeSquare.X + adjustedPos.X) + 5,
                                (tile.LargeSquare.Y + adjustedPos.Y) + 5), Color.Yellow);

                            spriteBatch.DrawString(_ui.DebugText, $"X: {tile.LargeSquare.X}| " +
                                $"Y: {tile.LargeSquare.Y}", new Vector2((tile.LargeSquare.X + adjustedPos.X) + 5,
                                (tile.LargeSquare.Y + adjustedPos.Y) + 25), Color.LimeGreen);

                        }
                    }
                }
            }

            if (showSliderData)
            {
                spriteBatch.DrawString(_ui.DebugText,$"MapOffset: {(int)_mapOffset.X}, {(int)_mapOffset.Y}", new Vector2(_game.CenterX, 80), Color.LimeGreen);

                spriteBatch.DrawString(_ui.DebugText, $"", Vector2.Zero, Color.Orange);

            }
            
        } // End -----------------------------------------------------------------------------------------------------

        #endregion
    }
}
