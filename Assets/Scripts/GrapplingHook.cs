using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public LineRenderer Line; 
    private DistanceJoint2D Joint;
    private Vector3 TargetPos;
    private RaycastHit2D Hit;
    public float Distance = 5.0f;
    public LayerMask Mask;
    public float Step = 0.2f;
    private GameObject AimingArrow;
    //private GameObject Box;
    //public float LineDistance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.Joint = GetComponent<DistanceJoint2D>();
        this.Joint.enabled = false;
        this.Line.enabled = false;
        this.AimingArrow = GameObject.FindGameObjectWithTag("Arrow");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.Joint.distance > 3.0f)
        {
            this.Joint.distance -= this.Step;
        }
        //else
        //{
        //    this.Line.enabled = false;
        //    this.Joint.enabled = false;
        //}
        //Physics2D.queriesStartInColliders = false;
        //RaycastHit2D hitter = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, this.Distance, this.Mask);

        // Press Space to fire grappling hook.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //AimingArrow.GetComponent<Aimer>().Fire();
            Debug.Log("Space");
            this.TargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.TargetPos.z = 0;
            this.Hit = Physics2D.Raycast(transform.position, this.TargetPos - transform.position, this.Distance, this.Mask);

            if (this.Hit.collider != null && this.Hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                this.Joint.enabled = true;
                this.Joint.connectedBody = this.Hit.collider.gameObject.GetComponent<Rigidbody2D>();
                this.Joint.connectedAnchor = this.Hit.point - new Vector2(this.Hit.collider.transform.position.x, this.Hit.collider.transform.position.y);
                this.Joint.distance = Vector2.Distance(transform.position, this.Hit.point);

                this.Line.enabled = true;
                this.Line.SetPosition(0, transform.position);
                this.Line.SetPosition(1, this.Hit.point);
                this.Line.GetComponent<RopeRatio>().GrabPostion = this.Hit.point;

                // Disable aiming arrow
                AimingArrow.GetComponent<SpriteRenderer>().enabled = false;
            }

            //if (hitter.collider != null && hitter.collider.gameObject.tag == "Pushable")
            //{
            //    this.Box = hitter.collider.gameObject;
            //    this.Box.GetComponent<FixedJoint2D>().enabled = true;
            //    this.Box.GetComponent<BoxPull>().BeingPushed = true;
            //    this.Box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            //}
            //else if (Input.GetKeyUp(KeyCode.Space))
            //{
            //    this.Box.GetComponent<FixedJoint2D>().enabled = false;
            //    this.Box.GetComponent<BoxPull>().BeingPushed = false;
            //}
        }

        if (Input.GetKey(KeyCode.Space))
        {
            this.Line.SetPosition(0, transform.position);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.Joint.enabled = false;
            this.Line.enabled = false;

            // Enable aiming arrow
            AimingArrow.GetComponent<SpriteRenderer>().enabled = true;
        }

    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * this.LineDistance);

    //}
}
