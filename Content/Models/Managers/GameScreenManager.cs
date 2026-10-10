using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace dragon_hoard.Content.Models.Managers
{
    //////////////////////////////////////////// GAME SCREEN MANAGER CLASS /////////////////////////////////////////
    /*
        Supervisory Game State manager.
        * Handles the transition between the game's different menues/ screens and states such as 'pause'.
        * Utilizes a stack to Push or Pop a screen from the top
        * Delegates current Game Screen to set itself active/ inactive.
        * Delegates current Game Screen to load-/ unload the assosciated content resources.        

    :: TODO ---------------------------

        v.001b Declaration of next set of screens:
            - Start new game screen
            - Load game screen
            - Game settings screen
            - About game screen (ev. turn into pop-up info box)
            - Fake loadscreen (black screen w. 1.s countdown before transition)
            


         * Declaration of next set of screens:
            - Character select screen
            - Gameplay screen
            - Pause screen
            - OnDeath screen
            - OnWin screen

        * Game Screen content unloading/ removal.
      */
    #region DONE
    /*
    :: DONE ---------------------------

        (2026)
         v.001a (09/24) Core class setup. Initialization of first Game Screen and basic Stack functionality and  
            subclass-delegation.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    public class GameScreenManager
    {
        //========================================== PROPERTIES ==================================================//

        // MAIN
        private MainGame _game;

        private bool _debugMode;

        private UiManager _ui;

        // TRANSITION LOGIC
        private readonly Stack<GameScreen> _gameScreens;
        
        private GameScreen _nextScreen;
        
        public bool ScreenTransition { get; set; } = false;

        private float countDown = 1.0f;


        // GAME SCREENS       
        public MainMenu MainMenu { get; private set; }
        
        public NewGameScreen NewGame { get; private set; }

        public GamePlayScreen GamePlay { get; private set; }


        // DEBUG
        public List<string> ScreensInStack { get; private set; }

        private string _timer = string.Empty;


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public GameScreenManager(MainGame game, UiManager ui)
        {
            _game = game;
            _ui = ui;
            _gameScreens = new Stack<GameScreen>();
            
            ScreensInStack = new List<string>();
        }


        //============================================ METHODS ===================================================//
        #region methods

        /*----------------------------------------------------------------------------------------------------------
                                                   Initialize					   
        ------------------------------------------------------------------------------------------------------------
            - Class specific method for initializing the first screen of the game; the Splash Screen and
            push it onto the stack, load its assosciated contents (including UI) and set its state to 'Active'. 
        ----------------------------------------------------------------------------------------------------------*/
        public void Initialize()
        {
            // Initialize screens
            //SplashScreen = new SplashScreen(_game, this, _ui);
            
            MainMenu = new MainMenu(_game, this, _ui);

            NewGame = new NewGameScreen(_game, this, _ui);

            GamePlay = new GamePlayScreen(_game, this, _ui);

            // Push Splashscreen onto stack, load content & set to active
            //_gameScreens.Push(MainMenu);

            //MainMenu.LoadContent();

            //MainMenu.SetActive();

            //ScreensInStack.Add(MainMenu.Description);




            // NEWGAME DEBUG
            _gameScreens.Push(NewGame);
            NewGame.LoadContent();
            NewGame.SetActive();
            ScreensInStack.Add(NewGame.Description);

            // GAMEPLAY DEBUG
            //_gameScreens.Push(GamePlay);
            //GamePlay.LoadContent();
            //GamePlay.SetActive();
            //ScreensInStack.Add(GamePlay.Description);



        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                   Update					   
        ------------------------------------------------------------------------------------------------------------
            - Class specific method of delegating the Game Screen object currently at top 
                of the stack to update their respective assosciated contents, at runtime.
              
              Also handles Game Screen transition logic.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            _debugMode = _game.DebugMode; // Check main game debug property.

            _ui.Update(gameTime);

            // When a screen tranition is called, run "load screen" process.
            if (ScreenTransition)
                countDown = DoScreenTransition(_nextScreen, gameTime, countDown);
            else
                _gameScreens.Peek().Update(gameTime);


        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                   Draw					   
        ------------------------------------------------------------------------------------------------------------
            - Class specific method of delegating the UI Manager and Game Screen object currently at top 
                of the stack to draw their respective assosciated contents, at runtime.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            if(ScreenTransition)
                _game.GraphicsDevice.Clear(Color.Black);

            else
            {
                //foreach(var screen in _gameScreens)
                //{
                //    if(screen.RenderOnPause)
                //        screen.Draw(spriteBatch);
                //}
                
                _gameScreens.Peek().Draw(spriteBatch);
            }

            if (_debugMode)
                DebugGame(spriteBatch);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                   Load Screen					   
        ------------------------------------------------------------------------------------------------------------
            - Class specific method called as part of the screen transition process.
                Loads a new Game Screen object onto the stack.
                Any previous screen will be set to 'Inactive'.
                The new Game Screen object is pushed to the top and its content loaded (if not already). 
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadScreen(GameScreen newScreen)
        {

            if (_gameScreens.Count > 0)
            {
                _gameScreens.Peek().SetInactive();
            }
            
            _gameScreens.Push(newScreen);
            
            // If the screen content has been removed or not yet been loaded into memory, run load method.
            if(!newScreen.IsContentLoaded)
            {
                newScreen.LoadContent();
            }
            
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                  Do Screen Transition					   
       ------------------------------------------------------------------------------------------------------------
           - Class specific method to transition between different game screens.
                A countdown is utilized to give impression of a loading sequence and to prevent
                'instant screen swap'.  
       ----------------------------------------------------------------------------------------------------------*/
        private float DoScreenTransition(GameScreen nextScreen, GameTime gameTime, float countdown)
        {
            LoadScreen(nextScreen);    // Run method for loading next screen

            if (countdown > 0)      // Countdown to give impression of loading screen
            {
                countdown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                _timer = countdown.ToString("n2");
                return countdown;
            }
            else
            {
                ScreenTransition = false; // End transition state

                _nextScreen.SetActive(); // Activate new screen

                
                ScreensInStack.Add(_nextScreen.Description); // Add some debug info.

                // Reset and return timer value.
                countdown = 1.0f;
                return countdown;
            }
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                   Close Screen					   
        ------------------------------------------------------------------------------------------------------------
            - Class specific method of closing a Game Screen fully, meaning all its UI elements and
            other assosciated contents will be removed from the cache.
        ----------------------------------------------------------------------------------------------------------*/
        public void CloseScreen()
        {
            if (ScreensInStack.Count > 0)
            {
                //_gameScreens.Peek().UnloadContent();
                _gameScreens.Peek().IsContentLoaded = false; // Set back to 'content not loaded'
                _gameScreens.Pop().SetInactive();
                
                ScreensInStack.RemoveAt(ScreensInStack.Count - 1); // DEBUG

            }
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                  Set Next Screen					   
        ------------------------------------------------------------------------------------------------------------
           - Callback method for Game Screen objects to inform ScreenManager which screen to transition to.
        ----------------------------------------------------------------------------------------------------------*/
        public void SetNextScreen(GameScreen nextScreen) => _nextScreen = nextScreen;



        /*----------------------------------------------------------------------------------------------------------
                                                  Debug Game				   
        ------------------------------------------------------------------------------------------------------------
           - Main game debugging method. Displays Game specific debugging information in the game window.
        ----------------------------------------------------------------------------------------------------------*/
        private void DebugGame(SpriteBatch spriteBatch)
        {
            //_game.SetDebugMode();
            bool showTransitionTimer = false;
            bool showStackContents = false;

            if(showTransitionTimer)
                spriteBatch.DrawString(_ui.DebugText, $"Transition in: {_timer}s", new Vector2(100, 10), Color.White);

            if (showStackContents)
            {
                Rectangle stackBG = new Rectangle(_game.GraphicsDevice.Viewport.Width - 150, 50, 130, 200);
                spriteBatch.Draw(_ui.RectangleTexture, stackBG, Color.DarkBlue);
                spriteBatch.DrawString(_ui.DebugText, "Screens in stack:", new Vector2(stackBG.X + 10, stackBG.Y + 5), Color.Yellow);
                for (int i = 0; i < ScreensInStack.Count; i++)
                {
                    spriteBatch.DrawString(_ui.DebugText, $"{ScreensInStack[i]}", new Vector2(stackBG.X + 10, stackBG.Y + 25 + (i * 20)), Color.White);
                }
            }
            

        }// End -----------------------------------------------------------------------------------------------------


        

        #endregion

    }
}
