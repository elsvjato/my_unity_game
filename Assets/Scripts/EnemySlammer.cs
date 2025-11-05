using UnityEngine;

public class EnemySlammer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            hero hero = col.GetComponent<hero>();
            if (hero != null && hero.IsVulnerable) //check on visibility
            {
                hero.TakeDamage();
            }
        }

        if (col.CompareTag("Player") && col.gameObject.GetComponent<Rigidbody2D>().velocity.y < -1f)
        {
            Die(); 
        }
    }

    public void Die()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("IsDead"); 
        }

        Destroy(gameObject, 1f);
    }
}