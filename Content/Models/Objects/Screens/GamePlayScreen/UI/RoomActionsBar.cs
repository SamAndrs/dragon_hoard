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
         v.001b (10/31) Added textures to buttons.
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
        private bool _isInCombat;

        private ClickButton _btnSearchRoom;

        private ClickButton _btnSearchCorpse;
        
        private ClickButton _btnOpen;
        
        private ClickButton _btnPickUp;


        // GRAPHICS
        private Point _position;

        private int _buttonWidth;

        private int _buttonHeight;

        private Texture2D _uiTextures;                      // Reference to texture of UI icon/ button images

        private Rectangle _searchRoomOn;                    // Snapshot rectangle of UiTexture for the button image.

        private Rectangle _searchRoomOff;

        private Rectangle _searchCorpseOn;

        private Rectangle _searchCorpseOff;

        private Rectangle _openOn;

        private Rectangle _openOff;

        private Rectangle _pickUpOn;

        private Rectangle _pickUpOff;


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public RoomActionsBar(UiManager ui, GameSession session, Texture2D uiTextures)
        {
            _ui = ui;

            _session = session;

            _uiTextures = uiTextures;
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

            _buttonWidth = 96;

            _buttonHeight = 52;

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

            _isInCombat = _session.PlayerInCombat;

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
            if(_btnSearchRoom.IsClickable) _btnSearchRoom.Draw(spriteBatch, _searchRoomOn);
            else _btnSearchRoom.Draw(spriteBatch, _searchRoomOff);

            if (_btnSearchCorpse.IsClickable) _btnSearchCorpse.Draw(spriteBatch, _searchCorpseOn);
            else _btnSearchCorpse.Draw(spriteBatch, _searchCorpseOff);

            if (_btnOpen.IsClickable) _btnOpen.Draw(spriteBatch, _openOn);
            else _btnOpen.Draw(spriteBatch, _openOff);

            if (_btnPickUp.IsClickable) _btnPickUp.Draw(spriteBatch, _pickUpOn);
            else _btnPickUp.Draw(spriteBatch, _pickUpOff);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Handle Room Search				   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for handling button status for searching a room, and events tied to it.
        ----------------------------------------------------------------------------------------------------------*/
        private void HandleRoomSearch(int searches, RoomTile currentTile)
        {
            if (searches > 0 && !_isInCombat)
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
            if (_currentCard.CardEvent == RoomCardEvent.FallenWarrior && !_currentCard.CorpseSearched && !_isInCombat)
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
                if (!_currentCard.ChestOpened && !_isInCombat)
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
            if (_currentCard.CardEvent == RoomCardEvent.Item && !_currentCard.ItemPickedUp && !_isInCombat)
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

            _searchRoomOn = new Rectangle(0, 197, _buttonWidth, _buttonHeight);

            _searchRoomOff = new Rectangle(0, 145, _buttonWidth, _buttonHeight);

            _searchCorpseOn = new Rectangle(96, 197, _buttonWidth, _buttonHeight);

            _searchCorpseOff = new Rectangle(96, 145, _buttonWidth, _buttonHeight);

            _openOn = new Rectangle(194, 197, _buttonWidth, _buttonHeight);

            _openOff = new Rectangle(194, 145, _buttonWidth, _buttonHeight);

            _pickUpOn = new Rectangle(293, 197, _buttonWidth, _buttonHeight);

            _pickUpOff = new Rectangle(293, 145, _buttonWidth, _buttonHeight);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                     Load Buttons					   
       ------------------------------------------------------------------------------------------------------------
        - Class support method for creating and initiating the 3 buttons used during a room action sequence.
       ----------------------------------------------------------------------------------------------------------*/
        private void LoadButtons(Point position, int width, int height)
        {
            _btnSearchRoom = new ClickButton(_ui, new Rectangle(position.X, position.Y, width, height), true);
            _btnSearchRoom.Initialize(Color.White, "", null, Color.Black, Vector2.Zero, false, 0);
            _btnSearchRoom.Background = _uiTextures;

            float corpseX = position.X + (width + 5) * 1;
            _btnSearchCorpse = new ClickButton(_ui, new Rectangle((int)corpseX, position.Y, width, height), true);
            _btnSearchCorpse.Initialize(Color.White, "", null, Color.Black, Vector2.Zero, false, 0);
            _btnSearchCorpse.Background = _uiTextures;

            float openX = position.X + (width +5) * 2;
            _btnOpen = new ClickButton(_ui, new Rectangle((int)openX, position.Y, width, height), true);
            _btnOpen.Initialize(Color.White, "", null, Color.Black, Vector2.Zero, false, 0);
            _btnOpen.Background = _uiTextures;

            float pickX = position.X + (width + 5) * 3;
            _btnPickUp = new ClickButton(_ui, new Rectangle((int)pickX, position.Y, width, height), true);
            _btnPickUp.Initialize(Color.White, "", null, Color.Black, Vector2.Zero, false, 0);
            _btnPickUp.Background = _uiTextures;
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
