using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //Vector3 direction = new Vector3(horizontal, 0, 0);
        //gameObject.transform.Translate(direction.normalized * Time.deltaTime * this.Speed);

        var rigidBody = gameObject.GetComponent<Rigidbody2D>();

        if (Input.GetAxis("Horizontal") > 0.01)
        {
            ExecuteRight(this.gameObject);
        }
        if (Input.GetAxis("Horizontal") < -0.01)
        {
            ExecuteLeft(this.gameObject);
        }


        if (Input.GetAxis("Vertical") > 0.01)
        {
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 3.0f);
            }
        }

    }

    public void ExecuteLeft(GameObject gameObject)
    {
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        if (rigidBody != null)
        {
            rigidBody.velocity = new Vector2(-this.Speed, rigidBody.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void ExecuteRight(GameObject gameObject)
    {
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        if (rigidBody != null)
        {
            rigidBody.velocity = new Vector2(this.Speed, rigidBody.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
