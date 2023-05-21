using Gameplay.Bullets.Settings;
using UnityEngine;

namespace Gameplay.Bullets
{
    [CreateAssetMenu(fileName = "_BulletSettings", menuName = "BulletSettings")]
    public class BulletSettingsSO : ScriptableObject
    {
        public BulletParam[] BulletParams;
    }
}