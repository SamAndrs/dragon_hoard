using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace dragon_hoard.Content.Models.Objects.Screens
{
    //////////////////////////////////////////////// NEW GAME SCREEN CLASS /////////////////////////////////////////
    /*

        Start a new game menu.
            Child class of the GameScreen abstract class.
            Utilizes various UI class objects to display and provide functionality for 
            player options such as;
                ? Enter player name or select existing player (maybe)
                ? Select game difficulty (maybe)
                * Display of selection of player characters & assosciated information about currently
                    focused character.


    :: TODO ---------------------------
        
        * Methods for switching stored character data values depending on currently selected playercharacter,
            using buttons.
        * Method for saving new player object.
        * Method for instantiate new player character object with selected stats & data
        * Method for cancelling process and clear data
        * (?) Method for animating UI on menu load/ transition.

        v.001b (09/27) Setup of basic test UI objects.

    :: DONE ---------------------------
        (2026)
        v.001b (09/28) Setup of temporary UI elements for core functionality testing
        v.001a (09/26) Setup of basic properties and methods for core functionality.
        

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class NewGameScreen : GameScreen
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
        private PlayerCharacter _character;

        private string _pcName;

        private int[] _pcStats;                 // health, str, dex, lck, armr

        private string _equipment;              // Just use text here instead of actual objects.

        private List<string> _charGear;

        // private Item[] _rings;

        // private List<string> _pcBio;

        private GameSession _gameSession;       // Holds the current game session data, including player character and explored rooms.

        // FUNCTIONAL
        private bool _animDone;                // Flag for animation being done or not.


        // FUNCTIONAL
        private Rectangle _sidebar;
        //private Texture2D _sidebarTexture;

        private ClickButton btn_pilgrim;

        private ClickButton btn_knight;

        private ClickButton btn_viking;

        private ClickButton btn_archer;

        private ClickButton btn_startGame;
        

        // GRAPHICS
        //private Texture2D _sidebarTexture;
        private Texture2D img_pilgrim;

        private Texture2D img_knight;

        private Texture2D img_viking;

        private Texture2D img_archer;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public NewGameScreen(MainGame game, GameScreenManager screenManager, UiManager ui) : base(game, screenManager, ui)
        {
            IsActive = false;

            Description = "New Game Screen";

            RenderOnPause = false;

            //_pcStats = new int[5];

            // _rings = new int[?];

            // _pcBio = new List<string>();

            _charGear = new List<string>();
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
            int sidebarSize = _game.vpSize[0] / 4; // Calculate size of the sidebar.
            _sidebar = new Rectangle(_game.VirtualView.Width - (sidebarSize * (int)_game.ScaleX), 0, sidebarSize, _game.VirtualView.Height);

            LoadHeroImages();

            int buttonWidth = (_game.vpSize[0] - (sidebarSize +50))/4 * (int)_game.ScaleX;
            int buttonHeight = 341 * (int)_game.ScaleY;
            int row1Pos = 100;

            btn_pilgrim = new ClickButton(_ui, new Rectangle(10, row1Pos, buttonWidth, buttonHeight), true);

            btn_knight = new ClickButton(_ui, new Rectangle(btn_pilgrim.BoundingBox.Right + 10, row1Pos, buttonWidth, buttonHeight), true);

            btn_viking = new ClickButton(_ui, new Rectangle(btn_knight.BoundingBox.Right + 10, row1Pos, buttonWidth, buttonHeight), true);

            btn_archer = new ClickButton(_ui, new Rectangle(btn_viking.BoundingBox.Right + 10, row1Pos, buttonWidth, buttonHeight), true);

            btn_startGame = new ClickButton(_ui, new Rectangle(_sidebar.X + 10, 500, buttonWidth, 35), true);

            InitializeButtons();

            SetDefaultHeroStats();

            _equipment = _charGear[0];

            ScreenUi = new UiElement[] { btn_pilgrim, btn_knight, btn_viking, btn_archer, btn_startGame };

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
                btn_pilgrim.Update(gameTime);
                btn_knight.Update(gameTime);
                btn_viking.Update(gameTime);
                btn_archer.Update(gameTime);
                btn_startGame.Update(gameTime);


                if(btn_pilgrim.Clicked)
                {
                    SetHeroStats("pilgrim");
                    //btn_pilgrim.ResetButton();
                }

                if (btn_knight.Clicked)
                {
                    SetHeroStats("knight");
                    //btn_knight.ResetButton();
                }

                if (btn_viking.Clicked)
                {
                    SetHeroStats("viking");
                    //btn_viking.ResetButton();
                }

                if (btn_archer.Clicked)
                {
                    SetHeroStats("archer");
                    //btn_archer.ResetButton();
                }

                if (btn_startGame.Clicked)
                {
                    CreateNewGameSession();
                    //LoadNextScreen(_screenManager.GamePlay);
                }

                // Update the UI if there are any elements in the array
                //if (ScreenUi.Length > 0)
                //{
                //    for (int i = 0; i < ScreenUi.Length; i++)
                //    {
                //        ScreenUi[i].Update(gameTime);
                //    }
                //}
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

                int sidebarSize = _sidebar.Size.X;
                
                //Rectangle sidebar = new Rectangle(_game.VirtualView.Width - (200 * (int)_game.ScaleX),0,200, _game.VirtualView.Height);
                //spriteBatch.Draw(_ui.RectangleTexture, sidebar, Color.DarkGray);

                //Rectangle sidebar = new Rectangle(_game.VirtualView.Width - (sidebarSize * (int)_game.ScaleX), 0, sidebarSize, _game.VirtualView.Height);
                spriteBatch.Draw(_ui.RectangleTexture, _sidebar, Color.DarkGray);                

               // Draw headline
                string titleString = "CHOOSE YOUR HERO";
                Vector2 titleLength = _ui.HeadLine.MeasureString(titleString)/2;
                float titleScale = 1.5f;
                int titlePos = (_game.vpSize[0] - (sidebarSize + 20));
                spriteBatch.DrawString(_ui.HeadLine, titleString, new Vector2(titlePos/2 - (titleLength.X * titleScale), 20), Color.Red,
                    rotation: 0f, origin: Vector2.Zero, titleScale, effects: SpriteEffects.None, layerDepth: 0f);

                // Draw the UI if there are any elements in the array
                if (ScreenUi.Length > 0)
                {
                    for (int i = 0; i < ScreenUi.Length; i++)
                    {
                        ScreenUi[i].Draw(spriteBatch);
                    }
                }

                DrawHeroStats(spriteBatch);
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
            bool showIMGSize = false;

            if (showContentLoaded)
                spriteBatch.DrawString(_ui.DebugText, $"Content loaded: {IsContentLoaded}", new Vector2(_game.GraphicsDevice.Viewport.Width / 2,
                100), Color.White);


            if (showIMGSize)
                spriteBatch.DrawString(_ui.DebugText, $"Img-Size: W{btn_pilgrim.BoundingBox.Width} H{btn_pilgrim.BoundingBox.Height}", new Vector2(_game.CenterX, _game.CenterY + 100), Color.White);


        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                Load Hero Images				   
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadHeroImages()
        {
            img_pilgrim = _game.Content.Load<Texture2D>("Images/Characters/img_pilgrim");
            img_knight = _game.Content.Load<Texture2D>("Images/Characters/img_knight");
            img_viking = _game.Content.Load<Texture2D>("Images/Characters/img_viking");
            img_archer = _game.Content.Load<Texture2D>("Images/Characters/img_archer");
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                 Setup Hero Buttons				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method for initializing all the hero selection buttons displayed on screen.
        ----------------------------------------------------------------------------------------------------------*/
        private void InitializeButtons()
        {          
            btn_pilgrim.Initialize(Color.Brown, "", Color.White, Vector2.Zero, true, 0);
            btn_pilgrim.Background = img_pilgrim;

            btn_knight.Initialize(Color.Gray, "", Color.Black, Vector2.Zero, true, 0);
            btn_knight.Background = img_knight;

            btn_viking.Initialize(Color.Yellow, "", Color.Black, Vector2.Zero, true, 0);
            btn_viking.Background = img_viking;

            btn_archer.Initialize(Color.Blue, "", Color.White, Vector2.Zero, true, 0);
            btn_archer.Background = img_archer;

            btn_startGame.Initialize(Color.LimeGreen, "Start Game", Color.Black, new Vector2(btn_startGame.BoundingBox.Left + 25,
                btn_startGame.BoundingBox.Top + 10), true, 0);
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                 Set Hero Stats				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method for switching the hero stats displayed in the info panel, depending on
            currently choosen hero.
        ----------------------------------------------------------------------------------------------------------*/
        private void SetHeroStats(string heroName)
        {
            switch (heroName)
            {
                case "pilgrim":
                    _pcName = "the Pilgrim";
                    _pcStats = new int[5] { 15, 4, 7, 8, 4 }; // health, str, dex, lck, armr
                    _equipment = _charGear[0];
                    btn_pilgrim.ResetButton();
                    break;

                case "knight":
                    _pcName = "the Knight";
                    _pcStats = new int[5] { 17, 6, 4, 4, 9 };
                    _equipment = _charGear[1];
                    btn_knight.ResetButton();
                    break;

                case "viking":
                    _pcName = "the Viking";
                    _pcStats = new int[5] { 16, 7, 5, 5, 6 };
                    _equipment = _charGear[2];
                    btn_viking.ResetButton();
                    break;

                case "archer":
                    _pcName = "the Archer";
                    _pcStats = new int[5] { 11, 3, 8, 7, 5 };
                    _equipment = _charGear[3];
                    btn_archer.ResetButton();
                    break;


               default: // Pilgrim is set as default
                    _pcName = "the Pilgrim";
                    _pcStats = new int[5] { 15, 4, 7, 8, 4 }; // health, str, dex, lck, armr
                    _equipment = _charGear[0];
                    break;

            }
        }// End -----------------------------------------------------------------------------------------------------


         /*----------------------------------------------------------------------------------------------------------
                                                 Set DefaultHero Stats				   
        ----------------------------------------------------------------------------------------------------------*/
        private void SetDefaultHeroStats()
        {
            _pcName = "the Pilgrim";
            _pcStats = new int[5] { 15, 4, 7, 8, 4 };   // health, str, dex, lck, armr

            _charGear.AddRange(new List<string>
            {
                "Morningstar, quarterstaff",
                "Longsword, shield",
                "Battleaxe, roundshield",
                "Crossbow, shortsword, \nbuckler"
            });
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                Draw Hero Stats				   
        ----------------------------------------------------------------------------------------------------------*/
        private void DrawHeroStats(SpriteBatch spriteBatch)
        {

            Rectangle sideWindow = new Rectangle(_sidebar.X + 10, _sidebar.Y + 50, 180 * (int)_game.ScaleX, 300 * (int)_game.ScaleY);
            spriteBatch.Draw(_ui.RectangleTexture, sideWindow, Color.DarkBlue);

            // Name
            Vector2 nameLength = _ui.DebugText.MeasureString(_pcName) / 2;
            spriteBatch.DrawString(_ui.DebugText, _pcName, new Vector2(sideWindow.Center.X - nameLength.X, sideWindow.Y + 5), Color.White);

            // Health
            float xPos1 = sideWindow.X + 15;
            float xPos2 = xPos1 + 130;
            spriteBatch.DrawString(_ui.DebugText, "Health:", new Vector2(xPos1, sideWindow.Y + 105), Color.White);
            spriteBatch.DrawString(_ui.DebugText, $"{_pcStats[0]}", new Vector2(xPos2, sideWindow.Y + 105), Color.White);
            // strength
            spriteBatch.DrawString(_ui.DebugText, "Strength:", new Vector2(xPos1, sideWindow.Y + 145), Color.White);
            spriteBatch.DrawString(_ui.DebugText, $"{_pcStats[1]}", new Vector2(xPos2, sideWindow.Y + 145), Color.White);
            // Dexterity
            spriteBatch.DrawString(_ui.DebugText, "Dexterity:", new Vector2(xPos1, sideWindow.Y + 165), Color.White);
            spriteBatch.DrawString(_ui.DebugText, $"{_pcStats[2]}", new Vector2(xPos2, sideWindow.Y + 165), Color.White);
            // Luck
            spriteBatch.DrawString(_ui.DebugText, "Luck:", new Vector2(xPos1, sideWindow.Y + 185), Color.White);
            spriteBatch.DrawString(_ui.DebugText, $"{_pcStats[4]}", new Vector2(xPos2, sideWindow.Y + 185), Color.White);
            // Armor
            spriteBatch.DrawString(_ui.DebugText, "Armor:", new Vector2(xPos1, sideWindow.Y + 205), Color.White);
            spriteBatch.DrawString(_ui.DebugText, $"{_pcStats[3]}", new Vector2(xPos2, sideWindow.Y + 205), Color.White);
            // Equipment
            spriteBatch.DrawString(_ui.DebugText, "Equipment:", new Vector2(xPos1, sideWindow.Y + 235), Color.White);
            spriteBatch.DrawString(_ui.DebugText, _equipment, new Vector2(xPos1, sideWindow.Y + 255), Color.White);
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                               Create New Game Session				   
        ------------------------------------------------------------------------------------------------------------
        ------------------------------------------------------------------------------------------------------------
          - Class specific method for saving the new player character data and creating a new game session 
            with the selected character.
        ----------------------------------------------------------------------------------------------------------*/
        private void CreateNewGameSession()
        {
            // Create a new game session
            _game.StartGameSession();
            _gameSession = _game.GameSession;
            // Create new character based on the choosen one.
            _character = new PlayerCharacter(_pcName, _pcStats[0], _pcStats[1], _pcStats[2], _pcStats[3], _pcStats[4]);
            
            // DEBUG
            _character.CurrentHealth = _character.MaxHealth - 2;

            // Assign the player character to the game session and initialize the session.
            _gameSession.Initialize(_character);

            LoadNextScreen(_screenManager.GamePlay);


        }// End -----------------------------------------------------------------------------------------------------

        #endregion
       
    }
}
