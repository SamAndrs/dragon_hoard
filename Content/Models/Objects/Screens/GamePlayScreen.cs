using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace dragon_hoard.Content.Models.Objects.Screens
{
    ////////////////////////////////////////////// GAME PLAY SCREEN CLASS //////////////////////////////////////
    /*

       
    :: TODO ---------------------------
        
       
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

        // GRAPHICS
        private Vector2 _mapPosition; // Position of the map on the screen



        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public GamePlayScreen(MainGame game, GameScreenManager screenManager, UiManager ui, GameSession gameSession) : base(game, screenManager, ui)
        {
            IsActive = false;

            Description = "Game Play Screen";

            RenderOnPause = false;

            _gameSession = gameSession;

            _character = _gameSession.PlayerCharacter;
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
            _mapPosition = new Vector2((_game.GraphicsDevice.Viewport.Width - 300), 50);
            _map = new GameMap(_ui, 10, 13, _mapPosition); // Example: Create a 10x13 game map
            _map.Initialize();
            
            _healthbar = new Healthbar(_ui, new Vector2(_game.CenterX, _game.CenterY - 150)); // Example position
            _healthbar.Load();
            _healthbar.Initialize(_character.Health, _gameSession.CurrentPCHealth, 20);

            IsContentLoaded = true;


            // DEBUG
            _gameSession.SetCurrentRoom(_map.MiniMap[0, 2]);
            _activeRoom = _map.MiniMap[1, 2];
            _activeRoom.SetTestValues(Enums.RoomType.Empty, true);
           _activeRoom.Initialize(_gameSession);

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
                _healthbar.Update(_gameSession.CurrentPCHealth);
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

            if (showContentLoaded)
            {
                spriteBatch.DrawString(_ui.DebugText, $"Content loaded: {IsContentLoaded}",
                   new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 100), Color.White);
            }
        }// End -----------------------------------------------------------------------------------------------------


        #endregion
    }
}
