using System.Collections.Generic;
using hues.Game.RespackElements;

namespace hues.Game.Managers
{
    public class ImageManager : RespackElementManager<Image>
    {
        public IReadOnlyCollection<Image> Images => AllElements;

        public ImageManager()
        {
            Mode = AdvanceMode.Random;
        }
    }
}
