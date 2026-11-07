using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI
{
    /////////////////////////////////////// CHARACTER STAT DISPLAY CLASS /////////////////////////////////////////////
    /*
                    
         Child UI class to the GamePlayScreen. Handles everything related to the displaying a visual
            bar of icons representing a character's name, abilities (and their respective scores) 
            and collected amount of gold coins.

    :: TODO ---------------------------


    :: DONE ---------------------------
        v.001b (11/01) Added textures to buttons.
        v.001a (10/22) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class CharacterStatDisplay
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        private UiManager _ui;

        private GameSession _session;

        private PlayerCharacter _character;

        private string _charName;

        private string _charGold;

        private SpriteFont _textFont;

        private BitmapFont _scoreFont;

        // FUNCTIONAL
        private ClickButton _btnSTR;
        
        private ClickButton _btnDEX;
        
        private ClickButton _btnARM;
        
        private ClickButton _btnLCK;


        // GRAPHICS
        private Point _position;

        private int _buttonSize;

        private Texture2D _uiTextures;

        private Rectangle _strSourceRect;

        private Rectangle _dexSourceRect;

        private Rectangle _armSourceRect;
        
        private Rectangle _lckSourceRect;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public CharacterStatDisplay(UiManager ui, GameSession session, Texture2D uiTextures)
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
         - Class method for loading and initiating the bar of character stat buttons.
        ----------------------------------------------------------------------------------------------------------*/
        public void Load(PlayerCharacter character, Point position, int buttonSize)
        {
            _textFont = _ui.Text;

            _scoreFont = _ui.Digits;

            _character = character;

            _charName = character.Name;

            _charGold = character.Gold.ToString();

            _position = position;

            _buttonSize = buttonSize;

            LoadTextures();

            LoadButtons(_position, _buttonSize);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Update					   
        ------------------------------------------------------------------------------------------------------------
         - Class method to update represented player character property values.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            _charGold = _character.Gold.ToString();
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Draw					   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for drawing the various player character stats and other assosciated
            information, on the screen.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            // Center the character name above the array of stat icons.
            Vector2 nameLength = _textFont.MeasureString(_charName) / 2;
            float centeredName = _position.X + _buttonSize * 2 - nameLength.X;
            spriteBatch.DrawString(_textFont, $"{_charName}", new Vector2(centeredName, _position.Y - 25), Color.Gold);

            
            _btnSTR.Draw(spriteBatch, _strSourceRect);
            
            _btnDEX.Draw(spriteBatch, _dexSourceRect);
            
            _btnARM.Draw(spriteBatch, _armSourceRect);
            
            _btnLCK.Draw(spriteBatch, _lckSourceRect);

                       
            // Draw character gold amount
            spriteBatch.DrawString(_textFont, "Gold:", new Vector2(_position.X, _position.Y + (_buttonSize + 5)), Color.Gold);
            spriteBatch.DrawString(_textFont, _charGold, new Vector2(_position.X + 50, _position.Y + (_buttonSize + 5)), Color.White);

        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                      Load textures					   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for loading the class specific related texture content.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadTextures()
        {
            _strSourceRect = new Rectangle(0, 70, _buttonSize, _buttonSize);

            _dexSourceRect = new Rectangle(67, 70, _buttonSize, _buttonSize);

            _armSourceRect = new Rectangle(136, 70, _buttonSize, _buttonSize);

            _lckSourceRect = new Rectangle(206, 70, _buttonSize, _buttonSize);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Load Buttons					   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for creating and initiating the buttons that acts as
            character stat representations. While the iconbuttons are clickable, they act simply as
            static visual representations.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadButtons(Point position, int btnSize)
        {
            _btnSTR = new ClickButton(_ui, new Rectangle(position.X, position.Y, btnSize, btnSize), true);
            _btnSTR.Initialize(Color.White, _character.Strength.ToString(), _scoreFont, Color.White, 
                CenterScoreValue(_character.Strength.ToString(), _scoreFont, _btnSTR.BoundingBox), true, 0);
            
            _btnSTR.Background = _uiTextures;

            float dexX = position.X + (btnSize + 5) * 1;
            _btnDEX = new ClickButton(_ui, new Rectangle((int)dexX, position.Y, btnSize, btnSize), true);
            _btnDEX.Initialize(Color.White, _character.Dexterity.ToString(), _scoreFont, Color.White,
                CenterScoreValue(_character.Dexterity.ToString(), _scoreFont, _btnDEX.BoundingBox), true, 0);
            
            _btnDEX.Background = _uiTextures;

            float armX = position.X + (btnSize + 5) * 2;
            _btnARM = new ClickButton(_ui, new Rectangle((int)armX, position.Y, btnSize, btnSize), true);
            _btnARM.Initialize(Color.White, _character.Armor.ToString(), _scoreFont, Color.White,
                CenterScoreValue(_character.Armor.ToString(), _scoreFont, _btnARM.BoundingBox), true, 0);

            _btnARM.Background = _uiTextures;

            float lckX = position.X + (btnSize + 5) * 3;
            _btnLCK = new ClickButton(_ui, new Rectangle((int)lckX, position.Y, btnSize, btnSize), true);
            _btnLCK.Initialize(Color.White, _character.Luck.ToString(), _scoreFont, Color.White,
                CenterScoreValue(_character.Luck.ToString(), _scoreFont, _btnLCK.BoundingBox), true, 0);

            _btnLCK.Background = _uiTextures;
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Center Score Value					   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for centering the score value within the button bounding box.
        ----------------------------------------------------------------------------------------------------------*/
        private Vector2 CenterScoreValue(string value, BitmapFont font, Rectangle boundingBox)
        {
            Vector2 vector2 = font.MeasureString(value) / 2;
            float centeredX = boundingBox.X + (boundingBox.Width / 2) - vector2.X;

            return new Vector2(centeredX, boundingBox.Y + (boundingBox.Height / 2) - vector2.Y);
        }// End -----------------------------------------------------------------------------------------------------

        #endregion
    }
}
