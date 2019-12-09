using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathInPullStage : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        this.player.transform.position = new Vector3(-45.5f, 2.7f, 2.82f);
    }



    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
