using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Gameplay
{
    public interface IInputService
    {
        event Action<Vector2> EventTouchPosition;
        bool Lock { set; }
    }
    public class InputService : IInputService, IInitializable, IDisposable
    {
        private InputControls _inputControl;
        public event Action<Vector2> EventTouchPosition;
        private bool _isLock;

        public bool Lock
        {
            set
            {
                if (!value) _inputControl.Enable();
                else _inputControl.Disable();
            }
        }

        public void Initialize()
        {
            _inputControl = new InputControls();
            Lock = false;
            _inputControl.Touch.TouchPosition.performed += OnTouchAction;
        }

        private void OnTouchAction(InputAction.CallbackContext context)
        {
            EventTouchPosition?.Invoke(context.ReadValue<Vector2>());
        }

        public void Dispose()
        {
            _inputControl.Touch.TouchPosition.performed -= OnTouchAction;
            _inputControl?.Dispose();
        }
    }
}