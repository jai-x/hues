using osu.Framework.Allocation;
using osu.Framework.Platform;

namespace hues.Game.Tests.Visual
{
    public class TestSceneHuesGame : HuesTestScene
    {
        [Resolved]
        private GameHost host { get; set; }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            var game = new HuesGame();
            game.SetHost(host);

            Child = game;
        }
    }
}
