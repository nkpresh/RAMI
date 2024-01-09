using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    [CreateAssetMenu(menuName = "Game/" + nameof(AnimationClipData))]
    public class AnimationClipData : ScriptableObject
    {
        public Sprite[] sprites;
        public int fps = 24;
        public bool loop;

        public void Play()
        {
            
        }

        //public void Update(SpriteRenderer spriteRenderer)
        //{
        //    //int currentFrame = 
        //}
    }

}
