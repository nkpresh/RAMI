using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Controller), typeof(CollisionDataRetriever), typeof(Rigidbody2D))]
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField, Range(0f, 100f)] private float _maxSpeed = 4f;
        [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
        [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;

        [HideInInspector] public float gravityMultiplier = 1;

        private Controller _controller;
        private Vector2 _direction, _desiredVelocity, _velocity;
        private Rigidbody2D _body;
        private CollisionDataRetriever _collisionDataRetriever;

        private float _maxSpeedChange, _acceleration;
        private bool _onGround;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _collisionDataRetriever = GetComponent<CollisionDataRetriever>();
            _controller = GetComponent<Controller>();
        }

        private void Update()
        {
            _direction.x = _controller.input.RetrieveMoveInput(this.gameObject);
            if(Mathf.Abs(_direction.x) >= 0.4f)
            {
                if(_controller.anim != null)
                {
                    _controller.anim.xDirection = (int)Mathf.Sign(_direction.x);
                }
                   
            }
            _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _collisionDataRetriever.Friction, 0f);
        }

        private void FixedUpdate()
        {
            _onGround = _collisionDataRetriever.OnGround;
            _velocity = _body.velocity;

            if (_controller.isDashing) return;

            _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
            _maxSpeedChange = _acceleration * Time.deltaTime;
            _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x * gravityMultiplier, _maxSpeedChange);

            _body.velocity = _velocity;

            UpdateAnimations();
        }

        private void UpdateAnimations()
        {
            if (_onGround)
            {
                if (Mathf.Abs(_velocity.x) > 0.2f)
                {
                    _controller.anim?.UpdateState(_controller.anim.Move);
                    
                }
                else
                {
                    _controller.anim?.UpdateState(_controller.anim.Idle);
                }
            }
            else
            {
                //Air born
                _controller.anim?.UpdateState(_controller.anim.Idle);
            }
        }
    }
}
