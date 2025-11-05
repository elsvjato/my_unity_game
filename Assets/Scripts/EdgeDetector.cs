using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetector : MonoBehaviour
{

    private EnemyBehaviour enemyBehaviour;
    private void Start()
    {
        enemyBehaviour = gameObject.GetComponentInParent<EnemyBehaviour>();
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            enemyBehaviour.FilpMoveDirection();
        }
    }
}
