using dragon_hoard.Content.Models.Objects.Entities.Cards;
using dragon_hoard.Content.Services;
using System;
using System.Collections.Generic;

namespace dragon_hoard.Content.Models.Managers
{
    ///////////////////////////////////////// ROOM CARD STACK CLASS ///////////////////////////////////////////////
    /*
        Represents a "stack" of RoomCards and handles the functionality surrounding them, such as
        pulling a RoomCard, reshuffling the stack of cards etc.
        

    :: TODO ---------------------------

       * Add functionality for loading all the cards from the database via service & repository
       * CardImages: Load all cards from db. Then foreach card in list load the CardImage (Texture2D) by its [TextureReference]
        
        ex. foreach (RoomCard card in roomCards)
        {
            card.CardImage = Content.Load<Texture2D>(card.TextureReference);
        }


    :: DONE ---------------------------

        (2026)
         v.001a (v.001a (10/16) Setup of basic core properties and methods.

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RoomCardStack
    {
        //========================================== PROPERTIES ==================================================//
        public Stack<RoomCard> CardStack { get; private set; }

        private Random _randomizer;

        private int _totalCardCount;

        private readonly RoomCardService _service;

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public RoomCardStack(RoomCardService service, Random randomizer, int totalCardCount)
        {
            _service = service;

            _randomizer = randomizer;

            _totalCardCount = totalCardCount;
        }

        //============================================ METHODS ===================================================//
        #region methods

        /*----------------------------------------------------------------------------------------------------------
                                                 Initialize				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. Loads all the RoomCards and delegates the creation of the stack of
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
         - Class specific method. Fetches all RoomCard objects from the database.
            Places them in a temporary list then uses for x[_totalCardCount] amount of times
            randomly picks a card to push onto the card stack.
            IMPORTANT! [_totalCardCount] must not exceed the number of actual RoomCard objects in the database.
        ----------------------------------------------------------------------------------------------------------*/
        private void CreateStack()
        {

            CardStack = new Stack<RoomCard>();

            List<RoomCard> cards = new List<RoomCard>();

            // Fetch the cards from the database.
            cards.AddRange(_service.GetAllRoomCards());

            // Pick a random card from the list and push it onto the stack.
            // Remove the card from the list so it won't be picked again. Rinse repeat.
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
