using dragon_hoard.Content.Models.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragon_hoard.Content.Models.Objects.Screens
{
    /////////////////////////////////////////// LOAD SAVED GAME SCREEN CLASS ////////////////////////////////////
    /*

        Load a saved game menu.
            Child class of the GameScreen abstract class.
            Utilizes various UI class objects to display and provide functionality for 
            loading and erasing previously saved games;


    :: TODO ---------------------------
        
        v.001b (09/??) Setup test UI objects.

    :: DONE ---------------------------
        (2026)
        v.001a (09/27) Setup of basic properties and methods for core functionality.
        

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class LoadSaveMenu : GameScreen
    {
        //========================================== PROPERTIES ==================================================//


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public LoadSaveMenu(MainGame game, GameScreenManager screenManager, UiManager ui) : base(game, screenManager, ui)
        {
            IsActive = false;

            Description = "Load Save Menu";

            RenderOnPause = false;
        }



        //============================================ METHODS ===================================================//
        #region methods

        /*----------------------------------------------------------------------------------------------------------
                                                   Load Content					   
        ------------------------------------------------------------------------------------------------------------
            - Override of derived method. Loads the screen specific content and places the UI elements into 
                the array.
        ----------------------------------------------------------------------------------------------------------*/
        public override void LoadContent()
        {
            
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                   Update				   
        ------------------------------------------------------------------------------------------------------------
            - Override of derived method. Updates the screen specific content, including UI elements, provided the
                splash screen is set to 'Active'.
        ----------------------------------------------------------------------------------------------------------*/
        public override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                // Update the UI if there are any elements in the array
                //if (ScreenUi.Length > 0)
                //{
                //    for (int i = 0; i < ScreenUi.Length; i++)
                //    {
                //        ScreenUi[i].Update(gameTime);
                //    }
                //}
            }

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                  Draw					   
        ------------------------------------------------------------------------------------------------------------
           - Override of derived method. Draws the screen specific content, including UI elements onto the game screen,
                 provided the splash screen is set to 'Active'.
        ----------------------------------------------------------------------------------------------------------*/
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                // Update the UI if there are any elements in the array
                //if (ScreenUi.Length > 0)
                //{
                //    for (int i = 0; i < ScreenUi.Length; i++)
                //    {
                //        ScreenUi[i].Draw(spriteBatch);
                //    }
                //}
            }
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                  Debug Screen				   
        ------------------------------------------------------------------------------------------------------------
           - Override of derived method. Draws the screen specific debugging information on the game screen.
        ----------------------------------------------------------------------------------------------------------*/
        public override void DebugScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_ui.DebugText, $"Content loaded: {IsContentLoaded}",
                new Vector2(_game.GraphicsDevice.Viewport.Width / 2, 100), Color.White);
        }// End -----------------------------------------------------------------------------------------------------
    }

    #endregion
}
