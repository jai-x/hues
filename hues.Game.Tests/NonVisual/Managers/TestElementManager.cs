using NUnit.Framework;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Testing;
using hues.Game.Managers;
using hues.Game.Elements;
using hues.Game.Tests;

namespace hues.Game.Test.NonVisual.Managers
{
    [HeadlessTest]
    [TestFixture]
    public class TestElementManager : HuesTestScene
    {
        private class Foo : Element { }

        private class FooManager : ElementManager<Foo> { }

        [Cached]
        private readonly Bindable<Foo> currentObject = new Bindable<Foo>();

        [Cached]
        private readonly BindableList<Foo> allObjects = new BindableList<Foo>();

        private FooManager manager;

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = manager = new FooManager();
        }

        [SetUp]
        public void SetUp()
        {
            AddStep("Clear manager instance", () => { manager.Clear(); });
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

            AddAssert("Manager has no objects", () => allObjects.Count == 0);

            AddStep("Add single object", () => { manager.Add(oneObject); });

            AddStep("Add multiple objects", () => { manager.Add(multipleObjects); });

            AddAssert("Objects added to manager", () => allObjects.Count == 3);

            AddAssert("First object has correct value", () => allObjects.Skip(0).First() == oneObject);

            AddAssert("Second object has correct value", () => allObjects.Skip(1).First() == multipleObjects[0]);

            AddAssert("Third object has correct value", () => allObjects.Skip(2).First() == multipleObjects[1]);
        }

        [Test]
        public void TestNextChangesBindableToFirst()
        {
            var objects = new Foo[] { new Foo(), new Foo() };

            AddStep("Add objects", () => { manager.Add(objects); });

            AddAssert("Bindable is null", () => currentObject.Value == null);

            AddStep("Call Next", () => { manager.Next(); });

            AddAssert("Bindable is first object", () => currentObject.Value == objects[0]);
        }

        [Test]
        public void TestPreviousChangesBindableToFirst()
        {
            var objects = new Foo[] { new Foo(), new Foo() };

            AddStep("Add objects", () => { manager.Add(objects); });

            AddAssert("Bindable is null", () => currentObject.Value == null);

            AddStep("Call Previous", () => { manager.Previous(); });

            AddAssert("Bindable is last object", () => currentObject.Value == objects[1]);
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

            AddAssert("Bindable is third object", () => currentObject.Value == threeObjects[2]);

            AddStep("Call Previous", () => { manager.Previous(); });

            AddAssert("Bindable is second object", () => currentObject.Value == threeObjects[1]);

            AddStep("Call Previous", () => { manager.Previous(); });

            AddAssert("Bindable is first object", () => currentObject.Value == threeObjects[0]);

            AddStep("Call Previous", () => { manager.Previous(); });

            AddAssert("Bindable is third object", () => currentObject.Value == threeObjects[2]);
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
