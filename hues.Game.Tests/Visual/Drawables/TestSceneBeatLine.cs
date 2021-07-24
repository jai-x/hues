using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osuTK;

using hues.Game.Drawables;
using hues.Game.Managers;
using hues.Game.RespackElements;
using hues.Game.Stores;
using hues.Game.Tests.Resources;

using NUnit.Framework;

namespace hues.Game.Tests.Visual.Drawables
{
    public class TestSceneBeatLine : HuesTestScene
    {
        [Resolved]
        private RespackLoader respackLoader { get; set; }

        [Resolved]
        private SongManager songManager { get; set; }

        [Resolved]
        private Bindable<PlayableSong> playableSong { get; set; }

        [SetUp]
        public void SetUp()
        {
            Schedule(() =>
            {
                var respack = TestResources.OpenResource("Respacks/DefaultsHQ.zip");
                respackLoader.LoadStream(respack);
                Child = new TestBeatLine();
            });
        }

        [Test]
        public void TestVisual()
        {
            AddStep("Next song", () => { songManager.Next(); });
            AddStep("Previous song", () => { songManager.Previous(); });
            AddStep("Song stop", () => { playableSong.Value?.Stop(); });
            AddStep("Song start", () => { playableSong.Value?.Start(); });
            AddStep("Song reset", () => { playableSong.Value?.Reset(); });
        }

        private class TestBeatLine : CompositeDrawable
        {
            public TestBeatLine()
            {
                RelativeSizeAxes = Axes.Both;
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                InternalChildren = new Drawable[]
                {
                    new SpriteText
                    {
                        Text = ">>",
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    },
                    new BeatLine
                    {
                        X = 50,
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        MaxChars = 35,
                        CharSize = 30,
                    },
                };
            }
        }
    }
}
