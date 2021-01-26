using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

/*
 * John Shields
 *
 * The Battle - Orpheus Controller
 */

namespace Characters.Orpheus
{
    public class OrpheusController : MonoBehaviour
    {
        public NavMeshAgent orpheusAgent;
        public Transform xanthosPlayer;
        
        // animation 
        private Animator _animator;
        private int _idleActive;
        private int _walkActive;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();

            _idleActive = Animator.StringToHash("IdleActive");
            _walkActive = Animator.StringToHash("WalkActive");
        }
        
        private void FixedUpdate()
        {
            Follow();
        }

        private void Follow()
        {
            if (orpheusAgent.SetDestination(xanthosPlayer.position))
            {
                _animator.SetBool(_walkActive, true);
                _animator.SetBool(_idleActive, false);
            }
            else
            {
                _animator.SetBool(_walkActive, false);
                _animator.SetBool(_idleActive, true);
            }
        }
        
    }
    
}