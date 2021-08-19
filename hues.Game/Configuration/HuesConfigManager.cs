using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Platform;

namespace hues.Game.Configuration
{
    public class HuesConfigManager : IniConfigManager<HuesSetting>
    {
        public HuesConfigManager(Storage storage) : base(storage)
        { }

        // Original hues flash was 30fps, frame time in ms
        private const double flashFrame = 1000d / 30d;
        private const double defaultBlurTime = flashFrame * 15;
        private const double defaultBlackoutTime = flashFrame * 2;
        private const Easing defaultEasing = Easing.OutExpo;

        protected override void InitialiseDefaults()
        {

            SetDefault(HuesSetting.BlurSigma, 100f, 0f, 1000f);
            SetDefault(HuesSetting.BlurTimeMs, defaultBlurTime, 0d, 5000d);
            SetDefault(HuesSetting.BlackoutTimeMs, defaultBlackoutTime, 0d, 5000d);
            SetDefault(HuesSetting.BlurEasing, defaultEasing);
        }
    }

    public enum HuesSetting
    {
        BlurSigma,
        BlurTimeMs,
        BlackoutTimeMs,
        BlurEasing
    }
}
