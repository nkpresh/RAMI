using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    [CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
    public class PlayerController : InputController
    {
        private PlayerInputActions _inputActions;
        private bool _isJumping;

        private void OnEnable()
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Gameplay.Enable();
            _inputActions.Gameplay.Jump.started += JumpStarted;
            _inputActions.Gameplay.Jump.canceled += JumpCanceled;
            //_inputActions.Gameplay.Attack.performed += AttackStarted;
        }

        private void OnDisable()
        {
            _inputActions.Gameplay.Disable();
            _inputActions.Gameplay.Jump.started -= JumpStarted;
            _inputActions.Gameplay.Jump.canceled -= JumpCanceled;
            //_inputActions.Gameplay.Attack.performed -= AttackStarted;
            _inputActions = null;
        }

        private void JumpCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _isJumping = false;
        }

        private void JumpStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _isJumping = true;
        }

        public override bool RetrieveJumpInput(GameObject gameObject)
        {
            return _isJumping;
        }

        public override float RetrieveMoveInput(GameObject gameObject)
        {
            return _inputActions.Gameplay.Move.ReadValue<Vector2>().x;
        }

        public override bool RetrieveAttackInput(GameObject gameObject)
        {
            return _inputActions.Gameplay.Attack.IsPressed();
        }

        public override bool RetrieveDashInput(GameObject gameObject)
        {
            return _inputActions.Gameplay.Dash.IsPressed();
        }

        public override bool RetrieveWeaponSwitchInput(GameObject gameObject)
        {
            return _inputActions.Gameplay.SwitchWeapon.IsPressed();
        }
    }
}
