using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using hues.Game.Managers;

namespace hues.Game.Drawables
{
    public class HuesMain : BeatSyncedCompositeDrawable
    {
        [Resolved]
        private HueManager hueManager { get; set; }

        [Resolved]
        private ImageManager imageManager { get; set; }

        private BufferedContainer buffer;
        private Box blackout;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                buffer = new BufferedContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    BackgroundColour = Colour4.White,
                    Children = new Drawable[]
                    {
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
                            RelativeSizeAxes = Axes.Both,
                            Alpha = 0f,
                            Colour = Colour4.Black,
                        },
                    },
                },
            };
        }

        private const float blurAmount = 20f;
        private readonly Vector2 horizontalBlur = new Vector2(0, blurAmount);
        private readonly Vector2 verticalBlur = new Vector2(blurAmount, 0);
        private readonly Vector2 resetBlur = Vector2.Zero;

        private readonly char nullChar = '.';
        private readonly char[] nonBlackoutCancelChars = new char[] { '+', '|', '¤' };
        private readonly char[] verticalBlurChars = new char[] { 'x', 'X' };
        private readonly char[] horizontalBlurChars = new char[] { 'o', 'O', '+' };
        private readonly char[] colourChangeChars = new char[] { 'x', 'o', '-', '|', ':' };
        private readonly char[] imageChangeChars = new char[] { 'x', 'o', '-', '|', '*', '=', 'I' };
        private readonly char blackoutChar = '+';
        private readonly char shortBlackoutChar = '|';
        private readonly char whiteoutChar = '¤';

        protected override void OnNewBeat(int beatIndex, SongSection songSection, char beatChar, double beatLength)
        {
            base.OnNewBeat(beatIndex, songSection, beatChar, beatLength);

            // Null
            if (beatChar == nullChar)
                return;

            // Reset blackout
            if (!nonBlackoutCancelChars.Contains(beatChar))
            {
                blackout.Hide();
                blackout.Colour = Colour4.Black;
            }

            // Vertical blur
            if (verticalBlurChars.Contains(beatChar))
                buffer.BlurTo(verticalBlur).BlurTo(resetBlur, beatLength);

            // Horizonal blur
            if (horizontalBlurChars.Contains(beatChar))
                buffer.BlurTo(horizontalBlur).BlurTo(resetBlur, beatLength);

            // Hue
            if (colourChangeChars.Contains(beatChar))
                hueManager.Next();

            // Image
            if (imageChangeChars.Contains(beatChar))
                imageManager.Next();

            // Blackout
            if (beatChar == blackoutChar)
                blackout.FadeIn(beatLength);

            // Short blackout
            if (beatChar == shortBlackoutChar)
                blackout.FadeIn(beatLength / 1.7).FadeOut();

            // Whiteout
            if (beatChar == whiteoutChar)
            {
                blackout.Colour = Colour4.White;
                blackout.FadeIn(beatLength);
            }
        }
    }
}
