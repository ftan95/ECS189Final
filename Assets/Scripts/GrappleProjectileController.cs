using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleProjectileController : MonoBehaviour
{
    // Start is called before the first frame update
    private float Thrust = 100.0f;
    void Start()
    {
        this.GetComponent<Rigidbody2D>().AddForce(-transform.up * Thrust);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
