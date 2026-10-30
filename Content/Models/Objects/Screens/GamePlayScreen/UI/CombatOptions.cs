using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI
{
    /////////////////////////////////////////// COMBAT OPTIONS CLASS ////////////////////////////////////////////
    /*
            Child UI class to the GamePlayScreen. Handles everything related to the 2 combat options (fight or flee)
             and the events/ actions related to them.
        

    :: TODO ---------------------------
        
        v.001b (??/??) Add the necessary support for the actual options events.
         


    :: DONE ---------------------------
        v.001a (10/23) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class CombatOptions
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        private MainGame _game;

        private UiManager _ui;

        // private CombatManager _combat;

        private GameSession _session;

        private SpriteFont _font;


        // FUNCTIONAL
        private bool _isInCombat;

        private int _combatRound;

        private ClickButton _btnFight;

        private ClickButton _btnFlee;


        // GRAPHICS 
        private Point _position;

        private int _buttonWidth;

        private int _buttonHeight;

        private Texture2D _imgFight;

        private Texture2D _imgFlee;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public CombatOptions(MainGame game, UiManager ui, GameSession session)
        {
            _game = game;

            // _combat = _game.CombatManager;

            _ui = ui;

            _session = session;
        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                     Load					   
       ------------------------------------------------------------------------------------------------------------
        - Class method for loading and initiating the attack buttons bar specific UI content and 
           assosciated resources.
       ----------------------------------------------------------------------------------------------------------*/
        public void Load(Point position, SpriteFont font)
        {
            _position = position;

            _font = font;

            _combatRound = _session.CombatRound;

            _buttonWidth = 60;

            _buttonHeight = 30;

            LoadTextures(_game);

            LoadButtons(_position, _buttonWidth, _buttonHeight);
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                      Update					   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for looping through and updating the visibility and clickability status of 
            the 2 combat action buttons.
            These will only be available in the first round of any combat and depending on if any monster
            encountered (CardEvent) has choosen to flee or fight.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            _isInCombat = _session.PlayerInCombat;
            _combatRound = _session.CombatRound;

            if (!_isInCombat || _combatRound > 0)
            {
                _btnFight.SetInvisible();
                _btnFight.SetNonClickable();

                _btnFlee.SetInvisible();
                _btnFlee.SetNonClickable();
                return;
            }

            if (_isInCombat && _combatRound == 0)
            {
                _btnFight.SetVisibility();
                _btnFight.SetClickable();

                _btnFlee.SetVisibility();
                _btnFlee.SetClickable();

                _btnFight.Update(gameTime);
                _btnFlee.Update(gameTime);

                if (_btnFight.Clicked) { } // TODO

                if (_btnFlee.Clicked) { } // TODO
            }
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                      Draw					   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for drawing the class specific content.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            _btnFight.Draw(spriteBatch);

            _btnFlee.Draw(spriteBatch);
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Load Textures					   
        ------------------------------------------------------------------------------------------------------------
            - Class support method for loading the textures for the buttons.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadTextures(MainGame game)
        {
            _imgFight = _ui.RectangleTexture;    //_game.Content.Load<Texture2D>(""); // TODO

            _imgFlee = _ui.RectangleTexture;    //_game.Content.Load<Texture2D>(""); // TODO
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                     Load Buttons					   
        ------------------------------------------------------------------------------------------------------------
            - Class support method for creating and initiating the 2 combat action options buttons.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadButtons(Point position, int width, int height)
        {
            _btnFight = new ClickButton(_ui, new Rectangle(position.X - (width + 25), position.Y, width, height), false);
            _btnFight.Initialize(Color.White, "Fight!", Color.Red, new Vector2(_btnFight.BoundingBox.X + 5, position.Y + 5), false, 0);

            _btnFlee = new ClickButton(_ui, new Rectangle(position.X + 25, position.Y, width, height), false);
            _btnFlee.Initialize(Color.White, "Flee!", Color.Red, new Vector2(_btnFlee.BoundingBox.X + 5, position.Y + 5), false, 0);
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
