using System.Collections;
using UnityEngine;

namespace Characters.Orpheus
{
    /**
     * John Shields
     *
     * The Battle - Orpheus Combat
     */
    public class OrpheusCombat : MonoBehaviour
    {
        // Orpheus
        public int maxHealth = 100;
        public int currentHealth;

        // animation 
        private Animator _animator;
        private int _hitActive;
        private int _deathActive;

        private void Start()
        {
            currentHealth = maxHealth;

            _animator = GetComponent<Animator>();
            _hitActive = Animator.StringToHash("HitActive");
            _deathActive = Animator.StringToHash("DeathActive");
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            // hit animation
            _animator.SetTrigger(_hitActive);
            if (currentHealth <= 0)
            {
                FallToDeath();
            }
        }

        private void FallToDeath()
        {
            Debug.Log("Orpheus died!");

            // death animation
            _animator.SetBool(_deathActive, true);

            // disable Orpheus
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<OrpheusController>().enabled = false;
            this.enabled = false;
        }
    }
}