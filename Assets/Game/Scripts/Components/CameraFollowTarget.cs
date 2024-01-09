using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CameraFollowTarget : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        [HideInInspector] public int lookDirection;


        private void Update()
        {
            lookDirection = (spriteRenderer.flipX ? -1 : 1);
        }
    }

}

