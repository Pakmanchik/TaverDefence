using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Addressable.Contract;

namespace Addressable.Impl
{
    public class AddressableImplUpdated : IAddressable
    {
        private readonly Dictionary<string, AssetInfo> _handles = new();

        public async Task Load()
        {
            var operation = Addressables.InitializeAsync();
            await operation.Task;
        }

        public async Task<TResource> GetAsset<TResource>(string key) where TResource : class
        {
            if (_handles.TryGetValue(key, out var entry))
            {
                Debug.Log($"Resource already loaded (key: {key}, Type: {typeof(TResource)})");

                entry.ReferenceCount++;
                _handles[key] = entry;
                return (TResource)entry.Handle.Result;
            }

            Debug.Log($"Load Asset (key: {key}, Type: {typeof(TResource)})");

            var handleResource = Addressables.LoadAssetAsync<TResource>(key);
            var loadedAsset    = await AssetCreate(key, handleResource);
            return loadedAsset;
        }

        public async Task<TResource> GetInstanceAsset<TResource>(string key, Transform parent = null)
            where TResource : class
        {
            if (_handles.TryGetValue(key, out var entry))
            {
                Debug.Log($"Resource already loaded (key: {key}, Type: {typeof(TResource)})");

                ++entry.ReferenceCount;
                _handles[key] = entry;
                var instanceHandle = Object.Instantiate((GameObject)entry.HandleResource, parent);

                if (typeof(TResource) != typeof(GameObject))
                    return instanceHandle.GetComponent<TResource>();

                return instanceHandle as TResource;
            }

            Debug.Log($"Instantiate Asset (key: {key}, Type: {typeof(TResource)})");

            var handleResource = Addressables.InstantiateAsync(key, parent);
            var result         = await AssetCreateInstance<TResource>(key, handleResource);
            return result;
        }

        private async Task<TResource> AssetCreate<TResource>(string key, AsyncOperationHandle<TResource> handle)
            where TResource : class
        {
            var loadedAsset = await handle.Task;
            _handles[key] =
                new(handle, 1, false,
                    loadedAsset); // Устанавливаем начальный счетчик ссылок в 1, отмечаем как обычный актив
            return loadedAsset;
        }

        private async Task<TResource> AssetCreateInstance<TResource>(
            string key, AsyncOperationHandle<GameObject> handle) where TResource : class
        {
            var instance = await handle.Task;
            _handles[key] =
                new(handle, 1, true, instance); // Устанавливаем начальный счетчик ссылок в 1, отмечаем как экземпляр

            if (typeof(TResource) != typeof(GameObject))
                return instance.GetComponent<TResource>();

            return instance as TResource;
        }

        public async Task ReleaseAsset(string key)
        {
            if (!_handles.TryGetValue(key, out var entry))
            {
                Debug.Log($"No such AssetGUID. Perhaps the asset has not been created yet (key: {key})");
                
                await Task.CompletedTask;
                return;
            }
            
            entry.ReferenceCount -= 1;

            if (entry.ReferenceCount > 0)
                _handles[key] = entry;
            else
            {
                _handles.Remove(key);
                if (entry.IsInstance)
                    Addressables.ReleaseInstance(entry.Handle);
                else
                    Addressables.Release(entry.Handle);

                Debug.Log($"Released Asset (key: {key})");
            }

            Resources.UnloadUnusedAssets();

            await Task.CompletedTask;
        }

        public Task ReleaseAllAsset()
        {
            foreach (var (key, entry) in _handles)
            {
                if (entry.IsInstance)
                    Addressables.ReleaseInstance(entry.Handle);
                else
                    Addressables.Release(entry.Handle);
            }

            _handles.Clear();
            Resources.UnloadUnusedAssets();

            return Task.CompletedTask;
        }

        private class AssetInfo
        {
            public readonly object HandleResource;
            public readonly bool IsInstance;
            public int ReferenceCount;
            public AsyncOperationHandle Handle;

            public AssetInfo(AsyncOperationHandle handle, int referenceCount, bool isInstance, object handleResource)
            {
                Handle         = handle;
                ReferenceCount = referenceCount;
                IsInstance     = isInstance;
                HandleResource = handleResource;
            }
        }
    }
}