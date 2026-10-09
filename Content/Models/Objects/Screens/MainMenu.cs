using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace dragon_hoard.Content.Models.Objects.Screens
{
    //////////////////////////////////////////////// MAIN MENU SCREEN CLASS /////////////////////////////////////////
    /*

        Classical Title (first) screen with start Menu of the game.
            Child class of the GameScreen abstract class.
            Utilizes various UI class objects to display the game title, game title image (dragon & hoard) 
            and other atmospheric content, as well as provide functionality for 
            player options selection such as:
                * Start new game
                * Load saved game
                * Game settings
                * About (game information etc).
                * Display game version.

    :: TODO ---------------------------
        
        * Animations for button appearances (?).
        (09/30) Removal of SplashScreen and transfer of content to here.

    :: DONE ---------------------------
        (2026) 
        v.001b (09/ 27) Setup test options for 'Start New Game', 'Game Settings', 'About' with respective transitions using
            test UI objects w. text and click functionality.
        v.001a (09/26) Basic setup of derived class methods and properties. Simple test of screen functionality
            and screen transitions.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class MainMenu : GameScreen
    {
        //========================================== PROPERTIES ==================================================//

        // GRAPHICS
        private Rectangle _logoBox;

        private Texture2D _logo;

        private Rectangle _imageBox;

        private Texture2D _image;

        // FUNCTIONALITY
        private ClickButton btn_newGame;

        private ClickButton btn_loadSaved;

        private ClickButton btn_settings;

        private bool _menuVisible;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public MainMenu(MainGame game, GameScreenManager screenManager, UiManager ui) : base(game, screenManager, ui)
        {
            IsActive = false;

            Description = "Main Menu";

            RenderOnPause = false;
        }


        //============================================ METHODS ===================================================//
        #region methods



        /*----------------------------------------------------------------------------------------------------------
                                                   Load Content					   
        ------------------------------------------------------------------------------------------------------------
            - Override of derived method. Loads the screen specific content and places the UI elements into 
                the array.
        ----------------------------------------------------------------------------------------------------------*/
        public override void LoadContent()
        {
            _menuVisible = false;

            int screenWidth = _game.GraphicsDevice.Viewport.Width;
            int screenHeight = _game.GraphicsDevice.Viewport.Height;
            

            _logo = _game.Content.Load<Texture2D>("Images/dragonhoard_title");
            //_logoBox = new Rectangle((int)_game.CenterX - (int)_logo.Width / 2, 25, _logo.Width, _logo.Height);
            //_logoBox = new Rectangle((int) _game.CenterX - (int)_logo.Width / 2, 25, screenWidth/2, screenHeight/4);
            Vector2 logoPosition = new Vector2(_game.CenterX - _logo.Width / 2, 25);
            _logoBox = new Rectangle((int)logoPosition.X, (int)logoPosition.Y, 727 * (int)_game.ScaleX, 133 * (int)_game.ScaleY);

            _image = _game.Content.Load<Texture2D>("Images/dragon_title_img");
            //_imageBox = new Rectangle((int)_game.CenterX - (int)_image.Width / 2, 50, 800, 600);
            _imageBox = new Rectangle(1, 25, screenWidth, screenHeight);


            LoadButtonContent();

            InitializeButtons();

            ScreenUi = new UiElement[] {btn_newGame, btn_loadSaved, btn_settings};
            
            IsContentLoaded = true;
            

        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                   Update				   
        ------------------------------------------------------------------------------------------------------------
            - Override of derived method. Updates the screen specific content, including UI elements, provided the
                splash screen is set to 'Active'.
        ----------------------------------------------------------------------------------------------------------*/
        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                // Display menu buttons only if 'flag' is on
                if(_menuVisible)
                {
                    // DEBUG -----------------------------------------------------------------------------
                    if (btn_newGame.Clicked)
                    {
                        LoadNextScreen(_screenManager.NewGame);
                        btn_newGame.ResetButton();
                    }

                    if (btn_loadSaved.Clicked)
                    {
                        //LoadNextScreen(/* Load saved game menu */);
                        btn_loadSaved.ResetButton();
                    }

                    if (btn_settings.Clicked)
                    {
                        //LoadNextScreen(/* Load settings menu */);
                        btn_settings.ResetButton();
                    }

                    // ----------------------------------------------------------------------------------
                }
                else
                {
                    // If player clicks LMB or Enter, set menu visible 'flag' on.
                    if (_ui.keyboard.IsKeyDown(Keys.Enter) || _ui.Mouse.LeftButton == ButtonState.Pressed)
                    {
                        _menuVisible = true;
                    }
                }
               


                // Update the UI if there are any elements in the array

                if (ScreenUi.Length > 0)
                {
                    for (int i = 0; i < ScreenUi.Length; i++)
                    {
                        ScreenUi[i].Update(gameTime);
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
            
            if(IsActive)
            {
                //_game.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(_image, _imageBox, Color.White);
                spriteBatch.Draw(_logo, _logoBox, Color.White);

                spriteBatch.DrawString(_ui.DebugText, _game.GameVersion, new Vector2(_game.GraphicsDevice.Viewport.Width - 100,
                    10), Color.Yellow);


                // Draw menu buttons on screen when player clicks LMB or Enter.
                if(_menuVisible)
                {
                    // Draw the UI if there are any elements in the array
                    if (ScreenUi.Length > 0)
                    {
                        for (int i = 0; i < ScreenUi.Length; i++)
                        {
                            ScreenUi[i].Draw(spriteBatch);
                        }
                    }
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

            if(showContentLoaded)
                spriteBatch.DrawString(_ui.DebugText, $"Content loaded: {IsContentLoaded}",
                    new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 100), Color.White);



        }// End -----------------------------------------------------------------------------------------------------


        private void LoadButtonContent()
        {
            //Vector2 menuPosition = new Vector2(_game.VirtualWidth, _game.VirtualHeight/3);

            Vector2 menuPosition = new Vector2(_game.VirtualView.Width, _game.VirtualView.Height/2);

            int newGameBottom;  // Reference value for bottom of new game button

            int savedGameBottom; // Reference value for bottom of save game button

            int buttonWidth = 200 * (int)_game.ScaleX;
            int buttonHeight = 35 * (int)_game.ScaleY;

            //btn_newGame = new ClickButton(_ui, new Rectangle((int)menuPosition.X - (buttonWidth +25), 200, buttonWidth, 35), true);
            //newGameBottom = btn_newGame.BoundingBox.Bottom;

            btn_newGame = new ClickButton(_ui, new Rectangle((int)menuPosition.X - (buttonWidth + 25), (int)menuPosition.Y, buttonWidth, buttonHeight), true);
            newGameBottom = btn_newGame.BoundingBox.Bottom;

            btn_loadSaved = new ClickButton(_ui, new Rectangle((int)menuPosition.X - (buttonWidth + 25), newGameBottom + 15, buttonWidth, buttonHeight), true);
            savedGameBottom = btn_loadSaved.BoundingBox.Bottom;

            btn_settings = new ClickButton(_ui, new Rectangle((int)menuPosition.X - (buttonWidth + 25), savedGameBottom + 15, buttonWidth, buttonHeight), true);
        }

        private void InitializeButtons()
        {
            string newGameTxt = "START NEW GAME";

            string savedGameTxt = "LOAD SAVED GAME";

            string settingsTxt = "SETTINGS";

            btn_newGame.Initialize(Color.LimeGreen, newGameTxt, Color.White, new Vector2(btn_newGame.BoundingBox.X + 40,
               btn_newGame.BoundingBox.Y + 8), true, 0);


            btn_loadSaved.Initialize(Color.Yellow, savedGameTxt, Color.Black, new Vector2(btn_loadSaved.BoundingBox.X + 40,
                btn_loadSaved.BoundingBox.Y + 8), true, 0);

            btn_settings.Initialize(Color.Orange, settingsTxt, Color.Black, new Vector2(btn_settings.BoundingBox.X + 70,
                btn_settings.BoundingBox.Y + 8), true, 0);
        }

        #endregion
    }
}
