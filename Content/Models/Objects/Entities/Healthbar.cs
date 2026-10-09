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

            v.001a (10/07) Setup of basic core properties and methods.



        :: DONE ---------------------------
            (2026)

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class Healthbar
    {
        private UiManager _ui;

        private int _currentHealth;

        private int _maxHealth;

        private int _healthBoxSize;

        private Texture2D _barImage;

        public Vector2 Position { get; private set; }



        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public Healthbar(UiManager uiManager, Vector2 position)
        {
            _ui = uiManager;
            Position = position;
        }

        //============================================ METHODS ===================================================//
        #region methods


        public void Load()
        {
            _barImage = _ui.RectangleTexture;
        }

        public void Initialize(int maxHealth, int currentHealth, int squareSize)
        {
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
            for (int i = 0; i < _currentHealth; i++)
            {
                //spriteBatch.Draw(_barImage, _healthBoxes[i], Color.Red);
                spriteBatch.Draw(_barImage, new Rectangle((int)Position.X, (int)Position.Y + i * (_healthBoxSize +3), _healthBoxSize, _healthBoxSize), Color.Red);
            }
        }


        #endregion
    }
}
