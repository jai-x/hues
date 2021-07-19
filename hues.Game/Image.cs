using System;

namespace hues.Game
{
    public class Image
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
