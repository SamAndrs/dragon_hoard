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

        
        :: DONE ---------------------------
            (2026)
        v.001a (10/15) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class SunDial
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        public Rectangle[] Steps { get; private set; }
        
        private GameSession _gameSession;

        private int _currentStep;
        
        // FUNCTIONAL


        // GRAPHICAL
        private Texture2D _sunIcon;

        private Texture2D _sunSkull;

        public Vector2 Position { get; set; }

        private int _iconSize;

        private bool _isVisible;



        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public SunDial(GameSession thisSession, Texture2D sunTexture, Texture2D sunSkullImage)
        {
            _gameSession = thisSession;

            Steps = new Rectangle[27];

            _sunIcon = sunTexture;

            _sunSkull = sunSkullImage;

            _iconSize = 32;
        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                 Load				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. Initializes the array of rectangles for the position of the sun icons.
            Also ensures they are visible at start.
        ----------------------------------------------------------------------------------------------------------*/
        public void Load(Vector2 setPosition)
        {
            Position = setPosition;
            for (int i = 0; i < Steps.Length; i++)
            {
                Steps[i] = new Rectangle((int)Position.X + (i * 36), (int)Position.Y,_iconSize,_iconSize);
            }

            _isVisible = true;

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                 Update				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. Checks wether the sun dial are to be displayed or temporarily hidden
            in favor of displaying a flavor message.
            Also keeps track of the current game turn.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            if (_gameSession.DisplayFlavorMessage)
                _isVisible = false;
            else
                _isVisible = true;
  
            _currentStep = _gameSession.GameTurn;

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                 Draw				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method for drawing the sun icons onto the screen provided the sun dial is visible.
            Makes sure the current gameturn is represented as a white sun icon, and the last step marked as 
            a skull icon.
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

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                 Check Color				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific support method for returning a specific color depending on position in the 
            sun step array, when a sun icon is drawn onto the GamePlayScreen.
        ----------------------------------------------------------------------------------------------------------*/
        private Color CheckColor(int step)
        {
            if (step < 2 || step > 23)
                return Color.Orange;

            if (step == 25)
                return Color.Red;

            return Color.Yellow;
        }// End -----------------------------------------------------------------------------------------------------

        #endregion
    }
}
