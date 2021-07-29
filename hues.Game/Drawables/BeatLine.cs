using System;
using System.Text;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace hues.Game.Drawables
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
        }

        private class FixedWidthSpriteText : SpriteText
        {
            protected override char[] FixedWidthExcludeCharacters => Array.Empty<char>();
        }

        private FixedWidthSpriteText displayBeatchars;

        private StringBuilder builder = new StringBuilder();

        private void displayText(int beatIndex, SongSection songSection)
        {
            if (MaxChars < 1)
            {
                displayBeatchars.Text = String.Empty;
                return;
            }

            builder.Clear();

            var i = beatIndex;
            var remaining = MaxChars;

            // TODO: Maybe optimise this to not be string builder memes
            switch (songSection)
            {
                case SongSection.Buildup:
                {
                    if (String.IsNullOrEmpty(BuildupBeatchars))
                        goto case SongSection.Loop;

                    while (remaining > 0)
                    {
                        builder.Append(BuildupBeatchars[i++]);
                        remaining--;
                        if (i > BuildupBeatchars.Length - 1)
                        {
                            i = 0;
                            goto case SongSection.Loop;
                        }
                    }
                    break;
                }
                case SongSection.Loop:
                {
                    if (String.IsNullOrEmpty(LoopBeatchars))
                    {
                        builder.Append('.', remaining);
                        break;
                    }

                    while (remaining > 0)
                    {
                        builder.Append(LoopBeatchars[i++]);
                        remaining--;
                        i = (i > LoopBeatchars.Length - 1) ? 0 : i;
                    }
                    break;
                }
            }

            displayBeatchars.Text = builder.ToString();
        }

        protected override void OnNewBeat(int beatIndex, SongSection songSection, char beatChar, double beatLength)
        {
            base.OnNewBeat(beatIndex, songSection, beatChar, beatLength);

            displayText(beatIndex, songSection);
        }
    }
}
