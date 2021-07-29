using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace hues.Game.Drawables
{
    public class BeatCircle : BeatSyncedCompositeDrawable
    {
        private SpriteText centreBeatchar;

        public BeatCircle()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            InternalChildren = new Drawable[]
            {
                new Circle
                {
                    Size = new Vector2(60),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Masking = true,
                    Colour = Colour4.Gray,
                },
                centreBeatchar = new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Alpha = 0f,
                    Font = FontUsage.Default.With(size: 30),
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
