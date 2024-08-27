namespace DI.Contract
{
    public interface IDiContainer
    {
        public void BindTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        public void BindTransient<TService>()
            where TService : class;

        public void BindSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        public void BindSingleton<TService>()
            where TService : class;

        public void BindFromInstance<TService>(TService instance)
            where TService : class;

        public void BindFromInstance<TService, TImplementation>(TImplementation instance)
            where TService : class
            where TImplementation : class, TService;

        public void BindAsCached<TService, TImplementation>()
            where TService : class
            where TImplementation : TService;

        public void BindAsCachedFromResolve<TService, TImplementation>()
            where TService : class
            where TImplementation : TService;
    }
}