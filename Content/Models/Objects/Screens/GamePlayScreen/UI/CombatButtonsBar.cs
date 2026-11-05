using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI
{
    /////////////////////////////////////////// COMBAT BUTTONS BAR CLASS ////////////////////////////////////////////
    /*
            Child UI class to the GamePlayScreen. Handles everything related to the combat buttons
             and the events/ actions related to them.
        

    :: TODO ---------------------------
        
         *(??/??) Add the necessary support for the actual combat management.
         
        

    :: DONE ---------------------------
        v.001b (11/05) Added functionality for the buttons to display the player character's combat cards.
        v.001a (10/22) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class CombatButtonsBar
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        private MainGame _game;

        private UiManager _ui;

        // private CombatManager _combat;

        private GameSession _session;

        private SpriteFont _font;

        private PlayerCharacter _character;


        // FUNCTIONAL
        private bool _isInCombat;

        private int _combatRound;

        private ClickButton _btnAtkA;
        
        private ClickButton _btnAtkB;
        
        private ClickButton _btnAtkC;




        // GRAPHICS 
        private Point _position;

        private int _buttonWidth;

        private int _buttonHeight;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public CombatButtonsBar(MainGame game, UiManager ui, GameSession session) 
        {
            _game = game;

            // _combat = _game.CombatManager;

            _ui = ui;

            _session = session;

            _character = _session.PlayerCharacter;
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

            _buttonWidth = 150;

            _buttonHeight = 225;

            LoadButtons(_position, _buttonWidth, _buttonHeight);

        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                      Update					   
        ------------------------------------------------------------------------------------------------------------
         - Class method for updating the combat buttons status, the 'in combat' flag and give feedback
            to the combat manager class.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            _isInCombat = _session.PlayerInCombat;

            
            if (!_isInCombat)
            {
                _btnAtkA.SetInvisible();
                _btnAtkB.SetInvisible();
                _btnAtkC.SetInvisible();


                _btnAtkA.SetNonClickable();
                _btnAtkB.SetNonClickable();
                _btnAtkC.SetNonClickable();
                return;
            }

                _combatRound = _session.CombatRound;
            
            if (_combatRound > 0)
            {
                _btnAtkA.SetVisibility();
                _btnAtkB.SetVisibility();
                _btnAtkC.SetVisibility();


                _btnAtkA.SetClickable();
                _btnAtkB.SetClickable();
                _btnAtkC.SetClickable();

                _btnAtkA.Update(gameTime);
                _btnAtkB.Update(gameTime);
                _btnAtkC.Update(gameTime);

                if (_btnAtkA.Clicked) { } // TODO

                if (_btnAtkB.Clicked) { } // TODO

                if (_btnAtkC.Clicked) { } // TODO
            }
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                      Draw					   
        ------------------------------------------------------------------------------------------------------------
         - Class method for drawing the attack buttons onto the game screen.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            _btnAtkA.Draw(spriteBatch);
            
            _btnAtkB.Draw(spriteBatch);
            
            _btnAtkC.Draw(spriteBatch);
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                     Load Buttons					   
        ------------------------------------------------------------------------------------------------------------
            - Class support method for creating and initiating the 3 buttons used during a combat sequence.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadButtons(Point position, int btnWidth, int btnHeight)
        {
            _btnAtkA = new ClickButton(_ui, new Rectangle(position.X, position.Y, btnWidth, btnHeight), false);
            _btnAtkA.Initialize(Color.White, "", null, Color.Black, Vector2.Zero, false, 0);
            _btnAtkA.Background = _character.CombatCardA;

            float posB_X = position.X + (btnWidth + 5) * 1;
            _btnAtkB = new ClickButton(_ui, new Rectangle((int)posB_X, position.Y, btnWidth, btnHeight), false);
            _btnAtkB.Initialize(Color.White, "", null, Color.Black, Vector2.Zero, false, 0);
            _btnAtkB.Background = _character.CombatCardB;

            float posC_X = position.X + (btnWidth + 5) * 2;
            _btnAtkC = new ClickButton(_ui, new Rectangle((int)posC_X, position.Y, btnWidth, btnHeight), false);
            _btnAtkC.Initialize(Color.White, "", null, Color.Black, Vector2.Zero, false, 0);
            _btnAtkC.Background = _character.CombatCardC;

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
