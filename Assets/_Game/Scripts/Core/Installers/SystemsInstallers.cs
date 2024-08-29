using DI.Contract;
using Systems.LoadingScreen.Contract;

namespace TowerDefence.Core.Installers
{
    public sealed class SystemsInstallers
    {
        private readonly ILoadScreenSystem _loadScreenSystem;

        public SystemsInstallers(ILoadScreenSystem loadScreenSystem)
        {
            _loadScreenSystem = loadScreenSystem;
        }
        
        public void InstallTo(IDiContainer diContainer)
        {
            diContainer.BindFromInstance(_loadScreenSystem);
        }
    }
}