using System.Threading.Tasks;
using Localization.Shared;

namespace Localization.Contract
{
    public interface ILocalizationService
    {
        public void SwitchLanguage(Languages languages);

        public void RemoveAllWrapper(LocalizationWindow localizationWindow);
        public Languages LanguagesNow { get; }
    }
}