using osu.Framework;
using osu.Framework.Platform;

namespace hues.Game.Tests
{
    class Program
    {
        public static void Main(string[] args)
        {
            using (GameHost host = Host.GetSuitableHost("hues-visual-test"))
            using (var game = new HuesTestBrowser())
                host.Run(game);
        }
    }
}
