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
    private GameObject GrappleProjectile;
    private GameObject CollidedObject;
    private Vector3 CollisionPoint;
    private bool IsFirstConnectedFrame = true;
    private bool GrappleIsActive = false;
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
        //if (this.Joint.distance > 1.0f)
        //{
        //    this.Joint.distance -= this.Step;
        //}
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
            // If player can fire a projectile
            if(!this.GrappleIsActive)
            {
                this.GrappleProjectile = AimingArrow.GetComponent<Aimer>().Fire();
                this.GrappleIsActive = true;
            }

            // If a connection is active
            if(!this.IsFirstConnectedFrame)
            {
                this.Joint.enabled = false;
                this.Line.enabled = false;

                // Enable aiming arrow
                AimingArrow.GetComponent<SpriteRenderer>().enabled = true;

                // Reset variables
                this.GrappleIsActive = false;
                this.IsFirstConnectedFrame = true;
                Debug.Log("Destroying projectile");
                Destroy(this.GrappleProjectile);
            }
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

        if(this.GrappleProjectile && this.GrappleProjectile.activeInHierarchy)
        {
            if (this.GrappleProjectile.GetComponent<GrappleProjectileController>().GetConnected())
            {
                Debug.Log("Making connection");
                this.CollidedObject = this.GrappleProjectile.GetComponent<GrappleProjectileController>().GetHit();
                this.CollisionPoint = this.GrappleProjectile.GetComponent<GrappleProjectileController>().GetHitPoint();
                this.Joint.enabled = true;
                this.Joint.connectedBody = this.CollidedObject.GetComponent<Rigidbody2D>();

                this.Joint.connectedAnchor = this.CollisionPoint - new Vector3(this.CollidedObject.transform.position.x, this.CollidedObject.transform.position.y);
                
                if(this.IsFirstConnectedFrame)
                {
                    this.Joint.distance = Vector2.Distance(this.transform.position, this.CollisionPoint);
                }

                this.Line.enabled = true;
                this.Line.SetPosition(0, this.transform.position);
                this.Line.SetPosition(1, this.CollisionPoint);
                this.Line.GetComponent<RopeRatio>().GrabPostion = this.CollisionPoint;

                // Disable aiming arrow
                AimingArrow.GetComponent<SpriteRenderer>().enabled = false;
                this.GrappleProjectile.SetActive(false);
                this.IsFirstConnectedFrame = false;
               
            } 
        }
        else
        {
            this.GrappleIsActive = false;
        }

        // Keep updating the line if the grapple is still 'connected'.
        if(!IsFirstConnectedFrame)
        {
            this.Line.enabled = true;
            this.Line.SetPosition(0, this.transform.position);
            this.Line.SetPosition(1, this.CollisionPoint);
            this.Line.GetComponent<RopeRatio>().GrabPostion = this.CollisionPoint;
        }


    }
}
