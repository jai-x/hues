using System;
using System.IO;

using NUnit.Framework;

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Logging;
using osu.Framework.Testing;

using hues.Game.Stores;
using hues.Game.Tests.Resources;

namespace hues.Game.Tests.NonVisual
{
    [HeadlessTest]
    public class TestRespackTrackResourceStore : HuesTestScene
    {
        [Resolved]
        private AudioManager audioManager { get; set; }

        private readonly RespackTrackResourceStore respackTrackResourceStore = new RespackTrackResourceStore();

        [Test]
        public void TestAddTrack()
        {
            var respackTracks = audioManager.GetTrackStore(respackTrackResourceStore);

            using (var source = TestResources.OpenResource("deepnote.mp3"))
                respackTrackResourceStore.Add("deepnote", source);

            var addedTrack = respackTracks.Get("deepnote");

            Assert.NotNull(addedTrack);
        }
    }
}
