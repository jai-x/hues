using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Platform;
using osu.Framework.Testing;

namespace hues.Game.Tests
{
    public class HuesTestBrowser : HuesGameBase
    {
        protected override void LoadComplete()
        {
            AddRange(new Drawable[]
            {
                new TestBrowser("hues"),
                new CursorContainer(),
            });
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);
            host.Window.CursorState |= CursorState.Hidden;
        }
    }
}
