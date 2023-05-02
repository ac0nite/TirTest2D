using UnityEngine;
using Zenject;

namespace Gameplay
{
    public interface IBird
    {
        IMovement Movement { get; }
        IAnimator Animator { get; }
    }
    
    public class Bird : MonoBehaviour, IBird
    {
        [SerializeField] private Movement _movement;
        [SerializeField] private Animator _animator;
        
        public IMovement Movement => _movement;
        public IAnimator Animator => _animator;

        #region FACTORY

        public class Factory : PlaceholderFactory<IBird> 
        { }

        #endregion
    }
}