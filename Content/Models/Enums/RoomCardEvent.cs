using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dragon_hoard.Content.Models.Enums
{
    public enum RoomCardEvent
    {
        Empty,

        // Containers
        Chest,
        Sarcophagus,
        FallenWarrior,

        // Traps
        PitTrap,
        SpearTrap,
        CaveIn,
        WallArrows,

        // Damage
        VampireBats,

        // Ambush
        AmbushSkeleton,
        AmbushOgre,
        AmbushOrc,

        // Monsters (static)
        GiantSpider,
        PosionCaterpillar,
        CorpseEaters,

        // Monsters (dynamic)
        Darkelf,
        Ogre,
        Skeleton,
        Orc,
        TwoOrcs,

        // Special events
        TorchOut,
        CurseOfMadMage,
        PursuingShadow,
        // Reshuffle,
        // WayDown,
    }
}
