using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRatio : MonoBehaviour
{
    public GameObject Player;
    public Vector3 GrabPostion;
    public float Ratio;

    void Start()
    {
        
    }


    void Update()
    {
        var scaleX = Vector3.Distance(this.Player.transform.position, this.GrabPostion) / this.Ratio;
        GetComponent<LineRenderer>().material.mainTextureScale = new Vector2(scaleX, 1.0f);
    }
}
