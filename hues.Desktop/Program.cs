using osu.Framework;
using osu.Framework.Platform;

using hues.Game;

namespace hues.Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            using (GameHost host = Host.GetSuitableHost("hues"))
            using (var game = new HuesGame())
                host.Run(game);
        }
    }
}
