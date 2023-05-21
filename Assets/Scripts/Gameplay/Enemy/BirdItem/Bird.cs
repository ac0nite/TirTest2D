using System;
using Gameplay.Enemy.Settings;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Gameplay.Enemy.BirdItem
{
    public interface IBird
    {
        IMovement Movement { get; }
        IAnimator Animator { get; }
        void ApplyDamage(int damage);
        event Action<EnemyType, Bird> OnDeath;
    }
    
    public class Bird : MonoBehaviour, IBird
    {
        [SerializeField] private Movement _movement;
        [FormerlySerializedAs("_animator")] [SerializeField] private Animator2D animator2D;

        public IMovement Movement => _movement;
        public IAnimator Animator => animator2D;

        private IGeneratorRandomPoint _generator;
        private int _health;
        private EnemyParam _param;
        
        public event Action<EnemyType, Bird> OnDeath;

        [Inject]
        public void Construct(IGeneratorRandomPoint generatorRandomPoint)
        {
            _generator = generatorRandomPoint;
        }
        
        private void Reinitialize(EnemyParam param, EnemyResource resource)
        {
            _param = param;
            _health = param.Health;
            
            Movement.Initialise(param, _generator.GetRandomData());
            LoadResource(resource);
        }

        private void LoadResource(EnemyResource resource)
        {
            transform.localScale = Vector3.one * resource.Scale;
            Animator.Initialise(resource.SpriteAnimationObject);
            Animator.ResetAnimation();
            Animator.PlayFlyAnimation();
        }

        private void Reset()
        {
            transform.position = Vector3.zero;
        }

        public class Pool : MonoMemoryPool<EnemyParam,EnemyResource, Bird>
        {
            protected override void Reinitialize(EnemyParam param, EnemyResource resource, Bird item)
            {
                item.Reinitialize(param,resource);
                base.Reinitialize(param,resource,item);
            }

            protected override void OnDespawned(Bird item)
            {
                item.Reset();
                base.OnDespawned(item);
            }
        }

        public void ApplyDamage(int damage)
        {
            _health -= damage;
            
            if(_health <= 0)
                OnDeath?.Invoke(_param.Type, this);
        }
    }
}