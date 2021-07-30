using hues.Game.Elements;

namespace hues.Game.Managers
{
    public class HueManager : ElementManager<Hue>
    {
        public HueManager()
        {
            Mode = AdvanceMode.Random;
        }
    }
}
