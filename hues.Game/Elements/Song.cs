using hues.Game.Extensions;

namespace hues.Game.Elements
{
    public class Song : Element
    {
        public string Title { get; init; }
        public string Source { get; init; }

        public string BuildupSource { get; init; }
        public string BuildupBeatchars { get; init; }

        public string LoopSource { get; init; }
        public string LoopBeatchars { get; init; }

        public Respack Respack { get; init; }

        public override string ToString() =>
            $"<{nameof(Song)}> " +
            $"Title: {Title}, " +
            $"Source: {Source}, " +
            $"BuildupSource: {BuildupSource ?? "[null]"}, " +
            $"BuildupBeatchars: {BuildupBeatchars?.Truncate(5) ?? "[null]"}, " +
            $"LoopSource: {LoopSource}, " +
            $"LoopBeatchars: {LoopBeatchars.Truncate(5)}";
    }
}
