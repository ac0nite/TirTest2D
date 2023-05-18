using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _muzzleTransform;

        public Rigidbody2D Rigidbody => _rigidbody;
        public Transform MuzzleTransform => _muzzleTransform;

        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        #region FACTORY

        public class Factory : PlaceholderFactory<Cannon> 
        { }

        #endregion
    }
}