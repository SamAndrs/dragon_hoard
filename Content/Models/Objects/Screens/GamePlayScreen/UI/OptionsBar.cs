using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI
{
    //////////////////////////////////////////// OPTIONSBAR CLASS //////////////////////////////////////////////////
    /*
            Child UI class to the GamePlayScreen. Handles everything related to the game options bar
            and the menues assosciated with it.
        

    :: TODO ---------------------------   
        

    :: DONE ---------------------------
        v.001b (10/31) Added textures to buttons, and callback functionality on buttonpresses.
        v.001a (10/22) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public class OptionsBar
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        private MainGame _game;

        private UiManager _ui;

        private GameSession _session;


        // FUNCTIONAL
        private ClickButton _btnMap;

       
        private ClickButton _btnSettings;
               
        private ClickButton _btnSave;
        
        private ClickButton _btnQuit;


        // GRAPHICS
        private Point _position;

        private int _buttonSize;

        private Texture2D _uiTextures;                      // Reference to texture of UI icon/ button images

        private Rectangle _optMapSource;                    // Snapshot rectangle of UiTexture for the button image. 

        private Rectangle _optSettingsSource;

        private Rectangle _optSaveSource;

        private Rectangle _optQuitSource;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public OptionsBar(MainGame game, UiManager ui, GameSession session, Texture2D uiTexture)
        {
            _game = game;

            _ui = ui;

            _session = session;

            _uiTextures = uiTexture;
        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                      Load					   
        ------------------------------------------------------------------------------------------------------------
         - Class method for loading and initiating the options bar specific UI content and assosciated resources.
        ----------------------------------------------------------------------------------------------------------*/
        public void Load(Point position, int buttonSize)
        {
            _position = position;

            _buttonSize = buttonSize;

            LoadTextures();

            LoadButtons(position, _buttonSize);

        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                      Update					   
        ------------------------------------------------------------------------------------------------------------
         - Class method for updating the optionsbar buttons
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            _btnMap.Update(gameTime);
                                   
            _btnSettings.Update(gameTime);
            
            _btnSave.Update(gameTime);

            _btnQuit.Update(gameTime);

            if (_btnMap.Clicked)
                _game.GameScreenManager.GamePlay.OpenGameMapScreen();   // TODO     Switch to GameMapScreen.

            if (_btnSettings.Clicked)
                _game.GameScreenManager.GamePlay.OpenSettingsScreen();  // TODO     Open Settings menu

            if (_btnSave.Clicked)
                _game.GameScreenManager.GamePlay.OpenSaveGameScreen();  // TODO     Conditional: Save possible. Savegame menu.

            if (_btnQuit.Clicked)
                _game.GameScreenManager.GamePlay.OpenQuitPrompt();      // TODO     Quit prompt. Get back to main menu
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Draw					   
        ------------------------------------------------------------------------------------------------------------
         - Class method for drawing the class specific content on screen.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            _btnMap.Draw(spriteBatch, _optMapSource);
                      
            _btnSettings.Draw(spriteBatch, _optSettingsSource);
            
            _btnSave.Draw(spriteBatch, _optSaveSource);

            _btnQuit.Draw(spriteBatch, _optQuitSource);
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                      Load Textures					   
        ------------------------------------------------------------------------------------------------------------
         - Class specific support method for loading the textures for the buttons.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadTextures()
        {
            _optMapSource = new Rectangle(340, 4, _buttonSize, _buttonSize);

            _optSettingsSource = new Rectangle(402, 4, _buttonSize, _buttonSize);

            _optSaveSource = new Rectangle(464, 4, _buttonSize, _buttonSize);

            _optQuitSource = new Rectangle(526, 4, _buttonSize, _buttonSize);


        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                Load Buttons
        ------------------------------------------------------------------------------------------------------------
          - Class specific support method for creating and initiating the buttons of the options menu bar.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadButtons(Point position, int btnSize)
        {
            _btnMap = new ClickButton(_ui, new Rectangle(position.X, position.Y, btnSize, btnSize), true);
            _btnMap.Initialize(Color.White, "", null, Color.Black, Vector2.Zero, true, 0);
            _btnMap.Background = _uiTextures;

            float settingsX = position.X + (btnSize + 5) * 1;
            _btnSettings = new ClickButton(_ui, new Rectangle((int)settingsX, position.Y, btnSize, btnSize), true);
            _btnSettings.Initialize(Color.White, "", null, Color.Black, Vector2.Zero, true, 0);
            _btnSettings.Background = _uiTextures;

            float saveX = position.X + (btnSize + 5) * 2;
            _btnSave = new ClickButton(_ui, new Rectangle((int)saveX, position.Y, btnSize, btnSize), true);
            _btnSave.Initialize(Color.White, "", null, Color.Black, Vector2.Zero, true, 0);
            _btnSave.Background = _uiTextures;

            float quitX = position.X + (btnSize + 5) * 3;
            _btnQuit = new ClickButton(_ui, new Rectangle((int)quitX, position.Y, btnSize, btnSize), true);
            _btnQuit.Initialize(Color.White, "", null, Color.Black, Vector2.Zero, true, 0);
            _btnQuit.Background = _uiTextures;
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
