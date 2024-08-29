using System;
using UnityEngine;
using TowerDefence.Game.Entity.Towers;
using TowerDefence.Game.Shared;
using UnityEngine.AddressableAssets;

namespace TowerDefence.Game.Entity.Configs.Towers
{
    [CreateAssetMenu(fileName = "TowerConfig", menuName = "Configs/Game/Towers/TowerConfig")]
    public sealed class TowerConfig : ScriptableObject
    {
        [SerializeField] private TowerType _towerType;
        [SerializeField] private TowerLevelData[] _levels;

        public TowerLevelData[] Levels => _levels;
        public TowerType TowerType => _towerType;
    }

    [Serializable]
    public sealed class TowerLevelData
    {
        [SerializeField] public int _level;
        [SerializeField] public float _maxHealth = 100;
        [SerializeField] public float _armor = 20;
        [SerializeField] public float _attackRate = 0.5f;
        [SerializeField] public float _attackRange = 2;
        
        [SerializeField] public AssetReference _abstractTowerPrefab;
    }

}