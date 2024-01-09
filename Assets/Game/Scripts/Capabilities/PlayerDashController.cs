using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace Game
{
    public class PlayerDashController : MonoBehaviour
    {

        public float dashDelay = 0.5f;
        public float dashDistance = 20f;
        [Range(0f, 0.5f)]
        public float dashTime = 0.1f;

        private Controller _controller;
        private Rigidbody2D _body;
        private CollisionDataRetriever _collisionDataRetriever;

        private bool canDash = true;
        private bool Dashing = false;
        
        private float dashDelayTracker;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _collisionDataRetriever = GetComponent<CollisionDataRetriever>();
            _controller = GetComponent<Controller>();

            canDash = true;
            
        }

        void Update()
        {
            bool dashPerformed = _controller.input.RetrieveDashInput(gameObject);
            //Debug.Log(dashPerformed);
            if(canDash && dashPerformed == true)
            {
                if(dashDelayTracker > dashDelay)
                {
                    StartCoroutine(Dash());
                }
            }

            dashDelayTracker += Time.deltaTime;

            bool grounded = _collisionDataRetriever.OnGround;
            if(grounded == true && canDash == false && Dashing == false)
            {
                canDash = true;
            }
        }

        private void FixedUpdate()
        {
            if(Dashing)
            {
                Vector3 velocity = _body.velocity;
                velocity.y = 0;
                //
                _body.velocity = velocity;
            }
        }

        private IEnumerator Dash()
        {
            canDash = false;
            Dashing = true;
            _controller.isDashing = true;

            _body.AddForce(Vector2.right * _controller.Direction * dashDistance, ForceMode2D.Impulse);

            yield return new WaitForSeconds(dashTime);
            Dashing = false;
            _controller.isDashing = false;
            _body.velocity = Vector2.zero;
            dashDelayTracker = 0;

            yield return new WaitForSeconds(0.5f);
            canDash = true;
            
        }
    }

}
