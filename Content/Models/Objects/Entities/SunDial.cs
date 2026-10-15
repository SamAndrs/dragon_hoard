using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragon_hoard.Content.Models.Objects.Entities
{
    ///////////////////////////////////////// SUN DIAL CLASS ////////////////////////////////////////////////////
    /*
        Game class for handling the visual representation of the current game session turn progression.
        Displays an mulit-colored array of "sun icons" at the top of the GamePlayScreen.
      
       :: TODO ---------------------------

            v.001a (10/15) Setup of basic core properties and methods.



        :: DONE ---------------------------
            (2026)

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class SunDial
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        public Rectangle[] Steps { get; private set; }
        
        private GameSession _gameSession;

        private int _currentStep;

        private SpriteFont _textFont;
        
        // FUNCTIONAL


        // GRAPHICAL
        private Texture2D _sunIcon;

        private Texture2D _sunSkull;

        public Vector2 Position { get; set; }

        private int _iconSize;

        private bool _isVisible;

        private bool _displayMessage;

        private string _message;

        private float _messageTimer;




        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public SunDial(GameSession thisSession, Texture2D sunTexture, Texture2D sunSkullImage, SpriteFont font)
        {
            _gameSession = thisSession;

            Steps = new Rectangle[27];

            _sunIcon = sunTexture;

            _sunSkull = sunSkullImage;

            _iconSize = 32;

            _textFont = font;

        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                 Initialize				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. 
        ----------------------------------------------------------------------------------------------------------*/
        public void Initialize(Vector2 setPosition)
        {
            Position = setPosition;
            for (int i = 0; i < Steps.Length; i++)
            {
                Steps[i] = new Rectangle((int)Position.X + (i * 36), (int)Position.Y,_iconSize,_iconSize);
            }

            _isVisible = true;

            _displayMessage = false;

            _message = "";

            _messageTimer = 3f;

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                 Update				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. 
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            if(_displayMessage && _messageTimer > 0)
            {
                _isVisible = false;
                _message = "This is a message";
                _messageTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                _messageTimer = 1;
                _message = "";
                _displayMessage = false;
                _isVisible = true;
            }    
                _currentStep = _gameSession.GameTurn;

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                 Draw				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. 
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isVisible)
            {
                for (int i = 0; i < Steps.Length - 1; i++)
                {
                    spriteBatch.Draw(_sunIcon, Steps[i], CheckColor(i));
                    if (i == _currentStep)
                        spriteBatch.Draw(_sunIcon, Steps[i], Color.White);
                }

                spriteBatch.Draw(_sunSkull, Steps[26], Color.White);
            }

            if(_displayMessage)
            {
                spriteBatch.DrawString(_textFont, _message, Position, Color.Red);
            }

        }// End -----------------------------------------------------------------------------------------------------


        private Color CheckColor(int step)
        {
            if (step < 2 || step > 23)
                return Color.Orange;

            if (step == 25)
                return Color.Red;

            return Color.Yellow;
        }

        public void DisplayMessage() => _displayMessage = true;
        
        #endregion
    }
}
