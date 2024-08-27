using System.Collections.Generic;
using System.Threading.Tasks;
using Addressable.Contract;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Addressable.Impl
{
    public class AddressableImpl : IAddressable
    {
        private readonly Dictionary<string, AsyncOperationHandle> _handles = new();
       
       public async Task<TResource> GetAsset<TResource>(AssetReference reference) where TResource : class =>
           await GetAsset<TResource>(reference.AssetGUID);
       
       public async Task Load()
       {
           var operation = Addressables.InitializeAsync();
           await operation.Task;
       }
       
        public async Task<TResource> GetAsset<TResource>(string key) where TResource : class
        {
            var exist = CheckUploadedResource(key);
            if (exist)
                await ReleaseAsset(key);

            var handleResource = Addressables.LoadAssetAsync<TResource>(key);
            
            Debug.Log($"Send Asset (key: {key}, Type: {typeof(TResource)})");
            return await AssetCreate(key, handleResource);
        }

        public async Task<TResource> GetInstanceAsset<TResource>(AssetReference reference, Transform parent = null) where TResource : class =>
            await GetInstanceAsset<TResource>(reference.AssetGUID, parent);
        
        public async Task<TResource> GetInstanceAsset<TResource>(string key, Transform parent = null) where TResource : class
        {
            var  exist = CheckUploadedResource(key);
            if (exist)
                await ReleaseInstanceAsset(key);

            var handleResource = Addressables.InstantiateAsync(key, parent);
            var result         = await AssetCreate(key, handleResource);
            
            Debug.Log($"Send Instance Asset (key: {key}, Type: {result.name})");
                
            if (typeof(TResource) == typeof(GameObject))
            {
                var res = result as TResource;

                return res;
            }
            
            return result.GetComponent<TResource>();
        }
        
        private async Task<TResource> AssetCreate<TResource>(string key, AsyncOperationHandle<TResource> handle)
        {
            var loadedAsset = await handle.Task;
            _handles.Add(key, handle);
            
            return loadedAsset;
        }
        
        public Task ReleaseAsset(AssetReference reference) => ReleaseAsset(reference.AssetGUID);

        public Task ReleaseAsset(string key)
        {
            if (!CheckPresenceOfDictionary(key, out var handle))
            {
                Debug.Log($"No such AssetGUID. Perhaps the asset has not been created yet  (key: {key})");
                return Task.CompletedTask;
            }
            
            _handles.Remove(key);
            Addressables.Release(handle);
            Resources.UnloadUnusedAssets();
            
            Debug.Log($"Release Asset (key: {key})");
            
            return Task.CompletedTask;
        }
  
        public Task ReleaseInstanceAsset(AssetReference reference) => ReleaseInstanceAsset(reference.AssetGUID);

        public Task ReleaseInstanceAsset(string key)
        {
            if (!CheckPresenceOfDictionary(key, out var handle))
            {
                Debug.Log($"No such AssetGUID. Perhaps the asset has not been created yet  (key: {key})");
                return Task.CompletedTask;
            }
            
            _handles.Remove(key);
            Addressables.ReleaseInstance(handle);
            Resources.UnloadUnusedAssets();
            
            Debug.Log($"Release Instance Asset (key: {key})");

            return Task.CompletedTask;
        }

        public Task ReleaseAllAsset()
        {
            foreach (var handle in _handles)
                Addressables.Release(handle.Value);
            
            Resources.UnloadUnusedAssets();
            _handles.Clear();

            return Task.CompletedTask;
        }

        private bool CheckPresenceOfDictionary(string key, out AsyncOperationHandle handle)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                handle = default;
                return false;
            }

            if (_handles.ContainsKey(key!))
                return _handles.TryGetValue(key, out handle);

            handle = default;
            return false;
        }

        private  bool CheckUploadedResource(string key)
        {
            return _handles.TryGetValue(key, out var handle);
        }
    }
}