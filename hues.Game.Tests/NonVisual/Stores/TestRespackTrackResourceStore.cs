using System;
using System.IO;

using NUnit.Framework;

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Logging;
using osu.Framework.Testing;

using hues.Game.Stores;
using hues.Game.Tests.Resources;

namespace hues.Game.Tests.NonVisual.Stores
{
    [HeadlessTest]
    public class TestRespackTrackResourceStore : HuesTestScene
    {
        [Resolved]
        private AudioManager audioManager { get; set; }

        private readonly RespackTrackResourceStore trackResources = new RespackTrackResourceStore();

        private ITrackStore trackStore;

        [Test]
        public void TestAddTrack()
        {
            AddStep("Create a tracks store from the tracks resources", () =>
            {
                trackStore = audioManager.GetTrackStore(trackResources);
            });

            AddStep("Add track file to resources", () =>
            {
                using (var source = TestResources.OpenResource("Tracks/sample.mp3"))
                    trackResources.Add("sample_track", source);
            });

            AddStep("Assert track file has beed added to resources", () =>
            {
                Assert.NotNull(trackResources.Get("sample_track"));
            });

            AddStep("Assert track can be fetched from the track store", () =>
            {
                Assert.NotNull(trackStore.Get("sample_track"));
            });
        }
    }
}
