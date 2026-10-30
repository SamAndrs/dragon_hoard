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
        v.001b (??/??) Extend and setup functionality for opening the various menues or the map.
         


    :: DONE ---------------------------
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

        private ClickButton _btnQuit;
        
        private ClickButton _btnAudio;
        
        private ClickButton _btnVideo;
        
        private ClickButton _btnSave;


        // GRAPHICS
        private Point _position;

        private int _buttonSize;

        private Texture2D _optMapImg;

        private Texture2D _optQuitImg;
        
        private Texture2D _optAudioImg;
        
        private Texture2D _optVideoImg;
        
        private Texture2D _optSaveImg;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public OptionsBar(MainGame game, UiManager ui, GameSession session)
        {
            _game = game;

            _ui = ui;

            _session = session;
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

            LoadTextures(_game);

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
            
            _btnQuit.Update(gameTime);
            
            _btnAudio.Update(gameTime);
            
            _btnVideo.Update(gameTime);
            
            _btnSave.Update(gameTime);

            // if (_btnMap.Clicked)     // TODO     Switch to GameMapScreen.

            // if (_btnQuit.Clicked)    // TODO     Quit prompt. Get back to main menu

            // if (_btnAudio.Clicked)   // TODO     Open Audio menu

            // if (_btnVideo.Clicked)   // TODO     Open video menu

            // if (_btnSave.Clicked)    // TODO     Conditional: Save possible. Savegame menu.
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Draw					   
        ------------------------------------------------------------------------------------------------------------
         - Class method for drawing the class specific content on screen.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            _btnMap.Draw(spriteBatch);
            
            _btnQuit.Draw(spriteBatch);
            
            _btnAudio.Draw(spriteBatch);
            
            _btnVideo.Draw(spriteBatch);
            
            _btnSave.Draw(spriteBatch);
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                      Load Textures					   
        ------------------------------------------------------------------------------------------------------------
         - Class specific support method for loading the textures for the buttons.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadTextures(MainGame game)
        {
            _optMapImg = _ui.RectangleTexture;     //_game.Content.Load<Texture2D>("");
            
            _optQuitImg = _ui.RectangleTexture;     //_game.Content.Load<Texture2D>("");
            
            _optAudioImg = _ui.RectangleTexture;    //_game.Content.Load<Texture2D>("");
            
            _optVideoImg = _ui.RectangleTexture;    //_game.Content.Load<Texture2D>("");
            
            _optSaveImg = _ui.RectangleTexture;     //_game.Content.Load<Texture2D>("");
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                Load Buttons
        ------------------------------------------------------------------------------------------------------------
          - Class specific support method for creating and initiating the buttons of the options menu bar.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadButtons(Point position, int btnSize)
        {
            _btnMap = new ClickButton(_ui, new Rectangle(position.X, position.Y, btnSize, btnSize), true);
            _btnMap.Initialize(Color.White, "", Color.Black, Vector2.Zero, true, 0);

            float quitX = position.X + btnSize * 1 + 5;
            _btnQuit = new ClickButton(_ui, new Rectangle((int)quitX, position.Y, btnSize, btnSize), true);
            _btnQuit.Initialize(Color.White, "", Color.Black, Vector2.Zero, true, 0);

            float audioX = position.X + btnSize * 2 + 10;
            _btnAudio = new ClickButton(_ui, new Rectangle((int)audioX, position.Y, btnSize, btnSize), true);
            _btnAudio.Initialize(Color.White, "", Color.Black, Vector2.Zero, true, 0);

            float videoX = position.X + btnSize * 3 + 15;
            _btnVideo = new ClickButton(_ui, new Rectangle((int)videoX, position.Y, btnSize, btnSize), true);
            _btnVideo.Initialize(Color.White, "", Color.Black, Vector2.Zero, true, 0);

            float saveX = position.X + btnSize * 4 + 20;
            _btnSave = new ClickButton(_ui, new Rectangle((int)saveX, position.Y, btnSize, btnSize), true);
            _btnSave.Initialize(Color.White, "", Color.Black, Vector2.Zero, true, 0);
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
