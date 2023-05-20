using Gameplay.Bullets;
using Gameplay.Input;
using Gameplay.Settings;
using UnityEngine;

namespace Gameplay.Player
{
    public interface IPlayerShooting
    {
        void Initialise(IPLayer player);
        void SetActive(bool active);
    }

    public class PlayerShooting : IPlayerShooting
    {
        private Vector3 _positionSpawn;
        
        private readonly Camera _camera;
        private readonly IBulletFacade _bulletFacade;
        private readonly IInputService _input;
        
        private IPLayer _player;
        private Vector3 _worldPosition;
        private readonly Cannon.Settings _playerSettings;
        private Ray _directionShotRay;
        private CustomDoTweenTimer _timer;
        private Vector2 _screenPosition;

        public PlayerShooting(
            Camera camera,
            IInputService inputService, 
            IBulletFacade bulletFacade,
            GameplaySettings gameplaySettings)
        {
            _camera = camera;
            _bulletFacade = bulletFacade;
            _input = inputService;
            _playerSettings = gameplaySettings.PLayerSettings;
        }
        
        public void Initialise(IPLayer player)
        {
            _player = player;
            _timer = new CustomDoTweenTimer(_bulletFacade.CurrentBulletParam.ShotTimer);
        }

        public void SetActive(bool active)
        {
            if (active)
            {
                _timer.RunLoop(TryToShot);
            }
            else
            {
                _timer.Dispose();
            }
        }

        private void TryToShot()
        {
            if (CanShot)
            {
                Shot();
            }
        }
        
        private bool CanShot => _input.GetPressTouch(out _screenPosition);

        private void Shot()
        {
            _worldPosition = WorldPosition(_screenPosition);
            OneShot(_worldPosition);
            ApplyForce(_worldPosition);
        }

        private Vector3 WorldPosition(Vector2 mousePosition)
        {
            return _camera.ScreenToWorldPoint(mousePosition);
        }

        private void OneShot(Vector3 worldPosition)
        {
            //Vector2 pos = _camera.ScreenToWorldPoint(mousePosition);
            var dir = worldPosition - _player.Transform.position;
            
            Debug.DrawLine(worldPosition, _player.Transform.position, Color.red, .5f);
            //

            _directionShotRay = new Ray(_player.MuzzleTransform.position, dir);
            //_positionSpawn = _muzzle.position.SetY(_muzzle.position.y + _muzzleSpawnOffsetY);
            _positionSpawn = _directionShotRay.GetPoint(_playerSettings.MuzzleSpawnBulletOffsetY);
            
            var bullet = _bulletFacade.Spawn(_positionSpawn);
            bullet.Shot(dir.normalized);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_positionSpawn, .1f);
        }

        private void MuzzleTurning(Vector3 worldPosition)
        {
            _player.MuzzleTransform.rotation = LookAt2D(worldPosition);
        }
        
        private Quaternion LookAt2D(Vector3 worldPosition)
        {
            // Vector3 target = _camera.ScreenToWorldPoint(mousePosition);
            Vector3 direction = worldPosition - _player.Transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void ApplyForce(Vector3 position)
        {
            // Vector2 pos = _camera.ScreenToWorldPoint(mousePosition);
            var dir = _player.Transform.position - position;
            _player.Rigidbody.AddForce(dir * _playerSettings.PowerMovementImpulse, ForceMode2D.Impulse);
        }
    }
}