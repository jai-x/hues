using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Sprites;
using hues.Game.RespackElements;
using hues.Game.Stores;

namespace hues.Game.Drawables
{
    public class ImageBox : Sprite
    {
        [Resolved]
        private RespackTextureStore textureStore { get; set; }

        [Resolved]
        private Bindable<Image> currentImage { get; set; }

        [BackgroundDependencyLoader]
        private void load()
        {
            currentImage.BindValueChanged(imageChange =>
            {
                var image = imageChange.NewValue;

                if (image == null)
                    Texture = null;
                else
                    Texture = textureStore.Get(image.TexturePath);
            });
        }
    }
}
