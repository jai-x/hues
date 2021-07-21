using System;

namespace hues.Game.RespackElements
{
    public class Image : RespackElement
    {
        public string Name { get; init; }
        public string Source { get; init; }
        public string TexturePath { get; init; }
        public Respack Respack { get; init; }

        public override string ToString() =>
            $"<{nameof(Image)}> " +
            $"Name: {Name}, " +
            $"Source: {Source}, " +
            $"TexturePath: {TexturePath}";
    }
}
