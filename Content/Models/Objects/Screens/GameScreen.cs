using dragon_hoard.Content.Models.Managers;
using dragon_hoard.Content.Models.Objects.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dragon_hoard.Content.Models.Objects.Screens
{
    ///////////////////////////////////////////// GAME SCREEN CLASS ////////////////////////////////////////////////
    /*

        Parent class for all Game Screen objects.
        Contains necessary inheritable properties to gain access to instance resources;
        such as Content (game), fonts (via UiManager) etc. as well as methods for loading, 
        updating and drawing screen specific content.
        Also contain wrapper methods for setting state to Active/ Inactive (effectively
        prohibiting the screen to update and render anything unless 'Active') and
        callback to transition to another game screen.
        
        

    :: TODO ---------------------------

        v.001b ???

    :: DONE ---------------------------

        (2026)
        v.001a (09/24) Core methods and properties

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public abstract class GameScreen(MainGame game, GameScreenManager screenManager, UiManager ui)
    {

        //========================================== PROPERTIES ==================================================//
        internal MainGame _game = game;

        internal GameScreenManager _screenManager = screenManager;

        internal UiManager _ui = ui;

        internal bool IsContentLoaded { get; set; } = false;

        internal bool IsActive {  get; set; }

        public bool RenderOnPause { get; set; }

        internal string Description { get; set; }

        internal UiElement[] ScreenUi { get; set; }

        //============================================ METHODS ===================================================//
        public abstract void LoadContent();

        public abstract void Update(GameTime gameTime);

       

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_game.DebugMode)
            {
                DebugScreen(spriteBatch);
            }
        }


        /*----------------------------------------------------------------------------------------------------------
                                                 Debug Screen					   
        -------------------------------------------------------------------------------------------------------------
            - Method for handling the drawing of any screen specific debugging information is 
                placed here, then called from the main 'Draw' method if the ScreenManager's 
                'DebugMode' variable is set to 'True'.
        ----------------------------------------------------------------------------------------------------------*/
        public abstract void DebugScreen(SpriteBatch spriteBatch);



       /*----------------------------------------------------------------------------------------------------------
                                                 Load Next Screen					   
       ------------------------------------------------------------------------------------------------------------
          - Callback method utilized by Game Screens to inform GameScreenManager to transition to another
               game screen.
       ----------------------------------------------------------------------------------------------------------*/
        protected internal virtual void LoadNextScreen(GameScreen nextScreen)
        {
            _screenManager.SetNextScreen(nextScreen);
            _screenManager.ScreenTransition = true; 
            //_screenManager.LoadScreen(nextScreen);
            
        }


        /*----------------------------------------------------------------------------------------------------------
                                                  Set Active					   
        ------------------------------------------------------------------------------------------------------------
           - Wrapper method utilized by GameScreenManager to activate the current screen.
        ----------------------------------------------------------------------------------------------------------*/
        public void SetActive() => IsActive = true;


        /*----------------------------------------------------------------------------------------------------------
                                                   Set Inactive					   
        ------------------------------------------------------------------------------------------------------------
           - Wrapper method utilized by GameScreenManager to inactivate the current screen.
        ----------------------------------------------------------------------------------------------------------*/
        public void SetInactive() => IsActive = false;



    }
}
