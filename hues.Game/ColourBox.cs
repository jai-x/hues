using System.Collections.Generic;
using System.Linq;

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Utils;

namespace hues.Game
{
    public class ColourBox : Box
    {
        // List of hues corresponding to "default" colorset in the flash.
        // These colours are a 4x4x4 (=0x40...) color cube, probably generated
        // programatically in the original. The names were chosen by
        // Anonymous D for the 0x40 hues of megumi edit.
        // This list was compiled by kepstin for their 0x40hues-html5 project.
        private readonly Colour4[] allColours = new Colour4[]
        {
            Colour4.FromHex("#000000"), // Black
            Colour4.FromHex("#550000"), // Brick
            Colour4.FromHex("#aa0000"), // Crimson
            Colour4.FromHex("#ff0000"), // Red
            Colour4.FromHex("#005500"), // Turtle
            Colour4.FromHex("#555500"), // Sludge
            Colour4.FromHex("#aa5500"), // Brown
            Colour4.FromHex("#ff5500"), // Orange
            Colour4.FromHex("#00aa00"), // Green
            Colour4.FromHex("#55aa00"), // Grass
            Colour4.FromHex("#aaaa00"), // Maize
            Colour4.FromHex("#ffaa00"), // Citrus
            Colour4.FromHex("#00ff00"), // Lime
            Colour4.FromHex("#55ff00"), // Leaf
            Colour4.FromHex("#aaff00"), // Chartreuse
            Colour4.FromHex("#ffff00"), // Yellow
            Colour4.FromHex("#000055"), // Midnight
            Colour4.FromHex("#550055"), // Plum
            Colour4.FromHex("#aa0055"), // Pomegranate
            Colour4.FromHex("#ff0055"), // Rose
            Colour4.FromHex("#005555"), // Swamp
            Colour4.FromHex("#555555"), // Dust
            Colour4.FromHex("#aa5555"), // Dirt
            Colour4.FromHex("#ff5555"), // Blossom
            Colour4.FromHex("#00aa55"), // Sea
            Colour4.FromHex("#55aa55"), // Ill
            Colour4.FromHex("#aaaa55"), // Haze
            Colour4.FromHex("#ffaa55"), // Peach
            Colour4.FromHex("#00ff55"), // Spring
            Colour4.FromHex("#55ff55"), // Mantis
            Colour4.FromHex("#aaff55"), // Brilliant
            Colour4.FromHex("#ffff55"), // Canary
            Colour4.FromHex("#0000aa"), // Navy
            Colour4.FromHex("#5500aa"), // Grape
            Colour4.FromHex("#aa00aa"), // Mauve
            Colour4.FromHex("#ff00aa"), // Purple
            Colour4.FromHex("#0055aa"), // Cornflower
            Colour4.FromHex("#5555aa"), // Deep
            Colour4.FromHex("#aa55aa"), // Lilac
            Colour4.FromHex("#ff55aa"), // Lavender
            Colour4.FromHex("#00aaaa"), // Aqua
            Colour4.FromHex("#55aaaa"), // Steel
            Colour4.FromHex("#aaaaaa"), // Grey
            Colour4.FromHex("#ffaaaa"), // Pink
            Colour4.FromHex("#00ffaa"), // Bay
            Colour4.FromHex("#55ffaa"), // Marina
            Colour4.FromHex("#aaffaa"), // Tornado
            Colour4.FromHex("#ffffaa"), // Saltine
            Colour4.FromHex("#0000ff"), // Blue
            Colour4.FromHex("#5500ff"), // Twilight
            Colour4.FromHex("#aa00ff"), // Orchid
            Colour4.FromHex("#ff00ff"), // Magenta
            Colour4.FromHex("#0055ff"), // Azure
            Colour4.FromHex("#5555ff"), // Liberty
            Colour4.FromHex("#aa55ff"), // Royalty
            Colour4.FromHex("#ff55ff"), // Thistle
            Colour4.FromHex("#00aaff"), // Ocean
            Colour4.FromHex("#55aaff"), // Sky
            Colour4.FromHex("#aaaaff"), // Periwinkle
            Colour4.FromHex("#ffaaff"), // Carnation
            Colour4.FromHex("#00ffff"), // Cyan
            Colour4.FromHex("#55ffff"), // Turquoise
            Colour4.FromHex("#aaffff"), // Powder
            Colour4.FromHex("#ffffff"), // White
        };

        private readonly Colour4 black;
        private readonly Colour4[] colours;
        private HashSet<int> unusedColours;

        public ColourBox()
        {
            black = allColours.First();
            colours = allColours.Skip(1).ToArray();

            unusedColours = new HashSet<int>();
            unusedColours.UnionWith(Enumerable.Range(0, colours.Length));

            RelativeSizeAxes = Axes.Both;
            Colour = colours.First();
        }

        public void NextColour()
        {
            if (unusedColours.Count == 0)
                unusedColours.UnionWith(Enumerable.Range(0, colours.Length));

            var colIdx = unusedColours.ElementAt(RNG.Next(unusedColours.Count));
            unusedColours.Remove(colIdx);

            Colour = colours[colIdx];
        }

        public void Black()
        {
            Colour = black;
        }
    }
}
