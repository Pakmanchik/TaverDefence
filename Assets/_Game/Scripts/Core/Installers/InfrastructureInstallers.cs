using Addressable.Contract;
using Addressable.Impl;
using DI.Contract;
using Localization.Contract;
using Localization.Impl;
using MessageBus.Contract;
using MessageBus.Impl;

namespace TowerDefence.Core.Installers
{
    public sealed class InfrastructureInstallers
    {
        private readonly IAddressable _addressable;

        public InfrastructureInstallers(IAddressable addressable)
        {
            _addressable = addressable;
        }
        
        public async void InstallTo(IDiContainer diContainer)
        {
            diContainer.BindSingleton<IMessageBus, MessageBusImpl>();
            diContainer.BindFromInstance(_addressable);

            diContainer.BindSingleton<LocalizationService>();
            diContainer.BindAsCachedFromResolve<ILocalizationCollection, LocalizationService>();
            diContainer.BindAsCachedFromResolve<ILocalizationService, LocalizationService>();
        }
    }
}