using System;
using System.Collections.Generic;

namespace hues.Game.Managers
{
    public class SongManager : ObjectManager<Song>
    {
        public IReadOnlyCollection<Song> Songs => AllItems;
    }
}
