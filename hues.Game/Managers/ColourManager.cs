using System;
using System.Collections.Generic;

using hues.Game.RespackElements;

namespace hues.Game.Managers
{
    public class HueManager : RespackElementManager<Hue>
    {
        public IReadOnlyCollection<Hue> Hues => AllElements;
    }
}
