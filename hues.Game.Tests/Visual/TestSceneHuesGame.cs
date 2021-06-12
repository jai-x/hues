using osu.Framework.Allocation;
using osu.Framework.Platform;

namespace hues.Game.Tests.Visual
{
    public class TestSceneHuesGame : HuesTestScene
    {
        private HuesGame game;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            game = new HuesGame();
            game.SetHost(host);

            Add(game);
        }
    }
}
