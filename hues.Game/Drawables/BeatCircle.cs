using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Shapes;

namespace hues.Game.Drawables
{
    public class BeatCircle : BeatSyncedCompositeDrawable
    {
        public int CharSize = 30;

        private SpriteText centreBeatchar;

        public BeatCircle()
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new CircularContainer
                {
                    Masking = true,
                    BorderThickness = 3,
                    BorderColour = Colour4.DimGray,
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Colour = Colour4.Black,
                            RelativeSizeAxes = Axes.Both,
                        },
                        centreBeatchar = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Alpha = 0f,
                            Font = FontUsage.Default.With(size: CharSize),
                        }
                    }
                }
            };
        }

        protected override void OnNewBeat(int beatIndex, SongSection songSection, char beatChar, double beatLength)
        {
            base.OnNewBeat(beatIndex, songSection, beatChar, beatLength);

            if (beatChar == '.')
                return;

            centreBeatchar.Text = beatChar.ToString();

            centreBeatchar.FadeIn()
                          .Then()
                          .Delay(0.8 * beatLength)
                          .Then()
                          .FadeOut();
        }
    }
}
