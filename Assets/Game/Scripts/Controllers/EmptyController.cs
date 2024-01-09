using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "EmptyController", menuName = "InputController/EmptyController")]
    public class EmptyController : InputController
    {
        public override bool RetrieveAttackInput(GameObject gameObject)
        {
            throw new System.NotImplementedException();
        }

        public override bool RetrieveDashInput(GameObject gameObject)
        {
            throw new System.NotImplementedException();
        }

        public override bool RetrieveJumpInput(GameObject gameObject)
        {
            return false;
        }

        public override float RetrieveMoveInput(GameObject gameObject)
        {
            return 0;
        }

        public override bool RetrieveWeaponSwitchInput(GameObject gameObject)
        {
            throw new System.NotImplementedException();
        }
    }
}
