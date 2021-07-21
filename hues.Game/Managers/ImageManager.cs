using System;
using System.Collections.Generic;

namespace hues.Game.Managers
{
    public class ImageManager : ObjectManager<Image>
    {
        public IReadOnlyCollection<Image> Images => AllItems;
    }
}
