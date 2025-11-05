using System.Collections;
using UnityEngine;

public class hero : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public bool isGrounded;
    private Animator animator;
    private SpriteRenderer flip;
    private ScoreCounter scoreCounter;
    private UnInstanceController unInstanceController;
    private float inputX;
    private bool isVulnerable = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody == null)
        {
            Debug.LogError("Rigidbody2D not found");
        }

        isGrounded = false;
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found");
        }

        flip = GetComponent<SpriteRenderer>();
        if (flip == null)
        {
            Debug.LogError("SpriteRenderer not found");
        }

        scoreCounter = GameObject.Find("ScoreUI").GetComponent<ScoreCounter>();
        if (scoreCounter == null)
        {
            Debug.LogError("ScoreCounter not found");
        }

        unInstanceController = GameObject.Find("HeartContainer").GetComponent<UnInstanceController>();
        if (unInstanceController == null)
        {
            Debug.LogError("UnInstanceController not found");
        }
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(inputX));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rigidbody.AddForce(new Vector2(0, 500));
                SpecialEffects.specialEffects.CreateSmoke(transform);
            }
        }

        if (inputX < 0)
            flip.flipX = true;

        if (inputX > 0)
            flip.flipX = false;

        //checking if player is atacking
        if (Input.GetKeyDown(KeyCode.E) && !isGrounded)
        {
            Collider2D[] ghosts = Physics2D.OverlapCircleAll(transform.position, 0.5f, LayerMask.GetMask("Ghost"));
            foreach (Collider2D ghost in ghosts)
            {
                GhostDamageAndDeath ghostScript = ghost.GetComponent<GhostDamageAndDeath>();
                if (ghostScript != null)
                {
                    ghostScript.Die(); 
                }
            }
        }
    }

    void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(inputX * 100 * Time.deltaTime, rigidbody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void TakeDamage()
    {
        if (!isVulnerable) return; 

        Debug.Log("Гравець отримав шкоду!");

        if (unInstanceController != null)
        {
            unInstanceController.DestroyInstance(); 
        }

        StartCoroutine(InvulnerabilityRoutine()); //activating invisibility
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        isVulnerable = false;
        float invulnerabilityDuration = 2f; 

        while (invulnerabilityDuration > 0)
        {
            flip.color = new Color(1, 1, 1, 0.5f); 
            yield return new WaitForSeconds(invulnerabilityDuration / 10f);

            flip.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(invulnerabilityDuration / 10f);

            invulnerabilityDuration -= invulnerabilityDuration / 10f;
        }

        isVulnerable = true; 
    }


    public bool IsGrounded => isGrounded;

    public bool IsVulnerable => isVulnerable;
}