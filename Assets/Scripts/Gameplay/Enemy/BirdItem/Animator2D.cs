using System;
using GabrielBigardi.SpriteAnimator;
using UnityEngine;

namespace Gameplay.Enemy.BirdItem
{
    public interface IAnimator
    {
        void Initialise(SpriteAnimationObject spriteAnimationObject);
        void ResetAnimation();
        void PlayFlyAnimation(int frames = default);
        void PlayExplosionAnimation(Action callback);
    }
    public class Animator2D : MonoBehaviour, IAnimator
    {
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        private SpriteAnimator _spriteAnimator;

        private const string FlyAnimation = "fly";
        private const string ExplosionAnimation = "explosion";

        private void Awake()
        {
            _spriteAnimator = GetComponent<SpriteAnimator>();
            _spriteRenderer = GetComponentInParent<SpriteRenderer>();
            _rigidbody = GetComponentInParent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _spriteRenderer.flipX = _rigidbody.velocity.x < 0;
        }

        public void Initialise(SpriteAnimationObject spriteAnimationObject)
        {
            _spriteAnimator.SetSpriteAnimation(spriteAnimationObject);
        }

        public void ResetAnimation()
        {
            _spriteAnimator.Resume();
        }

        public void PlayFlyAnimation(int frames = default)
        {
            _spriteAnimator.Play(FlyAnimation);
            
            if(frames != default) 
                _spriteAnimator.SetCurrentFrame(frames);
        }

        public void PlayExplosionAnimation(Action callback)
        {
            _spriteAnimator.Play(ExplosionAnimation);
            _spriteAnimator.OnComplete(callback);
        }
    }
}