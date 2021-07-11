using System;

namespace hues.Game.Extensions
{
    public static class ObjectExtensions
    {
        public static TOutput Transform<TInput, TOutput>(this TInput input, Func<TInput, TOutput> transform) => transform(input);
    }
}
