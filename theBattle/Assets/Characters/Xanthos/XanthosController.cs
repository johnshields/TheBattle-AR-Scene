using UnityEngine;
/*
 * John Shields
 *
 * The Battle - Xanthos Controller
 */

namespace Characters.Xanthos
{
    public class XanthosController : MonoBehaviour
    {
        // Xanthos Stats
        private float _currentProfile;
        [SerializeField] public float lowProfile = 0.02f;

        private Rigidbody _bodyPhysics;
        private Animator _animator;
        private BoxCollider _boxCollider;

        // animation bools
        private int _idleActive;
        private int _walkActive;
        private int _backWActive;
        private int _attackActive;

        private void Start()
        {
            _bodyPhysics = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider>();

            _idleActive = Animator.StringToHash("idleActive");
            _walkActive = Animator.StringToHash("walkActive");
            _backWActive = Animator.StringToHash("backWActive");
            _attackActive = Animator.StringToHash("AttackActive");
        }

        // Fixed Update is called once per fixed frame 
        private void FixedUpdate()
        {
            Movement();
            WalkBack();
            Attack();
        }

        private void Movement()
        {
            var z = Input.GetAxis("Horizontal") * _currentProfile;
            transform.Translate(0, 0, z);

            // Player Inputs
            var forwardPressed = Input.GetKey("d");

            switch (forwardPressed)
            {
                case true:
                    // Walk
                    _animator.SetBool(_idleActive, false);
                    _animator.SetBool(_walkActive, true);
                    _animator.SetBool(_backWActive, false);
                    _animator.SetBool(_attackActive, false);
                    break;
                case false:
                    // Idle
                    _animator.SetBool(_idleActive, true);
                    _animator.SetBool(_walkActive, false);
                    _animator.SetBool(_backWActive, false);
                    _animator.SetBool(_attackActive, false);
                    break;
            }

            _currentProfile = lowProfile;
        }

        private void WalkBack()
        {
            // Player Inputs
            var backPressed = Input.GetKey("a");
            // Animator bool
            var backActive = _animator.GetBool(_backWActive);
            
            if (backPressed)
            {
                // Move Back
                _animator.SetBool(_walkActive, false);
                _animator.SetBool(_attackActive, false);
                _animator.SetBool(_idleActive, false);
                _animator.SetBool(_backWActive, true);
            }

            if (!backActive || backPressed) return;
            // Idle
            _animator.SetBool(_walkActive, false);
            _animator.SetBool(_attackActive, false);
            _animator.SetBool(_idleActive, true);
            _animator.SetBool(_backWActive, false);
        }

        private void Attack()
        {
            // Player Inputs
            var attackPressed = Input.GetKey("left shift");
            // Animator bool
            var attackActive = _animator.GetBool(_attackActive);

            switch (attackPressed)
            {
                // Inspect
                case true:
                    _animator.SetBool(_walkActive, false);
                    _animator.SetBool(_idleActive, false);
                    _animator.SetBool(_attackActive, true);
                    _currentProfile = 0;
                    break;
            }

            // Idle
            if (!attackActive || attackPressed) return;
            _animator.SetBool(_walkActive, false);
            _animator.SetBool(_idleActive, false);
            _animator.SetBool(_attackActive, false);
        }
        
    }
}