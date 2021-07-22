using System;

using NUnit.Framework;

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Bindables;
using osu.Framework.Testing;

using hues.Game.RespackElements;
using hues.Game.ResourceStores;
using hues.Game.Tests.Resources;

namespace hues.Game.Tests.NonVisual
{
    [HeadlessTest]
    [TestFixture]
    public class TestSongPlayer : HuesTestScene
    {
        [Resolved]
        private AudioManager audioManager { get; set; }

        [Cached]
        private readonly Bindable<Song> currentSong = new Bindable<Song>();

        [Cached]
        private readonly Bindable<PlayableSong> currentPlayable = new Bindable<PlayableSong>();

        private SongPlayer player;

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
        {
            var audioManager = parent.Get(typeof(AudioManager)) as AudioManager;

            var dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

            var trackResources = new RespackTrackResourceStore();

            using (var stream = TestResources.OpenResource("Tracks/sample.mp3"))
                trackResources.Add("sample_track_1", stream);

            using (var stream = TestResources.OpenResource("Tracks/sample.mp3"))
                trackResources.Add("sample_track_2", stream);

            var trackStore = audioManager.GetTrackStore(trackResources);

            dependencies.CacheAs(trackStore);

            return dependencies;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Child = player = new SongPlayer();
        }

        [SetUp]
        public void SetUp()
        {
            AddStep("Set current song to null", () => { currentSong.Value = null; });
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
