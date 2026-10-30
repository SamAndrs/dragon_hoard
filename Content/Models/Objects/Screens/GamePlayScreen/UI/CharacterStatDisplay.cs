using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Net.Mime.MediaTypeNames;

namespace dragon_hoard.Content.Models.Objects.Screens.GamePlayScreen.UI
{
    /////////////////////////////////////// CHARACTER STAT DISPLAY CLASS /////////////////////////////////////////////
    /*
                    
         Child UI class to the GamePlayScreen. Handles everything related to the displaying a visual
            bar of icons representing a character's name, abilities (and their respective scores) 
            and collected amount of gold coins.

    :: TODO ---------------------------


    :: DONE ---------------------------
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

        private SpriteFont _font;

        // FUNCTIONAL
        private ClickButton _btnSTR;
        
        private ClickButton _btnDEX;
        
        private ClickButton _btnARM;
        
        private ClickButton _btnLCK;


        // GRAPHICS
        private Point _position;

        private int _buttonSize;

        private Texture2D _imgSTR;
        
        private Texture2D _imgDEX;
        
        private Texture2D _imgARM;
        
        private Texture2D _imgLCK;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public CharacterStatDisplay(UiManager ui, GameSession session)
        {
            _ui = ui;

            _session = session;
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
            _font = _ui.Text;

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
            Vector2 nameLength = _font.MeasureString(_charName) / 2;
            float centeredName = _position.X + _buttonSize * 2 - nameLength.X;
            spriteBatch.DrawString(_font, $"{_charName}", new Vector2(centeredName, _position.Y - 25), Color.Gold);


            _btnSTR.Draw(spriteBatch);
            
            _btnDEX.Draw(spriteBatch);
            
            _btnARM.Draw(spriteBatch);
            
            _btnLCK.Draw(spriteBatch);

                       
            // Draw character gold amount
            spriteBatch.DrawString(_font, "Gold:", new Vector2(_position.X, _position.Y + (_buttonSize + 5)), Color.Gold);
            spriteBatch.DrawString(_font, _charGold, new Vector2(_position.X + 50, _position.Y + (_buttonSize + 5)), Color.White);


        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                      Load textures					   
        ------------------------------------------------------------------------------------------------------------
         - Class support method for loading the class specific related texture content.
        ----------------------------------------------------------------------------------------------------------*/
        private void LoadTextures()
        {
            _imgSTR = _ui.RectangleTexture;     //_game.Content.Load<Texture2D>("");    //TODO
            
            _imgDEX = _ui.RectangleTexture;     //_game.Content.Load<Texture2D>("");    //TODO
            
            _imgARM = _ui.RectangleTexture;     //_game.Content.Load<Texture2D>("");    //TODO
            
            _imgLCK = _ui.RectangleTexture;     //_game.Content.Load<Texture2D>("");    //TODO
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
            _btnSTR.Initialize(Color.White, $"{_character.Strength}", Color.Black, new Vector2(position.X + 5, position.Y + 5), true, 0);

            float dexX = position.X + (btnSize * 1 + 5);
            _btnDEX = new ClickButton(_ui, new Rectangle((int)dexX, position.Y, btnSize, btnSize), true);
            _btnDEX.Initialize(Color.White, $"{_character.Dexterity}", Color.Black, new Vector2(dexX + 5, position.Y + 5), true, 0);

            float armX = position.X + (btnSize * 2 + 10);
            _btnARM = new ClickButton(_ui, new Rectangle((int)armX, position.Y, btnSize, btnSize), true);
            _btnARM.Initialize(Color.White, $"{_character.Armor}", Color.Black, new Vector2(armX + 5, position.Y + 5), true, 0);

            float lckX = position.X + (btnSize * 3 + 15);
            _btnLCK = new ClickButton(_ui, new Rectangle((int)lckX, position.Y, btnSize, btnSize), true);
            _btnLCK.Initialize(Color.White, $"{_character.Luck}", Color.Black, new Vector2(lckX + 5, position.Y + 5), true, 0);
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
