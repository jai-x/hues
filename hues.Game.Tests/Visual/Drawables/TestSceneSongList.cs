using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using hues.Game.Drawables;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestSceneSongList : HuesRespackLoadedTestScene
    {
        private SongList songList;

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = songList = new SongList
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };
        }

        [Test]
        public void TestVisual()
        {
            AddStep("Show", () => { songList.Show(); });
            AddStep("Hide", () => { songList.Hide(); });
        }
    }
}
