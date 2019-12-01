 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float Distance = 1.0f;
    public LayerMask BoxMask;
    private GameObject Box;

    void Start()
    {
        
    }

    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, this.Distance, this.BoxMask);

        if (hit.collider != null && hit.collider.gameObject.tag == "Pushable" && Input.GetKey(KeyCode.E))
        {
            this.Box = hit.collider.gameObject;
            this.Box.GetComponent<FixedJoint2D>().enabled = true;
            this.Box.GetComponent<BoxPull>().BeingPushed = true;
            this.Box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            this.Box.GetComponent<FixedJoint2D>().enabled = false;
            this.Box.GetComponent<BoxPull>().BeingPushed = false;
        }
    }

    void OnDrawGizmos()
    {
        // Yellow Line doesn't change direction when player does.
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2) transform.position + Vector2.right * transform.localScale.x * this.Distance);
       
    }
}
