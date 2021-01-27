using UnityEngine;
using UnityEngine.AI;


namespace Characters.Orpheus
{
    /**
     * John Shields
     *
     * The Battle - Orpheus Controller
     */
    public class OrpheusController : MonoBehaviour
    {
        // Xanthos
        public Transform xanthosPlayer;
        public LayerMask xanthosMask;

        // Orpheus
        public NavMeshAgent orpheusAgent;

        // animation 
        private Animator _animator;
        private int _idleActive;
        private int _walkActive;
        private int _attackActive;

        // walking
        private bool _walkPointSet;

        // attacking
        public float timeBetweenAttacks;
        private bool _alreadyAttacked;

        // states
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        private void Start()
        {
            _animator = GetComponent<Animator>();

            _idleActive = Animator.StringToHash("IdleActive");
            _walkActive = Animator.StringToHash("WalkActive");
            _attackActive = Animator.StringToHash("AttackActive");
        }

        private void FixedUpdate()
        {
            var position = transform.position;
            playerInSightRange = Physics.CheckSphere(position, sightRange, xanthosMask);
            playerInAttackRange = Physics.CheckSphere(position, attackRange, xanthosMask);

            if (playerInSightRange && !playerInAttackRange) Follow();
            if (playerInAttackRange && playerInSightRange) Attack();
        }

        private void Follow()
        {
            // trigger walk animation
            if (orpheusAgent.SetDestination(xanthosPlayer.position))
            {
                _animator.SetBool(_walkActive, true);
                _animator.SetBool(_idleActive, false);
                _animator.SetBool(_attackActive, false);
            }
            else
            {
                _animator.SetBool(_walkActive, false);
                _animator.SetBool(_idleActive, true);
                _animator.SetBool(_attackActive, false);
            }
        }

        private void Attack()
        {
            // Make sure Orpheus doesn't move
            // and have him look at Xanthos
            orpheusAgent.SetDestination(transform.position);
            transform.LookAt(xanthosPlayer);

            switch (_alreadyAttacked)
            {
                case false:
                {
                    // attack
                    _animator.SetBool(_walkActive, false);
                    _animator.SetBool(_idleActive, false);
                    _animator.SetBool(_attackActive, true);

                    // magic fire projectile


                    // reset attack
                    _alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                    break;
                }
            }
        }

        private void ResetAttack()
        {
            _alreadyAttacked = false;
        }
    }
}