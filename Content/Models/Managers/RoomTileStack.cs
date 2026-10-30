using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Objects.Entities;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
using dragon_hoard.Content.Services;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace dragon_hoard.Content.Models.Managers
{
    ///////////////////////////////////////// ROOM TILE STACK CLASS /////////////////////////////////////////////////
    /*
        Represents a "stack" of RoomTiles and handles the functionality surrounding them, such as
        pulling a RoomTile, reshuffling the stack of cards etc.
        

    :: TODO ---------------------------


    :: DONE ---------------------------

        (2026)
        v.001a (v.001a (10/17) Setup of basic core properties and methods.
        

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class RoomTileStack
    {
        //========================================== PROPERTIES ==================================================//

        private MainGame _game;

        private readonly RoomTileService _service;

        public Stack<RoomTile> TileStack { get; private set; }

        private Random _randomizer;

        private int _totalTileCount;

        

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public RoomTileStack(RoomTileService service, Random random, int totalCount)
        {
            _service = service;

            _randomizer = random;

            _totalTileCount = totalCount;
        }

        //============================================ METHODS ===================================================//
        #region methods

        /*----------------------------------------------------------------------------------------------------------
                                                 Initialize				   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method. Loads all the necessary textures and delegates the creation of the stack of
            RoomTiles to sub-method.
        ----------------------------------------------------------------------------------------------------------*/
        public void Initialize()
        {
           CreateStack();

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                Pull Tile				   
        ------------------------------------------------------------------------------------------------------------
         - Class specific method. Returns the topmost RoomTile of the stack of cards.
        ----------------------------------------------------------------------------------------------------------*/
        public RoomTile PullTile()
        {
            if (TileStack.Count > 0)
                return TileStack.Pop();

            return null;
        }// End -----------------------------------------------------------------------------------------------------

      

        /*----------------------------------------------------------------------------------------------------------
                                                Create Stack				   
        ------------------------------------------------------------------------------------------------------------
         - Class specific method. TODO: Fetch all tiles in database.
            Places all available tiles in a temporary list then uses for x[_totalCardCount] amount of times
            randomly picks a tile to push onto the tile stack.
            IMPORTANT! [_totalCardCount] must not exceed the number of actual RoomTile objects in the database.
        ----------------------------------------------------------------------------------------------------------*/
        private void CreateStack()
        {
            // 1) Create all cards/ fetch all cards from database and put into temp list
            // 2) Pick cards from temp list at random and place into Stack.


            TileStack = new Stack<RoomTile>();

            List<RoomTile> tiles = new List<RoomTile>();

            tiles.AddRange(_service.GetAllRoomTiles());


            // Pick random cards from the list and push them onto the stack
            for (int i = 0; i < _totalTileCount; i++)
            {
                // Pick a random card
                int result = _randomizer.Next(tiles.Count);
                RoomTile getTile = tiles[result];
                // Push the card onto the stack
                TileStack.Push(getTile);
                // Remove the card from the list
                tiles.RemoveAt(result);
            }
        }
        // End -----------------------------------------------------------------------------------------------------
        #endregion
    }
}
