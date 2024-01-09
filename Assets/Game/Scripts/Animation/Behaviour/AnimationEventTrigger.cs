using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Game.AnimationEventTrigger;

namespace Game
{
    public enum StateStage
    {
        Enter,
        Exit
    }

    [System.Serializable]
    public struct ClipStateProperty
    {
        public StateStage stateStage;
        public string animationName;
    }

    public class AnimationEventTrigger : StateMachineBehaviour
    {
        

        public ClipStateProperty[] clipStateInfo;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            for (int i = 0; i < clipStateInfo.Length; i++)
            {
                if (clipStateInfo[i].stateStage == StateStage.Enter)
                {
                    animator.GetComponent<IAnimationController>().OnAnimationStart(clipStateInfo[i].animationName);
                }

            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            for (int i = 0; i < clipStateInfo.Length; i++)
            {
                if (clipStateInfo[i].stateStage == StateStage.Exit)
                {
                    animator.GetComponent<IAnimationController>().OnAnimationStart(clipStateInfo[i].animationName);
                }

            }
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }

}

