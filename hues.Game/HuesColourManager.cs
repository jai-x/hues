using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Logging;

namespace hues.Game
{
    public class HuesColourManager : Component
    {
        public readonly Bindable<HuesColour> Current = new Bindable<HuesColour>();

        private int position = 0;
        private Logger logger = Logger.GetLogger();

        public HuesColourManager()
        {
            Current.Value = HuesColour.Black;
            Current.ValueChanged += (hc) => logger.Debug($"{nameof(HuesColourManager)}: HuesColour Current Changed ({hc.OldValue.Name} -> {hc.NewValue.Name})");
        }

        public void Next()
        {
            if (position == HuesColour.HuesColourAll.Length - 1)
                position = 0;
            else
                position++;

            Current.Value = HuesColour.HuesColourAll[position];

        }

        public void Previous()
        {
            if (position == 0)
                position = HuesColour.HuesColourAll.Length - 1;
            else
                position--;

            Current.Value = HuesColour.HuesColourAll[position];
        }

        public void SetBlack()
        {
            Current.Value = HuesColour.Black;
        }
    }
}
