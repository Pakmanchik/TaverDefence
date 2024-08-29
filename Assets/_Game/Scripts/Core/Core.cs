using UnityEngine;

namespace TowerDefence.Core
{
    public sealed class Core
    {
        private const string EntryPointKey = "Core/EntryPoint";
        
        private static Core _instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void PreSet()
        {
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Start()
        {
            _instance = new Core();

            _instance.StartEntryPoint();
        }

        private void StartEntryPoint()
        {
            var entryPoint = Object.Instantiate(Resources.Load<EntryPoint>(EntryPointKey));
            entryPoint.Entry();
        }
    }
}