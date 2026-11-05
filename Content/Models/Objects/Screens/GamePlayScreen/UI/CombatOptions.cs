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
        
        * (??/??) Add the necessary support for the actual options events.
         


    :: DONE ---------------------------
        v.001b (10/31) Added textures to buttons.
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

        private Texture2D _uiTextures;

        private Rectangle _fleeSourceRect;

        private Rectangle _fightSourceRect;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public CombatOptions(MainGame game, UiManager ui, GameSession session, Texture2D uiTextures)
        {
            _game = game;

            // _combat = _game.CombatManager;

            _ui = ui;

            _session = session;

            _uiTextures = uiTextures;
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

            _buttonWidth = 105;

            _buttonHeight = 26;

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
            _btnFight.Draw(spriteBatch, _fightSourceRect);

            _btnFlee.Draw(spriteBatch, _fleeSourceRect);
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Load Textures					   
        ------------------------------------------------------------------------------------------------------------
            - Class support method for loading the textures for the buttons.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadTextures(MainGame game)
        {

            _fightSourceRect = new Rectangle(282, 77, _buttonWidth, _buttonHeight);

            _fleeSourceRect = new Rectangle(282, 106, _buttonWidth, _buttonHeight);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                     Load Buttons					   
        ------------------------------------------------------------------------------------------------------------
            - Class support method for creating and initiating the 2 combat action options buttons.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadButtons(Point position, int width, int height)
        {
            _btnFight = new ClickButton(_ui, new Rectangle(position.X - (width + 25), position.Y, width, height + 10), false);
            _btnFight.Initialize(Color.White, "", null, Color.Red, Vector2.Zero, false, 0);
            _btnFight.Background = _uiTextures;

            _btnFlee = new ClickButton(_ui, new Rectangle(position.X + 25, position.Y, width, height + 10), false);
            _btnFlee.Initialize(Color.White, "", null, Color.Red, Vector2.Zero, false, 0);
            _btnFlee.Background = _uiTextures;
        }// End -----------------------------------------------------------------------------------------------------


        #endregion
    }
}
