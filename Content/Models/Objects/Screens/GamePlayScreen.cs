using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using dragon_hoard.Content.Models.Objects.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace dragon_hoard.Content.Models.Objects.Screens
{
    ////////////////////////////////////////////// GAME PLAY SCREEN CLASS //////////////////////////////////////
    /*

       
    :: TODO ---------------------------
        * Account for RoomTile events preventing movement (traps, etc)
        * Account for RoomCard events preventing movement (combat, traps, special etc.) 
       
    :: DONE ---------------------------
        (2026)
        v.001b (10/09) Implementation of player movement and corresponding minimap updates.
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

        private RoomCard _currentCard;

        private GameMap _map;

        private Healthbar _healthbar;

        private SunDial _sunDial;

        // FUNCTIONALITY
        private ClickButton _goUp;
        private ClickButton _goDown;
        private ClickButton _goLeft;
        private ClickButton _goRight;

        // GRAPHICS
        //private Vector2 _mapPosition;

        private Vector2 _healthBarPosition;

        private Vector2 _sunDialPosition;

        private Vector2 _currentCardPosition;

        private float _eventMessageTimer;

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

            _eventMessageTimer = 3f;

            // DEBUG
            CreateMoveButtons();
            //-------------------------------------------------
            
            _healthBarPosition = new Vector2(_game.CenterX, _game.CenterY + 150);
            _healthbar = new Healthbar(_ui, _healthBarPosition);
            _healthbar.Load(_character.MaxHealth, _character.CurrentHealth, 20);

            _sunDialPosition = new Vector2(_game.CenterX - 486, 35);
            _sunDial = new SunDial(_gameSession, _ui.SunIcon, _ui.SunSkull);
            _sunDial.Load(_sunDialPosition);

            _currentCardPosition = new Vector2(150, 120);
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
                // Display the SunDial message until LMB is clicked.
                if (_gameSession.DisplayFlavorMessage)
                {
                    if (_ui.Mouse.LeftButton == ButtonState.Pressed
                    && _ui.PrevMouse.LeftButton == ButtonState.Released)
                        _gameSession.DisplayFlavorMessage = false;
                }

                _currentRoom = _gameSession.CurrentRoom;
                _sunDial.Update(gameTime, _gameSession.Randomizer);
                _map.Update(gameTime);
                _healthbar.Update(_character.CurrentHealth);

                UpdateScreenButtons(gameTime, _gameSession.PlayerCanMove);

                _currentCard = _gameSession.CurrentRoomCard;
                if (_currentCard != null) 
                    _currentCard.Update(_currentCardPosition);

                // DEBUG
                if(!string.IsNullOrEmpty(_gameSession.EventMessage) && _gameSession.DisplayEventMessage)
                {
                    if(_eventMessageTimer > 0)
                    {
                        _eventMessageTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else
                    {
                        _gameSession.DisplayEventMessage = false;
                        _eventMessageTimer = 3f;
                    }
                }

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

                _sunDial.Draw(spriteBatch);

                _map.Draw(spriteBatch);
                _healthbar.Draw(spriteBatch);

                spriteBatch.Draw(_ui.RectangleTexture, _currentRoom.MiniSquare, Color.White);
                
                DrawMoveButtons(spriteBatch);


                //DEBUG
                if(_currentCard != null)
                {
                    _currentCard.Draw(spriteBatch, _ui.Text);

                    DrawEventMessage(spriteBatch, _gameSession.DisplayEventMessage, _gameSession.EventMessage, new Vector2(_currentCardPosition.X + (_gameSession.CurrentRoomCard.BoundingBox.Width / 2), _game.CenterY));
                }
                    

                DrawFlavorMessage(spriteBatch, _gameSession.DisplayFlavorMessage, _gameSession.FlavorMessage, new Vector2(_game.CenterX, _sunDial.Position.Y));

                

            }

            base.Draw(spriteBatch);
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                Player Exit Room					   
        ------------------------------------------------------------------------------------------------------------
         - Class specific method. Assigns the direction which the player leaves the current room, and then 
            callbacks to the game session method for going through the events of a turn.
        ----------------------------------------------------------------------------------------------------------*/
        private void PlayerExitRoom(Direction direction)
        {
            _gameSession.MoveDirection = direction;
            _gameSession.SetPlayerCanMove(false);
            _gameSession.DiscardCurrentRoomCard(); //TODO implement check. Certain cards should not be dicarded upon moving.
            _gameSession.CreateTestRound();
            
        }// End -----------------------------------------------------------------------------------------------------
        



        /*----------------------------------------------------------------------------------------------------------
                                               Create Move Buttons				   
        ------------------------------------------------------------------------------------------------------------
         - Class specific button for creating and initiating the 4 movement buttons.
        ----------------------------------------------------------------------------------------------------------*/
        private void CreateMoveButtons()
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
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                               Update Screen Buttons				   
        ------------------------------------------------------------------------------------------------------------
         - Class specific main method for looping through and updating the status and catch any events 
            of all the buttons.
        ----------------------------------------------------------------------------------------------------------*/
        private void UpdateScreenButtons(GameTime gameTime, bool playerCanExit)
        {
            _goUp.Update(gameTime);
            _goRight.Update(gameTime);
            _goDown.Update(gameTime);
            _goLeft.Update(gameTime);

            UpdateMoveButtonStatus();

            // Movement only allowed if Player isn't facing an event that prevents it (trap, monster etc.)
            if (playerCanExit)
            {
                if (_goUp.Clicked) PlayerExitRoom(Direction.Up);

                if (_goRight.Clicked) PlayerExitRoom(Direction.Right);

                if (_goDown.Clicked) PlayerExitRoom(Direction.Down);

                if (_goLeft.Clicked) PlayerExitRoom(Direction.Left);
            }
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                              Update Move Button Status
        ------------------------------------------------------------------------------------------------------------
         - Class specific support method for looping through and updating the status visibility and 
            clickability status of the move buttons.
        ----------------------------------------------------------------------------------------------------------*/
        private void UpdateMoveButtonStatus()
        {
            // Allow only exits that are available

            // Check if there's an exit up and if the current position on gamemap is not on upmost row.
            if (_currentRoom.ExitUp && _currentRoom.Row > 1)
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
            {
                _goLeft.SetVisibility();
                _goLeft.SetClickable();

            }
            else
            {
                _goLeft.SetInvisible();
                _goLeft.SetNonClickable();
            }
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                               Draw Move Buttons				   
        ------------------------------------------------------------------------------------------------------------
         - Class specific button for drawing the move buttons onto the game screen.
        ----------------------------------------------------------------------------------------------------------*/
        private void DrawMoveButtons(SpriteBatch spriteBatch)
        {
            if(_gameSession.PlayerCanMove)
            {
                _goUp.Draw(spriteBatch);
                _goRight.Draw(spriteBatch);
                _goDown.Draw(spriteBatch);
                _goLeft.Draw(spriteBatch);
            }
            
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                               Draw Flavor Message				   
        ------------------------------------------------------------------------------------------------------------
         - Class specific method for displaying the randomly choosen flavor message, picked at the 
            start of a game turn.
        ----------------------------------------------------------------------------------------------------------*/
        private void DrawFlavorMessage(SpriteBatch spriteBatch, bool showMessage, string message, Vector2 position)
        {
            Vector2 modifiedPos = new Vector2();
            if (!string.IsNullOrEmpty(message))
            {
                // Measure the length of the string to be ble to center its position correctly
                Vector2 messageLength = _ui.HeadLine.MeasureString(message) / 2;
                modifiedPos = new Vector2(position.X - messageLength.X, position.Y);
            }

            if (showMessage)
                spriteBatch.DrawString(_ui.HeadLine, message, modifiedPos, Color.Red);

            else
                return;
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                               Draw Event Message				   
        ------------------------------------------------------------------------------------------------------------
         - Class specific method for displaying any Event message on screen.
        ----------------------------------------------------------------------------------------------------------*/
        private void DrawEventMessage(SpriteBatch spriteBatch, bool showMessage, string message, Vector2 position)
        {
            Vector2 modifiedPos = new Vector2();
            if (!string.IsNullOrEmpty(message))
            {
               // Measure the length of the string to be able to center its position correctly
               Vector2 messageLength = _ui.HeadLine.MeasureString(message) / 2;
                modifiedPos = new Vector2(position.X - messageLength.X, position.Y);
            }

            if (showMessage)
                spriteBatch.DrawString(_ui.HeadLine, message, modifiedPos, Color.Red);
                //spriteBatch.DrawString(_ui.HeadLine, message, position, Color.Red);

            else
                return;
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

            bool showTilesInStack = false;

            bool displayHeroStats = true;

            bool mapNumbers = false;

            bool displayCardEvent = true;

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
                spriteBatch.DrawString(_ui.DebugText, $"CurrentRoom: [{_currentRoom.Row}, {_currentRoom.Col}]",
                   new Vector2(50, 60), Color.Green);

                spriteBatch.DrawString(_ui.DebugText, $"MoveDirection: {_gameSession.MoveDirection}",
                  new Vector2(180, 80), Color.LightCyan);

                spriteBatch.DrawString(_ui.DebugText, $"EnteredFrom: {_currentRoom.EnteredFrom}",
                   new Vector2(50, 80), Color.LightCyan);

                spriteBatch.DrawString(_ui.DebugText, $"Doors: [value: {_currentRoom.DoorValue}]", new Vector2(50, 100), Color.White);

                if (_gameSession.RoomTileStack.TileStack.Count > 0)
                {
                    spriteBatch.DrawString(_ui.DebugText, $"NextRoomTileType: {_gameSession.RoomTileStack.TileStack.Peek().RoomType}",
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
                spriteBatch.DrawString(_ui.DebugText, $"RoomTilesStack: {_gameSession.RoomTileStack.TileStack.Count}",
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

            if(displayCardEvent && _currentCard != null)
                spriteBatch.DrawString(_ui.DebugText, $"Event: {_currentCard.CardEvent}", new Vector2(_currentCardPosition.X + 10, _currentCardPosition.Y + 25), Color.Black);
        }// End -----------------------------------------------------------------------------------------------------
        #endregion

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
