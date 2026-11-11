using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace dragon_hoard.Content.Models.Objects.UIElements
{
    /////////////////////////////////////////// SCROLLBAR CLASS ////////////////////////////////////////////////////
    /*

    :: TODO ---------------------------
        - Scrollbar class with standard horizontal / vertical scroll functionality.
            Child class of the abstract 'UiElement' class.
            - Functionality can be toggled On/Off (Clickable)
            - Visibility can be switched On/ Off.
            - Can have solid color background or texture
            - Fontcolor and textpositioning.

    :: DONE ---------------------------
        (2026)
        v.001a (11/10) Setup of core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public sealed class Scrollbar : UiElement
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
         * BitmapFont Font
        */


        // DATA
        public ScrollOrientation Orientation { get; set; }

        public float ScrollValue { get; private set; }

        public float ContentSize { get; set; }

        public float ViewSize { get; set; }

        private int _dragOffset;


        // FUNCTIONAL
        private bool _isDragging;

        private bool _isHovered;


        // GRAPHICS 
        public Color TrackColor { get; set; } = Color.DarkGray;

        public Color ThumbColor { get; set; } = Color.LightGray;

        public Color HoverColor { get; set; } = Color.White;

        private Rectangle _thumbBounds;



        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public Scrollbar(UiManager uiManager, Rectangle rect, bool visible, ScrollOrientation orientation) : base(uiManager, rect, visible)
        {
            BoundingBox = rect;

            Orientation = orientation;

            Background = uiManager.RectangleTexture;

            UpdateThumb();
        }


        /*----------------------------------------------------------------------------------------------------------
                                                      Update					   
        ------------------------------------------------------------------------------------------------------------
            - Class main method for updating the state of the sliderbar and calculate the position of the
                'slider thumb' in real time.
        ----------------------------------------------------------------------------------------------------------*/
        public override void Update(GameTime gameTime)
        {
            Point mousePos = _ui.Mouse.Position;

            _isHovered = _thumbBounds.Contains(mousePos);

            if ( _ui.Mouse.LeftButton == ButtonState.Pressed)
            {
                if (!_isDragging && _thumbBounds.Contains(mousePos))
                {
                    _isDragging = true;

                    _dragOffset = Orientation == ScrollOrientation.Vertical
                        ? _ui.Mouse.Y - _thumbBounds.Y
                        : _ui.Mouse.X - _thumbBounds.X;
                        
                }

                if (_isDragging)
                {
                    // Dragging vertically
                    if (Orientation == ScrollOrientation.Vertical)
                    {
                        int newY = mousePos.Y - _dragOffset;

                        float travel = BoundingBox.Height - _thumbBounds.Height;

                        if (travel > 0)
                            ScrollValue = MathHelper.Clamp((float)(newY - BoundingBox.Y) / travel, 0f, 1f);
                    }
                    // Dragging horizontally
                    else
                    {
                        int newX = mousePos.X - _dragOffset;

                        float travel = BoundingBox.Width - _thumbBounds.Width;

                        if (travel > 0)
                            ScrollValue = MathHelper.Clamp(((float)(newX - BoundingBox.X) / travel), 0f, 1f);
                    }

                    // Update the position of the 'slider thumb'.
                    UpdateThumb();
                }
            }
            else
                _isDragging = false;
        }// End ---------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                      Draw					   
        ------------------------------------------------------------------------------------------------------------
         - Override of derived method. Draws the slider- / scrollbar and the 'slider thumb' onto the screen.
            The color of the 'slider thumb' changes depending on 'dragged' state.
        ----------------------------------------------------------------------------------------------------------*/
        public override void Draw(SpriteBatch spriteBatch)
        {
            // If the GameScreen size is less than the size of the Viewport, we hide the bar.
            if (ContentSize <= ViewSize)
                SetInvisible();


            spriteBatch.Draw(_ui.RectangleTexture, BoundingBox, TrackColor);

            Color color = _isHovered || _isDragging ? HoverColor : ThumbColor;

            spriteBatch.Draw(_ui.RectangleTexture, _thumbBounds, color);
        }// End ---------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                     Draw					   
       ------------------------------------------------------------------------------------------------------------
        - Override of derived method. Not used in this instance. TODO use this for Texturized 
            visual representation.
       ----------------------------------------------------------------------------------------------------------*/
        public override void Draw(SpriteBatch spriteBatch, Rectangle sourceRect)
        {
            throw new System.NotImplementedException();
        }// End ---------------------------------------------------------------------------------------------------


        //============================================ METHODS ===================================================//
        #region methods

        
        /*----------------------------------------------------------------------------------------------------------
                                                      Set Scroll Value					   
        ------------------------------------------------------------------------------------------------------------
            - Class specific method. Allows the scroll value to be preset. Utilized to focus the Viewport on the
                current position of the Player character MapPin.
        ----------------------------------------------------------------------------------------------------------*/
        public void SetScrollValue(float value)
        {
            ScrollValue = MathHelper.Clamp(value, 0f, 1f);
            if (Orientation == ScrollOrientation.Vertical)
                UpdateVerticalThumb();

            else
                UpdateHorizontalThumb();
        }// End ---------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Update Thumb					   
        ------------------------------------------------------------------------------------------------------------
            - Class support method. Delegates to sub methods to update the 'slider thumbs' on the sliderbars,
                depending on orientation (horizontal or vertical bar).
        ----------------------------------------------------------------------------------------------------------*/
        private void UpdateThumb()
        {
            if (Orientation == ScrollOrientation.Vertical)
                UpdateVerticalThumb();

            else 
                UpdateHorizontalThumb();
        }// End ---------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                      Update Vertical Thumb					   
        ------------------------------------------------------------------------------------------------------------
            - Class support method. Updates the position, size and scroll value of the 'slider thumb' for 
                a vertical bar.
        ----------------------------------------------------------------------------------------------------------*/
        private void UpdateVerticalThumb()
        {
            float ratio = ViewSize / ContentSize;

            int thumbHeight = Math.Max(20, (int)(BoundingBox.Height * ratio));

            thumbHeight = Math.Min(thumbHeight, BoundingBox.Height);

            int travel = BoundingBox.Height - thumbHeight;

            int thumbY = BoundingBox.Y + (int)(travel * ScrollValue);

            _thumbBounds = new Rectangle(BoundingBox.X, thumbY, BoundingBox.Width, thumbHeight);
        }// End ---------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                      Update Horizontal Thumb					   
        ------------------------------------------------------------------------------------------------------------
           - Class support method. Updates the position, size and scroll value of the 'slider thumb' for 
                a horizontal bar.
        ----------------------------------------------------------------------------------------------------------*/
        private void UpdateHorizontalThumb()
        {
            float ratio = ViewSize / ContentSize;

            int thumbWidth = Math.Max(20, (int)(BoundingBox.Width * ratio));

            thumbWidth = Math.Min(thumbWidth, BoundingBox.Width);

            int travel = BoundingBox.Width - thumbWidth;

            int thumbX = BoundingBox.X + (int)(travel * ScrollValue);

            _thumbBounds = new Rectangle(thumbX, BoundingBox.Y, thumbWidth, BoundingBox.Height);
        }// End ---------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                       Method					   
        ------------------------------------------------------------------------------------------------------------
          - Description
        ----------------------------------------------------------------------------------------------------------*/



        // End ---------------------------------------------------------------------------------------------------
    }
    #endregion
}
