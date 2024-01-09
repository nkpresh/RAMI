using UnityEngine;

namespace Game
{
    public class Controller : MonoBehaviour
    {
        public InputController input = null;
        public PlayerAnimation anim = null;

        public SpriteRenderer spriteRenderer;

        public float Direction => spriteRenderer.flipX? -1f : 1f;

        public bool isDashing;
    }
}
