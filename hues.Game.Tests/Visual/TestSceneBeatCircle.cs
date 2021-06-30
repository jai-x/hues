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
        protected readonly Bindable<WorkingBeatmap> workingBeatmap = new Bindable<WorkingBeatmap>();

        private WorkingBeatmap testBeatmap;

        public TestSceneBeatCircle()
        {
            RelativeSizeAxes = Axes.Both;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Child = new BeatCircle();

            testBeatmap = new WorkingBeatmap(Beatmap.All[0], audioManager);

            testBeatmap.Load();
            workingBeatmap.Value = testBeatmap;

            AddStep("Reset", testBeatmap.Reset);
            AddStep("Start", testBeatmap.Start);
        }

        protected override void Dispose(bool isDisposing)
        {
            testBeatmap?.Dispose();
            base.Dispose(isDisposing);
        }
    }
}
