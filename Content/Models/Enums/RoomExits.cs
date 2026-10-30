using System;


//////////////////////////////////////////////// ROOMEXITS ENUM//////////////////////////////////////////////////
/*

    Small bitwise enum to handle amount of Exits a RoomTile can have available.
    The number of exits are based on the combined bit-value stored as the 
    DoorValue property of a RoomTile.

    Last modified: 30/10 - 26 : Created.

*//////////////////////////////////////////////////////////////////////////////////////////////////////////////
namespace dragon_hoard.Content.Models.Enums
{
    [Flags]
    public enum RoomExits
    {
        None    = 0,
        Up      = 1 << 0,   // 1
        Right   = 1 << 1,   // 2
        Down    = 1 << 2,   // 4
        Left    = 1 << 3,   // 8

    }
}
/*
 
TABLE  
Room exits      Enum expression                                 DoorValue
--------------------------------------------------------------------------

No exits        RoomExits.None                                          0

Up              RoomExits.Up                                            1

Right           RoomExits.Right                                         2

Down            RoomExits.Down                                          4

Left            RoomExits.Left                                          8

2 Exits--------------------------------------------------------------------
Up + Down       RoomExits.Up \| RoomExits.Down                          5

Left + Down     RoomExits.Left \| RoomExits.Down                       12

Right + Down    RoomExits.Right \| RoomExits.Down                       6


3 Exits -------------------------------------------------------------------
Left + Right + Down                                                    14

Up + Left + Down                                                       13

Up + Right + Down                                                       7


All four        RoomExits.Up \| RoomExits.Right \| 
                RoomExits.Down \| RoomExits.Left                       15


*/