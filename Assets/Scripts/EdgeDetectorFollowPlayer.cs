using UnityEngine;

public class EdgeDetectorFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 2f; 
    private EnemyBehaviourAI enemyBehaviour;

    private void Start()
    {
        enemyBehaviour = GetComponentInParent<EnemyBehaviourAI>();
        if (enemyBehaviour == null)
        {
            Debug.LogError("EnemyBehaviourAI is not found");
        }
    }

    private void Update()
    {
        if (player == null)
        {
            Debug.LogError("player is not sighned");
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        //start to follow if player is in range
        if (distanceToPlayer < detectionRange)
        {
            enemyBehaviour.UpdatePath(); //calculating path
        }
    }
}