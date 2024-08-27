using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace Addressable.Contract
{
    public interface IAddressable
    {
        public Task Load();
        public Task<TResource> GetAsset<TResource>(string key) where TResource : class;
        public Task<TResource> GetAsset<TResource>(AssetReference reference) where TResource : class;
        public  Task<TResource> GetInstanceAsset<TResource>(string key, Transform parent = null) where TResource : class;
        public Task<TResource> GetInstanceAsset<TResource>(AssetReference reference, Transform parent = null) where TResource : class;
        public Task ReleaseAsset(string key);
        public Task ReleaseAsset(AssetReference reference);
        public Task ReleaseInstanceAsset(string key);
        public Task ReleaseInstanceAsset(AssetReference reference);
        public Task ReleaseAllAsset();
    }
}