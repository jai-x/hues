using osu.Framework;
using osu.Framework.Platform;

namespace hues.Desktop
{
    class Program
    {
        static void Main(string[] args)
        {
            using (GameHost host = Host.GetSuitableHost("hues", portableInstallation: true))
            using (var game = new HuesGameDesktop())
                host.Run(game);
        }
    }
}
