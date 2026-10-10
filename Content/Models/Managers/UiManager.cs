using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using System;

namespace dragon_hoard.Content.Models.Managers
{

    ///////////////////////////////////////////// UI MANAGER CLASS ////////////////////////////////////////////////
    /*
        
        Currently functions as referetial class for loaded UI fonts.

    :: TODO ---------------------------


    :: DONE ---------------------------
        (2026)
        v.001a (09/25) Setup of basic font reference

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class UiManager
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        private MainGame _game;

        public MouseState Mouse { get; private set; }

        public MouseState PrevMouse { get; private set; }


        // FUNCTIONALITY
        public Point MousePos { get; private set; }

        public KeyboardState keyboard { get; private set; }

        public Random Randomizer { get; set; }

        // GRAPHICS

        public SpriteFont Text { get; private set; }

        public SpriteFont HeadLine { get; private set; }

        //public SpriteFont Digits { get; private set; }

        public SpriteFont DebugText { get; private set; }

        public Texture2D RectangleTexture { get; set; }

        public BitmapFont bitFont;


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public UiManager(MainGame game, Texture2D rectTexture)
        {
            _game = game;
            
            Text = game.Content.Load<SpriteFont>("Fonts/BLKCHCRY");

            DebugText = game.Content.Load<SpriteFont>("Fonts/Roboto_Condensed-Bold");

            HeadLine = game.Content.Load<SpriteFont>("Fonts/twoforjuannf.regular");

            RectangleTexture = new Texture2D(game.GraphicsDevice, 1, 1); // Pixel rectangle
            RectangleTexture.SetData(new[] { Color.White }); // Pixel rectangle color

            Randomizer = new Random();

        }// End


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                Update				   
        ------------------------------------------------------------------------------------------------------------
         - 
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();

            PrevMouse = _game.PrevMouseState;
            Mouse = _game.currentMouseState;
                        
            MousePos = new Point(Mouse.X, Mouse.Y);
        }// End -----------------------------------------------------------------------------------------------------


            #endregion
        }
}
