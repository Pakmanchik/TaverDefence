using System.Linq;
using System.Threading.Tasks;
using Addressable.Contract;
using TowerDefence.Game.Entity.Configs;
using TowerDefence.Game.Entity.Configs.Towers;
using TowerDefence.Game.Entity.Towers;
using TowerDefence.Game.Shared;
using TowerDefence.Game.Stats.Managers;
using UnityEngine;
using Zenject;

namespace TowerDefence.Game.Factory
{
    public sealed class TowerFactory
    {
        [Inject] private readonly CollectionTowerConfigs _collectionTowerConfigs;
        [Inject] private readonly IAddressable _addressable;

        /// <param name="level"> от 0  до ...</param>
        public async Task<AbstractNativeTower> GetTower(TowerType type, int level)
        {
            var config = GetConfig(type);
            var value  = config.Levels[level];

            var viewTower = await _addressable.GetInstanceAsset<AbstractTower>(value._abstractTowerPrefab.AssetGUID);
            var stats = new TowerStatsManager(value._maxHealth, value._armor, value._attackRate, value._attackRange);

            return new(stats, viewTower, new BuffManager(), new DamageReceiver());
        }
        
        /// <param name="abstractTowerNow">Удаляет экземпаляр внутри</param>
        public async Task<AbstractTower> UpgradeTower(TowerType type, int newLevel, int levelNow, AbstractTower abstractTowerNow)
        {
            Object.Destroy(abstractTowerNow);

            var config = GetConfig(type);
            await _addressable.ReleaseAsset(config.Levels[levelNow]._abstractTowerPrefab.AssetGUID);

            var value = config.Levels[newLevel];

            return await _addressable.GetInstanceAsset<AbstractTower>(value._abstractTowerPrefab.AssetGUID);
        }

        /// <param name="abstractTowerNow">Удаляет экземпаляр внутри</param>
        public async void TowerDestroyed(TowerType type, int levelNow, AbstractTower abstractTowerNow)
        {
            Object.Destroy(abstractTowerNow);
            
            var config = GetConfig(type);
            await _addressable.ReleaseAsset(config.Levels[levelNow]._abstractTowerPrefab.AssetGUID);
        }

        private TowerConfig GetConfig(TowerType type) =>
            _collectionTowerConfigs.TowerConfigs.First(towerConfig => towerConfig.TowerType == TowerType.Arrow);
    }
}