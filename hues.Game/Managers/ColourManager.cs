using hues.Game.RespackElements;

namespace hues.Game.Managers
{
    public class HueManager : RespackElementManager<Hue>
    {
        public HueManager()
        {
            Mode = AdvanceMode.Random;
        }
    }
}
