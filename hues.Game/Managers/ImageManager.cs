using hues.Game.Elements;

namespace hues.Game.Managers
{
    public class ImageManager : ElementManager<Image>
    {
        public ImageManager()
        {
            Mode = AdvanceMode.Random;
        }
    }
}
