using dragon_hoard.Content.Models.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace dragon_hoard.Content.Models.Objects.UIElements
{
    public class TestButton : UiElement
    {

        private Vector2 _textPos;

        private string _text;

        public bool Clicked { get; private set; }

        public TestButton(UiManager uiManager, Rectangle rect, bool visible) :base(uiManager, rect, visible)
        {
            Background = uiManager.RectangleTexture;
        }

        public void Initialize(Color bgColor, string text, Color fontColor, Vector2 fontPos, bool clickable)
        {
            BackgroundColor = bgColor;

            _text = text;

            FontColor = fontColor;

            _textPos = fontPos;

            IsClickable = clickable;

            Clicked = false;
        }


        public override void Update(GameTime gameTime)
        {
            if(BoundingBox.Contains(_ui.MousePos) && _ui.Mouse.LeftButton == ButtonState.Pressed)
            {
                Clicked = true;
                /* Run animation, play click sound etc. */
               
            }
            Clicked = false;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(IsVisible)
            {
                spriteBatch.Draw(Background, BoundingBox, BackgroundColor);
                spriteBatch.DrawString(_ui.DebugText, _text, _textPos, FontColor);
            }
           
        }      
        
        //public void IsClicked() => Clicked = true;

        //public void ResetClicked() => Clicked = false;
    }
}
