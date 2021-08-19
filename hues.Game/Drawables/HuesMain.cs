using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Logging;
using osuTK;
using hues.Game.Configuration;
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

        [Resolved]
        private HuesConfigManager config { get; set; }

        private BufferedContainer buffer;
        private Sprite image;
        private Box blackout;

        private readonly Bindable<float> blurSigmaBindable = new Bindable<float>();
        private readonly Bindable<double> blurTimeBindable = new Bindable<double>();
        private readonly Bindable<Easing> blurEasingBindable = new Bindable<Easing>();
        private readonly Bindable<double> blackoutTimeBindable = new Bindable<double>();

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

            // Bind local bindables with the config
            config.BindWith(HuesSetting.BlurSigma, blurSigmaBindable);
            config.BindWith(HuesSetting.BlurTimeMs, blurTimeBindable);
            config.BindWith(HuesSetting.BlurEasing, blurEasingBindable);
            config.BindWith(HuesSetting.BlackoutTimeMs, blackoutTimeBindable);

            blurSigmaBindable.BindValueChanged(change =>
            {
                setBlurAmount(change.NewValue);
                Logger.Log($"Blur Sigma Changed: {change.NewValue}", level: LogLevel.Debug);
            }, true);

            blurTimeBindable.BindValueChanged(change =>
            {
                blurTimeMs = change.NewValue;
                Logger.Log($"Blur Time Changed: {change.NewValue}", level: LogLevel.Debug);
            }, true);

            blurEasingBindable.BindValueChanged(change =>
            {
                blurEasing = change.NewValue;
                Logger.Log($"Blur Easing Changed: {change.NewValue}", level: LogLevel.Debug);
            }, true);

            blackoutTimeBindable.BindValueChanged(change =>
            {
                blackoutTimeMs = change.NewValue;
                Logger.Log($"Blackout Time Changed: {change.NewValue}", level: LogLevel.Debug);
            }, true);
        }

        // Timings
        private double blurTimeMs;
        private double blackoutTimeMs;
        private const double shortBlackoutFactor = 1d / 1.7d;

        private void setBlurAmount(float amount)
        {
            horizontalBlur = new Vector2(0, amount);
            verticalBlur = new Vector2(amount, 0);
        }

        // Blurs
        private Vector2 horizontalBlur;
        private Vector2 verticalBlur;
        private readonly Vector2 resetBlur = Vector2.Zero;
        private Easing blurEasing;

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
                buffer.BlurTo(verticalBlur).BlurTo(resetBlur, blurTimeMs, blurEasing);

            // Horizonal blur
            if (horizontalBlurChars.Contains(beatChar))
                buffer.BlurTo(horizontalBlur).BlurTo(resetBlur, blurTimeMs, blurEasing);

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

                blackout.FadeIn(blackoutTimeMs);
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

                blackout.FadeIn(blackoutTimeMs);
            }
        }
    }
}
