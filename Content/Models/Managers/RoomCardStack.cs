using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragon_hoard.Content.Models.Managers
{
    ///////////////////////////////////////// ROOM CARD STACK CLASS ///////////////////////////////////////////////
    /*
        Represents a "stack" of RoomCards and handles the functionality surrounding them, such as
        pulling a RoomCard, reshuffling the stack of cards etc.
        

    :: TODO ---------------------------

        v.001a (v.001a (10/16) Setup of basic core properties and methods.


    :: DONE ---------------------------

        (2026)
        

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RoomCardStack
    {
        //========================================== PROPERTIES ==================================================//
        public Stack<RoomCard> CardStack { get; private set; }

        private UiManager _ui;

        private Random _randomizer;

        private int _totalCardCount;

        // CARD IMAGES
        private Texture2D _emptyRoom;
        
        // Containers
        private Texture2D _chest;
        private Texture2D _sSarcophagus;
        private Texture2D _fallenWarrior;

        // Traps
        private Texture2D _pitTrap;
        private Texture2D _spearTrap;
        private Texture2D _caveIn;
        private Texture2D _wallArrows;

        // Damage
        private Texture2D _vampirBats;

        // Ambushes
        private Texture2D _ambushSkeleton;
        private Texture2D _ambushOgre;
        private Texture2D _ambushOrc;


        // Monsters (static)
        private Texture2D _giantSpider;
        private Texture2D _poisonCaterpillar;
        private Texture2D _corpseEaters;

        // Monsters (dynamic)
        private Texture2D _darkElf;
        private Texture2D _ogre;
        private Texture2D _skeleton;
        private Texture2D _orc;
        private Texture2D _twoOrcs;

        // Special
        private Texture2D _torchOut;
        private Texture2D _curseMadMage;
        private Texture2D _pursuingShadow;
        // private Texture2D _wayDown;
        // private Texture2D _reshuffle;


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public RoomCardStack(UiManager uiManager, Random randomizer, int totalCardCount)
        {
            _ui = uiManager;
            _randomizer = randomizer;

            _totalCardCount = totalCardCount;
        }

        //============================================ METHODS ===================================================//
        #region methods

        /*----------------------------------------------------------------------------------------------------------
                                                 Initialize				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method.
        ----------------------------------------------------------------------------------------------------------*/
        public void Initialize()
        {
            CreateStack();

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                Pull Card				   
        ------------------------------------------------------------------------------------------------------------
         - Class specific method.
        ----------------------------------------------------------------------------------------------------------*/
        public RoomCard PullCard()
        {
            if(CardStack.Count > 0)
                return CardStack.Pop();

            return null;
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                Create Stack				   
        ------------------------------------------------------------------------------------------------------------
         - Class specific method.
        ----------------------------------------------------------------------------------------------------------*/
        private void CreateStack()
        {

            CardStack = new Stack<RoomCard>();
            // 1) Create all cards/ fetch all cards from database and put into temp list
            // 2) Pick cards from temp list at random and place into Stack.

            List<RoomCard> cards = new List<RoomCard>();

            int cardNumber = 0;
            for (int i = 0; i < _totalCardCount; i++)
            {
                cards.Add(new RoomCard(cardNumber, _ui.RectangleTexture, RoomCardEvent.Empty));
                cardNumber++;
            }

            // Pick random cards from the list and push them onto the stack
            for (int i = 0; i < _totalCardCount; i++)
            {
                // Pick a random card
                int result = _randomizer.Next(cards.Count);
                RoomCard getCard = cards[result];
                // Push the card onto the stack
                CardStack.Push(getCard);
                // Remove the card from the list
                cards.RemoveAt(result);
            }


        }// End -----------------------------------------------------------------------------------------------------
        #endregion
    }
}
