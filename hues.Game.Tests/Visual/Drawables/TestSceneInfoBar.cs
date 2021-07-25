using System;

using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Logging;
using osuTK;

using hues.Game.Drawables;
using hues.Game.Managers;
using hues.Game.RespackElements;
using hues.Game.Stores;
using hues.Game.Tests.Resources;

using NUnit.Framework;

namespace hues.Game.Tests.Visual.Drawables
{
    [TestFixture]
    public class TestSceneInfoBar : HuesTestScene
    {
        [SetUp]
        public void SetUp()
        {
            Schedule(() =>
            {
                Child = new InfoBar
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                };
            });
        }

        [Test]
        public void TestVisual()
        {
        }
    }
}
