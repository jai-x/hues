using System;
using System.Collections.Generic;

using hues.Game.RespackElements;

namespace hues.Game.Managers
{
    public class ColourManager : RespackElementManager<Colour>
    {
        public IReadOnlyCollection<Colour> Colours => AllElements;
    }
}
