using System;
using System.IO;

using NUnit.Framework;

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Logging;
using osu.Framework.Testing;

using hues.Game.ResourceStores;
using hues.Game.Tests.Resources;

namespace hues.Game.Tests.NonVisual.ResourceStores
{
    [HeadlessTest]
    public class TestRespackTrackResourceStore : HuesTestScene
    {
        [Resolved]
        private AudioManager audioManager { get; set; }

        private readonly RespackTrackResourceStore trackResources = new RespackTrackResourceStore();

        [Test]
        public void TestAddTrack()
        {
            var trackStream = TestResources.OpenResource("Tracks/sample.mp3");
            ITrackStore trackStore = null;

            AddAssert("Track store is null", () =>
            {
                return trackStore == null;
            });

            AddStep("Create track store from the track resources", () =>
            {
                trackStore = audioManager.GetTrackStore(trackResources);
            });

            AddAssert("Track store is created", () =>
            {
                return trackStream != null;
            });

            AddStep("Add track file to resources", () =>
            {
                trackResources.Add("sample_track", trackStream);
            });

            AddAssert("Track file has beed added to resources", () =>
            {
                return trackResources.Get("sample_track") != null;
            });

            AddAssert("Track can be fetched from track store", () =>
            {
                return trackStore.Get("sample_track") != null;
            });
        }
    }
}
