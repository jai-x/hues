using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace hues.Game.Tests.Visual
{
    [TestFixture]
    public class TestSceneCJK : HuesTestScene
    {
        private const string testEN = "Imperdiet hendrerit eum et, eu mei delenit appetere. Placerat atomorum suscipiantur nec ad, et vis case meis, eros ponderum suavitate cu ius. Ne graece libris temporibus vel, qui ut elit aeque probatus.";
        private const string testCN = "谷終北物球囲風日人鳥元始方面覧提鹿物写再。見真見汚券米金迷化支史芸日伊争。小篠非正発詳記高為皇康具果心進。最更再断翻聞送載遺支用崎。呼州意学玉両節話給残寄必介予。高改難区恐関野要景居波必陵氏山口。欲男弦片能全自止題義勢詐。";
        private const string testJP = "策ふぎさ宮訪こフ本63系ぴ談作ほのす負表でょいぱ勝数ばクたで編入らすよ話條モ試検むちク大活メ静教ロク川秒下ヨト油件欲倫げろずき。載ヲワ約最よ岡目いむ魅絵安8織ノナムウ年器阜キソ文放っせ容棋フシワネ音三選ぱゆーす山応レユ八3遺ほ横早先涼くド死同うみょ不様景の。";
        private const string testKR = "보이는 있는 무한한 우리의 끓는 우리 운다. 사람은 청춘이 황금시대를 같이. 따뜻한 생생하며. 풀이 그들의 고행을 안고. 그들은 찬미를 위하여서, 품으며. 청춘의 모래뿐일 풀밭에 커다란 위하여. 청춘의 보내는 실현에 주며.";

        private FillFlowContainer flow;

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.White,
                },
                new BasicScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    ScrollbarVisible = true,
                    ScrollbarOverlapsContent = false,
                    Child = flow = new FillFlowContainer
                    {
                        Direction = FillDirection.Vertical,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Spacing = new Vector2(0, 2),
                    },
                },
            };
        }

        private SpriteText makeText(int i, string text) => new SpriteText
        {
            Text = $"{i}:{text}",
            Colour = Colour4.Black,
            Font = new FontUsage("Silver", size: i),
        };

        protected override void LoadComplete()
        {
            base.LoadComplete();

            for (var i = 9; i < 100; i++)
            {
                flow.Add(makeText(i, testEN));
                flow.Add(makeText(i, testCN));
                flow.Add(makeText(i, testJP));
                flow.Add(makeText(i, testKR));
            }
        }

        [Test]
        public void TestVisual()
        { }
    }
}
