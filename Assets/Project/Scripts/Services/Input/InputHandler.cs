using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FebJam
{
    public class InputHandler : IDisposable
    {
        private InputSystemActions _inputActions;

        public event Action<Vector2> Moved;
        public event Action Canceled;

        public InputHandler()
        {
            _inputActions = new InputSystemActions();
            _inputActions.Enable();
            _inputActions.Player.Enable();
            _inputActions.UI.Enable();
            _inputActions.Player.Move.performed += OnMoved;
            _inputActions.UI.Cancel.performed += OnCanceld;
        }

        private void OnMoved(InputAction.CallbackContext obj)
        {
            Moved?.Invoke(obj.ReadValue<Vector2>());
        }

        private void OnCanceld(InputAction.CallbackContext _)
        {
            Canceled?.Invoke();
        }

        public Vector2 GetMousePosition()
        {
            return _inputActions.UI.Point.ReadValue<Vector2>();
        }

        public void Dispose()
        {
            _inputActions.Player.Move.performed -= OnMoved;
            _inputActions.UI.Cancel.performed -= OnCanceld;
            _inputActions.Player.Disable();
            _inputActions.UI.Disable();
            _inputActions.Disable();
        }
    }
}

