namespace DI.Contract
{
    public interface IServiceResolver
    {
        public TService Resolve<TService>()
            where TService : class;
    }
}