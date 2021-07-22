using System;
using System.Linq;
using System.Collections.Generic;

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osu.Framework.Testing;
using osu.Framework.Platform;

using hues.Game.Managers;
using hues.Game.RespackElements;
using hues.Game.ResourceStores;
using hues.Game.Tests;
using hues.Game.Tests.Resources;

using NUnit.Framework;

namespace hues.Game.Test.NonVisual.Managers
{
    [HeadlessTest]
    [TestFixture]
    public class TestRespackElementManager: HuesTestScene
    {
        private class Foo : RespackElement { } 

        private class FooManager : RespackElementManager<Foo>
        {
            public IReadOnlyCollection<Foo> Items => AllElements;
        }

        [Cached]
        private readonly Bindable<Foo> currentObject = new Bindable<Foo>();

        private FooManager manager;

        [SetUp]
        public void SetUp()
        {
            AddStep("Set bindable to null", () =>  { currentObject.Value = null; });

            AddStep("Expire manager instance", () => { manager?.Expire(); });

            AddStep("Recreate manage instance", () => { Child = manager = new FooManager(); });
        }

        [Test]
        public void TestEmpty()
        {
            AddAssert("Bindable is null", () => currentObject.Value == null);

            AddStep("Call Next", () => { manager.Next(); });

            AddAssert("Bindable is null", () => currentObject.Value == null);

            AddStep("Call Previous", () => { manager.Previous(); });

            AddAssert("Bindable is null", () => currentObject.Value == null);
        }

        [Test]
        public void TestAddObjects()
        {
            var oneObject = new Foo();
            var multipleObjects= new Foo[] { new Foo(), new Foo() };

            AddAssert("Manager has no objects", () => manager.Items.Count == 0);

            AddStep("Add single object", () => { manager.Add(oneObject); });

            AddStep("Add multiple objects", () => { manager.Add(multipleObjects); });

            AddAssert("Objects added to manager", () => manager.Items.Count == 3);

            AddAssert("First object has correct value", () => manager.Items.Skip(0).First() == oneObject);

            AddAssert("Second object has correct value", () => manager.Items.Skip(1).First() == multipleObjects[0]);

            AddAssert("Third object has correct value", () => manager.Items.Skip(2).First() == multipleObjects[1]);
        }

        [Test]
        public void TestNextChangesBindableToFirst()
        {
            var firstObject = new Foo();

            AddStep("Add single object", () => { manager.Add(firstObject); });

            AddAssert("Bindable is null", () => currentObject.Value == null);

            AddStep("Call Next", () => { manager.Next(); });

            AddAssert("Bindable is first object", () => currentObject.Value == firstObject);
        }

        [Test]
        public void TestPreviousChangesBindableToFirst()
        {
            var firstObject = new Foo();

            AddStep("Add single object", () => { manager.Add(firstObject); });

            AddAssert("Bindable is null", () => currentObject.Value == null);

            AddStep("Call Previous", () => { manager.Previous(); });

            AddAssert("Bindable is first object", () => currentObject.Value == firstObject);
        }

        [Test]
        public void TestNextLoopsObjects()
        {
            var threeObjects = new Foo[] { new Foo(), new Foo(), new Foo() };

            AddStep("Add objects", () => { manager.Add(threeObjects); });

            AddAssert("Bindable is null", () => currentObject.Value == null);

            AddStep("Call Next", () => { manager.Next(); });

            AddAssert("Bindable is first object", () => currentObject.Value == threeObjects[0]);

            AddStep("Call Next", () => { manager.Next(); });

            AddAssert("Bindable is second object", () => currentObject.Value == threeObjects[1]);

            AddStep("Call Next", () => { manager.Next(); });

            AddAssert("Bindable is third object", () => currentObject.Value == threeObjects[2]);

            AddStep("Call Next", () => { manager.Next(); });

            AddAssert("Bindable loops back to first object", () => currentObject.Value == threeObjects[0]);
        }

        [Test]
        public void TestPreviousLoopsObjects()
        {
            var threeObjects = new Foo[] { new Foo(), new Foo(), new Foo() };

            AddStep("Add objects", () => { manager.Add(threeObjects); });

            AddAssert("Bindable is null", () => currentObject.Value == null);

            AddStep("Call Previous", () => { manager.Previous(); });

            AddAssert("Bindable is first object", () => currentObject.Value == threeObjects[0]);

            AddStep("Call Previous", () => { manager.Previous(); });

            AddAssert("Bindable loops to last object", () => currentObject.Value == threeObjects[2]);

            AddStep("Call Previous", () => { manager.Previous(); });

            AddAssert("Bindable is second object", () => currentObject.Value == threeObjects[1]);
        }

        [Test]
        public void TestNextWhenCurrentIsNotStored()
        {
            var otherObject = new Foo();
            var threeObjects = new Foo[] { new Foo(), new Foo(), new Foo() };

            AddStep("Add objects", () => { manager.Add(threeObjects); });

            AddAssert("Bindable is null", () => currentObject.Value == null);

            AddStep("Set bindable to other object", () => { currentObject.Value = otherObject; });

            AddAssert("Bindable is other object", () => currentObject.Value == otherObject);

            AddStep("Call Next", () => { manager.Next(); });

            AddAssert("Bindable is first object", () => currentObject.Value == threeObjects[0]);
        }
    }
}
