using System.Collections.Generic;
using System.Threading.Tasks;
using Addressable.Impl;
using DI.Contract;
using DI.UnityZenject;
using Systems.LoadingScreen.Impl;
using TowerDefence.Core.Installers;
using TowerDefence.Core.Shared;
using TowerDefence.Game.Entity.Towers;
using TowerDefence.Game.Factory;
using TowerDefence.Game.Shared;
using UnityEngine;

namespace TowerDefence.Core
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ConfigCollection _configCollection;
        [SerializeField] private LoadScreenSystem _loadScreenSystem;

        private IDiContainer _container;
        private IServiceResolver _resolver;

        private AddressableImplUpdated _addressable;

        public async void Entry()
        {
            await Init();

            LaunchInstallers();

            StartGame();
        }

        private async Task Init()
        {
            ZenjectContext.Create(out _, out _container, out _resolver);

            _addressable = new();
            await _addressable.Load();
        }
        
        /// <summary>
        /// Должна соблюдаться последовательность
        /// Cистема сохранения/загрузки -> конфиги -> Ифраструктура -> зависимости -> остальное
        /// </summary>
        private void LaunchInstallers()
        {
            var configs        = new ConfigInstallers(_configCollection);
            var infrastructure = new InfrastructureInstallers(_addressable);
            var dependencies   = new DependenciesInstallers();
            var systems        = new SystemsInstallers(_loadScreenSystem);
            var presenters     = new PresenterInstallers();
            var managers       = new ManagersInstallers();

            infrastructure.InstallTo(_container);
            configs.InstallTo(_container);
            dependencies.InstallTo(_container);
            systems.InstallTo(_container);
            presenters.InstallTo(_container);
            managers.InstallTo(_container);
        }

        private void StartGame()
        {
            _container.Inject(new RootManager()).Start();
        }
    }
}