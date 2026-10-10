using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace dragon_hoard
{

    //////////////////////////////////////////////// MAINGAME CLASS ////////////////////////////////////////////////
    /*

     Main game class that ties everything together.
        Handles initialization, updating, loading and content drawing necessary for the game loop via 
        a set of manager-classes (GameScreenManager, SoundManager, UIManager etc).
        

    :: TODO ---------------------------

        * Setup system for (basic) UI Management & handling (UI Manager)
        
        v0.001c Setup of core basic game loop (Game Session management)
        

    :: DONE ---------------------------
        (2026)
        v0.001b (09/26) Basic screen management finished.
        v0.001a (09/24) Setup system for (basic) Game Screen management & handling (Game Screen Manager)

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class MainGame : Game
    {

        //========================================== PROPERTIES ==================================================//


        // GRAPHICS SETUP
        private GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;

        // The desired resolution for the game window
        //private readonly int _resolutionWidth = 800;
        private readonly int _resolutionWidth = 1280;
        //private readonly int _resolutionHeight = 600;
        private readonly int _resolutionHeight = 720;

        public float AspectRatio;

        // The rendering resolution
        private int _virtualWidth = 800;
        private int _virtualHeight = 600;

        public float CenterX { get; private set; }
        public float CenterY { get; private set; }

        public int[] vpSize { get; private set; }

        private bool _isResizing;

        private Matrix _screenScaleMatrix;
        private Viewport _virtualViewport;

        public Viewport VirtualView { get { return _virtualViewport; } }

        public float ScaleX {  get; private set; }
        public float ScaleY { get; private set; }


        // DEBUG
        public string GameVersion { get; private set; }
        public bool DebugMode { get; private set; } = true;
        public Texture2D RectangleTexture { get; set; }     // Pixel

        // CONTENT MANAGERS
        public GameScreenManager GameScreenManager { get; private set; }
        public readonly UiManager _uiManager;

        public GameSession GameSession { get; private set; }

        // DEVICES
        public MouseState currentMouseState { get; private set; }
        public MouseState PrevMouseState { get; private set; }

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferredBackBufferWidth = _resolutionWidth;
            _graphics.PreferredBackBufferHeight = _resolutionHeight;
            _graphics.ApplyChanges();


            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // TODO: Remove later in favor of fixed resizing options -----------
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnClientSizeChanged;
            // -----------------------------------------------------------------

            GameVersion = "v0.001b";

            RectangleTexture = new Texture2D(GraphicsDevice, 1, 1); // Needed for solid backgrounds

            _uiManager = new UiManager(this, RectangleTexture);
            GameScreenManager = new GameScreenManager(this, _uiManager);
        }


        //============================================ METHODS ===================================================//
        #region methods




        // TODO: Remove later in favor of fixed resizing options
        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            if(!_isResizing && Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0)
            {
                _isResizing = true;
                UpdateScreenScaleMatrix();
                _isResizing = false;
            }
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                    Initialize					   
        ------------------------------------------------------------------------------------------------------------
             - Startup method to load and set specific variables, set screen resolution and scaling.
                Initializes various Managers required for core game loop.
        ----------------------------------------------------------------------------------------------------------*/
        protected override void Initialize()
        {
            UpdateScreenScaleMatrix();

            base.Initialize();
            GameScreenManager.Initialize();
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                   Load Content					   
        ------------------------------------------------------------------------------------------------------------
            - Game class specific content loading and setup method.
            Tasks various sub-Managers with running their content load methods.
        ----------------------------------------------------------------------------------------------------------*/
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                   Update					   
        ------------------------------------------------------------------------------------------------------------
            - Game specific content handling method and core game loop at runtime.
        
        ----------------------------------------------------------------------------------------------------------*/
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentMouseState = ReadMouseState(gameTime);

            base.Update(gameTime);
            GameScreenManager.Update(gameTime);

        }// End ----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                   Draw				   
        ------------------------------------------------------------------------------------------------------------
            - Game specific content display method.
              Initializes the content drawing method and tasks GameScreenManager with drawing text and elements 
              on the screen.
        ----------------------------------------------------------------------------------------------------------*/
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.Viewport = _virtualViewport;

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _screenScaleMatrix);

            GameScreenManager.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }// End ----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                   UpdateScreenScaleMatrix					   
        ------------------------------------------------------------------------------------------------------------
            - Handles retention of aspect ratio when game window is being resized.

        ----------------------------------------------------------------------------------------------------------*/
        private void UpdateScreenScaleMatrix()
        {
            try
            {
                // Determine the size of actual screen
                float screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
                float screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

                // Calculate the scale factors for width and height based on the virtual resolution and actual screen size
                if (screenWidth / _resolutionWidth > screenHeight / _resolutionHeight)
                {
                    float aspect = screenHeight / _resolutionWidth;
                    _virtualWidth = (int)(aspect * _resolutionWidth);
                    _virtualHeight = (int)screenHeight;
                }
                else
                {
                    float aspect = screenWidth / _resolutionWidth;
                    _virtualWidth = (int)screenWidth;
                    _virtualHeight = (int)(aspect * _resolutionHeight);
                }

                _screenScaleMatrix = Matrix.CreateScale(_virtualWidth / (float)_resolutionWidth);

                _virtualViewport = new Viewport
                {
                    X = (int)(screenWidth / 2 - _virtualWidth / 2),
                    Y = (int)(screenHeight / 2 - _virtualHeight / 2),
                    Width = _virtualWidth,
                    Height = _virtualHeight,
                    MinDepth = 0,
                    MaxDepth = 1
                };

                ScaleX = _virtualWidth / _resolutionWidth;
                ScaleY = _virtualHeight / _resolutionHeight;

                // Set values for quick-access to center of screen
                CenterX = _resolutionWidth / 2;
                CenterY = _resolutionHeight / 2;

                vpSize = new int[2] { _virtualWidth, _virtualHeight };
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR!",$"Error updating screen scale matrix: {ex.Message}", ["Ok"]);

            }
        }// End ----------------------------------------------------------------------------------------------------


        private MouseState ReadMouseState(GameTime gameTime)
        {
            // Get current state of mouse
            PrevMouseState = currentMouseState;

            MouseState mouseState = Mouse.GetState();
            

            // Get X and Y coordinates
            int mouseX = currentMouseState.X;
            int mouseY = currentMouseState.Y;

            return mouseState;
        }


        /*----------------------------------------------------------------------------------------------------------
                                                   Set Debug Mode					   
        ------------------------------------------------------------------------------------------------------------
            - Callback method for later to turn Debug mode On/Off
        ----------------------------------------------------------------------------------------------------------*/
        public void SetDebugMode() => DebugMode = !DebugMode;

        public void StartGameSession() => GameSession = new GameSession();

        #endregion
    }
}
