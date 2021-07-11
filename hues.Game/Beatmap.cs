using System;
using System.Text;

using hues.Game.Extensions;

namespace hues.Game
{
    public struct Beatmap
    {
        public string Name { get; init; }

        public string BuildupSource { get; init; }
        public string BuildupBeatchars { get; init; }

        public string LoopSource { get; init; }
        public string LoopBeatchars { get; init; }

        public bool IndependantBuild { get; init; }

        public Respack Respack { get; init; }

        public override string ToString() =>
            $"<{nameof(Beatmap)}> " +
            $"Name: {Name}, " +
            $"BuildupSource: {BuildupSource ?? "[null]"}, " +
            $"BuildupBeatchars: {BuildupBeatchars?.Truncate(5) ?? "[null]"}, " +
            $"LoopSource: {LoopSource}, " +
            $"LoopBeatchars: {LoopBeatchars.Truncate(5)}";

        public static Beatmap[] All = new Beatmap[]
        {
            new Beatmap
            {
                Name = "Madeon - Finale",
                BuildupSource = "build_Finale",
                LoopSource = "loop_Finale",
                LoopBeatchars = "x..xo...x...o...x..xo...x...o...x..xo...x...o...x..xo...x...oxoox..xo...x...o...x..xo...x...o...x..xo...x...o...x...o...x...oooo",
            },
            new Beatmap
            {
                Name = "Imagine Dragons - Radioactive",
                LoopSource ="loop_Radioactive",
                LoopBeatchars = "o...x.o.o...x.o.o...x...o...x.o.o...x.o.o...x.......x.......x...",
            },
            new Beatmap
            {
                Name = "Row Row Fight the Powah (RAGEFOXX & SLUTTT MIX)",
                LoopSource ="loop_RowRow",
                LoopBeatchars = "o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...xxx.x...x...o...x...o...x...o...x...o...x...o...x...o...o...o...o...",
            },
            new Beatmap
            {
                Name = "Outlaw Star OST - Desire",
                BuildupSource = "build_Desire",
                BuildupBeatchars = "x.....x.x.x.xxx.",
                LoopSource ="loop_Desire",
                LoopBeatchars = "o...x...o.o.x.x...o.x...o.o.x...o...x...o.o.x.x...o.x...oo..x.x.o...x...o.o.x.x...o.x...oo..x.x.o...x...o.o.x.x...o.x...x...x.xx",
            },
            new Beatmap
            {
                Name = "The Bloody Beetroots - Out of Sight",
                LoopSource ="loop_OutOfSight",
                LoopBeatchars = "o.....oox.......o.o....ox.......o.....oox...o...o.o....ox.......o......ox.......o.o....ox.......o.....oox...o...o.o.....+.......",
            },
            new Beatmap
            {
                Name = "Buckethead - Smile Without a Face",
                BuildupSource = "build_SmileWithoutAFace",
                BuildupBeatchars = "..o.....-:x.......o.....-:x.......o.....-:x.......o.....-:x.......o.....-:x.......o.....-:x.x.....",
                LoopSource ="loop_SmileWithoutAFace",
                LoopBeatchars = "o......ox.....o.o.......x.......o......ox.....o.o.....o.x.ooooooo......ox.....o.o.......x.......o......ox.....o.o.....o.x.oooooo",
            },
            new Beatmap
            {
                Name = "Crystal Castles - Courtship Dating",
                LoopSource ="loop_CourtshipDate",
                LoopBeatchars = "o...x.....o.x...o...x.....o.x...o...x.....o.x...o...x.....o.+...",
            },
            new Beatmap
            {
                Name = "Aphex Twin - Vordhosbn",
                BuildupSource = "build_Vordhosbn",
                BuildupBeatchars = ":...x..------.......-.-----.+...............x..-.-.--.-.-.-.---.",
                LoopSource ="loop_Vordhosbn",
                LoopBeatchars = "o...x..---o.x...-.o.x------.x..-o.-.x.o..-.ox-.-----x-o-------o-o...x..---o.x...-.o.x------.x..-o.-.x.o..-.ox-.-----x-o-------o-o...x..---o.x...-.o.x------.x..-o.-.x.o..-.ox-.-----x-o-------o-o...x..---o.x...-.o.x------.x..-o.-.x.o..-.ox-.-----x-o-------o-o...x..---o.x...-.o.x------.x..-o.-.x.o..-.ox-.-----x-o-------o-o...x..---o.x...-.o.x------.x..-o.-.x.o..-.ox-.-----x-o-------o-",
            },
            new Beatmap
            {
                Name = "Culprate - Orange Sunrise, Sunset",
                LoopSource ="loop_Orange",
                LoopBeatchars = "o.o.x..o.x.x....o..ox......x....o...x....o.x....o...x....o.xx...o.o.x..o.x.x....o..ox......x....o...x....o.x...xo...x....o.xx...",
            },
            new Beatmap
            {
                Name = "Hyper - Spoiler",
                LoopSource ="loop_Spoiler",
                LoopBeatchars = "o+......x+......o+......x+......o+......x+......o+......x+......o+......x+......o+......x+......o+......x+......o+..............o+......x+......o+......x+......o+......x+......o+......x+......o+......x+......o+......x+......o---------------o+......x.......",
            },
            new Beatmap
            {
                Name = "DJ Fresh - Kryptonite",
                LoopSource ="loop_Kryptonite",
                LoopBeatchars = "o.x..ox.o.x..ox.o.x..ox.o.x..ox.o.x..xx.o.x..xx.o.x..xx.o.x..xx.o.x..ox.o.x..ox.o.x..ox.o.x..ox.o.x..xx.o.x..xx.o.xx.xx.o.x..xx.",
            },
            new Beatmap
            {
                Name = "STS9 - Beyond Right Now (Glitch Mob Remix)",
                LoopSource ="loop_BeyondRightNow",
                LoopBeatchars = "o.......x...o.......o...xxxxx...o.o.....x...o.......o...xxxxx...o.......x...o.......o...xxxxx...o.......x...o...o.......+.......o.......x...o.......o...xxxxx...o.o.....x...o...-.-.o.-.xxx+x+x+o.......x...o.......o...xxxxx...o.......x...o...o...o.o.+.......",
            },
            new Beatmap
            {
                Name = "Kanye West - Hold My Liquor",
                LoopSource ="loop_HoldMyLiquor",
                LoopBeatchars = "o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...o...............+...",
            },
            new Beatmap
            {
                Name = "Savant - Heart",
                BuildupSource = "build_Heart",
                BuildupBeatchars = "o....",
                LoopSource ="loop_Heart",
                LoopBeatchars = "o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x.-.o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...xx-.",
            },
            new Beatmap
            {
                Name = "SynSUN - Future People (Sonic Elysium Remix)",
                LoopSource ="loop_FuturePeople",
                LoopBeatchars = "o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x...o---o---o-------o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x...o...x...:.:.:.:.:.:.:.+.....x+..x+..x+..",
            },
            new Beatmap
            {
                Name = "Singularity - Nanox",
                LoopSource ="loop_Nanox",
                LoopBeatchars = "o.....o.x...o.......o...x.......o.....o.x...o.......o...x.......|x|x|x|.x...|x|x|x|xo...x.......|x|x|x|.x...|x|x|x|xo...x.......o.....o.x...o.......o...x.......o.....o.x...o.......o...x.......|x|x|x|.x...|x|x|x|xo...x.......|x|x|x|.x...|x|x|x|xo...x.......",
            },
            new Beatmap
            {
                Name = "Dayseeker - Black Earth",
                LoopSource ="loop_BlackEarth",
                LoopBeatchars = "--------o.....o.....o...+...............................x.......x.x.x.x.o.....o.....o...+...........................x...x.......--------o.....o.....o...+...............................x.......x.x.x.x.o.....o.....o...+...........................x...x.......",
            },
            new Beatmap
            {
                Name = "Ronald Jenkees - Early Morning May",
                LoopSource ="loop_EarlyMorningMay",
                LoopBeatchars = "o.......x.......o.......x.....o.o.......o.......o...o...o...o...o.......x.......o.......x.....o.o.......x.......o.......x.....o.",
            },
            new Beatmap
            {
                Name = "deadmau5 - Raise Your Weapon (Madeon Remix)",
                LoopSource ="loop_Weapon",
                LoopBeatchars = "o.-.x...o...x...o.-.x...o...x...o.-.x...o...x...o.-.x...o...x...o.-.x...o...x...o.-.x...o...x...o..o..o...o...o.............x...",
                BuildupSource = "build_Weapon",
                BuildupBeatchars = "----",
            },
            new Beatmap
            {
                Name = "BT - Love On Haight Street ",
                LoopSource ="loop_LoveOnHaightStreet",
                LoopBeatchars = "o.....x.........o.ox....oo..o...x....o...o..x......o..o..x...o.....o..x....oo..o...x....+...o+..x.+...x..o.ox.....o..o...x.....oo..o..x.....o.o.o..x..oooo......x..o.....o..x.....oo..o..x.....o..oo..x............x.......oo.ox.....oo..o..x.....o...+.........",
                BuildupSource = "build_LoveOnHaightStreet",
                BuildupBeatchars = "x......:......:.....:......:.....:.....:......:.....",
            },
            new Beatmap
            {
                Name = "Vexare - The Clockmaker",
                LoopSource ="loop_TheClockmaker",
                LoopBeatchars = "o.......-...+...x...-----...-...o..:..:.o....:..x.......o...-...o...........o...x.....x.....x...o...............+...............o.......----o--.x.......-...-...o..:..:.o....:..x.......o.-.-.-.o...........o...x...............o.......o.......x...............:...:...:...:.+.x...-----...-...o..:..:.o....:..x.......o...-...o...........o...x.....x.....x...o...............+...............o...........----x.....-.-...-...o..:..:.o....:..x.......o.-.-.-.o...........o...x...............o.......o.......x.....x.....-...",
                BuildupSource = "build_TheClockmaker",
                BuildupBeatchars = "+....",
            },
        };
    }
}
