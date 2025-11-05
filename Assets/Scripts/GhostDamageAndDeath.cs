using UnityEngine;

public class GhostDamageAndDeath : MonoBehaviour
{
    public float damageRadius = 0.5f;
    public float destroyDelay = 1f;

    private EnemyBehaviourAI enemyAI;
    private Animator animator;

    void Start()
    {
        enemyAI = GetComponent<EnemyBehaviourAI>();
        animator = GetComponent<Animator>();

        if (enemyAI == null || animator == null)
        {
            Debug.LogError("can't find EnemyBehaviourAI or Animator!");
        }
    }

    private void Update()
    {
        //checking on killing
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);

            hero hero = playerObject.GetComponent<hero>();
            if (hero != null && distanceToPlayer < damageRadius && Input.GetKeyDown(KeyCode.E) && !hero.isGrounded)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        animator.SetTrigger("IsDead");
        Destroy(gameObject, destroyDelay);
    }
}