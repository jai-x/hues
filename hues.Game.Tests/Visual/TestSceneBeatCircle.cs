using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;

using osuTK;

namespace hues.Game.Tests.Visual
{
    public class TestSceneBeatCircle : HuesTestScene
    {
        [Resolved]
        private AudioManager audioManager { get; set; }

        [Cached]
        protected readonly Bindable<WorkingSong> workingSong = new Bindable<WorkingSong>();

        private WorkingSong testSong;

        public TestSceneBeatCircle()
        {
            RelativeSizeAxes = Axes.Both;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Child = new BeatCircle();

            testSong = new WorkingSong(Song.All[0], audioManager);

            testSong.Load();
            workingSong.Value = testSong;

            AddStep("Reset", testSong.Reset);
            AddStep("Start", testSong.Start);
        }

        protected override void Dispose(bool isDisposing)
        {
            testSong?.Dispose();
            base.Dispose(isDisposing);
        }
    }
}
