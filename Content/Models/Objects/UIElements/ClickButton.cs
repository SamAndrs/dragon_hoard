using dragon_hoard.Content.Models.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragon_hoard.Content.Models.Objects.UIElements
{
    ///////////////////////////////////////////// CLICK BUTTON CLASS //////////////////////////////////////////////
    /*
        - Button class with standard click functionality.
            Child class of the abstract 'UiElement' class.
            - Functionality can be toggled On/Off (Clickable)
            - Can play animation sequence upon clicked.
            - Visibility can be switched On/ Off.
            - Can have solid color background or texture
            - Fontcolor and textpositioning.


    :: TODO ---------------------------


    :: DONE ---------------------------

        (2026)
        v.001a (09/29)  Setup of core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class ClickButton : UiElement
    {

        //========================================== PROPERTIES ==================================================//

        /* INHERITED
         * Rectangle BoundingBox
         * Texture2D Background
         * Color BackgroundColor
         * Color FontColor
         * bool Clickable
         * bool IsVisible
         * UiManager _ui 
        */

        private Vector2 _textPos;

        private string _text = string.Empty;

        public bool Clicked { get; private set; }

        public bool AnimDone { get; set; }                // Flag for animation being done or not.

        private float _animTimer;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public ClickButton(UiManager uiManager, Rectangle rect, bool visible) :base(uiManager, rect, visible)
        {
            Background = uiManager.RectangleTexture; // Utilizes UIManager pixel to draw solid color background
        }


        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                   Initialize					   
        ------------------------------------------------------------------------------------------------------------
            - Class specific method for setting additional property values
        ----------------------------------------------------------------------------------------------------------*/
        public void Initialize(Color bgColor, string text, Color fontColor, Vector2 fontPos, bool clickable, float animTimer)
        {
            BackgroundColor = bgColor;

            _text = text;
            FontColor = fontColor;
            _textPos = fontPos;

            IsClickable = clickable;
            Clicked = false;

            AnimDone = false;

            if (animTimer <= 0)
                _animTimer = 0.1f;  // set default value
            else
                _animTimer = animTimer;           
        }



        /*----------------------------------------------------------------------------------------------------------
                                                   Update					   
        ------------------------------------------------------------------------------------------------------------
            - Override method utilized as 'callback' (i.e ClickEvent) if the mouse pointer is within
                the box of the button, and LMB is clicked
        ----------------------------------------------------------------------------------------------------------*/
        public override void Update(GameTime gameTime)
        {
            if(IsClickable)
            {
                if (BoundingBox.Contains(_ui.MousePos) && _ui.Mouse.LeftButton == ButtonState.Released
                    && _ui.PrevMouse.LeftButton == ButtonState.Pressed)
                {
                    Clicked = true;
                }
                else Clicked = false;
            }
        }// End -----------------------------------------------------------------------------------------------------



        public override void Draw(SpriteBatch spriteBatch)
        {
            if(IsVisible)
            {
                if(IsClickable)
                {
                    spriteBatch.Draw(Background, BoundingBox, BackgroundColor);
                    spriteBatch.DrawString(_ui.DebugText, _text, _textPos, FontColor);
                }
                else
                {
                    spriteBatch.Draw(Background, BoundingBox, Color.DarkGray);
                    spriteBatch.DrawString(_ui.DebugText, _text, _textPos, FontColor);
                }
            }
        }// End -----------------------------------------------------------------------------------------------------



        public void ResetButton()
        {
            Clicked = false;
        }// End -----------------------------------------------------------------------------------------------------


        #endregion


    }
}
