using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace hues.Game.Drawables
{
    public enum BeatlineDirection
    {
        Left,
        Right,
    }

    public class BeatLine : BeatSyncedCompositeDrawable
    {
        public int MaxChars { get; init; } = 40;
        public int CharSize { get; init; } = 20;
        public BeatlineDirection Direction { get; init; } = BeatlineDirection.Left;

        private FixedWidthSpriteText displayBeatchars;
        private List<char> builder = new List<char>();

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                displayBeatchars = new FixedWidthSpriteText
                {
                    Font = FontUsage.Default.With(size: CharSize, fixedWidth: true),
                },
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            switch (Direction)
            {
                case BeatlineDirection.Left:
                    displayBeatchars.Anchor = Anchor.CentreLeft;
                    displayBeatchars.Origin = Anchor.CentreLeft;
                    break;
                case BeatlineDirection.Right:
                    displayBeatchars.Anchor = Anchor.CentreRight;
                    displayBeatchars.Origin = Anchor.CentreRight;
                    break;
            }

            displayText(0, SongSection.Buildup);
        }

        protected override void OnNewBeat(int beatIndex, SongSection songSection, char beatChar, double beatLength)
        {
            base.OnNewBeat(beatIndex, songSection, beatChar, beatLength);
            displayText(beatIndex, songSection);
        }

        private void displayText(int beatIndex, SongSection songSection)
        {
            if (MaxChars < 1)
            {
                displayBeatchars.Text = String.Empty;
                return;
            }

            if (String.IsNullOrEmpty(BuildupBeatchars) && String.IsNullOrEmpty(LoopBeatchars))
            {
                displayBeatchars.Text = new String('.', MaxChars);
                return;
            }

            builder.Clear();

            var i = beatIndex;
            var remaining = MaxChars;

            // TODO: Maybe optimise this to not be List<char> memes
            switch (songSection)
            {
                case SongSection.Buildup:
                {
                    if (String.IsNullOrEmpty(BuildupBeatchars))
                    {
                        i = 0;
                        goto case SongSection.Loop;
                    }

                    while (remaining > 0)
                    {
                        builder.Add(BuildupBeatchars[i++]);
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
                    while (remaining > 0)
                    {
                        builder.Add(LoopBeatchars[i++]);
                        remaining--;
                        i = (i > LoopBeatchars.Length - 1) ? 0 : i;
                    }
                    break;
                }
            }

            switch (Direction)
            {
                case BeatlineDirection.Left:
                    displayBeatchars.Text = new String(builder.ToArray());
                    break;
                case BeatlineDirection.Right:
                    builder.Reverse();
                    displayBeatchars.Text = new String(builder.ToArray());
                    break;
            }
        }

        private class FixedWidthSpriteText : SpriteText
        {
            protected override char[] FixedWidthExcludeCharacters => Array.Empty<char>();
        }
    }
}
