using UnityEngine;

namespace Characters.Xanthos
{
    /**
     * John Shields
     *
     * The Battle - Xanthos Controller
     */
    public class XanthosController : MonoBehaviour
    {
        // Xanthos Stats
        private float _currentProfile;
        [SerializeField] public float lowProfile = 0.05f;

        private Rigidbody _bodyPhysics;
        private Animator _animator;
        private BoxCollider _boxCollider;

        // animation bools
        private int _idleActive;
        private int _walkActive;
        private int _backWActive;
        private int _attackActive;
        private int _jumpActive;

        // combat
        public float timeBetweenAttacks;
        private bool _alreadyAttacked;

        private void Start()
        {
            _bodyPhysics = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider>();

            _idleActive = Animator.StringToHash("idleActive");
            _walkActive = Animator.StringToHash("walkActive");
            _backWActive = Animator.StringToHash("backWActive");
            _attackActive = Animator.StringToHash("AttackActive");
            _jumpActive = Animator.StringToHash("JumpActive");
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
            var forwardAPressed = Input.GetKey("right");

            switch (forwardPressed | forwardAPressed)
            {
                case true:
                    // Walk
                    _animator.SetBool(_idleActive, false);
                    _animator.SetBool(_walkActive, true);
                    _animator.SetBool(_backWActive, false);
                    _animator.SetBool(_attackActive, false);
                    _animator.SetBool(_jumpActive, false);
                    break;
                case false:
                    // Idle
                    _animator.SetBool(_idleActive, true);
                    _animator.SetBool(_walkActive, false);
                    _animator.SetBool(_backWActive, false);
                    _animator.SetBool(_attackActive, false);
                    _animator.SetBool(_jumpActive, false);
                    break;
            }

            _currentProfile = lowProfile;
        }

        private void WalkBack()
        {
            // Player Inputs
            var backPressed = Input.GetKey("a");
            var backAPressed = Input.GetKey("left");
            // Animator bool
            var backActive = _animator.GetBool(_backWActive);

            if (backPressed | backAPressed)
            {
                // Move Back
                _animator.SetBool(_walkActive, false);
                _animator.SetBool(_attackActive, false);
                _animator.SetBool(_idleActive, false);
                _animator.SetBool(_backWActive, true);
                _animator.SetBool(_jumpActive, false);
            }

            if (!backActive || backPressed | backAPressed) return;
            // Idle
            _animator.SetBool(_walkActive, false);
            _animator.SetBool(_attackActive, false);
            _animator.SetBool(_idleActive, true);
            _animator.SetBool(_backWActive, false);
            _animator.SetBool(_jumpActive, false);
        }

        private void Attack()
        {
            // Player Inputs
            var attackPressed = Input.GetKey("left shift");
            // Animator bool
            var attackActive = _animator.GetBool(_attackActive);

            if (attackPressed)
            {
                _animator.SetBool(_walkActive, false);
                _animator.SetBool(_idleActive, false);
                _animator.SetBool(_attackActive, true);
                _animator.SetBool(_backWActive, false);
                _animator.SetBool(_jumpActive, false);
                _currentProfile = 0;
            }

            if (!_alreadyAttacked)
            {
                switch (attackPressed)
                {
                    // Attack
                    case true:
                        GetComponent<XanthosCombat>().AttackHit();
                        _alreadyAttacked = true;
                        break;
                }

                _alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
            // Idle
            else if (!attackActive || attackPressed) return;

            _animator.SetBool(_walkActive, false);
            _animator.SetBool(_idleActive, false);
            _animator.SetBool(_backWActive, false);
            _animator.SetBool(_attackActive, false);
            _animator.SetBool(_jumpActive, false);
            _currentProfile = 0;
        }

        private void ResetAttack()
        {
            _alreadyAttacked = false;
        }
    }
}