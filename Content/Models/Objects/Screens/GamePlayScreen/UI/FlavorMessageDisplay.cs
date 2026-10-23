using dragon_hoard.Content.Models.Objects.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI
{
    /////////////////////////////////////// FLAVOR MESSAGE DISPLAY CLASS ///////////////////////////////////////////
    /*
            Child UI class to the GamePlayScreen. Handles everything related to displaying a random
            'flavor' message at the top of the screen (temporarily obscuring the sundial) at the beginning
             of each gameturn.

    :: TODO ---------------------------

            * Add sound 

    :: DONE ---------------------------
        v.001a (10/22) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public class FlavorMessageDisplay
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        private GameSession _session;

        private SpriteFont _font;


        // FUNCTIONAL
        private Random _random;

        private bool _displayMessage;

        private float _messageTimer;

        private Vector2 _messageLength;

        // GRAPHICS 
        private Point _position;

        private Point _modifiedPos;

        private string _flavorMessage = string.Empty;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public FlavorMessageDisplay(GameSession session, SpriteFont font)
        {
            _session = session;

            _font = font;
        }


        //============================================ METHODS ===================================================//
        #region methods

        /*----------------------------------------------------------------------------------------------------------
                                                       Load					   
        ------------------------------------------------------------------------------------------------------------
          - Class method for setting up the properties of the message display. 
        ----------------------------------------------------------------------------------------------------------*/
        public void Load(Point position, Random random)
        {
            _position = position;

            _random = random;

            _messageTimer = 5f;

            _messageLength = new Vector2(0, 0);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Update					   
        ------------------------------------------------------------------------------------------------------------
          - Class method for updating the display position and flag based on the message timer.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            _displayMessage = _session.DisplayFlavorMessage;

            if (!_displayMessage)
                return;
            
            DisplayFlavorMessage();

            // Measure the length of the string to be able to center its position correctly
            if (!string.IsNullOrEmpty(_flavorMessage))
            {
                _messageLength = _font.MeasureString(_flavorMessage) / 2;
                _modifiedPos = new Point((int)_position.X - (int)_messageLength.X, _position.Y);
            }
            
            // Only display the message for a limited time.
            if (_messageTimer > 0)
                _messageTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Once timer is up, reset
            else
            {
                _session.DisplayFlavorMessage = false;
                _messageTimer = 5f;
                _flavorMessage = string.Empty;
                
            }

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Draw					   
        ------------------------------------------------------------------------------------------------------------
          - Class method for drawing the flavor message onto the screen.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_displayMessage)
                return;

            spriteBatch.DrawString(_font, _flavorMessage, new Vector2(_modifiedPos.X, _modifiedPos.Y), Color.Red);
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                Display Flavor Message				   
        ------------------------------------------------------------------------------------------------------------
          - Class support method for controlling when the message is to be displayed.
        ----------------------------------------------------------------------------------------------------------*/
        private void DisplayFlavorMessage()
        {
            _displayMessage = true;

            if (string.IsNullOrEmpty(_flavorMessage)) 
                _flavorMessage = PickFlavorMessage(_random);
        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                       Pick Flavor Message					   
        ------------------------------------------------------------------------------------------------------------
          - Class support method to randomly pick one of the listed flavor messages.
        ----------------------------------------------------------------------------------------------------------*/
        private string PickFlavorMessage(Random random)
        {
            int result = random.Next(3);

            switch (result)
            {
                case 0: return "Mind the path of the sun. As night draws closer the denizens of the castle grows fiercer";

                case 1: return "The  brave,  the  foolhardy,  the  desperate.  The  promise  of  the  hoard  calls  to  them  each.";

                case 2: return "Take  heed  to  escape  the  castle  before  sunset,  for  none  have  survived  the  horrors  of  the  night.";

                case 3: return "The  promise  of  wealth  beckons.  Lurking  close  behind  is  the  promise  of  peril.";

                case 4: return "Night  draws  ever  closer,  as  another  hour  passes.";

                default: return "Bravery  or  madness;  to challenge  the  castle  is  both";
            }// End -----------------------------------------------------------------------------------------------------

        }
        #endregion

    }
}
