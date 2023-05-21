using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Gameplay.Input
{
    public interface IInputService
    {
        bool Lock { set; }
        bool GetPressTouch(out Vector2 screenPosition);
        event Action EventToPress;
    }
    public class InputService : IInputService, IInitializable, IDisposable
    {
        private InputControls _inputControl;
        private bool _isLock;
        private Vector2 _position;
        
        public event Action EventToPress;

        public bool Lock
        {
            set
            {
                if (!value) _inputControl.Enable();
                else _inputControl.Disable();
            }
        }

        public bool GetPressTouch(out Vector2 screenPosition)
        {
            screenPosition = _position;
            return _inputControl.Touch.TouchPress.IsPressed();
        }

        public void Initialize()
        {
            _inputControl = new InputControls();
            Lock = false;

            _inputControl.Touch.TouchPosition.performed += OnTouchPerformedAction;
            _inputControl.Touch.TouchPress.started += OnTouchPressStartedAction;
        }
        
        private void OnTouchPressStartedAction(InputAction.CallbackContext context)
        {
            EventToPress?.Invoke();
        }

        private void OnTouchPerformedAction(InputAction.CallbackContext context)
        {
            _position = context.ReadValue<Vector2>();
        }

        public void Dispose()
        {
            _inputControl.Touch.TouchPosition.performed -= OnTouchPerformedAction;
            _inputControl.Touch.TouchPress.started -= OnTouchPressStartedAction;
            
            _inputControl?.Dispose();
        }
    }
}