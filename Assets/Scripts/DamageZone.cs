using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private UnInstanceController unInstanceController;
    private bool isVarnuable;
    void Start()
    {
        isVarnuable = true;
        unInstanceController = GameObject.Find("HeartContainer").
        GetComponent<UnInstanceController>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Waste" && isVarnuable)
        {
            unInstanceController.DestroyInstance();
            StartCoroutine(Invarnuability());
        }
    }
    private IEnumerator Invarnuability()
    {
        isVarnuable = false;
        yield return new WaitForSeconds(3);
        isVarnuable = true;
    }

    void Update()
    {

    }
}
