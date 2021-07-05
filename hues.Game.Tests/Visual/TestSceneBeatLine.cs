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
    public class TestSceneBeatLine : HuesTestScene
    {
        [Resolved]
        private AudioManager audioManager { get; set; }

        [Cached]
        protected readonly Bindable<WorkingBeatmap> workingBeatmap = new Bindable<WorkingBeatmap>();

        private WorkingBeatmap testBeatmap;

        private BeatLine beatLine;

        public TestSceneBeatLine()
        {
            RelativeSizeAxes = Axes.Both;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Children = new Drawable[]
            {
                new SpriteText
                {
                    Text = ">>",
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                },
                beatLine = new BeatLine
                {
                    X = 50,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    MaxChars = 35,
                    CharSize = 30,
                },
            };

            testBeatmap = new WorkingBeatmap(Beatmap.All[13], audioManager);

            testBeatmap.Load();
            workingBeatmap.Value = testBeatmap;

            AddStep("Reset", () => {
                testBeatmap.Reset();
                beatLine.Reset();
            });
            AddStep("Start", testBeatmap.Start);
        }

        protected override void Dispose(bool isDisposing)
        {
            testBeatmap?.Dispose();
            base.Dispose(isDisposing);
        }
    }
}
