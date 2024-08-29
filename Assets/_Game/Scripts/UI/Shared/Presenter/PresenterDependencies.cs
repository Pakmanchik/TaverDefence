using Addressable.Contract;
using Localization.Contract;
using MessageBus.Contract;

namespace TowerDefence.UI.Shared.Presenter
{
    public sealed class PresenterDependencies
    {
        public PresenterDependencies(IAddressable addressable, ILocalizationService localizationCollection, IMessageBus messageBus)
        {
            Addressable            = addressable;
            LocalizationCollection = localizationCollection;
            MessageBus             = messageBus;
        }

        public IAddressable Addressable { get; }
        public ILocalizationService LocalizationCollection { get; }
        public IMessageBus MessageBus { get; }

    }
}