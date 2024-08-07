using LightInject;

namespace MemoryLeakTests.Tests;

public static class TestRegistrator
{
    public static void Register(ServiceContainer serviceContainer)
    {
        serviceContainer.Register<CollectionViewTests>();
    }
}