using System;
using Gameplay.Bullets;
using MyBox;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class ForceTest : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _target;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _force = 1f;
        [SerializeField] private float _muzzleSpawnOffsetY = 0.1f;
        
        private IInputService _input;
        private IBulletSpawner _bulletSpawner;
        private IWeaponModelSetter _weaponModelSetter;
        private IBulletFacade _bulletFacade;
        private Vector3 _positionSpawn;

        [Inject]
        public void Construct(IInputService inputService, IWeaponModelSetter weaponModelSetter, IBulletFacade bulletFacade)
        {
            _weaponModelSetter = weaponModelSetter;
            _bulletFacade = bulletFacade;
            
            _input = inputService;
            _input.EventTouchPosition += OnTouch;
        }

        private void Start()
        {
            _weaponModelSetter.Level = 1;
            _weaponModelSetter.BulletType = BulletType.BOMB;
        }

        private void OnTouch(Vector2 position)
        {
            ApplyForce(position);
            MuzzleTurning(position);
            OneShot(position);
        }

        private void OneShot(Vector3 mousePosition)
        {
            Vector2 pos = _camera.ScreenToWorldPoint(mousePosition);
            var dir = pos - _target.position;
            //
            Debug.DrawLine(pos, _target.position, Color.red, .5f);
            //

            var ray = new Ray(_muzzle.position, dir);
            //_positionSpawn = _muzzle.position.SetY(_muzzle.position.y + _muzzleSpawnOffsetY);
            _positionSpawn = ray.GetPoint(_muzzleSpawnOffsetY);
            
            var bullet = _bulletFacade.Spawn(_positionSpawn);
            bullet.Shot(dir.normalized * .9f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_positionSpawn, .1f);
        }

        private void MuzzleTurning(Vector3 mousePosition)
        {
            _muzzle.rotation = LookAt2D(mousePosition);
        }
        
        private Quaternion LookAt2D(Vector3 mousePosition)
        {
            Vector3 target = _camera.ScreenToWorldPoint(mousePosition);
            Vector3 direction = target - _muzzle.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void ApplyForce(Vector3 mousePosition)
        {
            Vector2 pos = _camera.ScreenToWorldPoint(mousePosition);
            var dir = _target.position - pos;
            _target.AddForce(dir * _force, ForceMode2D.Impulse);
        }
    }
}