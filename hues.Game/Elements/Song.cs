using System;
using hues.Game.Extensions;

namespace hues.Game.Elements
{
    public class Song : Element, IEquatable<Song>
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


        // We only really care about title as that is what the user sees
        public bool Equals(Song other) => this.Title == other.Title;

        public override int GetHashCode() => this.Title.GetHashCode();

        public override bool Equals(object other)
        {
            if (other is Song)
                return this.Equals(other as Song);
            else
                return false;
        }
    }
}
