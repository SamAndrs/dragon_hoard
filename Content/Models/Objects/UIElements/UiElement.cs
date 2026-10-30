using dragon_hoard.Content.Models.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragon_hoard.Content.Models.Objects.UIElements
{

    ///////////////////////////////////////////// UI ELEMENT CLASS /////////////////////////////////////////////////
    /*
        Parent class for all UI element classes such as buttons, menues etc.
        Contains inheritable properties and methods common across the board of subclasses.

    :: TODO ---------------------------


    :: DONE ---------------------------

        (2026)
        v.001a (09/25)  Setup of core properties and methods for update and draw.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public abstract class UiElement(UiManager uiManager, Rectangle rect, bool visible)
    {

        //========================================== PROPERTIES ==================================================//

        // FUNCTIONAL
        internal Rectangle BoundingBox { get; set; } = rect;        // Bounding box of the UI Object

        internal bool IsClickable { get; set; }                     // On/Off for when/ if the UI Object can be clicked.

        internal UiManager _ui = uiManager;                         // Reference to UIManager for access to Fonts etc.

        // VISUAL
        internal bool IsVisible { get; private set; } = visible;             // Option to turn object visibility On/Off 

        internal Texture2D Background { get; set; } = null; // If object is an image, textured button etc.

        internal Color BackgroundColor { get; set; }        // If object is to have a color rather than image.

        internal Color FontColor { get; set; }              // Main color of the font.

        //============================================ METHODS ===================================================//

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);


        public void SetVisibility() => IsVisible = true;

        public void SetInvisible() => IsVisible = false;


        public void SetClickable() => IsClickable = true;

        public void SetNonClickable() => IsClickable = false;

    }
}
