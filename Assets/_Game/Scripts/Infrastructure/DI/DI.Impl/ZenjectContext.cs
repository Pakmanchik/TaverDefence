using DI.Contract;

namespace DI.UnityZenject
{
    public sealed class ZenjectContext : IDiContext
    {
        public IDiContainer Container { get; }
        public IServiceResolver ServiceResolver { get; }

        public static void Create(out IDiContext diContext)
        {
            diContext = new ZenjectContext();
        }

        public static void Create(out IDiContext diContext, out IDiContainer diContainer)
        {
            diContext = new ZenjectContext();
            diContainer = diContext.Container;
        }

        public static void Create(out IDiContext diContext, out IDiContainer diContainer, out IServiceResolver serviceResolver)
        {
            diContext = new ZenjectContext();
            diContainer = diContext.Container;
            serviceResolver = diContext.ServiceResolver;
        }

        private ZenjectContext()
        {
            var baseContainerObj = new Zenject.DiContainer();

            var guard = new PostBindingGuard();
            Container = new ZenjectContainer(guard, baseContainerObj);
            ServiceResolver = new ZenjectServiceResolver(guard, baseContainerObj);
        }
    }
}