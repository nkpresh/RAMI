using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerAttackController : MonoBehaviour
    {

        public float attackCooldown = 0.15f;

        private Controller _controller;
        private Rigidbody2D _body;
        private CollisionDataRetriever _ground;

        private float _attackCooldown;

        bool attackDown = false;
        

        void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<CollisionDataRetriever>();
            _controller = GetComponent<Controller>();

            _controller.anim.animationEnd += OnAttackAnimationEnd;
            _attackCooldown = attackCooldown;
        }

        private void Update()
        {
            bool attack = _controller.input.RetrieveAttackInput(gameObject);
            if (attack && attackDown == false)
            {
                if(_attackCooldown > attackCooldown)
                {
                    attackDown = true;
                    AttackTriggered();
                }
                
            }
            else if(attack == false)
            {
                if(attackDown == true)
                {
                    attackDown = false;
                }
            }
            _attackCooldown += Time.deltaTime;
        }

        private void AttackTriggered()
        {
            _controller.anim.LockAnimation(_controller.anim.Attack);
            _controller.anim.UpdateState(_controller.anim.Attack, true);
            _attackCooldown = -1;
        }


        private void OnAttackAnimationEnd(string animationName)
        {
            _controller.anim.UnlockAnimation();
            _attackCooldown = 0;
        }


        private void OnDestroy()
        {
            _controller.anim.animationEnd -= OnAttackAnimationEnd;
            
        }
    }

}

