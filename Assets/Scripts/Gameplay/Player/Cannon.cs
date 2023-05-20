using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public interface IPLayer
    {
        Transform Transform { get; }
        Rigidbody2D Rigidbody { get; }
        Transform MuzzleTransform { get; }
        void SetActive(bool active, Vector2 position = default);
    }
    public class Cannon : MonoBehaviour, IPLayer
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _muzzleTransform;

        public Transform Transform => transform;
        public Rigidbody2D Rigidbody => _rigidbody;
        public Transform MuzzleTransform => _muzzleTransform;

        public void SetActive(bool active, Vector2 position = default)
        {
            if (position != default) 
                transform.position = position;
            
            gameObject.SetActive(active);
        }
        
        #region FACTORY

        public class Factory : PlaceholderFactory<Cannon> 
        { }

        #endregion

        #region SETTINGS

        [Serializable]
        public class Settings
        {
            public float MuzzleSpawnBulletOffsetY;
            public float PowerMovementImpulse;
        }

        #endregion
    }
}