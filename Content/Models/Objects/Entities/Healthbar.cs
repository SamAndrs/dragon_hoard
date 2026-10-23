using dragon_hoard.Content.Models.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragon_hoard.Content.Models.Objects.Entities
{
    ///////////////////////////////////////// HEALTH BAR CLASS ///////////////////////////////////////////////////
    /*
       Small data class for storing, affecting and displaying the player character health in a visual
        bar. 
      
       :: TODO ---------------------------

            * Add a skull icon to the bottom of the array of healthboxes.

        :: DONE ---------------------------
            (2026)
            v.001a (10/07) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class Healthbar
    {
        private UiManager _ui;

        private int _currentHealth;

        private int _maxHealth;

        private int _healthBoxSize;

        private Texture2D _barImage;

        private Point _position;



        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public Healthbar(UiManager uiManager, Point position)
        {
            _ui = uiManager;
            _position = position;
        }

        //============================================ METHODS ===================================================//
        #region methods


        public void Load(int maxHealth, int currentHealth, int squareSize)
        {
            _barImage = _ui.RectangleTexture;

            _maxHealth = maxHealth;

            _currentHealth = currentHealth;

            _healthBoxSize = squareSize;
        }

        public void Update(int currentHealth)
        {
            _currentHealth = currentHealth;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _maxHealth; i++)
            {

                //spriteBatch.Draw(_barImage, _healthBoxes[i], Color.Red);
                spriteBatch.Draw(_barImage, new Rectangle((int)_position.X, (int)_position.Y - i * (_healthBoxSize + 3), _healthBoxSize, _healthBoxSize), Color.DarkBlue);

                if (i <= _currentHealth)
                    spriteBatch.Draw(_barImage, new Rectangle((int)_position.X, (int)_position.Y - i * (_healthBoxSize +3), _healthBoxSize, _healthBoxSize), Color.Red);

                
            }
        }


        #endregion
    }
}
