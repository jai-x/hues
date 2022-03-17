using System;
using System.Linq;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Caching;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input;
using osu.Framework.Input.Events;
using osu.Framework.Platform;
using osuTK;

namespace hues.Game.Drawables.Editor
{
    public class EditorFlow : FillFlowContainer
    {
        private int maxBeatcharsPerRow = 32;

        public int MaxBeatcharsPerRow
        {
            get => maxBeatcharsPerRow;
            set
            {
                if (value == maxBeatcharsPerRow)
                    return;

                if (value < 1)
                    return;

                maxBeatcharsPerRow = value;

                InvalidateLayout();
            }
        }

        public EditorFlow()
        {
            Direction = FillDirection.Full;
        }

        protected override bool ForceNewRow(Drawable child)
            => (int)GetLayoutPosition(child) % MaxBeatcharsPerRow == 0;
    }

    public class EditorHighlight : CompositeDrawable
    {
        private Colour4 highlight_colour = Colour4.Red;
        private const float highlight_alpha = 0.4f;

        private readonly Box box;

        public EditorHighlight()
        {
            InternalChild = box = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = highlight_colour,
                Alpha = 0,
            };
        }


        public void DisplayAt(Vector2? position)
        {
            if (position == null)
            {
                box.Hide();
                return;
            }

            this.MoveTo(position.Value);

            if (box.Alpha == 0)
                box.Alpha = highlight_alpha;
        }
    }

    public class EditorCursor : CompositeDrawable
    {
        private Colour4 caret_color = Colour4.Black;
        private const float caret_width = 2f;
        private const float alpha_normal = 0.7f;
        private const float alpha_select = 0.4f;

        private readonly Box box;

        public EditorCursor()
        {
            Width = caret_width;

            InternalChild = box = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = caret_color,
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            box.Loop(b => b.FadeTo(alpha_normal).FadeTo(alpha_select, 750, Easing.InOutSine));
        }

        public void DisplayAt(Vector2 position)
        {
            var finalPosition = new Vector2(position.X - (caret_width / 2), position.Y);
            this.MoveTo(finalPosition, 100, Easing.OutExpo);
        }
    }

    public class EditorSelection : CompositeDrawable
    {
    }

    public class EditorText : SpriteText
    {
    }

    public class EditorTextbox : CompositeDrawable
    {
        private readonly EditorHighlight highlight;
        private readonly EditorFlow flow;
        private readonly EditorSelection selction;
        private readonly EditorCursor cursor;

        private readonly List<SpriteText> beatcharDrawables = new List<SpriteText>();

        private readonly Cached layoutCache = new Cached();
        private readonly Cached highlightCache = new Cached();
        private readonly Cached cursorCache = new Cached();

        [Resolved]
        private TextInputSource textInput { get; set; }

        private Clipboard clipboard;

        private const int defaultBeatcharSize = 12;

        private const int highlightPositionMin = 0;
        private int highlightPositionMax => beatcharDrawables.Count - 1;
        private int? highlightPosition = null;

        private const int cursorPositionMin = 0;
        private int cursorPositionMax => beatcharDrawables.Count;
        private int cursorPosition = 0;

        private string beatchars = string.Empty;
        private readonly char[] validBeatchars = new char[] { '.', 'x', 'X', 'o', 'O', 'i', 'I', '+', '-', '*', '=', ':', '|', '¤' };

        private Vector2 flowTopRight => new Vector2(flow.Padding.Left, flow.Padding.Top);
        private Vector2 flowBottomLeft => new Vector2(flow.DrawWidth - flow.Padding.Right, flow.DrawHeight - flow.Padding.Bottom);

        public EditorTextbox()
        {
            Masking = true;
            BorderColour = Colour4.Red;

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = Colour4.White,
                    RelativeSizeAxes = Axes.Both,
                },
                highlight = new EditorHighlight(),
                flow = new EditorFlow
                {
                    RelativeSizeAxes = Axes.Both,
                    Padding = new MarginPadding(5),
                },
                selction = new EditorSelection(),
                cursor = new EditorCursor
                {
                    Height = BeatcharSize,
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(GameHost gameHost)
        {
            clipboard = gameHost.GetClipboard();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            textInput.OnTextInput += text => insertString(text);
        }

        public int BeatcharSize { get; init; } = defaultBeatcharSize;

        public int MaxBeatcharsPerRow
        {
            get => flow.MaxBeatcharsPerRow;
            set
            {
                flow.MaxBeatcharsPerRow = value;

                cursorCache.Invalidate();
                highlightCache.Invalidate();
            }
        }

        public int? HighlightPosition
        {
            get => highlightPosition;
            set
            {
                if (value == highlightPosition)
                    return;

                highlightPosition = value;
                highlightCache.Invalidate();
            }
        }

        public string Beatchars
        {
            get => beatchars;
            set
            {
                if (value == beatchars)
                    return;

                beatchars = value;

                beatcharDrawables.Clear();
                flow.Clear();
                cursorPosition = 0;
                highlightPosition = null;

                insertString(beatchars);
                layoutCache.Invalidate();
                cursorCache.Invalidate();
                highlightCache.Invalidate();
            }
        }

        #region Input Handling

        public override bool AcceptsFocus => true;

        protected override void OnFocus(FocusEvent e)
        {
            base.OnFocus(e);

            textInput?.Activate(false);
            BorderThickness = 3;
        }

        protected override void OnFocusLost(FocusLostEvent e)
        {
            base.OnFocusLost(e);

            textInput?.Deactivate();
            BorderThickness = 0;
        }

        private void killFocus()
        {
            var manager = GetContainingInputManager();

            if (manager?.FocusedDrawable == this)
                manager.ChangeFocus(null);
        }

        protected override bool OnClick(ClickEvent e)
        {
            moveCursorToClosestBeatchar(e.MouseDownPosition);
            return true;
        }

        public bool OnPressed(PlatformAction action)
        {
            if (!HasFocus)
                return false;

            switch (action)
            {
                case PlatformAction.MoveBackwardChar:
                    moveCursor(-1);
                    return true;

                case PlatformAction.MoveForwardChar:
                    moveCursor(1);
                    return true;

                case PlatformAction.MoveBackwardLine:
                    moveCursor(-MaxBeatcharsPerRow);
                    return true;

                case PlatformAction.MoveForwardLine:
                    moveCursor(MaxBeatcharsPerRow);
                    return true;

                case PlatformAction.DeleteBackwardChar:
                    removeString(-1);
                    return true;

                case PlatformAction.DeleteForwardChar:
                    removeString(1);
                    return true;

                case PlatformAction.Paste:
                    insertString(clipboard?.GetText());
                    return true;

                default:
                    return false;
            }
        }

        public void OnReleased(PlatformAction action)
        {
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (!HasFocus)
                return base.OnKeyDown(e);

            switch (e.Key)
            {
                case osuTK.Input.Key.Up:
                    moveCursor(-MaxBeatcharsPerRow);
                    return true;

                case osuTK.Input.Key.Down:
                    moveCursor(MaxBeatcharsPerRow);
                    return true;

                case osuTK.Input.Key.Escape:
                    killFocus();
                    return true;
            }

            return base.OnKeyDown(e);
        }

        #endregion

        #region Text and Cursor manipulation

        private void moveCursor(int movement)
        {
            cursorCache.Invalidate();

            cursorPosition = clampCursorPosition(cursorPosition + movement);
        }

        /// <remarks>
        /// When trying to find if a click is within the bounds of a beatchar, it is shifted
        /// by the beatchar offset to account for the baseline of the character font being so low
        /// which should match user expectation better.
        /// </remarks>
        private Vector2 clickOffset => new Vector2(0, BeatcharSize / 3);

        private void moveCursorToClosestBeatchar(Vector2 position)
        {
            if (beatcharDrawables.Count == 0)
                return;

            cursorCache.Invalidate();
            position = Parent.ToSpaceOfOtherDrawable(position, flow);

            // shortcut first
            var first = beatcharDrawables.First();
            if (position.Y < first.DrawPosition.Y)
            {
                cursorPosition = cursorPositionMin;
                return;
            }

            // shortcut last
            var last = beatcharDrawables.Last();
            if (position.Y > last.DrawPosition.Y + last.DrawHeight)
            {
                cursorPosition = cursorPositionMax;
                return;
            }

            // find if clicked inside
            for (var i = 0; i < beatcharDrawables.Count; i++)
            {
                var ch = beatcharDrawables[i];
                var drawableSpaceCursor = position - ch.DrawPosition - clickOffset;
                var cursorInDrawableSpace = ch.DrawRectangle.Contains(drawableSpaceCursor);

                if (cursorInDrawableSpace)
                {
                    cursorPosition = i + 1;
                    return;
                }
            }

            cursorPosition = cursorPositionMin;
        }

        // Deletes amount number of characters forward or backward from the cursor position.
        ///A negative amount represents a backward deletion, and a positive amount represents a forward deletion.
        private void removeString(int amount)
        {
            if (amount == 0)
                return;

            if (amount < 0)
            {
                // don't want to delete forwards when started from the beginnin
                if (cursorPosition == 0)
                    return;

                cursorPosition = clampCursorPosition(cursorPosition + amount);
                amount = Math.Abs(amount);
            }

            beatchars = beatchars.Remove(cursorPosition, amount);

            var drawablesToRemove = beatcharDrawables.GetRange(cursorPosition, amount);

            foreach (var drawable in drawablesToRemove)
            {
                drawable.Hide();
                drawable.Expire();
                beatcharDrawables.Remove(drawable);
                flow.Remove(drawable);
            }

            layoutCache.Invalidate();
            cursorCache.Invalidate();
        }

        // Inserts a string after the current cursor position.
        // Returns true if the string is successfully inserted.
        private bool insertString(string str)
        {
            str = filterNonBeatchars(str);

            if (string.IsNullOrEmpty(str))
                return false;

            beatchars = beatchars.Insert(cursorPosition, str);

            foreach (char c in str)
            {
                var charDrawable = makeChar(c);
                flow.Add(charDrawable);
                beatcharDrawables.Insert(cursorPosition++, charDrawable);
            }

            layoutCache.Invalidate();
            cursorCache.Invalidate();

            return true;
        }

        #endregion

        #region Internal functions

        private int clampCursorPosition(int position)
        {
            // TODO: Figure out why Math.Clamp doesn't work?
            if (position < cursorPositionMin)
                return cursorPositionMin;

            if (position > cursorPositionMax)
                return cursorPositionMax;

            return position;
        }

        private string filterNonBeatchars(string input)
            => new string(input.Where(validBeatchars.Contains).ToArray());

        private SpriteText makeChar(char c) => new EditorText
        {
            Text = c.ToString(),
            Colour = Colour4.Black,
            Font = new FontUsage("PetMe64", size: BeatcharSize),
        };

        private void updateLayout()
        {
            for (var i = 0; i < beatcharDrawables.Count; i++)
                flow.SetLayoutPosition(beatcharDrawables[i], i);
        }

        private void updateHighlight()
        {
            if (beatcharDrawables.Count == 0 || highlightPosition == null)
            {
                highlight.DisplayAt(null);
                highlightPosition = null;
                return;
            }

            if (highlightPosition < highlightPositionMin)
                highlightPosition = highlightPositionMin;

            if (highlightPosition > highlightPositionMax)
                highlightPosition = highlightPositionMax;

            var beatchar = beatcharDrawables[highlightPosition.Value];
            var position = beatchar.DrawPosition + flowTopRight;

            highlight.Size = beatchar.DrawSize;
            highlight.DisplayAt(position);
        }

        private void updateCursor()
        {
            if (beatcharDrawables.Count == 0)
            {
                cursor.DisplayAt(flowTopRight);
                return;
            }

            if (cursorPosition < cursorPositionMax)
            {
                var beatchar = beatcharDrawables[cursorPosition];
                var position = beatchar.DrawPosition + flowTopRight;
                cursor.DisplayAt(position);
            }
            else
            {
                var beatchar = beatcharDrawables[cursorPosition - 1];
                var position = beatchar.DrawPosition + flowTopRight;
                position.X += beatchar.DrawWidth;
                cursor.DisplayAt(position);
            }
        }

        protected override void Update()
        {
            base.Update();

            if (!layoutCache.IsValid)
            {
                updateLayout();
                layoutCache.Validate();
            }
        }

        // Highlight and cursor are updated after children to allow for layout to flow it's beatchar drawables to the
        // intended position first before attempting to draw over them
        protected override void UpdateAfterChildren()
        {
            base.UpdateAfterChildren();

            if (!highlightCache.IsValid)
            {
                updateHighlight();
                highlightCache.Validate();
            }

            if (!cursorCache.IsValid)
            {
                updateCursor();
                cursorCache.Validate();
            }
        }

        #endregion
    }
}
