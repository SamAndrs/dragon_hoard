using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using dragon_hoard.Content.Models.Objects.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Data;

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

        private RoomTile _currentRoom;

        private GameMap _map;

        private Healthbar _healthbar;

        // FUNCTIONALITY
        private ClickButton _goUp;
        private ClickButton _goDown;
        private ClickButton _goLeft;
        private ClickButton _goRight;

        // GRAPHICS

        // DEBUG

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public GamePlayScreen(MainGame game, GameScreenManager screenManager, UiManager ui) : base(game, screenManager, ui)
        {
            IsActive = false;

            Description = "Game Play Screen";

            RenderOnPause = false;
        }


        //============================================ METHODS ===================================================//
        #region methods

        /*----------------------------------------------------------------------------------------------------------
                                                  Load Content					   
        ------------------------------------------------------------------------------------------------------------
           - Override of derived method. Loads the screen specific content, connects the game minimap, health bar, 
            and other UI elements for the game play screen.
        ----------------------------------------------------------------------------------------------------------*/
        public override void LoadContent()
        {
            _gameSession = _game.GameSession;
            _character = _gameSession.PlayerCharacter;

            _map = _gameSession.GameMap;

            _currentRoom = _gameSession.CurrentRoom;

            // DEBUG
            CreateTestButtons();
            //-------------------------------------------------

            _healthbar = new Healthbar(_ui, new Vector2(_game.CenterX, _game.CenterY + 150));
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
                _currentRoom = _gameSession.CurrentRoom;
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
                    spriteBatch.Draw(_ui.RectangleTexture, _currentRoom.MiniSquare, Color.White);
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

            bool showCurrentRoom = true;

            bool showTilesInStack = true;

            bool displayHeroStats = true;

            bool mapNumbers = true;

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
                spriteBatch.DrawString(_ui.DebugText, $"PlayerMove: {_gameSession.PlayerMove}",
                   new Vector2(50, 40), Color.Orange);

                spriteBatch.DrawString(_ui.DebugText, $"CurrentRoom: [{_currentRoom.Row}, {_currentRoom.Col}]",
                   new Vector2(50, 60), Color.Green);

                spriteBatch.DrawString(_ui.DebugText, $"MoveDirection: {_gameSession.MoveDirection}",
                  new Vector2(180, 80), Color.LightCyan);

                spriteBatch.DrawString(_ui.DebugText, $"EnteredFrom: {_currentRoom.EnteredFrom}",
                   new Vector2(50, 80), Color.LightCyan);

                spriteBatch.DrawString(_ui.DebugText, $"Doors: [value: {_currentRoom.DoorValue}]", new Vector2(50, 100), Color.White);

                if (_gameSession.RoomTileStack.Count > 0)
                {
                    spriteBatch.DrawString(_ui.DebugText, $"NextRoomTileType: {_gameSession.RoomTileStack.Peek().RoomType}",
                     new Vector2(250, 60), Color.Yellow);

                    if (_gameSession.NextRoom != null)
                    {
                        spriteBatch.DrawString(_ui.DebugText, $"NextRoom: [{_gameSession.NextRoom.Row}, {_gameSession.NextRoom.Col}]",
                           new Vector2(250, 70), Color.Red);

                        spriteBatch.DrawString(_ui.DebugText, $"NextRoom: {_gameSession.NextRoom.RoomType}",
                      new Vector2(250, 120), Color.Gray);
                    }
                }

                for (int i = 0; i < _gameSession.ExploredRooms.Count; i++)
                {
                    spriteBatch.DrawString(_ui.DebugText, $"ExploredRooms: [{_gameSession.ExploredRooms[i].Row}, {_gameSession.ExploredRooms[i].Col}]",
                  new Vector2(50, 210 + (i * 15)), Color.LightBlue);
                }
            }

            if (showTilesInStack)
            {
                spriteBatch.DrawString(_ui.DebugText, $"RoomTilesStack: {_gameSession.RoomTileStack.Count}",
                   new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 60), Color.Orange);
            }

            if (displayHeroStats) DrawTestCharacter(spriteBatch);
              

            if (mapNumbers)
            {
                for (int row = 0; row < _map.Rows; row++)
                    spriteBatch.DrawString(_ui.DebugText, $"{row}", new Vector2(_map.MapPosition.X - 25, _map.MapPosition.Y + (row * 18)), Color.LightBlue);

                for (int col = 0; col < _map.Columns; col++)
                    spriteBatch.DrawString(_ui.DebugText, $"{col}", new Vector2(_map.MapPosition.X + (col * 18), _map.MapPosition.Y - 25), Color.LightBlue);
            }
        }// End -----------------------------------------------------------------------------------------------------

       
        private void PlayerExitRoom(Direction direction)
        {
            _gameSession.CreateTestRound(direction);
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

            // Set initial state to non-clickable
            _goUp.SetNonClickable();
            _goRight.SetNonClickable();
            _goDown.SetNonClickable();
            _goLeft.SetNonClickable();
        }

        private void UpdateTestButtons(GameTime gameTime)
        {           
            _goUp.Update(gameTime);
            _goRight.Update(gameTime);
            _goDown.Update(gameTime);
            _goLeft.Update(gameTime);

            // Allow only exits that are available

            // Check if there's an exit up and if the current position on gamemap is not on upmost row.
            if (_currentRoom.ExitUp && _currentRoom.Row > 1)
            //if (_currentRoom.Row > 1)
            {
                _goUp.SetVisibility();
                _goUp.SetClickable();
                
            }
            else
            {
                _goUp.SetInvisible();
                _goUp.SetNonClickable();
            }

            if (_currentRoom.ExitRight && _currentRoom.Col < _map.Columns - 2)
            //if (_currentRoom.Col < _map.Columns - 2)
            {
                _goRight.SetVisibility();
                _goRight.SetClickable();
                
            }
            else
            {
                _goRight.SetInvisible();
                _goRight.SetNonClickable();
            }

            if (_currentRoom.ExitDown && _currentRoom.Row < _map.Rows - 2)
            //if (_currentRoom.Row < _map.Rows - 2)
            {
                _goDown.SetVisibility();
                _goDown.SetClickable();
                
            }
            else
            {
                _goDown.SetInvisible();
                _goDown.SetNonClickable();
            }

            if (_currentRoom.ExitLeft && _currentRoom.Col > 1)
            //if (_currentRoom.Col > 1)
            {
                _goLeft.SetVisibility();
                _goLeft.SetClickable();
                
            }
            else
            {
                _goLeft.SetInvisible();
                _goLeft.SetNonClickable();
            }

            if (_goUp.Clicked) PlayerExitRoom(Direction.Up);

            if (_goRight.Clicked) PlayerExitRoom(Direction.Right);

            if (_goDown.Clicked) PlayerExitRoom(Direction.Down);

            if (_goLeft.Clicked) PlayerExitRoom(Direction.Left);
        }


        private void DrawTestButtons(SpriteBatch spriteBatch)
        {
            _goUp.Draw(spriteBatch);
            _goRight.Draw(spriteBatch);
            _goDown.Draw(spriteBatch);
            _goLeft.Draw(spriteBatch);


            //spriteBatch.DrawString(_ui.DebugText, $"{_goUp.Clicked}", new Vector2(_goUp.BoundingBox.X, _goUp.BoundingBox.Top - 10), Color.White);
            //spriteBatch.DrawString(_ui.DebugText, $"{_goRight.Clicked}", new Vector2(_goRight.BoundingBox.Right + 10, _goRight.BoundingBox.Y), Color.White);
            //spriteBatch.DrawString(_ui.DebugText, $"{_goDown.Clicked}", new Vector2(_goDown.BoundingBox.X, _goDown.BoundingBox.Bottom + 10), Color.White);
            //spriteBatch.DrawString(_ui.DebugText, $"{_goLeft.Clicked}", new Vector2(_goLeft.BoundingBox.Left - 40, _goLeft.BoundingBox.Y), Color.White);
        }


        private void DrawTestCharacter(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_ui.DebugText, $"Character: {_character.Name}",
                   new Vector2(_game.GraphicsDevice.Viewport.Width - 300, 400), Color.White);

            spriteBatch.DrawString(_ui.DebugText, $"STR: {_character.Strength}",
                   new Vector2(_game.GraphicsDevice.Viewport.Width - 300, 420), Color.White);

            spriteBatch.DrawString(_ui.DebugText, $"DEX: {_character.Dexterity}",
                  new Vector2(_game.GraphicsDevice.Viewport.Width - 300, 440), Color.White);

            spriteBatch.DrawString(_ui.DebugText, $"ARM: {_character.Armor}",
                  new Vector2(_game.GraphicsDevice.Viewport.Width - 300, 460), Color.White);

            spriteBatch.DrawString(_ui.DebugText, $"LCK: {_character.Luck}",
                 new Vector2(_game.GraphicsDevice.Viewport.Width - 300, 480), Color.White);
        }
    }
}
