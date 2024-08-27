using DI.Contract;

namespace DI.UnityZenject
{
    public sealed class ZenjectServiceResolver : IServiceResolver
    {
        private readonly PostBindingGuard _postBindingGuard;
        private readonly Zenject.DiContainer _container;

        public ZenjectServiceResolver(PostBindingGuard postBindingGuard, Zenject.DiContainer container)
        {
            _postBindingGuard = postBindingGuard;
            _container = container;
        }

        public TService Resolve<TService>()
            where TService : class
        {
            _postBindingGuard.ArmGuard<TService>();
            return _container.Resolve<TService>();
        }
    }
}