using System;
using System.Linq;

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osu.Framework.Testing;
using osu.Framework.Platform;

using hues.Game.Managers;
using hues.Game.Stores;
using hues.Game.Tests;
using hues.Game.Tests.Resources;

using NUnit.Framework;

namespace hues.Game.Test.NonVisual.Managers
{
    [HeadlessTest]
    [TestFixture]
    public class TestImageManager : HuesTestScene
    {
        [Cached]
        protected readonly Bindable<Image> currentImage = new Bindable<Image>();

        private ImageManager manager;

        private void reset()
        {
            manager?.Expire();
            currentImage.Value = null;
            Child = manager = new ImageManager();
        }

        [Test]
        public void TestEmpty()
        {
            AddStep("Reset", () => { reset(); });
            AddAssert("Bindable is null", () => currentImage.Value == null);
            AddStep("Call Next", () => { manager.Next(); });
            AddAssert("Bindable is null", () => currentImage.Value == null);
            AddStep("Call Previous", () => { manager.Previous(); });
            AddAssert("Bindable is null", () => currentImage.Value == null);
        }

        [Test]
        public void TestAddImage()
        {
            var firstImage = new Image { Name = "Test Image", TexturePath = "test_texture_path" };

            AddStep("Reset", () => { reset(); });
            AddStep("Add single image", () => { manager.Add(firstImage); });
            AddAssert("Image is added", () =>
            {
                return manager.Images.Count == 1 &&
                       manager.Images.First() == firstImage;
            });
        }

        [Test]
        public void TestAddMultipleImages()
        {
            var newImages = new Image[]
            {
                new Image { Name = "Test Image 1", TexturePath = "test_texture_path_1" },
                new Image { Name = "Test Image 2", TexturePath = "test_texture_path_2" },
            };

            AddStep("Reset", () => { reset(); });
            AddStep("Add multiple images", () => { manager.Add(newImages); });
            AddAssert("Images are added", () =>
            {
                return manager.Images.Count == 2 &&
                       manager.Images.First() == newImages[0] &&
                       manager.Images.Skip(1).First() == newImages[1];
            });
        }

        [Test]
        public void TestNextChangesBindableToFirst()
        {
            var firstImage = new Image { Name = "Test Image", TexturePath = "test_texture_path" };

            AddStep("Reset", () => { reset(); });
            AddStep("Add single image", () => { manager.Add(firstImage); });
            AddAssert("Bindable is null", () => currentImage.Value == null);
            AddStep("Call Next", () => { manager.Next(); });
            AddAssert("Bindable is first image", () => currentImage.Value == firstImage);
        }

        [Test]
        public void TestPreviousChangesBindableToFirst()
        {
            var firstImage = new Image { Name = "Test Image", TexturePath = "test_texture_path" };

            AddStep("Reset", () => { reset(); });
            AddStep("Add single image", () => { manager.Add(firstImage); });
            AddAssert("Bindable is null", () => currentImage.Value == null);
            AddStep("Call Previous", () => { manager.Previous(); });
            AddAssert("Bindable is first image", () => currentImage.Value == firstImage);
        }

        [Test]
        public void TestNextLoopsImages()
        {
            var threeImages = new Image[]
            {
                new Image { Name = "Test Image 1", TexturePath = "test_texture_path_1" },
                new Image { Name = "Test Image 2", TexturePath = "test_texture_path_2" },
                new Image { Name = "Test Image 3", TexturePath = "test_texture_path_3" },
            };

            AddStep("Reset", () => { reset(); });
            AddStep("Add single image", () => { manager.Add(threeImages); });
            AddAssert("Bindable is null", () => currentImage.Value == null);
            AddStep("Call Next", () => { manager.Next(); });
            AddAssert("Bindable is first image", () => currentImage.Value == threeImages[0]);
            AddStep("Call Next", () => { manager.Next(); });
            AddAssert("Bindable is second image", () => currentImage.Value == threeImages[1]);
            AddStep("Call Next", () => { manager.Next(); });
            AddAssert("Bindable is third image", () => currentImage.Value == threeImages[2]);
            AddStep("Call Next", () => { manager.Next(); });
            AddAssert("Bindable loops back to first image", () => currentImage.Value == threeImages[0]);
        }

        [Test]
        public void TestPreviousLoopsImages()
        {
            var threeImages = new Image[]
            {
                new Image { Name = "Test Image 1", TexturePath = "test_texture_path_1" },
                new Image { Name = "Test Image 2", TexturePath = "test_texture_path_2" },
                new Image { Name = "Test Image 3", TexturePath = "test_texture_path_3" },
            };

            AddStep("Reset", () => { reset(); });
            AddStep("Add single image", () => { manager.Add(threeImages); });
            AddAssert("Bindable is null", () => currentImage.Value == null);
            AddStep("Call Previous", () => { manager.Previous(); });
            AddAssert("Bindable is first image", () => currentImage.Value == threeImages[0]);
            AddStep("Call Previous", () => { manager.Previous(); });
            AddAssert("Bindable loops to last image", () => currentImage.Value == threeImages[2]);
            AddStep("Call Previous", () => { manager.Previous(); });
            AddAssert("Bindable is second image", () => currentImage.Value == threeImages[1]);
        }
    }
}
