using System.Collections.Generic;
using TowerDefence.Game.Entity.Configs.Towers;
using UnityEngine;

namespace TowerDefence.Game.Entity.Configs
{
    [CreateAssetMenu(fileName = "CollectionTowerConfigs", menuName = "Configs/Game/CollectionTowerConfigs")]
    public sealed class CollectionTowerConfigs : ScriptableObject
    {
        [SerializeField] private List<TowerConfig> _towerConfigs;
        
         public IReadOnlyCollection<TowerConfig> TowerConfigs => _towerConfigs;
    }
}