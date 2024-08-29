using DI.Contract;

namespace DI.UnityZenject
{
    public sealed class ZenjectContainer : IDiContainer
    {
        private readonly PostBindingGuard _postBindingGuard;
        private readonly Zenject.DiContainer _container;

        public ZenjectContainer(PostBindingGuard postBindingGuard, Zenject.DiContainer container)
        {
            _postBindingGuard = postBindingGuard;
            _container = container;
        }

        public void BindTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _postBindingGuard.Check<TService>();
            _postBindingGuard.Check<TImplementation>();
            _container.Bind<TService>().To<TImplementation>().AsTransient();
        }

        public void BindTransient<TService>()
            where TService : class
        {
            _postBindingGuard.Check<TService>();
            _container.Bind<TService>().AsTransient();
        }
        
        public TImplementation Inject<TImplementation>(TImplementation forInject)
        {
            _container.Inject(forInject);

            return (TImplementation)forInject;
        }

        public void BindSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _postBindingGuard.Check<TService>();
            _postBindingGuard.Check<TImplementation>();
            _container.Bind<TService>().To<TImplementation>().AsSingle();
        }

        public void BindSingletonSelfTo<TService>()
            where TService : class
        {
            _postBindingGuard.Check<TService>();
            _container.BindInterfacesAndSelfTo<TService>().AsSingle();
        }

        public void BindSingleton<TService>()
            where TService : class
        {
            _postBindingGuard.Check<TService>();
            _container.Bind<TService>().AsSingle();
        }

        public void BindFromInstance<TService>(TService instance)
            where TService : class
        {
            _postBindingGuard.Check<TService>();
            _container.Bind<TService>().FromInstance(instance);
        }

        public void BindAsCached<TService, TImplementation>()
            where TService : class
            where TImplementation : TService
        {
            _postBindingGuard.Check<TService>();
            _container.Bind<TService>().To<TImplementation>().AsCached();
        }
        
        public void BindAsCachedFromResolve<TService, TImplementation>()
            where TService : class
            where TImplementation : TService
        {
            _postBindingGuard.Check<TService>();
            _container.Bind<TService>().To<TImplementation>().FromResolve().AsCached();
        }

        public void BindFromInstance<TService, TImplementation>(TImplementation instance)
            where TService : class
            where TImplementation : class, TService
        {
            _postBindingGuard.Check<TService>();
            _postBindingGuard.Check<TImplementation>();
            _container.Bind<TService>().To<TImplementation>().FromInstance(instance);
        }
    }
}