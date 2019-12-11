using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleProjectileController : MonoBehaviour
{
    [SerializeField] private float Thrust = 100.0f;
    [SerializeField] private float MaxTimeWithoutCollision = 2.0f;
    private bool Connected = false;
    private GameObject Hit;
    private Vector3 HitPoint;
    private float TimeWithoutCollision = 0.0f;
    void Start()
    {
        this.GetComponent<Rigidbody2D>().AddForce(-transform.up * Thrust);
    }

    // Update is called once per frame
    void Update()
    {
      // Keep track of despawn time for projectile.
      if (!this.Connected)
      {
        this.TimeWithoutCollision += Time.deltaTime;
      }
      if(TimeWithoutCollision >= MaxTimeWithoutCollision)
      {
        Destroy(this.gameObject);
      }
    }

    void OnCollisionEnter2D (Collision2D collision) 
    {
      // FindObjectOfType<AudioManager>().Play("Connect");  
      // Determine whether the object should be connected to.
      if (collision.gameObject.tag.Equals("Grapplable") == true || collision.gameObject.tag.Equals("Pullable"))
      {
        this.Connected = true;
        this.Hit = collision.gameObject;
        this.HitPoint = collision.GetContact(0).point;
      }
      else if (!collision.gameObject.tag.Equals("Player") == true)
      {
        Destroy(this.gameObject);
      }

      if(collision.gameObject.tag.Equals("Pullable"))
      {
        collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
      }
    }
    public bool GetConnected ()
    {
        return this.Connected;
    }
    public GameObject GetHit ()
    {
        return this.Hit;
    }
    public Vector3 GetHitPoint ()
    {
        return this.HitPoint;
    }
 }

