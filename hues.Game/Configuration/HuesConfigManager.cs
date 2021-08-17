using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace hues.Game.Configuration
{
    public class HuesConfigManager : IniConfigManager<HuesSetting>
    {
        public HuesConfigManager(Storage storage) : base(storage)
        { }

        protected override void InitialiseDefaults()
        {
            // Original hues flash was 30fps, frame time in ms
            var flashFrame = 1000d / 30d;
            var defaultBlurTime = flashFrame * 15;
            var defaultBlackoutTime = flashFrame * 2;

            SetDefault(HuesSetting.BlurSigma, 100f, 0f, 1000f);
            SetDefault(HuesSetting.BlurTimeMs, defaultBlurTime, 0d, 5000d);
            SetDefault(HuesSetting.BlackoutTimeMs, defaultBlackoutTime, 0d, 5000d);
        }
    }

    public enum HuesSetting
    {
        BlurSigma,
        BlurTimeMs,
        BlackoutTimeMs,
    }
}
