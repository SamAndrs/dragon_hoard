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

        private UiManager _ui;      //TODO Remove reference later?

        private MainGame _game;

        private Random _randomizer;

        private int _totalCardCount;


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
          - Class specific method. Loads all the necessary textures and delegates the creation of the stack of
            RoomCards to sub-method.
        ----------------------------------------------------------------------------------------------------------*/
        public void Initialize()
        {

            CreateStack();

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                Pull Card				   
        ------------------------------------------------------------------------------------------------------------
         - Class specific method. Returns the topmost RoomCard of the stack of cards.
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
         - Class specific method. TODO: Fetch all cards in database.
            Places all available cards in a temporary list then uses for x[_totalCardCount] amount of times
            randomly picks a card to push onto the card stack.
            IMPORTANT! [_totalCardCount] must not exceed the number of actual RoomCard objects in the database.
        ----------------------------------------------------------------------------------------------------------*/
        private void CreateStack()
        {

            CardStack = new Stack<RoomCard>();
            // 1) Create all cards/ fetch all cards from database and put into temp list
            // 2) Pick cards from temp list at random and place into Stack.

            List<RoomCard> cards = new List<RoomCard>();

            //int cardNumber = 0;
            //for (int i = 0; i < _totalCardCount; i++)
            //{
            //    cards.Add(new RoomCard(cardNumber, _ui.RectangleTexture, RoomCardEvent.Empty));
            //    cardNumber++;
            //}
            cards.Add(new RoomCard(1, RoomCardEvent.PursuingShadow));
            cards.Add(new RoomCard(2, RoomCardEvent.CurseOfMadMage));
            cards.Add(new RoomCard(3, RoomCardEvent.TorchOut));
            cards.Add(new RoomCard(4, RoomCardEvent.TwoOrcs));
            cards.Add(new RoomCard(5, RoomCardEvent.Orc));
            cards.Add(new RoomCard(6, RoomCardEvent.Skeleton));
            cards.Add(new RoomCard(7, RoomCardEvent.Ogre));
            cards.Add(new RoomCard(8, RoomCardEvent.Darkelf));
            cards.Add(new RoomCard(9, RoomCardEvent.CorpseEaters));
            cards.Add(new RoomCard(10, RoomCardEvent.PosionCaterpillar));
            cards.Add(new RoomCard(11, RoomCardEvent.GiantSpider));
            cards.Add(new RoomCard(12, RoomCardEvent.AmbushSkeleton));
            cards.Add(new RoomCard(13, RoomCardEvent.AmbushOgre));
            cards.Add(new RoomCard(14, RoomCardEvent.AmbushOrc));
            cards.Add(new RoomCard(15, RoomCardEvent.VampireBats));
            cards.Add(new RoomCard(16, RoomCardEvent.TrapDoor));
            cards.Add(new RoomCard(17, RoomCardEvent.SpearTrap));
            cards.Add(new RoomCard(18, RoomCardEvent.CaveIn));
            cards.Add(new RoomCard(19, RoomCardEvent.WallArrows));
            cards.Add(new RoomCard(20, RoomCardEvent.FallenWarrior));
            cards.Add(new RoomCard(21, RoomCardEvent.Sarcophagus));
            cards.Add(new RoomCard(22, RoomCardEvent.Chest));
            cards.Add(new RoomCard(23, RoomCardEvent.Empty));
            cards.Add(new RoomCard(24, RoomCardEvent.Empty));
            cards.Add(new RoomCard(25, RoomCardEvent.Empty));

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
