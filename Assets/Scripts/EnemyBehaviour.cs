using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    private float speed;
    private int moveDirection;
    private bool isAlive = true;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        speed = 100;
        moveDirection = 1;
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            rigidbody.velocity = new Vector2(Time.deltaTime * moveDirection * speed, rigidbody.velocity.y);
        }
    }

    void Update()
    {
        animator.SetFloat("speed", speed);
    }

    public void FilpMoveDirection()
    {
        moveDirection *= -1;
        if (moveDirection < 0)
        {
            gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else if (moveDirection > 0)
        {
            gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
    public bool IsAlive { get { return isAlive; } }

    public void Die()
    {
        isAlive = false;
        animator.SetTrigger("IsDead");
    }
}
