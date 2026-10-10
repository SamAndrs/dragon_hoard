using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace dragon_hoard.Content.Models.Objects.Screens
{
    ////////////////////////////////////////////// GAME PLAY SCREEN CLASS //////////////////////////////////////
    /*

       
    :: TODO ---------------------------
        v.001b (10/09) Implementation of player movement and corresponding minimap updates.
       
    :: DONE ---------------------------
        (2026)
        v.001a (10/01) Setup of basic properties and methods for core functionality.
        

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class GamePlayScreen : GameScreen
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
        private GameSession _gameSession;

        private PlayerCharacter _character;

        private GameMap _map;

        private Healthbar _healthbar;

        private Room _activeRoom;

        // FUNCTIONALITY
        private ClickButton _goUp;
        private ClickButton _goDown;
        private ClickButton _goLeft;
        private ClickButton _goRight;

        // GRAPHICS
        private Vector2 _mapPosition; // Position of the map on the screen

        // DEBUG
        private Room _gameSessionRoom;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public GamePlayScreen(MainGame game, GameScreenManager screenManager, UiManager ui) : base(game, screenManager, ui)
        {
            IsActive = false;

            Description = "Game Play Screen";

            RenderOnPause = false;

            //_gameSession = _game.GameSession;

            //_character = _gameSession.PlayerCharacter;
        }


        //============================================ METHODS ===================================================//
        #region methods

        /*----------------------------------------------------------------------------------------------------------
                                                  Load Content					   
        ------------------------------------------------------------------------------------------------------------
           - Override of derived method. Loads the screen specific content, sets up and initializes the 
            game map, health bar, and other UI elements for the game play screen.
        ----------------------------------------------------------------------------------------------------------*/
        public override void LoadContent()
        {
            
            _gameSession = _game.GameSession;
            _character = _gameSession.PlayerCharacter;

            _mapPosition = new Vector2((_game.GraphicsDevice.Viewport.Width - 300), 50);
            _map = new GameMap(_ui, 10, 13, _mapPosition); // Example: Create a 10x13 game map
            _map.Initialize();

            // DEBUG
            _gameSession.SetCurrentRoom(_map.MiniMap[0, 2]);
            _gameSessionRoom = _gameSession.CurrentPlayerRoom;
            _activeRoom = _map.MiniMap[1, 2];
            _activeRoom.SetTestValues(RoomType.Empty, true);
            _activeRoom.Initialize(_gameSession);
            CreateTestButtons();
            //-------------------------------------------------


           

            _healthbar = new Healthbar(_ui, new Vector2(_game.CenterX, _game.CenterY + 150)); // Example position
            _healthbar.Load();
            _healthbar.Initialize(_character.MaxHealth, _character.CurrentHealth, 20);

            IsContentLoaded = true;


        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                  Update				   
        ------------------------------------------------------------------------------------------------------------
           - Override of derived method. Updates the screen specific content, including UI elements, provided the
              game play screen is set to 'Active'.
        ----------------------------------------------------------------------------------------------------------*/
        public override void Update(GameTime gameTime)
        {
            if(IsActive)
            {
                _map.Update(gameTime);
                _healthbar.Update(_character.CurrentHealth);

                // DEBUG
                UpdateTestButtons(gameTime);
            }
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                Draw					   
        ------------------------------------------------------------------------------------------------------------
         - Override of derived method. Draws the screen specific content, including UI elements onto the game screen,
               provided the splash screen is set to 'Active'.
        ----------------------------------------------------------------------------------------------------------*/
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                _game.GraphicsDevice.Clear(Color.Black);

                _map.Draw(spriteBatch);
                _healthbar.Draw(spriteBatch);

                try
                {
                    _activeRoom.Draw(spriteBatch);
                }
                catch (Exception ex)
                {
                    spriteBatch.DrawString(_ui.DebugText, $"ERROR! {ex.ToString()}", new Vector2(_game.CenterX, _game.CenterY), Color.Red);
                }

                DrawTestButtons(spriteBatch); // DEBUG
            }

            base.Draw(spriteBatch);
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                Debug Screen				   
        ------------------------------------------------------------------------------------------------------------
         - Override of derived method. Draws the screen specific debugging information on the game screen.
        ----------------------------------------------------------------------------------------------------------*/
        public override void DebugScreen(SpriteBatch spriteBatch)
        {
            bool showContentLoaded = false;

            bool showMousePos = false;

            bool showCurrentRoom = false;

            bool showTilesInStack = true;

            bool displayHeroStats = true;

            if (showContentLoaded)
            {
                spriteBatch.DrawString(_ui.DebugText, $"Content loaded: {IsContentLoaded}",
                   new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 100), Color.White);
            }

            if (showMousePos)
            {
                spriteBatch.DrawString(_ui.DebugText, $"PrevMouse: [{_game.PrevMouseState}]",
                   new Vector2(25, 25), Color.Yellow);

                spriteBatch.DrawString(_ui.DebugText, $"CurrMouse: [{_game.currentMouseState}]",
                   new Vector2(25, 50), Color.White);
            }

            if (showCurrentRoom)
            {
                spriteBatch.DrawString(_ui.DebugText, $"ActiveRoom: [{_activeRoom.RoomRow}, {_activeRoom.RoomColumn}]",
                   new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 130), Color.Green);

                spriteBatch.DrawString(_ui.DebugText, $"SessionRoom: [{_gameSessionRoom.RoomRow}, {_gameSessionRoom.RoomColumn}]",
                   new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 100), Color.LightBlue);

                spriteBatch.DrawString(_ui.DebugText, $"PlayerMove: {_gameSession.PlayerMove}",
                   new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 80), Color.Orange);
            }

            if (showTilesInStack)
            {
                spriteBatch.DrawString(_ui.DebugText, $"RoomTilesStack: {_gameSession.RoomTileStack.Count}",
                   new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 60), Color.Orange);
            }

            if (displayHeroStats) DrawTestCharacter(spriteBatch);


           
        }// End -----------------------------------------------------------------------------------------------------


        private void PlayerExitRoom(Direction direction)
        {
            _gameSession.SetCurrentRoom(_activeRoom);
            _gameSessionRoom = _gameSession.CurrentPlayerRoom;

            if (_gameSession.RoomTileStack.Count > 0)
            {
                _gameSession.RoomTileStack.Pop();
                _gameSession.PlayerMove--;

                if (direction == Direction.Up) _activeRoom = _map.MiniMap[_gameSessionRoom.RoomRow - 1, _gameSessionRoom.RoomColumn];
                else if (direction == Direction.Down) _activeRoom = _map.MiniMap[_gameSessionRoom.RoomRow + 1, _gameSessionRoom.RoomColumn];

                else if (direction == Direction.Right) _activeRoom = _map.MiniMap[_gameSessionRoom.RoomRow, _gameSessionRoom.RoomColumn + 1];
                else if (direction == Direction.Left) _activeRoom = _map.MiniMap[_gameSessionRoom.RoomRow, _gameSessionRoom.RoomColumn - 1];

                _activeRoom.SetTestValues(RoomType.Empty, true);
                _activeRoom.Initialize(_gameSession);
            }
        }
        #endregion




        private void CreateTestButtons()
        {
            int size = 50;

            Point center = new Point((int)_game.CenterX, (int)_game.CenterY);

            _goUp = new ClickButton(_ui, new Rectangle(center.X, center.Y + 80, size, size), true);
            _goUp.Initialize(Color.White, "U", Color.White, Vector2.Zero, true, 0);

            _goDown = new ClickButton(_ui, new Rectangle(center.X, center.Y + 80 + (size * 2), size, size), true);
            _goDown.Initialize(Color.White, "D", Color.White, Vector2.Zero, true, 0);

            _goLeft = new ClickButton(_ui, new Rectangle(center.X - size, center.Y + (80 + size), size, size), true);
            _goLeft.Initialize(Color.White, "L", Color.White, Vector2.Zero, true, 0);

            _goRight = new ClickButton(_ui, new Rectangle(center.X + size, center.Y + (80 + size), size, size), true);
            _goRight.Initialize(Color.White, "R", Color.White, Vector2.Zero, true, 0);
        }

        private void UpdateTestButtons(GameTime gameTime)
        {
            _goUp.Update(gameTime);
            _goDown.Update(gameTime);
            _goLeft.Update(gameTime);
            _goRight.Update(gameTime);


            if (_goUp.Clicked && _gameSession.PlayerMove > 0)
            {
                _goUp.BackgroundColor = Color.Yellow;
                PlayerExitRoom(Direction.Up);
                _gameSession.PlayerMove++;
            }
            else _goUp.BackgroundColor = Color.White;

            if (_goDown.Clicked && _gameSession.PlayerMove > 0)
            {
                _goDown.BackgroundColor = Color.Yellow;
                PlayerExitRoom(Direction.Down);
                _gameSession.PlayerMove++;
            }
            else _goDown.BackgroundColor = Color.White;

            if (_goRight.Clicked && _gameSession.PlayerMove > 0)
            {
                _goRight.BackgroundColor = Color.Yellow;
                PlayerExitRoom(Direction.Right);
                _gameSession.PlayerMove++;
            }
            else _goRight.BackgroundColor = Color.White;


            if (_goLeft.Clicked && _gameSession.PlayerMove > 0)
            {
                _goLeft.BackgroundColor = Color.Yellow;
                PlayerExitRoom(Direction.Left);
                _gameSession.PlayerMove++;
            }
            else _goLeft.BackgroundColor = Color.White;
        }


        private void DrawTestButtons(SpriteBatch spriteBatch)
        {
            _goUp.Draw(spriteBatch);
            spriteBatch.DrawString(_ui.DebugText, $"{_goUp.Clicked}", new Vector2(_goUp.BoundingBox.X, _goUp.BoundingBox.Top - 10), Color.White);

            _goDown.Draw(spriteBatch);
            spriteBatch.DrawString(_ui.DebugText, $"{_goDown.Clicked}", new Vector2(_goDown.BoundingBox.X, _goDown.BoundingBox.Bottom + 10), Color.White);

            _goLeft.Draw(spriteBatch);
            spriteBatch.DrawString(_ui.DebugText, $"{_goLeft.Clicked}", new Vector2(_goLeft.BoundingBox.Left - 40, _goLeft.BoundingBox.Y), Color.White);

            _goRight.Draw(spriteBatch);
            spriteBatch.DrawString(_ui.DebugText, $"{_goRight.Clicked}", new Vector2(_goRight.BoundingBox.Right + 10, _goRight.BoundingBox.Y), Color.White);
        }


        private void DrawTestCharacter(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_ui.DebugText, $"Character: {_character.Name}",
                   new Vector2(_game.GraphicsDevice.Viewport.Width - 300, 400), Color.White);

            spriteBatch.DrawString(_ui.DebugText, $"STR: {_character.Strength}",
                   new Vector2(_game.GraphicsDevice.Viewport.Width - 300, 420), Color.White);

            spriteBatch.DrawString(_ui.DebugText, $"DEX: {_character.Dexterity}",
                  new Vector2(_game.GraphicsDevice.Viewport.Width - 300, 440), Color.White);

            spriteBatch.DrawString(_ui.DebugText, $"LCK: {_character.Luck}",
                  new Vector2(_game.GraphicsDevice.Viewport.Width - 300, 460), Color.White);

            spriteBatch.DrawString(_ui.DebugText, $"ARM: {_character.Armor}",
                  new Vector2(_game.GraphicsDevice.Viewport.Width - 300, 480), Color.White);
        }
    }
}
