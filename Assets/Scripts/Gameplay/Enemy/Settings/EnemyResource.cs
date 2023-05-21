using System;
using GabrielBigardi.SpriteAnimator;

namespace Gameplay.Enemy.Settings
{
    [Serializable]
    public class EnemyResource
    {
        public EnemyType Type;
        public SpriteAnimationObject SpriteAnimationObject;
        public float Scale;
    }
}