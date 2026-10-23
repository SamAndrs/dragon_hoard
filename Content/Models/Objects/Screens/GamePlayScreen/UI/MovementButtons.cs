using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using dragon_hoard.Content.Models.Objects.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Point = Microsoft.Xna.Framework.Point;

namespace dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI
{
    ///////////////////////////////////////// MOVEMENT BUTTONS CLASS ///////////////////////////////////////////////
    /*

            Child UI class to the GamePlayScreen. Handles the UI movement buttons and event feedback.

    :: TODO ---------------------------


    :: DONE ---------------------------
        v.001a (10/23) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class MovementButtons
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        private MainGame _game;

        private UiManager _ui;

        private GameSession _session;

        // FUNCTIONAL
        private bool _playerCanMove;

        private RoomTile _currentRoom;
        
        public ClickButton BtnGoUp {  get; private set; }
        
        public ClickButton BtnGoDown { get; private set; }
        
        public ClickButton BtnGoLeft { get; private set; }
        
        public ClickButton BtnGoRight { get; private set; }


        // GRAPHICS 
        private Point _position;

        private int _buttonSize;

        private Texture2D _imgGoUp;
        
        private Texture2D _imgGoDown;
        
        private Texture2D _imgGoLeft;
        
        private Texture2D _imgGoRight;


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public MovementButtons(MainGame game, UiManager ui, GameSession session)
        { 
            _game = game;

            _ui = ui;

            _session = session;
        
        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                      Load					   
        ------------------------------------------------------------------------------------------------------------
         - Description
        ----------------------------------------------------------------------------------------------------------*/
        public void Load(Point position)
        {
            _position = position;

            _buttonSize = 50;

            LoadTextures(_game);

            LoadButtons(_position, _buttonSize);

            _currentRoom = _session.CurrentRoom;

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Update				   
        ------------------------------------------------------------------------------------------------------------
         - Class method for looping through and updating the visibility and clickability 
            status of the move buttons depending on certain conditions such as being inactive during a combat
            phase or a particular direction unavailable when next to any of the rows or columns assigned as 
            'outer wall' on the GameMap.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            _playerCanMove = _session.PlayerCanMove;
            _currentRoom = _session.CurrentRoom;

            BtnGoUp.Update(gameTime);
            BtnGoDown.Update(gameTime);
            BtnGoLeft.Update(gameTime);
            BtnGoRight.Update(gameTime);

            // The buttons can only be available outside of combat
            if (!_session.PlayerInCombat)
            {
                // Allow only exits that are available

                // Check if there's an exit up and if the current position on gamemap is not on upmost row.
                if (_currentRoom.ExitUp && _currentRoom.Row > 1)
                {
                    BtnGoUp.SetVisibility();
                    BtnGoUp.SetClickable();
                }
                else
                {
                    BtnGoUp.SetInvisible();
                    BtnGoUp.SetNonClickable();
                }

                if (_currentRoom.ExitRight && _currentRoom.Col < _session.GameMap.Columns - 2)
                {
                    BtnGoRight.SetVisibility();
                    BtnGoRight.SetClickable();

                }
                else
                {
                    BtnGoRight.SetInvisible();
                    BtnGoRight.SetNonClickable();
                }

                if (_currentRoom.ExitDown && _currentRoom.Row < _session.GameMap.Rows - 2)
                {
                    BtnGoDown.SetVisibility();
                    BtnGoDown.SetClickable();

                }
                else
                {
                    BtnGoDown.SetInvisible();
                    BtnGoDown.SetNonClickable();
                }

                if (_currentRoom.ExitLeft && _currentRoom.Col > 1)
                {
                    BtnGoLeft.SetVisibility();
                    BtnGoLeft.SetClickable();

                }
                else
                {
                    BtnGoLeft.SetInvisible();
                    BtnGoLeft.SetNonClickable();
                }
            }

        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                      Draw					   
        ------------------------------------------------------------------------------------------------------------
         - Class method for drawing the move buttons onto the game screen, provided the player can move 
            during the gameturn.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_playerCanMove)
            {
                BtnGoUp.Draw(spriteBatch);

                BtnGoDown.Draw(spriteBatch);

                BtnGoLeft.Draw(spriteBatch);

                BtnGoRight.Draw(spriteBatch);

                spriteBatch.Draw(_ui.RectangleTexture, new Rectangle(_position.X, _position.Y, 5, 5), Color.LimeGreen);
            }
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Load Textures					   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for loading the textures for the buttons.
        ----------------------------------------------------------------------------------------------------------*/
        public void LoadTextures(MainGame game)
        {
            _imgGoUp = _ui.RectangleTexture;    //_game.Content.Load<Texture2D>("");    //TODO
            
            _imgGoDown = _ui.RectangleTexture;  //_game.Content.Load<Texture2D>("");    //TODO
            
            _imgGoLeft = _ui.RectangleTexture;  //_game.Content.Load<Texture2D>("");    //TODO
            
            _imgGoRight = _ui.RectangleTexture; //_game.Content.Load<Texture2D>("");    //TODO
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Load Buttons					   
        ------------------------------------------------------------------------------------------------------------
         -  Class support method for creating and initiating the 3 buttons used during a combat sequence.
            The are positioned like a '+' around the position center point.
        ----------------------------------------------------------------------------------------------------------*/
        public void LoadButtons(Point position, int btnSize)
        {
            Point posUP = new Point(position.X - (btnSize/2), position.Y - (btnSize + (btnSize/2) ));
            BtnGoUp = new ClickButton(_ui, new Rectangle(posUP.X, posUP.Y, btnSize, btnSize), true);
            BtnGoUp.Initialize(Color.White, "U", Color.Black, new Vector2(posUP.X + 10, posUP.Y + 5), false, 0);

            Point posDown = new Point(position.X - (btnSize / 2), position.Y + (btnSize/2));
            BtnGoDown = new ClickButton(_ui, new Rectangle(posDown.X, posDown.Y, btnSize, btnSize), true);
            BtnGoDown.Initialize(Color.White, "D", Color.Black, new Vector2(posDown.X + 15, posDown.Y + 5), false, 0);

            Point posLeft = new Point(position.X - (btnSize + (btnSize / 2)), position.Y - (btnSize/2));
            BtnGoLeft = new ClickButton(_ui, new Rectangle(posLeft.X, posLeft.Y, btnSize, btnSize), true);
            BtnGoLeft.Initialize(Color.White, "L", Color.Black, new Vector2(posLeft.X  + 10, posLeft.Y + 10), false, 0);

            Point posRight = new Point(position.X + (btnSize/2), position.Y - (btnSize / 2));
            BtnGoRight = new ClickButton(_ui, new Rectangle(posRight.X, posRight.Y, btnSize, btnSize), true);
            BtnGoRight.Initialize(Color.White, "R", Color.Black, new Vector2(posRight.X + 10, posRight.Y + 10), false, 0);
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
