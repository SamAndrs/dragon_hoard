using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI
{
    ///////////////////////////////////////// HEALTH BAR CLASS ///////////////////////////////////////////////////
    /*
       Small data class for storing, affecting and displaying the player character health as a visual
        bar. 
      
       :: TODO ---------------------------


        :: DONE ---------------------------
            (2026)
            v.001b (11/06) Methods for loading graphics assets added.
            v.001a (10/07) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class Healthbar
    {

        // DATA
        private int _currentHealth;

        private int _maxHealth;

       
        // FUNCTIONALITY


        // GRAPHICS
        private Point _position;

        private Rectangle _frameSource;

        private Rectangle _healthBoxSource;

        private Rectangle _frameBox;

        private Texture2D _uiTextures;

        private int _healthBoxSize;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public Healthbar(Texture2D uiTextures)
        {
            _uiTextures = uiTextures;
        }

        //============================================ METHODS ===================================================//
        #region methods



        /*----------------------------------------------------------------------------------------------------------
                                                      Load					   
        ------------------------------------------------------------------------------------------------------------
         - Description
        ----------------------------------------------------------------------------------------------------------*/
        public void Load(Point position, int maxHealth, int currentHealth, int squareSize)
        {
            _position = position;

            _maxHealth = maxHealth;

            _currentHealth = currentHealth;

            _healthBoxSize = squareSize;

            _frameBox = new Rectangle(_position.X, _position.Y - 447, 121, 447);

            _frameSource = new Rectangle(479, 102, 121, 447);

            _healthBoxSource = new Rectangle(441, 121, _healthBoxSize, _healthBoxSize);
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                      Update					   
        ------------------------------------------------------------------------------------------------------------
         - Class method to update the current health value of the player character.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(int currentHealth) =>  _currentHealth = currentHealth;
        // End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Draw					   
        ------------------------------------------------------------------------------------------------------------
         - Class method to draw the health bar on the screen. This method is called during the
           draw cycle of the game loop to render the health bar and its current state.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch, int maxHealth, BitmapFont font)
        {
            // Calculate the position for the health boxes to place them in the middle of the _frameBox.
            int posX = _frameBox.X + ((_frameBox.Width / 2) - _healthBoxSize / 2);
            int posY = _frameBox.Y + (_frameBox.Height - 60) - _healthBoxSize;

            // Draw the health boxes based on the current health and maximum health.
            for (int i = 0; i < _maxHealth; i++)
            {
                spriteBatch.Draw(_uiTextures, new Rectangle(posX, posY - i * (_healthBoxSize), _healthBoxSize, _healthBoxSize), _healthBoxSource, Color.DarkBlue);

                if (i <= _currentHealth)
                    spriteBatch.Draw(_uiTextures, new Rectangle(posX, posY - i * (_healthBoxSize), _healthBoxSize, _healthBoxSize), _healthBoxSource, Color.Red);
            }

            // Draw the frame box around the health boxes.
            spriteBatch.Draw(_uiTextures, _frameBox, _frameSource, Color.White);

            // Measure the size of the text string to center it within the frame box
            Vector2 textString = font.MeasureString($"{_currentHealth.ToString()}/ {maxHealth.ToString()}");
            int textPosX = _frameBox.X + ((_frameBox.Width / 2) - (int)(textString.X / 2));
            int textPosY = _frameBox.Y + (_frameBox.Height - 25);

            // Adjust the color of the text based on the current health value
            spriteBatch.DrawString(font, $"{_currentHealth.ToString()}/ {maxHealth.ToString()}", new Vector2(textPosX, textPosY), GetColor(_currentHealth));

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Get Color					   
        ------------------------------------------------------------------------------------------------------------
         - Class support method to determine the color of the health text based on the current health value.
        ----------------------------------------------------------------------------------------------------------*/
        private Color GetColor(int value)
        {
            // At or below 50% health
            if (value > (_maxHealth * 0.25) && value <= (_maxHealth * 0.50))
                return Color.Orange;

            // At or less than 25% health
            else if (value <= (_maxHealth * 0.25))
                return Color.Red;
            
            else
                return Color.White;
        }// End -----------------------------------------------------------------------------------------------------

        #endregion
    }
}
