using System;
using System.Linq;
using osu.Framework.Graphics;

namespace hues.Game.RespackElements
{
    public class Colour : RespackElement
    {
        public readonly Colour4 Colour4;
        public readonly string Name;

        public Colour(string hex, string name)
        {
            Colour4 = Colour4.FromHex(hex);
            Name = name;
        }

        public override string ToString() =>
            $"<{nameof(Colour)}> " +
            $"Name: {Name}, " +
            $"Hex: {this.Colour4.ToHex()}";

        public static Colour Black => defaultSet.First();

        public static Colour[] All => defaultSet.Skip(1).ToArray();

        private static Colour[] defaultSet = new Colour[]
        {
            new Colour("#000000", "Black"),
            new Colour("#550000", "Brick"),
            new Colour("#aa0000", "Crimson"),
            new Colour("#ff0000", "Red"),
            new Colour("#005500", "Turtle"),
            new Colour("#555500", "Sludge"),
            new Colour("#aa5500", "Brown"),
            new Colour("#ff5500", "Orange"),
            new Colour("#00aa00", "Green"),
            new Colour("#55aa00", "Grass"),
            new Colour("#aaaa00", "Maize"),
            new Colour("#ffaa00", "Citrus"),
            new Colour("#00ff00", "Lime"),
            new Colour("#55ff00", "Leaf"),
            new Colour("#aaff00", "Chartreuse"),
            new Colour("#ffff00", "Yellow"),
            new Colour("#000055", "Midnight"),
            new Colour("#550055", "Plum"),
            new Colour("#aa0055", "Pomegranate"),
            new Colour("#ff0055", "Rose"),
            new Colour("#005555", "Swamp"),
            new Colour("#555555", "Dust"),
            new Colour("#aa5555", "Dirt"),
            new Colour("#ff5555", "Blossom"),
            new Colour("#00aa55", "Sea"),
            new Colour("#55aa55", "Ill"),
            new Colour("#aaaa55", "Haze"),
            new Colour("#ffaa55", "Peach"),
            new Colour("#00ff55", "Spring"),
            new Colour("#55ff55", "Mantis"),
            new Colour("#aaff55", "Brilliant"),
            new Colour("#ffff55", "Canary"),
            new Colour("#0000aa", "Navy"),
            new Colour("#5500aa", "Grape"),
            new Colour("#aa00aa", "Mauve"),
            new Colour("#ff00aa", "Purple"),
            new Colour("#0055aa", "Cornflower"),
            new Colour("#5555aa", "Deep"),
            new Colour("#aa55aa", "Lilac"),
            new Colour("#ff55aa", "Lavender"),
            new Colour("#00aaaa", "Aqua"),
            new Colour("#55aaaa", "Steel"),
            new Colour("#aaaaaa", "Grey"),
            new Colour("#ffaaaa", "Pink"),
            new Colour("#00ffaa", "Bay"),
            new Colour("#55ffaa", "Marina"),
            new Colour("#aaffaa", "Tornado"),
            new Colour("#ffffaa", "Saltine"),
            new Colour("#0000ff", "Blue"),
            new Colour("#5500ff", "Twilight"),
            new Colour("#aa00ff", "Orchid"),
            new Colour("#ff00ff", "Magenta"),
            new Colour("#0055ff", "Azure"),
            new Colour("#5555ff", "Liberty"),
            new Colour("#aa55ff", "Royalty"),
            new Colour("#ff55ff", "Thistle"),
            new Colour("#00aaff", "Ocean"),
            new Colour("#55aaff", "Sky"),
            new Colour("#aaaaff", "Periwinkle"),
            new Colour("#ffaaff", "Carnation"),
            new Colour("#00ffff", "Cyan"),
            new Colour("#55ffff", "Turquoise"),
            new Colour("#aaffff", "Powder"),
            new Colour("#ffffff", "White"),
        };
    }
}
