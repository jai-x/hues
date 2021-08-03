using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace hues.Game.Drawables
{
    public class ImageList : VisibilityContainer
    {
        [Resolved]
        private BindableList<Elements.Image> allImages { get; set; }

        [Resolved]
        private Bindable<Elements.Image> currentImage { get; set; }

        private FillFlowContainer imagesFlow;

        public ImageList()
        {
            Height = 150;
            Width = 300;
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
                    Child = imagesFlow = new FillFlowContainer
                    {
                        Spacing = new Vector2(0, 2),
                        Direction = FillDirection.Vertical,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                    },
                }
            };
        }

        private SpriteText makeImageText(string text)
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

        private void updateImages()
        {
            imagesFlow.Clear();

            if (allImages.Count == 0)
            {
                imagesFlow.Add(makeImageText("No Images! (Drag a Respack into the window)"));
                return;
            }

            foreach (var image in allImages)
            {
                imagesFlow.Add(new ClickableContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Action = () => { currentImage.Value = image; },
                    Child = makeImageText(image.Name),
                });
            }
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            updateImages();

            allImages.BindCollectionChanged((_,_) => { Schedule(updateImages); });
        }
    }
}
