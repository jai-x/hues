using osu.Framework.Allocation;
using osu.Framework.Bindables;

namespace hues.Game.Drawables
{
    public class ImageList : PopUpList
    {
        [Resolved]
        private BindableList<Elements.Image> allImages { get; set; }

        [Resolved]
        private Bindable<Elements.Image> currentImage { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            Height = 150;
            Width = 300;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            allImages.BindCollectionChanged((_,_) => { Schedule(updateImages); }, true);
        }

        private void updateImages()
        {
            ClearFlow();

            if (allImages.Count == 0)
            {
                AddToFlow("No Images! (Drag a Respack into the window)", null);
                return;
            }

            foreach (var image in allImages)
                AddToFlow(image.Name, () => { currentImage.Value = image; });
        }
    }
}
