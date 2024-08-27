using System.Collections.Generic;
using System.Threading.Tasks;
using Localization.Shared;

namespace Localization.Contract
{
    public interface ILanguageSwitcher
    {
        public  Dictionary<string, TextSettings> GetDictionary(Languages languages);
    }
}