using System.Collections;
using UnityEngine;

public class EnemyIntetaction : MonoBehaviour
{
    private UnInstanceController unInstanceController;
    private bool isVulnerable = true;

    void Start()
    {
        unInstanceController = GameObject.Find("HeartContainer").GetComponent<UnInstanceController>();
        if (unInstanceController == null)
        {
            Debug.LogError("UnInstanceController is not found!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check on collision
        if ((collision.CompareTag("Enemy") || collision.CompareTag("Ghost")) && isVulnerable)
        {
            //damage
            unInstanceController.DestroyInstance(); //lose heart
            StartCoroutine(Invulnerability()); //activate invisiblity
        }
    }

    private IEnumerator Invulnerability()
    {
        isVulnerable = false; //invisible
        float invulnerabilityDuration = 2f;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            while (invulnerabilityDuration > 0)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                yield return new WaitForSeconds(invulnerabilityDuration / 10f);

                spriteRenderer.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(invulnerabilityDuration / 10f);

                invulnerabilityDuration -= invulnerabilityDuration / 10f;
            }
        }

        isVulnerable = true;
    }
}