using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using hues.Game.Elements;
using hues.Game.Managers;
using hues.Game.Stores;

namespace hues.Game.Drawables
{
    public class HuesMain : BeatSyncedCompositeDrawable
    {
        [Resolved]
        private RespackTextureStore textureStore { get; set; }

        [Resolved]
        private HueManager hueManager { get; set; }

        [Resolved]
        private ImageManager imageManager { get; set; }

        [Resolved]
        private Bindable<Hue> currentHue { get; set; }

        [Resolved]
        private Bindable<Elements.Image> currentImage { get; set; }

        [Resolved]
        private Bindable<Song> currentSong { get; set; }

        private BufferedContainer buffer;
        private Sprite image;
        private Box blackout;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.White,
                },
                buffer = new BufferedContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    BackgroundColour = Colour4.White,
                    Child = image = new Sprite
                    {
                        RelativeSizeAxes = Axes.Both,
                    },
                },
                blackout = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0f,
                    Colour = Colour4.Black,
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            currentHue.BindValueChanged(hueChange =>
            {
                var newHue = hueChange.NewValue;

                if (newHue == null)
                    buffer.EffectColour= Colour4.White;
                else
                    buffer.EffectColour = newHue.Colour4;
            });

            currentImage.BindValueChanged(imageChange =>
            {
                var newImage = imageChange.NewValue;

                if (newImage == null)
                    image.Texture = null;
                else
                    image.Texture = textureStore.Get(newImage.TexturePath);
            });

            // Reset invert on song change
            currentSong.BindValueChanged(_ =>
            {
                isInvert = false;
                buffer.EffectBlending = normalBlend;
            });
        }

        // Timings
        // Original hues flash was 30fps, frame time in ms
        private const double flashFrame          = 1000 / 30;
        private const double blurTimeMs          = flashFrame;
        private const double blackoutTime        = flashFrame * 2;
        private const double shortBlackoutFactor = 1 / 1.7;

        // Blur amounts
        private const float blurLow    = 48f;
        private const float blurMedium = 96f;
        private const float blurHight  = 384f;

        // Blur decays
        private const double blurDecaySlow     = 7.8;
        private const double blurDecayMedium   = 14.1;
        private const double blurDecayFast     = 20.8;
        private const double blurDecayVeryFast = 28.7;

        // Medium blur params hardcoded for now
        private const float blurAmount = blurMedium;
        private const double blurDecay = blurDecayMedium;

        // Blurs
        private readonly Vector2 horizontalBlur = new Vector2(0, blurAmount);
        private readonly Vector2 verticalBlur = new Vector2(blurAmount, 0);
        private readonly Vector2 resetBlur = Vector2.Zero;

        // Blending
        private readonly BlendingParameters normalBlend = BlendingParameters.Inherit;
        private readonly BlendingParameters invertBlend = new BlendingParameters
        {
            Source = BlendingType.Zero,
            Destination = BlendingType.OneMinusSrcColor,
            RGBEquation = BlendingEquation.Inherit,
        };

        // Beatchars
        private readonly char nullChar                 = '.';
        private readonly char[] nonBlackoutCancelChars = new char[] { '+', '|', '¤' };
        private readonly char[] verticalBlurChars      = new char[] { 'x', 'X' };
        private readonly char[] horizontalBlurChars    = new char[] { 'o', 'O', '+' };
        private readonly char[] colourChangeChars      = new char[] { 'x', 'o', '-', '|', ':' };
        private readonly char[] imageChangeChars       = new char[] { 'x', 'o', '-', '|', '*', '=', 'I' };
        private readonly char[] invertChars            = new char[] { 'i', 'I' };
        private readonly char blackoutChar             = '+';
        private readonly char shortBlackoutChar        = '|';
        private readonly char whiteoutChar             = '¤';

        private bool isInvert = false;

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
                buffer.BlurTo(verticalBlur).BlurTo(resetBlur, blurTimeMs * blurDecay, Easing.OutExpo);

            // Horizonal blur
            if (horizontalBlurChars.Contains(beatChar))
                buffer.BlurTo(horizontalBlur).BlurTo(resetBlur, blurTimeMs * blurDecay, Easing.OutExpo);

            // Hue
            if (colourChangeChars.Contains(beatChar))
                hueManager.Next();

            // Image
            if (imageChangeChars.Contains(beatChar))
                imageManager.Next();

            // Invert
            if (invertChars.Contains(beatChar))
            {
                if (isInvert)
                {
                    buffer.EffectBlending = normalBlend;
                    isInvert = false;
                }
                else
                {
                    buffer.EffectBlending = invertBlend;
                    isInvert = true;
                }
            }

            // Blackout
            if (beatChar == blackoutChar)
            {
                if (isInvert)
                    blackout.Colour = Colour4.White;

                blackout.FadeIn(blackoutTime);
            }

            // Short blackout
            if (beatChar == shortBlackoutChar)
            {
                if (isInvert)
                    blackout.Colour = Colour4.White;

                blackout.FadeIn(beatLength * shortBlackoutFactor).FadeOut();
            }

            // Whiteout
            if (beatChar == whiteoutChar)
            {
                if (isInvert)
                    blackout.Colour = Colour4.Black;
                else
                    blackout.Colour = Colour4.White;

                blackout.FadeIn(blackoutTime);
            }
        }
    }
}
