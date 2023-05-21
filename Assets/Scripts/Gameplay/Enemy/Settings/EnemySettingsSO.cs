using UnityEngine;

namespace Gameplay.Enemy.Settings
{
    [CreateAssetMenu(fileName = "_EnemySettings", menuName = "EnemySettings")]
    public class EnemySettingsSO : ScriptableObject
    {
        public EnemyParam[] EnemyParams;
    }
}