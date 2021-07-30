using System.Threading.Tasks;
using osu.Framework.Platform;
using osu.Framework.Logging;
using hues.Game;

namespace hues.Desktop
{
    class HuesGameDesktop : HuesGame
    {
        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            var desktopWindow = (SDL2DesktopWindow)host.Window;
            desktopWindow.DragDrop += (f) => fileDrop(new[] { f });
        }

        private void fileDrop(string[] filepaths)
        {
            foreach (var path in filepaths)
            {
                Logger.Log($"Importing respack file from path: {path}");
                Task.Factory.StartNew(() => { respackLoader.LoadPath(path); });
            }
        }
    }
}
