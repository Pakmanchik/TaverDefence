namespace DI.Contract
{
    public interface IDiContext
    {
        public IDiContainer Container { get; }
        public IServiceResolver ServiceResolver { get; }
    }
}