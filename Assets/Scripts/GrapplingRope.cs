using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingRope: MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint2D;
    public hero playerScript;
    void Start()
    {
        _distanceJoint2D.enabled = false;
    }


    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (Input.GetKeyDown(KeyCode.Mouse0) && hit.collider != null && hit.collider.CompareTag("Ceiling"))
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _lineRenderer.SetPosition(0, mousePos);
            _lineRenderer.SetPosition(1, transform.position);
            _distanceJoint2D.connectedAnchor = mousePos;
            _distanceJoint2D.enabled = true;
            _lineRenderer.enabled = true;
            playerScript.enabled = false;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _distanceJoint2D.enabled = false;
            _lineRenderer.enabled = false;
        }
        if (_distanceJoint2D.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            playerScript.enabled = true;
        }
    }

}