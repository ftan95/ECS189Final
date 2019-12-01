using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPull : MonoBehaviour
{
    public bool BeingPushed;
    private float Xpos;
    // Start is called before the first frame update
    void Start()
    {
        this.Xpos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.BeingPushed == false)
        {
            transform.position = new Vector3(this.Xpos, transform.position.y);
        }
        else
        {
            this.Xpos = transform.position.x;
        }
    }
}
