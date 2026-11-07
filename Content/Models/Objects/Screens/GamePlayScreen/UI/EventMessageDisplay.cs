using dragon_hoard.Content.Models.Objects.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI
{
    /////////////////////////////////////// EVENT MESSAGE DISPLAY CLASS ///////////////////////////////////////////
    /*
            Child UI class to the GamePlayScreen. Handles everything related to displaying any message
            related to a RoomTile- or RoomCard event such as entering a Dark room, facing a giant spider etc.

    :: TODO ---------------------------

        * Add support for TrapCard events.

    :: DONE ---------------------------
        v.001c (11/06) UI texture update. Background now utilizes a new texture from the UI sprite sheet.
                        Multiline text wrapping now works properly for both RoomTile and RoomCard events.
        v.001b (10/24) Added CardEvent message timer.
        v.001a (10/23) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class EventMessageDisplay
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        private readonly GameSession _session;


        private SpriteFont _font;

        private string _eventTitle = string.Empty;

        private string _eventDescription = string.Empty;


        // FUNCTIONAL
        private Rectangle _background;


        // GRAPHICS 
        private Point _position;

        private int _width;

        private int _height;

        public float MessageTimer { get; set; }

        private Texture2D _uiTextures;

        private Rectangle _textureSource;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public EventMessageDisplay(GameSession session, SpriteFont font, Texture2D uiTextures)
        {
            _session = session;

            _font = font;

            MessageTimer = 2f;

            _uiTextures = uiTextures;
        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                       Load					   
        ------------------------------------------------------------------------------------------------------------
          - Class method for setting up the properties of the message display. 
        ----------------------------------------------------------------------------------------------------------*/
        public void Load(Point position)
        {
            _position = position;

            _width = 300;

            _height = 447;

            _background = new Rectangle(position.X, position.Y, _width, _height);

            _textureSource = new Rectangle(145, 251, 250, 350);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Update					   
        ------------------------------------------------------------------------------------------------------------
          - Class method for updating the display message based on RoomTile type or RoomCard event type.
            If a RoomCard is pulled, the RoomTile message will be replaced after [MessageTimer]x seconds.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            _eventTitle = $"{_session.CurrentRoom.EventName}";
            _eventDescription = WrapText(_font, $"{_session.CurrentRoom.EventText}", _width - 100);

            if (_session.CurrentRoomCard == null)
                return;
            else
            {
                if(MessageTimer > 0)
                    MessageTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                {
                    _eventTitle = $"{_session.CurrentRoomCard.EventName}";
                    _eventDescription = WrapText(_font, $"{_session.CurrentRoomCard.EventText}", _width - 100);
                    MessageTimer = 0;
                }
            }

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Draw					   
        ------------------------------------------------------------------------------------------------------------
          - Class method for drawing the event message onto the screen, upon a background texture.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_uiTextures, _background, _textureSource, Color.White);
            
            Vector2 titleSize = _font.MeasureString(_eventTitle);
            float titleX = _background.X + (_background.Width - titleSize.X) / 2; // Center the title horizontally
            spriteBatch.DrawString(_font, _eventTitle, new Vector2(titleX, _background.Y + 40), Color.Gold);

            spriteBatch.DrawString(_font, _eventDescription, new Vector2(_background.X + 55, _background.Y + 80), Color.Gold);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Wrap Text					   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for turning a long string into multiple lines based on the maximum 
            width of the display area.
        ----------------------------------------------------------------------------------------------------------*/
        private string WrapText(SpriteFont font, string text, float maxLineWidth)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            // Split the text into words
            string[] words = text.Split(' ');
            StringBuilder result = new StringBuilder();
            string currentLine = string.Empty;

            // Iterate through each word and build lines based on the maximum width
            foreach (string word in words)
            {
                string testLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;

                float width = font.MeasureString(testLine).X;

                if (width > maxLineWidth && !string.IsNullOrEmpty(currentLine))
                {
                    result.AppendLine(currentLine);
                    currentLine = word;
                }
                else
                {
                    currentLine = testLine;
                }
            }
            // Append the last line if it exists
            if (!string.IsNullOrEmpty(currentLine))
                result.AppendLine(currentLine);

            return result.ToString();
        }
        // End -----------------------------------------------------------------------------------------------------
        #endregion
    }
}
