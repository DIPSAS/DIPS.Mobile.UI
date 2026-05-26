using LightInject;

namespace MemoryLeakTests.Tests;

public static class TestRegistrator
{
    public static void Register(ServiceContainer serviceContainer)
    {
        // Existing tests
        serviceContainer.Register<CollectionViewTests>()
                        .Register<ModalTests>()
                        .Register<SimpleModalTests>()
                        .Register<NavigationTests>()
                        .Register<ShellItemChangedTests>();

        // Component tests
        serviceContainer.Register<EntryTest>()
                        .Register<EditorTest>()
                        .Register<InputFieldTest>()
                        .Register<SearchBarTest>()
                        .Register<ButtonTest>()
                        .Register<ListItemTest>()
                        .Register<SelectionTest>()
                        .Register<ChipTest>()
                        .Register<ImageTest>()
                        .Register<ActivityIndicatorTest>()
                        .Register<CollectionViewTest>()
                        .Register<LabelTest>()
                        .Register<ModalComponentTest>();

        // Effect tests
        serviceContainer.Register<TouchEffectTest>()
                        .Register<TouchLongPressEffectTest>()
                        .Register<LayoutEffectTest>();
    }
}