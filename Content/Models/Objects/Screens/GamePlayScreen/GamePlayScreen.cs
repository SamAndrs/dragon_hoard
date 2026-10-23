using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen
{
    ////////////////////////////////////////////// GAME PLAY SCREEN CLASS //////////////////////////////////////
    /*

       
    :: TODO ---------------------------
        * Account for RoomTile events preventing movement (traps, etc)
        * Account for RoomCard events preventing movement (combat, traps, special etc.) 
        * Implement combat button feedback
        * Implement room action feedback

        v.001d (10/22) Rework of UI into sub-classes.
        
       
    :: DONE ---------------------------
        (2026)
        v.001c (10/20) Addition of several UI buttons, features and assosciated methods.
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

        private RoomTile _currentRoom;                          // Reference to the current RoomTile pulled from the stack.

        private RoomCard _currentCard;                          // Reference to the current RoomCard pulled from the stack.

        private MiniMap _miniMap;

        private Healthbar _healthbar;

        private SunDial _sunDial;                               // Visual representation of total number of gameturns and the current one.

        private RoomImage _imageDisplay;                        // Displays an image representing the current RoomTile.

        private MovementButtons _movementButtons;

        private FlavorMessageDisplay _flavorMessageDisplay;     // Displays a random flavor message at the top of the screen.

        private EventMessageDisplay _eventMessageDisplay;       // Displays a message related to the current RoomTile or RoomCard.

        private RoomActionsBar _roomActions;                    // Options related to searching a room open a chest etc.

        private CombatOptions _combatOptions;                   // Player choice of fight or flee at start of any combat.

        private CombatButtonsBar _combatButtonsBar;             // Player choice of using attack A, B or C for the combat round.

        private OptionsBar _optionsbar;                         // Open gamemap, audio or video menues etc.

        private CharacterStatDisplay _characterStats;

        // FUNCTIONALITY

        private Point _miniMapPosition;

        private Point _healthBarPosition;

        private Point _sunDialPosition;                   

        private Point _flavorMessagePosition;

        private Point _eventMessagePosition;

        private Point _imageDisplayPosition;

        private Point _moveButtonPosition;

        private Point _attackButtonPosition;

        private Point _combatOptPosition;

        private Point _roomActsPosition;

        private Point _charStatsPosition;

        private Point _optMenuPosition;
   
        
        // GRAPHICS

       




        // DEBUG

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public GamePlayScreen(MainGame game, GameScreenManager screenManager, UiManager ui) : base(game, screenManager, ui)
        {
            IsActive = false;

            Description = "Game Play Screen";

            RenderOnPause = false;

            LoadTextures();
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

            _miniMap = new MiniMap(_ui, _gameSession, _gameSession.GameMap);
            _miniMapPosition = new Point((_game.GraphicsDevice.Viewport.Width - 300), 240);
            _miniMap.Load(_miniMapPosition);

            _currentRoom = _gameSession.CurrentRoom;

            _imageDisplay = _gameSession.ImageDisplay;

            _movementButtons = new MovementButtons(_game, _ui, _gameSession);
            _moveButtonPosition = new Point((int)_game.CenterX, (int)_game.CenterY + 250);
            _movementButtons.Load(_moveButtonPosition);
          
            _healthBarPosition = new Point((int)_game.CenterX - 75, (int)_game.CenterY + 150);
            _healthbar = new Healthbar(_ui, _healthBarPosition);
            _healthbar.Load(_character.MaxHealth, _character.CurrentHealth, 20);

            _sunDialPosition = new Point((int)_game.CenterX - 486, 35);
            _sunDial = new SunDial(_gameSession, _ui.SunIcon, _ui.SunSkull);
            _sunDial.Load(_sunDialPosition);

            _flavorMessageDisplay = new FlavorMessageDisplay(_gameSession, _ui.HeadLine);
            _flavorMessagePosition = new Point((int)_game.CenterX, _sunDialPosition.Y);
            _flavorMessageDisplay.Load(_flavorMessagePosition, _gameSession.Randomizer);

            _optionsbar = new OptionsBar(_game, _ui, _gameSession);
            _optMenuPosition = new Point(_miniMapPosition.X, _miniMapPosition.Y - 80);
            _optionsbar.Load(_optMenuPosition, 50);

            _characterStats = new CharacterStatDisplay(_ui, _gameSession);
            _charStatsPosition = new Point(_miniMapPosition.X, _miniMapPosition.Y + 300);
            _characterStats.Load(_character, _charStatsPosition, 50);


            _eventMessageDisplay = new EventMessageDisplay(_ui, _gameSession, _ui.Text);
            _eventMessagePosition = new Point((int)_game.CenterX + 50, 150);
            _eventMessageDisplay.Load(_eventMessagePosition);

            _imageDisplayPosition = new Point(150, 120);
            _imageDisplay.Initialize(_imageDisplayPosition);

            _combatOptions = new CombatOptions(_game, _ui, _gameSession);
            _combatOptPosition = new Point(_imageDisplayPosition.X + _imageDisplay.Width/ 2, (int)_imageDisplayPosition.Y + (_imageDisplay.Height/2 - 25));
            _combatOptions.Load(_combatOptPosition, _ui.Text);


            _combatButtonsBar = new CombatButtonsBar(_game, _ui, _gameSession);
            _attackButtonPosition = new Point(325, 600);
            _combatButtonsBar.Load(_attackButtonPosition, _ui.Text);

            _roomActsPosition = new Point(150, 600); // DEBUG // TODO
            _roomActions = new RoomActionsBar(_ui, _gameSession);
            _roomActions.Load(_roomActsPosition);

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

                if(_gameSession.CurrentRoomCard != null)
                    _currentCard = _gameSession.CurrentRoomCard;

                _miniMap.Update(gameTime);

                _flavorMessageDisplay.Update(gameTime);

                _eventMessageDisplay.Update(gameTime);

                _sunDial.Update(gameTime);

                _healthbar.Update(_character.CurrentHealth);
                
                _movementButtons.Update(gameTime);
                UpdateMoveButtons(_gameSession.PlayerCanMove);

                _roomActions.Update(gameTime);

                _combatButtonsBar.Update(gameTime);

                _combatOptions.Update(gameTime);

                _optionsbar.Update(gameTime);

                _characterStats.Update(gameTime);
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

                _miniMap.Draw(spriteBatch);

                _roomActions.Draw(spriteBatch);

                _healthbar.Draw(spriteBatch);
               
                _movementButtons.Draw(spriteBatch);

                _combatButtonsBar.Draw(spriteBatch);

                _imageDisplay.Draw(spriteBatch);

                _combatOptions.Draw(spriteBatch);

                _flavorMessageDisplay.Draw(spriteBatch);

                _eventMessageDisplay.Draw(spriteBatch);

                _optionsbar.Draw(spriteBatch);

                _characterStats.Draw(spriteBatch);
            }

            base.Draw(spriteBatch);
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                Player Exit Room					   
        ------------------------------------------------------------------------------------------------------------
         - Class specific support method. Assigns the direction which the player leaves the current room, and then 
            callbacks to the game session method for going through the events of a turn.
        ----------------------------------------------------------------------------------------------------------*/
        private void PlayerExitRoom(Direction direction)
        {
            _gameSession.MoveDirection = direction;
            _gameSession.SetPlayerCanMove(false);
            _gameSession.DiscardCurrentRoomCard(); //TODO implement check. Certain cards should not be dicarded upon moving.
            
            // Reset flags           
            if(_currentCard != null)
                _currentCard.ResetStats();

            // Progress to next round
            _gameSession.CreateTestRound();
            
        }// End -----------------------------------------------------------------------------------------------------
        

        /*----------------------------------------------------------------------------------------------------------
                                              Update Move Buttons
        ------------------------------------------------------------------------------------------------------------
            - Class support method for looping through and checking if any of the Ui movement buttons have been clicked.
              If so, the exit room event is to be fired.
        ----------------------------------------------------------------------------------------------------------*/
        private void UpdateMoveButtons(bool playerCanExit)
        {
            if (playerCanExit)
            {
                if (_movementButtons.BtnGoUp.Clicked) PlayerExitRoom(Direction.Up);

                if (_movementButtons.BtnGoDown.Clicked) PlayerExitRoom(Direction.Down);

                if (_movementButtons.BtnGoLeft.Clicked) PlayerExitRoom(Direction.Left);

                if (_movementButtons.BtnGoRight.Clicked) PlayerExitRoom(Direction.Right);
            }
        }// End -----------------------------------------------------------------------------------------------------

       

        /*----------------------------------------------------------------------------------------------------------
                                               Load Textures				   
        ------------------------------------------------------------------------------------------------------------
         - Class specific support method for loading all the texture content related to the GamePlayScreen
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadTextures()
        {

           
                       
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

            bool showAllPositions = true;

            bool showMovementData = false;

            bool showTilesInStack = false;

            //bool mapNumbers = false;

            bool displayCardData = false;

            bool displayRoomTileData = false;



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

            if (showAllPositions)
            {
                Point[] positions = {_miniMapPosition, _healthBarPosition, _sunDialPosition, _flavorMessagePosition, _eventMessagePosition, _imageDisplayPosition, 
                    _moveButtonPosition, _attackButtonPosition, _combatOptPosition, _roomActsPosition, _charStatsPosition, _optMenuPosition};

                for (int i = 0; i < positions.Length; i++)
                {
                    Rectangle point = new Rectangle(positions[i].X, positions[i].Y, 5, 5);
                    spriteBatch.Draw(_ui.RectangleTexture, point, Color.LimeGreen);
                }
            }

            if (showMovementData)
            {
                spriteBatch.DrawString(_ui.DebugText, $"MoveDirection: {_gameSession.MoveDirection}", new Vector2(180, 80), Color.LightCyan);

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

            //if (mapNumbers)
            //{
            //    for (int row = 0; row < _miniMap.Rows; row++)
            //        spriteBatch.DrawString(_ui.DebugText, $"{row}", new Vector2(_map.MapPosition.X - 25, _map.MapPosition.Y + row * 18), Color.LightBlue);

            //    for (int col = 0; col < _map.Columns; col++)
            //        spriteBatch.DrawString(_ui.DebugText, $"{col}", new Vector2(_map.MapPosition.X + col * 18, _map.MapPosition.Y - 25), Color.LightBlue);
            //}

            if (displayRoomTileData)
                _currentRoom.DisplayRoomTileData(spriteBatch, new Point(400, 100), _ui.RectangleTexture, _ui.DebugText);

            if (displayCardData && _currentCard != null)
                _currentCard.DisplayCardData(spriteBatch, new Point(700, 100), _ui.RectangleTexture, _ui.DebugText);

            {

            }
        }// End -----------------------------------------------------------------------------------------------------
        #endregion
    }
}
