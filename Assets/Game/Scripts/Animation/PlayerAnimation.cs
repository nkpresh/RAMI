using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class PlayerAnimation : MonoBehaviour , IAnimationController
    {
        [HideInInspector] public string Idle = "Idle";
        [HideInInspector] public string Move = "Move";
        [HideInInspector] public string Attack = "Attack";
        [HideInInspector] public string Death = "Death";

        [HideInInspector] public string Sword = "_Sword";
        [HideInInspector] public string Spear = "_Spear";

        [HideInInspector] public string currentWeapon = "_Sword";
        [HideInInspector] public Animator animator;
        [HideInInspector] public int xDirection = 1;
        [HideInInspector] public SpriteRenderer spriteRenderer;

        public Action<string> animationEnd;
        public Action<string> animationStart;

        private string currentState = string.Empty;
        private string lockedAnimation = string.Empty;

        private void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            int dir = (xDirection == 0) ? 1 : xDirection;
            spriteRenderer.flipX = dir == -1;
        }

        public void LockAnimation(string animation)
        {
            animation += currentWeapon;
            lockedAnimation = animation;
        }

        public void UnlockAnimation()
        {
            lockedAnimation = string.Empty;
        }

        public bool CannotPlay(string animation, bool isOnce)
        {
            if(lockedAnimation != string.Empty && lockedAnimation != animation)
            {
                return true;
            }

            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            return info.IsName(animation) && isOnce == true;
        }

        public void UpdateState(string animation, bool isOnce = true)
        {
            UpdateState(animation, 0, 0, isOnce);
        }

        public void UpdateState(string animation, int layer, bool isOnce = true)
        {
            UpdateState(animation, layer, 0, isOnce);
        }

        public void UpdateState(string animation, float time, bool isOnce = true)
        {
            UpdateState(animation, 0, time, isOnce);
        }

        public void UpdateState(string animation, int layer, float time, bool isOnce = true)
        {
            animation += currentWeapon;

            if (CannotPlay(animation, isOnce)) return;
            animator.Play(animation, layer, time);
            currentState = animation;
        }

        public void OnAnimationStart(string animation)
        {
            animationStart.Invoke(animation);
        }

        public void OnAnimationEnd(string animation)
        {
            animationEnd?.Invoke(animation);
        }

        public void AnimationEvent(int eventId)
        {

        }
    }
}