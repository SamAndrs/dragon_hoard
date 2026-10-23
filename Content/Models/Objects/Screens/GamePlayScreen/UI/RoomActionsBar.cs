using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using dragon_hoard.Content.Models.Objects.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI
{
    ////////////////////////////////////////// ROOM ACTIONS BAR CLASS //////////////////////////////////////////////
    /*

            Child UI class to the GamePlayScreen. Handles everything related to the room actions buttons
            and the events assosciated with them.
            Utilizes RoomTile- and RoomCard methods to fire off different events.

    :: TODO ---------------------------


    :: DONE ---------------------------
         v.001a (10/23) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RoomActionsBar
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        private UiManager _ui;

        private GameSession _session;

        private RoomCard _currentCard;

        private RoomTile _currentTile;

        private int _searchTimes;

        // FUNCTIONAL
        private ClickButton _btnSearchRoom;

        private ClickButton _btnSearchCorpse;
        
        private ClickButton _btnOpen;
        
        private ClickButton _btnPickUp;


        // GRAPHICS
        private Point _position;

        private int _buttonWidth;

        private int _buttonHeight;

        private Texture2D _imgSearchRoom;

        private Texture2D _imgSearchCorpse;

        private Texture2D _imgOpen;
        
        private Texture2D _imgPickUp;


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public RoomActionsBar(UiManager ui, GameSession session)
        {
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

            _buttonWidth = 80;

            _buttonHeight = 30;

            _currentTile = _session.CurrentRoom;

            _searchTimes = _session.PlayerCanSearch;

            LoadTextures();

            LoadButtons(_position, _buttonWidth, _buttonHeight);
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                     Update					   
        ------------------------------------------------------------------------------------------------------------
         - Class method for looping through and updating the clickability and state of the 3 room action buttons.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            _currentTile = _session.CurrentRoom;

            //if (_currentCard != null)
                _currentCard = _session.CurrentRoomCard;

            _searchTimes = _currentTile.RoomSearched;

            _btnSearchRoom.Update(gameTime);
            
            _btnSearchCorpse.Update(gameTime);
            
            _btnOpen.Update(gameTime);
            
            _btnPickUp.Update(gameTime);

            HandleRoomSearch(_searchTimes, _currentTile);
            
            // Abort if no RoomCard has been pulled/ activated.
            if (_currentCard == null)
                return;

            HandleCorpseSearch();

            HandleOpenContainer();

            HandlePickUpItem();
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Draw					   
        ------------------------------------------------------------------------------------------------------------
         - Class method for drawing the room action buttons onto the game screen.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            _btnSearchRoom.Draw(spriteBatch);
            
            _btnSearchCorpse.Draw(spriteBatch);

            _btnOpen.Draw(spriteBatch);
            
            _btnPickUp.Draw(spriteBatch);
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Handle Room Search				   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for handling button status for searching a room, and events tied to it.
        ----------------------------------------------------------------------------------------------------------*/
        private void HandleRoomSearch(int searches, RoomTile currentTile)
        {
            if (searches > 0)
            {
                _btnSearchRoom.SetClickable();
                if (_btnSearchRoom.Clicked)
                {
                    currentTile.PlayerSearchesRoom();
                    // Do room search event // TODO
                }
            }
            else _btnSearchRoom.SetNonClickable();
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Handle Corpse Search				   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for handling button status for searching a fallen warrior, and events with it.
        ----------------------------------------------------------------------------------------------------------*/
        private void HandleCorpseSearch()
        {
            if (_currentCard.CardEvent == RoomCardEvent.FallenWarrior && !_currentCard.CorpseSearched)
            {
                _btnSearchCorpse.SetClickable();
                if (_btnSearchCorpse.Clicked)
                {
                    _currentCard.PlayerSearchCorpse();
                    // Do Corpse search event // TODO
                    // if player has moved - End Turn // TODO
                }
            }
            else
                _btnSearchCorpse.SetNonClickable();
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Handle Open Container				   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for handling button status for searching a fallen warrior, and events with it.
        ----------------------------------------------------------------------------------------------------------*/
        private void HandleOpenContainer()
        {
            if (_currentCard.CardEvent == RoomCardEvent.Chest || _currentCard.CardEvent == RoomCardEvent.Sarcophagus)
            {
                if (!_currentCard.ChestOpened)
                {
                    _btnOpen.SetClickable();
                    if (_btnOpen.Clicked)
                    {
                        _currentCard.PlayerOpensChest();
                        // Do Chest open event // TODO
                        // End Turn // TODO
                    }
                }
                else
                    _btnOpen.SetNonClickable();
            }
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Handle Pick Up Item				   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for handling button status for searching a fallen warrior, and events with it.
        ----------------------------------------------------------------------------------------------------------*/
        private void HandlePickUpItem()
        {
            // If the current card has an Item not yet taken...
            if (_currentCard.CardEvent == RoomCardEvent.Item && !_currentCard.ItemPickedUp)
            {
                _btnPickUp.SetClickable();
                if (_btnPickUp.Clicked)
                {
                    _currentCard.ItemIsPickedUp();
                    // Do Item pck up event // TODO
                    // If player has moved - End Turn // TODO
                }
            }
            else
                _btnPickUp.SetNonClickable();
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Load Textures					   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for loading the class specific texture content.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadTextures()
        {
            _imgSearchRoom = _ui.RectangleTexture;  //_game.Content.Load<Texture2D>("");    //TODO
            
            _imgSearchCorpse = _ui.RectangleTexture;  //_game.Content.Load<Texture2D>("");    //TODO

            _imgOpen = _ui.RectangleTexture;    //_game.Content.Load<Texture2D>("");    //TODO
            
            _imgPickUp = _ui.RectangleTexture;  //_game.Content.Load<Texture2D>("");    //TODO
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                     Load Buttons					   
       ------------------------------------------------------------------------------------------------------------
        - Class support method for creating and initiating the 3 buttons used during a room action sequence.
       ----------------------------------------------------------------------------------------------------------*/
        private void LoadButtons(Point position, int width, int height)
        {
            _btnSearchRoom = new ClickButton(_ui, new Rectangle(position.X, position.Y, width, height), true);
            _btnSearchRoom.Initialize(Color.White, "Search room", Color.Black, new Vector2(position.X + 5, position.Y + 5), false, 0);

            float corpseX = position.X + width * 1 + 5;
            _btnSearchCorpse = new ClickButton(_ui, new Rectangle((int)corpseX, position.Y, width, height), true);
            _btnSearchCorpse.Initialize(Color.White, "Search corpse", Color.Black, new Vector2(corpseX + 5, position.Y + 5), false, 0);

            float openX = position.X + width * 2 + 10;
            _btnOpen = new ClickButton(_ui, new Rectangle((int)openX, position.Y, width, height), true);
            _btnOpen.Initialize(Color.White, "Open", Color.Black, new Vector2(openX + 5, position.Y + 5), false, 0);

            float pickX = position.X + width * 3 + 15;
            _btnPickUp = new ClickButton(_ui, new Rectangle((int)pickX, position.Y, width, height), true);
            _btnPickUp.Initialize(Color.White, "Pick up", Color.Black, new Vector2(pickX + 5, position.Y + 5), false, 0);
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
