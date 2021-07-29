using System.Linq;
using osu.Framework.Graphics;

namespace hues.Game.RespackElements
{
    public class Hue : RespackElement
    {
        public readonly Colour4 Colour4;
        public readonly string Name;

        public Hue(string hex, string name)
        {
            Colour4 = Colour4.FromHex(hex);
            Name = name;
        }

        public override string ToString() =>
            $"<{nameof(Hue)}> " +
            $"Name: {Name}, " +
            $"Hex: {Colour4.ToHex()}";

        public static Hue Black => defaultSet.First();

        public static Hue[] All => defaultSet.Skip(1).ToArray();

        // List of hues corresponding to "default" colorset in the flash.
        // These colours are a 4x4x4 (=0x40...) color cube, probably generated
        // programatically in the original. The names were chosen by
        // Anonymous D for the 0x40 hues of megumi edit.
        // This list was compiled by kepstin for their 0x40hues-html5 project.
        private static Hue[] defaultSet = new Hue[]
        {
            new Hue("#000000", "Black"),
            new Hue("#550000", "Brick"),
            new Hue("#aa0000", "Crimson"),
            new Hue("#ff0000", "Red"),
            new Hue("#005500", "Turtle"),
            new Hue("#555500", "Sludge"),
            new Hue("#aa5500", "Brown"),
            new Hue("#ff5500", "Orange"),
            new Hue("#00aa00", "Green"),
            new Hue("#55aa00", "Grass"),
            new Hue("#aaaa00", "Maize"),
            new Hue("#ffaa00", "Citrus"),
            new Hue("#00ff00", "Lime"),
            new Hue("#55ff00", "Leaf"),
            new Hue("#aaff00", "Chartreuse"),
            new Hue("#ffff00", "Yellow"),
            new Hue("#000055", "Midnight"),
            new Hue("#550055", "Plum"),
            new Hue("#aa0055", "Pomegranate"),
            new Hue("#ff0055", "Rose"),
            new Hue("#005555", "Swamp"),
            new Hue("#555555", "Dust"),
            new Hue("#aa5555", "Dirt"),
            new Hue("#ff5555", "Blossom"),
            new Hue("#00aa55", "Sea"),
            new Hue("#55aa55", "Ill"),
            new Hue("#aaaa55", "Haze"),
            new Hue("#ffaa55", "Peach"),
            new Hue("#00ff55", "Spring"),
            new Hue("#55ff55", "Mantis"),
            new Hue("#aaff55", "Brilliant"),
            new Hue("#ffff55", "Canary"),
            new Hue("#0000aa", "Navy"),
            new Hue("#5500aa", "Grape"),
            new Hue("#aa00aa", "Mauve"),
            new Hue("#ff00aa", "Purple"),
            new Hue("#0055aa", "Cornflower"),
            new Hue("#5555aa", "Deep"),
            new Hue("#aa55aa", "Lilac"),
            new Hue("#ff55aa", "Lavender"),
            new Hue("#00aaaa", "Aqua"),
            new Hue("#55aaaa", "Steel"),
            new Hue("#aaaaaa", "Grey"),
            new Hue("#ffaaaa", "Pink"),
            new Hue("#00ffaa", "Bay"),
            new Hue("#55ffaa", "Marina"),
            new Hue("#aaffaa", "Tornado"),
            new Hue("#ffffaa", "Saltine"),
            new Hue("#0000ff", "Blue"),
            new Hue("#5500ff", "Twilight"),
            new Hue("#aa00ff", "Orchid"),
            new Hue("#ff00ff", "Magenta"),
            new Hue("#0055ff", "Azure"),
            new Hue("#5555ff", "Liberty"),
            new Hue("#aa55ff", "Royalty"),
            new Hue("#ff55ff", "Thistle"),
            new Hue("#00aaff", "Ocean"),
            new Hue("#55aaff", "Sky"),
            new Hue("#aaaaff", "Periwinkle"),
            new Hue("#ffaaff", "Carnation"),
            new Hue("#00ffff", "Cyan"),
            new Hue("#55ffff", "Turquoise"),
            new Hue("#aaffff", "Powder"),
            new Hue("#ffffff", "White"),
        };
    }
}
