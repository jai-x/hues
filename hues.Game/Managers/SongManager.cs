using System;
using System.Collections.Generic;

using hues.Game.RespackElements;

namespace hues.Game.Managers
{
    public class SongManager : RespackElementManager<Song>
    {
        public IReadOnlyCollection<Song> Songs => AllElements;
    }
}
