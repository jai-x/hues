using System;
using System.Text;

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

using osuTK;

namespace hues.Game
{
    public class BeatLine : BeatSyncedCompositeDrawable
    {
        public int MaxChars = 40;
        public int CharSize = 20;

        protected override void LoadComplete()
        {
            base.LoadComplete();

            InternalChildren = new Drawable[]
            {
                displayBeatchars = new FixedWidthSpriteText
                {
                    Font = FontUsage.Default.With(size: CharSize, fixedWidth: true),
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                },
            };

            displayText(0, Section.Buildup);
        }

        public void Reset() => displayText(0, Section.Buildup);

        private class FixedWidthSpriteText : SpriteText
        {
            protected override char[] FixedWidthExcludeCharacters => Array.Empty<char>();
        }

        private FixedWidthSpriteText displayBeatchars;

        private StringBuilder builder = new StringBuilder();

        private void displayText(int beatIndex, Section beatSection)
        {

            if (MaxChars < 1)
                displayBeatchars.Text = String.Empty;

            builder.Clear();

            var i = beatIndex;
            var remaining = MaxChars;

            // TODO: Maybe optimise this to not be string builder memes
            switch (beatSection)
            {
                case Section.Buildup:
                {
                    if (String.IsNullOrEmpty(BuildupBeatchars))
                        goto case Section.Loop;

                    while (remaining > 0)
                    {
                        builder.Append(BuildupBeatchars[i++]);
                        remaining--;
                        if (i > BuildupBeatchars.Length - 1)
                        {
                            i = 0;
                            goto case Section.Loop;
                        }
                    }
                    break;
                }
                case Section.Loop:
                    while (remaining > 0)
                    {
                        builder.Append(LoopBeatchars[i++]);
                        remaining--;
                        i = (i > LoopBeatchars.Length - 1) ? 0 : i;
                    }
                    break;
            }

            displayBeatchars.Text = builder.ToString();
        }

        protected override void OnNewBeat(int beatIndex, Section beatSection, char beatChar, double beatLength)
        {
            base.OnNewBeat(beatIndex, beatSection, beatChar, beatLength);

            displayText(beatIndex, beatSection);
        }
    }
}
