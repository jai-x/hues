using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Testing;
using hues.Game.Elements;
using hues.Game.ResourceStores;
using hues.Game.Tests.Resources;

namespace hues.Game.Tests.NonVisual
{
    [HeadlessTest]
    [TestFixture]
    public class TestSongPlayer : HuesTestScene
    {
        [Resolved]
        private Bindable<Song> currentSong { get; set; }

        [Resolved]
        private Bindable<PlayableSong> currentPlayable { get; set; }

        [Resolved]
        private RespackTrackResourceStore trackResources { get; set; }

        [SetUp]
        public void SetUp()
        {
            AddStep("Add `sample_track_1` to resources", () =>
            {
                using (var stream = TestResources.OpenResource("Tracks/sample.mp3"))
                    trackResources.Add("sample_track_1", stream, stream.Length);
            });

            AddStep("Add `sample_track_2` to resources", () =>
            {
                using (var stream = TestResources.OpenResource("Tracks/sample.mp3"))
                    trackResources.Add("sample_track_2", stream, stream.Length);
            });
        }

        [Test]
        public void TestChangeSong()
        {
            var sampleSong1 = new Song { Title = "Sample Song 1", LoopSource = "sample_track_1" };
            var sampleSong2 = new Song { Title = "Sample Song 2", LoopSource = "sample_track_2" };

            AddAssert("Playable song is null", () => currentPlayable.Value == null);

            AddStep("Set current song to song1", () => { currentSong.Value = sampleSong1; });

            AddAssert("Playable song is not null", () => currentPlayable.Value != null);

            AddAssert("Playable song has correct song", () => currentPlayable.Value.Song == sampleSong1);

            AddUntilStep("Playable song is playing", () => currentPlayable.Value.IsPlaying == true);

            AddStep("Set current song to song2", () => { currentSong.Value = sampleSong2; });

            AddAssert("Playable song is not null", () => currentPlayable.Value != null);

            AddAssert("Playable song has correct song", () => currentPlayable.Value.Song == sampleSong2);

            AddUntilStep("Playable song is playing", () => currentPlayable.Value.IsPlaying == true);

            AddStep("Set current song to null", () => { currentSong.Value = null; });

            AddAssert("Playable song is null", () => currentPlayable.Value == null);
        }
    }
}
