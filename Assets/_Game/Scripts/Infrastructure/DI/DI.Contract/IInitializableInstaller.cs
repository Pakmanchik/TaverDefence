namespace DI.Contract
{
    public interface IInitializableInstaller : IInstaller
    {
        public void Initialize(IServiceResolver serviceResolver);
    }
}