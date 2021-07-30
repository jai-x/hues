using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using hues.Game.Drawables;
using hues.Game.Managers;

namespace hues.Game
{
    public class HuesMain : BeatSyncedCompositeDrawable
    {
        [Resolved]
        private HueManager hueManager { get; set; }

        [Resolved]
        private ImageManager imageManager { get; set; }

        private Box blackout;
        private Box whiteout;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Name = "Backlight",
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.White,
                },
                new ImageBox
                {
                    RelativeSizeAxes = Axes.Both,
                },
                new HueBox
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0.4f,
                },
                blackout = new Box
                {
                    Colour = Colour4.Black,
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0f,
                },
                whiteout = new Box
                {
                    Colour = Colour4.White,
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0f,
                },
            };
        }

        protected override void OnNewBeat(int beatIndex, SongSection songSection, char beatChar, double beatLength)
        {
            base.OnNewBeat(beatIndex, songSection, beatChar, beatLength);

            switch (beatChar)
            {
                // Vertical blur (snare)
                case 'x':
                    blackout.Hide();
                    whiteout.Hide();
                    hueManager.Next();
                    imageManager.Next();
                    break;

                // Horizontal blue (bass)
                case 'o':
                    blackout.Hide();
                    whiteout.Hide();
                    hueManager.Next();
                    imageManager.Next();
                    break;

                // No blur
                case '-':
                    blackout.Hide();
                    whiteout.Hide();
                    hueManager.Next();
                    imageManager.Next();
                    break;

                // Blackout
                case '+':
                    whiteout.Hide();
                    blackout.Show();
                    break;

                // Whiteout
                case '¤':
                    whiteout.Show();
                    blackout.Hide();
                    break;

                // Short blackout
                case '|':
                    whiteout.Hide();
                    blackout.Show();
                    break;

                // Colour only
                case ':':
                    blackout.Hide();
                    whiteout.Hide();
                    hueManager.Next();
                    break;

                // Image only
                case '*':
                    blackout.Hide();
                    whiteout.Hide();
                    imageManager.Next();
                    break;

                default:
                    break;
            }
        }
    }
}
