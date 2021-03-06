using System;
using osu.Framework.Graphics;

namespace hues.Game.Elements
{
    public class Image : Element, IEquatable<Image>
    {
        public string Name { get; init; }
        public string Source { get; init; }
        public string TexturePath { get; init; }
        public ImageAlign Align { get; init; }
        public Respack Respack { get; init; }

        public override string ToString() =>
            $"<{nameof(Image)}> " +
            $"Name: {Name}, " +
            $"Source: {Source}, " +
            $"TexturePath: {TexturePath}";

        // We only really care about name as that is what the user sees
        public bool Equals(Image other) => this.Name == other.Name;

        public override int GetHashCode() => this.Name.GetHashCode();

        public override bool Equals(object other)
        {
            if (other is Image)
                return this.Equals(other as Image);
            else
                return false;
        }

        public static Anchor AlignToOrigin(ImageAlign align)
        {
            switch (align)
            {
                case ImageAlign.Centre:
                    return Anchor.Centre;

                case ImageAlign.Left:
                    return Anchor.CentreLeft;

                case ImageAlign.Right:
                    return Anchor.CentreRight;

                default:
                    return Anchor.Centre;
            }
        }
    }

    public enum ImageAlign
    {
        Centre,
        Left,
        Right,
    }
}
