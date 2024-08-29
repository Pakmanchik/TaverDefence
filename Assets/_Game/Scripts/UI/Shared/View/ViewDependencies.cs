using Localization.Contract;
using MessageBus.Contract;

namespace TowerDefence.UI.Shared.View
{
    public sealed class ViewDependencies
    {
        public ViewDependencies(ILocalizationCollection localizationCollection, IMessageBus messageBus)
        {
            LocalizationCollection = localizationCollection;
            MessageBus             = messageBus;
        }

        public ILocalizationCollection LocalizationCollection { get; }
        public IMessageBus MessageBus { get; }

    }
}