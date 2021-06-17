using System;
using System.Linq;
using osu.Framework.Graphics;

namespace hues.Game
{
    public readonly struct HuesColour
    {
        public readonly Colour4 Colour;
        public readonly string Name;

        public HuesColour(string hex, string name)
        {
            Colour = Colour4.FromHex(hex);
            Name = name;
        }

        public static HuesColour Black => HuesColourAllWithBlack.First();

        public static HuesColour[] HuesColourAll => HuesColourAllWithBlack.Skip(1).ToArray();

        // List of hues corresponding to "default" colorset in the flash.
        // These colours are a 4x4x4 (=0x40...) color cube, probably generated
        // programatically in the original. The names were chosen by
        // Anonymous D for the 0x40 hues of megumi edit.
        // This list was compiled by kepstin for their 0x40hues-html5 project.
        public static HuesColour[] HuesColourAllWithBlack = new HuesColour[]
        {
            new HuesColour("#000000", "Black"),
            new HuesColour("#550000", "Brick"),
            new HuesColour("#aa0000", "Crimson"),
            new HuesColour("#ff0000", "Red"),
            new HuesColour("#005500", "Turtle"),
            new HuesColour("#555500", "Sludge"),
            new HuesColour("#aa5500", "Brown"),
            new HuesColour("#ff5500", "Orange"),
            new HuesColour("#00aa00", "Green"),
            new HuesColour("#55aa00", "Grass"),
            new HuesColour("#aaaa00", "Maize"),
            new HuesColour("#ffaa00", "Citrus"),
            new HuesColour("#00ff00", "Lime"),
            new HuesColour("#55ff00", "Leaf"),
            new HuesColour("#aaff00", "Chartreuse"),
            new HuesColour("#ffff00", "Yellow"),
            new HuesColour("#000055", "Midnight"),
            new HuesColour("#550055", "Plum"),
            new HuesColour("#aa0055", "Pomegranate"),
            new HuesColour("#ff0055", "Rose"),
            new HuesColour("#005555", "Swamp"),
            new HuesColour("#555555", "Dust"),
            new HuesColour("#aa5555", "Dirt"),
            new HuesColour("#ff5555", "Blossom"),
            new HuesColour("#00aa55", "Sea"),
            new HuesColour("#55aa55", "Ill"),
            new HuesColour("#aaaa55", "Haze"),
            new HuesColour("#ffaa55", "Peach"),
            new HuesColour("#00ff55", "Spring"),
            new HuesColour("#55ff55", "Mantis"),
            new HuesColour("#aaff55", "Brilliant"),
            new HuesColour("#ffff55", "Canary"),
            new HuesColour("#0000aa", "Navy"),
            new HuesColour("#5500aa", "Grape"),
            new HuesColour("#aa00aa", "Mauve"),
            new HuesColour("#ff00aa", "Purple"),
            new HuesColour("#0055aa", "Cornflower"),
            new HuesColour("#5555aa", "Deep"),
            new HuesColour("#aa55aa", "Lilac"),
            new HuesColour("#ff55aa", "Lavender"),
            new HuesColour("#00aaaa", "Aqua"),
            new HuesColour("#55aaaa", "Steel"),
            new HuesColour("#aaaaaa", "Grey"),
            new HuesColour("#ffaaaa", "Pink"),
            new HuesColour("#00ffaa", "Bay"),
            new HuesColour("#55ffaa", "Marina"),
            new HuesColour("#aaffaa", "Tornado"),
            new HuesColour("#ffffaa", "Saltine"),
            new HuesColour("#0000ff", "Blue"),
            new HuesColour("#5500ff", "Twilight"),
            new HuesColour("#aa00ff", "Orchid"),
            new HuesColour("#ff00ff", "Magenta"),
            new HuesColour("#0055ff", "Azure"),
            new HuesColour("#5555ff", "Liberty"),
            new HuesColour("#aa55ff", "Royalty"),
            new HuesColour("#ff55ff", "Thistle"),
            new HuesColour("#00aaff", "Ocean"),
            new HuesColour("#55aaff", "Sky"),
            new HuesColour("#aaaaff", "Periwinkle"),
            new HuesColour("#ffaaff", "Carnation"),
            new HuesColour("#00ffff", "Cyan"),
            new HuesColour("#55ffff", "Turquoise"),
            new HuesColour("#aaffff", "Powder"),
            new HuesColour("#ffffff", "White"),
        };
    }
}
