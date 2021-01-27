using Characters.Orpheus;
using UnityEngine;

namespace Characters.Xanthos
{
    /**
     * John Shields
     *
     * The Battle - Xanthos Combat
     */
    public class XanthosCombat : MonoBehaviour
    {
        // combat
        public Transform attackPoint;
        public float attackRange = 0.5f;
        public LayerMask orpheusMask;
        public int attackDamage = 40;

        public void AttackHit()
        {
            // Detect Orpheus in range of attack
            var hitOrpheus = Physics.OverlapSphere(attackPoint.position, attackRange, orpheusMask);

            // Damage Orpheus
            foreach (var orpheus in hitOrpheus)
            {
                Debug.Log("Xanthos hit " + orpheus.name);
                orpheus.GetComponent<OrpheusCombat>().TakeDamage(attackDamage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null)
                return;

            // make Attach Point visible 
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}