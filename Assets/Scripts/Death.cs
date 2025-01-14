﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject player;
    public Vector3 InitialPosition;
    private List<Vector3> BlockList = new List<Vector3>();
    private Vector3 BlockPosition;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {  
        if (collision.gameObject.name == "REALHENRY" || collision.gameObject.name == "Player")
        {
            player.transform.position = this.InitialPosition;
            for (int i = 1; i < 11; i++)
            {
                if (GameObject.Find("Bridge " + i) != null)
                {
                    GameObject.Find("Bridge " + i).transform.position = this.BlockPosition;
                    GameObject.Find("Bridge " + i).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                }
            }
            player.GetComponent<DistanceJoint2D>().enabled = false;
            player.GetComponent<GrapplingHook>().Line.enabled = false;

            // Reset variables
            player.GetComponent<GrapplingHook>().GrappleIsActive = false;
            player.GetComponent<GrapplingHook>().IsFirstConnectedFrame = true;
            player.GetComponent<GrapplingHook>().ConnectionIsActive = false;
        }
        FindObjectOfType<AudioManager>().Play("GameOver");
    }
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("REALHENRY");
        if (this.player == null)
        {
            this.player = GameObject.Find("Player");
        }
        this.InitialPosition = player.transform.position;
        for (int i = 1; i < 11; i++)
        {
            var index = "Bridge " + i;
            if (GameObject.Find(index) != null)
            {
                this.BlockPosition = GameObject.Find(index).transform.position;
                this.BlockList.Add(this.BlockPosition);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
