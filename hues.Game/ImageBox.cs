using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace hues.Game
{
    public class ImageBox : CompositeDrawable
    {
    /*
        [Resolved]
        private LargeTextureStore textures { get; set; }

        [Resolved]
        private ImageManager him { get; set; }

        private Dictionary<Image, Sprite> sprites = new Dictionary<Image, Sprite>();
        private Sprite shown;

        public ImageBox()
        {
            RelativeSizeAxes = Axes.Both;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            InternalChildren = Image.All.Select(img => {
                var sprite = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                    FillMode = FillMode.Fill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Texture = textures.Get(img.TexturePath),
                    Alpha = 0,
                };
                sprites.Add(img, sprite);
                return sprite;
            }).ToArray();

            showImage(him.Current.Value);
            him.Current.ValueChanged += (img) => showImage(img.NewValue);
        }

        protected void showImage(Image img)
        {
            shown?.Hide();
            shown = sprites[img];
            shown.Show();
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            sprites.Clear();
        }
    */
    }
}
