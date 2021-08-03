using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using hues.Game.Elements;

namespace hues.Game.Drawables
{
    public class SongList : VisibilityContainer
    {
        [Resolved]
        private BindableList<Song> allSongs { get; set; }

        [Resolved]
        private Bindable<Song> currentSong { get; set; }

        private FillFlowContainer songsFlow;

        public SongList()
        {
            Height = 150;
            Width = 435;
            Masking = true;
            BorderColour = Colour4.Black;
            BorderThickness = 1;
        }

        protected override void PopIn()
        {
            this.FadeIn(200);
        }

        protected override void PopOut()
        {
            this.FadeOut(200);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    Colour = Colour4.LightGray,
                    RelativeSizeAxes = Axes.Both,
                },
                new BasicScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    ScrollbarVisible = true,
                    ScrollbarOverlapsContent = false,
                    Child = songsFlow = new FillFlowContainer
                    {
                        Spacing = new Vector2(0, 2),
                        Direction = FillDirection.Vertical,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                    },
                }
            };
        }

        private SpriteText makeSongText(string text)
        {
            return new SpriteText
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Text = text,
                Colour = Colour4.Black,
                Font = FontUsage.Default.With(size: 9),
                Margin = new MarginPadding { Horizontal = 3, Vertical = 1 },
            };
        }

        private void updateSongs()
        {
            songsFlow.Clear();

            if (allSongs.Count == 0)
            {
                songsFlow.Add(makeSongText("No Songs! (Drag a Respack into the window)"));
                return;
            }

            foreach (var song in allSongs)
            {
                songsFlow.Add(new ClickableContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Action = () => { currentSong.Value = song; },
                    Child = makeSongText(song.Title),
                });
            }
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            updateSongs();

            allSongs.BindCollectionChanged((_,_) => { Schedule(updateSongs); });
        }
    }
}
