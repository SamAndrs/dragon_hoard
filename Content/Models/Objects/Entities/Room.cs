using dragon_hoard.Content.Models.Enums;
using dragon_hoard.Content.Models.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace dragon_hoard.Content.Models.Objects.Entities
{
    ///////////////////////////////////////// GAME MAP CLASS ///////////////////////////////////////////////
    /*
       :: TODO ---------------------------

            v.001a (10/01) Setup of basic core properties and methods.



        :: DONE ---------------------------
            (2026)

    *//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class Room
    {
        //========================================== PROPERTIES ==================================================//

        // DATA
        private int _roomId;

        public int RoomRow { get; private set; }

        public int RoomColumn { get; private set; }

        public bool IsDiscovered { get; private set; }

        public RoomType RoomType { get; private set; }   // Type of room (e.g., "Treasure Room", "Exit", etc.)

        public List<Door> RoomDoors { get; private set; }

        // FUNCTIONALITY
        private UiManager _ui;

   
        // GRAPHICS
        public Rectangle RoomSquare { get; private set; } // The rectangle representing the room's position and size on the map.


        // DEBUG
        private int _nrDoors = 0;


        //------------------------------------------- CONSTRUCTOR/ -S -------------------------------------------//
        public Room(UiManager uiManager, int roomId, int mapRow, int mapColumn, Vector2 roomPosition, int squareSize, Enum roomType)
        {
            _ui = uiManager;
            RoomSquare = new Rectangle((int)roomPosition.X, (int)roomPosition.Y, squareSize, squareSize);
            _roomId = roomId;
            RoomRow = mapRow;
            RoomColumn = mapColumn;
            RoomType = (RoomType)roomType;
            IsDiscovered = false;
        }



        //============================================ METHODS ===================================================//
        #region methods


        /*----------------------------------------------------------------------------------------------------------
                                                       Initialize					   
        ------------------------------------------------------------------------------------------------------------
          - Class specific method handling the initialization of a room as a Player character enters into it.
        ----------------------------------------------------------------------------------------------------------*/
        public void Initialize(GameSession session)
        {

            CheckEnterDirection(session);

            // Create random amount of doors. ( max 3)
            _nrDoors = _ui.Randomizer.Next(3);
            
            CreateRoomDoors(_nrDoors);

        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                                       Load					   
        ------------------------------------------------------------------------------------------------------------
          - Retrieves any room specific content and stored data from a saved Game Session
        ----------------------------------------------------------------------------------------------------------*/
        public void Load()
        {

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                                 Update				   
        ------------------------------------------------------------------------------------------------------------
          - Updates any room specific content changes.
        ----------------------------------------------------------------------------------------------------------*/
        public void Update(GameTime gameTime)
        {
            if (IsDiscovered)
            {
                
            }
           
        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                               Draw					   
        ------------------------------------------------------------------------------------------------------------
        - Main draw method for the room. Calls the helper methods to draw any visual representation of the room 
            based on its type, discovery status and possible events.
        ----------------------------------------------------------------------------------------------------------*/
        public void Draw(SpriteBatch spriteBatch)
        {
            DrawOnMap(spriteBatch);

            Debug(spriteBatch);

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                               Draw	On Map				   
        ------------------------------------------------------------------------------------------------------------
        - Helper method to handle drawing the corresponding room square on the map based on its type 
            and discovery status.
        ----------------------------------------------------------------------------------------------------------*/
        private void DrawOnMap(SpriteBatch spriteBatch)
        {
            Color darkGray = new Color(28, 30, 33); // Dark gray color for outer wall room squares

            // Draw the room square on the map
            if (RoomType == RoomType.Exit)
                spriteBatch.Draw(_ui.RectangleTexture, RoomSquare, Color.Yellow);

            else if (RoomType == RoomType.OuterWall)
                spriteBatch.Draw(_ui.RectangleTexture, RoomSquare, darkGray);
            else
            {
                if (IsDiscovered)
                {
                    spriteBatch.Draw(_ui.RectangleTexture, RoomSquare, Color.White);
                }

            }

        }// End -----------------------------------------------------------------------------------------------------


        /*----------------------------------------------------------------------------------------------------------
                                              Check Enter Direction				   
       ------------------------------------------------------------------------------------------------------------
       - Helper method to check from which cardinal direction the Player character enters the room.
        Depending on which, a matching door will be created as entrance for the room.
       ----------------------------------------------------------------------------------------------------------*/
        private void CheckEnterDirection(GameSession session)
        {
            //RoomDoors = new List<Door>();
            //Room previous = session.CurrentPlayerRoom;


            //// Enters from the north
            //if (previous.RoomRow == this.RoomRow -1)
            //    RoomDoors.Add(new Door(Direction.Up));

            //// Enters from the east
            //else if(previous.RoomColumn == this.RoomColumn +1)
            //        RoomDoors.Add(new Door(Direction.Right));

            //// Enters from the south
            //else if(previous.RoomRow == this.RoomRow + 1)
            //    RoomDoors.Add(new Door(Direction.Down));

            //// Enters from the west
            //else if (previous.RoomColumn == this.RoomColumn - 1) 
            //    RoomDoors.Add(new Door(Direction.Left));

        }// End -----------------------------------------------------------------------------------------------------



        /*----------------------------------------------------------------------------------------------------------
                                              Create Room Doors			   
       ------------------------------------------------------------------------------------------------------------
       - Helper method for creating additional doors in the room. The amount is determined by randomness and affected
        by roomtype.
       ----------------------------------------------------------------------------------------------------------*/
        private void CreateRoomDoors(int nrDoors)
        {
            // No more doors if the room is a bottomless pit or spinroom
            if (RoomType == RoomType.BottomLessPit || RoomType == RoomType.SpinRoom)
                nrDoors = 0;

            // Check if room is corridor or bridge room (allows only opposite direction)
            else if(RoomType == RoomType.Corridor || RoomType == RoomType.BridgeRoom)
            {
                switch (RoomDoors[0].Direction)
                {
                    // 2nd door direction determined by entrance direction
                    case Direction.Up:
                        nrDoors = 0;
                        RoomDoors.Add(new Door(Direction.Down));
                        break;

                        case Direction.Right:
                        nrDoors = 0;
                        RoomDoors.Add(new Door(Direction.Left));
                        break;

                        case Direction.Down:
                        nrDoors = 0;
                        RoomDoors.Add(new Door(Direction.Up));
                        break;

                        case Direction.Left:
                        nrDoors = 0;
                        RoomDoors.Add(new Door(Direction.Right));
                        break;
                }
            }

            
            else
            {
                // Create all directions and then remove the entrance direction from the selection
                List<Direction> directions = new List<Direction>();
                directions.AddRange(Direction.Up, Direction.Right, Direction.Down, Direction.Left);
                directions.Remove(RoomDoors[0].Direction);

                // If there are less than 3 other doors rolled, shuffle the list and pick exit directions randomly
                if(nrDoors < 3)
                {
                    var randomized = CollectionsMarshal.AsSpan(directions);
                    List<Direction> randomDirections = new List<Direction>();
                    randomDirections.AddRange(randomized);

                    // Prevent a direction to be picked multiple times.
                    int prevDirection = 3;
                    for(int i = 0; i < nrDoors; i++)
                    {
                        if (i != prevDirection)
                        {
                            RoomDoors.Add(new Door(randomDirections[i]));
                            prevDirection = i;
                        }
                        else
                            continue;
                        
                    }
                }
                // If the room has a full set of 3 other exits, then create doors for each exit.
                else
                {
                    for(int i = 0; i < directions.Count; i++)
                    {
                        RoomDoors.Add(new Door(directions[i]));
                    }
                }
            }

            // Check if door leads towards outer wall
        }


        private void Debug(SpriteBatch spriteBatch)
        {
            bool listDoors = true;

            if (listDoors && RoomDoors != null)
            {
                for (int i = 0; i < RoomDoors.Count; i++)
                {
                    spriteBatch.DrawString(_ui.DebugText, $"Doors rolled: {_nrDoors}", new Vector2(300, 270), Color.White);
                    spriteBatch.DrawString(_ui.DebugText, $"{RoomDoors[i].Direction}", new Vector2(300, 300 + (i * 15)), Color.Yellow);
                    
                }
            }
        }



        // DEBUGPURPOSES
        public void SetTestValues(RoomType roomType, bool discovered)
        {
            RoomType = roomType;

            IsDiscovered = discovered;
        }
        #endregion
    }
}