using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Objects.Entities.Cards;
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

        public Stack<RoomTile> TileStack { get; private set; }

        private Random _randomizer;

        private int _totalTileCount;

        

        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public RoomTileStack(MainGame game, Random random, int totalCount)
        {
            _game = game;

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
            // Load 4-tile Texures
            Load4DoorTextures();

            Load3DoorTextures();

            Load2DoorTextures();

            LoadDeadEndTextures();

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


        private void Load4DoorTextures()
        {
            // TODO
        }

        private void Load3DoorTextures()
        {
            // TODO
        }

        private void Load2DoorTextures()
        {
            // TODO
        }

        private void LoadDeadEndTextures()
        {
            // TODO
        }

       

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

            //for (int i = 0; i < _totalTileCount; i++)
            //{
            //    tiles.Add(new RoomTile(1, RoomType.Empty, 1));
            //}

            tiles.Add(new RoomTile(1, RoomType.Empty, 1));
            tiles.Add(new RoomTile(2, RoomType.Corridor, 1));
            tiles.Add(new RoomTile(3, RoomType.DarkRoom, 1));
            tiles.Add(new RoomTile(4, RoomType.TreasureRoom,  1));
            tiles.Add(new RoomTile(5, RoomType.Rubble, 1));
            tiles.Add(new RoomTile(6, RoomType.TrapRoom,1));
            tiles.Add(new RoomTile(7, RoomType.BottomLessPit, 7));
            tiles.Add(new RoomTile(8, RoomType.BridgeRoom, 7));
            tiles.Add(new RoomTile(9, RoomType.WebbedRoom, 1));
            tiles.Add(new RoomTile(10, RoomType.SpinRoom, 8));
            tiles.Add(new RoomTile(11, RoomType.Empty,  1));
            tiles.Add(new RoomTile(12, RoomType.Empty, 1));
            tiles.Add(new RoomTile(13, RoomType.Empty,  1));
            tiles.Add(new RoomTile(14, RoomType.Empty, 1));
            tiles.Add(new RoomTile(15, RoomType.Empty, 1));
            tiles.Add(new RoomTile(16, RoomType.Empty, 1));
            tiles.Add(new RoomTile(17, RoomType.Empty, 1));
            tiles.Add(new RoomTile(18, RoomType.Empty, 1));
            tiles.Add(new RoomTile(19, RoomType.Empty, 1));
            tiles.Add(new RoomTile(20, RoomType.Empty, 1));
            tiles.Add(new RoomTile(21, RoomType.Empty, 1));
            tiles.Add(new RoomTile(22, RoomType.Empty, 1));
            tiles.Add(new RoomTile(23, RoomType.Empty, 1));
            tiles.Add(new RoomTile(24, RoomType.Empty, 1));
            tiles.Add(new RoomTile(25, RoomType.Empty, 1));


            //TileStack.Push(new RoomTile(2, RoomType.Empty, 8)); // (only entrance)
            //TileStack.Push(new RoomTile(2, RoomType.Empty, 7)); // (entrance, opposite end)
            //TileStack.Push(new RoomTile(2, RoomType.Empty, 4)); // (entrance, up, down)
            //TileStack.Push(new RoomTile(2, RoomType.Empty, 7)); // (entrance, opposite end)
            //TileStack.Push(new RoomTile(2, RoomType.Empty, 7)); //(entrance, down, opposite end)

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
