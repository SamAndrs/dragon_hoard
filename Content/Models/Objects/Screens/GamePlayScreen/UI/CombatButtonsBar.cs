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
        
        v.001b (??/??) Add the necessary support for the actual combat management.
         


    :: DONE ---------------------------
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


        // FUNCTIONAL
        private bool _isInCombat;

        private int _combatRound;

        private ClickButton _btnAtkA;
        
        private ClickButton _btnAtkB;
        
        private ClickButton _btnAtkC;


        // GRAPHICS 
        private Point _position;

        private int _buttonSize;

        private Texture2D _imgAtkA;
        
        private Texture2D _imgAtkB;
        
        private Texture2D _imgAtkC;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public CombatButtonsBar(MainGame game, UiManager ui, GameSession session) 
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

            _buttonSize = 40;

            LoadTextures(_game);

            LoadButtons(_position, _buttonSize);
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
                _btnAtkA.SetNonClickable();
                _btnAtkB.SetNonClickable();
                _btnAtkC.SetNonClickable();
                return;
            }

                _combatRound = _session.CombatRound;
            
            if (_combatRound > 0)
            {
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
                                                      Load Textures					   
        ------------------------------------------------------------------------------------------------------------
            - Class support method for loading the textures for the buttons.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadTextures(MainGame game)
        {
            _imgAtkA = _ui.RectangleTexture;    //_game.Content.Load<Texture2D>(""); // TODO
            
            _imgAtkB = _ui.RectangleTexture;    //_game.Content.Load<Texture2D>(""); // TODO

            _imgAtkC = _ui.RectangleTexture;    //_game.Content.Load<Texture2D>(""); // TODO
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                     Load Buttons					   
        ------------------------------------------------------------------------------------------------------------
            - Class support method for creating and initiating the 3 buttons used during a combat sequence.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadButtons(Point position, int btnSize)
        {
            _btnAtkA = new ClickButton(_ui, new Rectangle(position.X, position.Y, btnSize, btnSize + 10), true);
            _btnAtkA.Initialize(Color.White, "A", Color.Black, new Vector2(position.X + 5, position.Y + 5), false, 0);

            float posB_X = position.X + btnSize * 1 + 5;
            _btnAtkB = new ClickButton(_ui, new Rectangle((int)posB_X, position.Y, btnSize, btnSize + 10), true);
            _btnAtkB.Initialize(Color.White, "B", Color.Black, new Vector2((int)posB_X + 5, position.Y + 5), false, 0);

            float posC_X = position.X + btnSize * 2 + 10;
            _btnAtkC = new ClickButton(_ui, new Rectangle((int)posC_X, position.Y, btnSize, btnSize + 10), true);
            _btnAtkC.Initialize(Color.White, "C", Color.Black, new Vector2((int)posC_X + 5, position.Y + 5), false, 0);
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
