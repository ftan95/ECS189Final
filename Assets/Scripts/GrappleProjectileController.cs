﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleProjectileController : MonoBehaviour
{
    // Start is called before the first frame update
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
      if (collision.gameObject.tag.Equals("Grapplable") == true)
      {
        this.Connected = true;
        this.Hit = collision.gameObject;
        this.HitPoint = collision.GetContact(0).point;
      }
      else if (!collision.gameObject.tag.Equals("Player") == true)
      {
        Destroy(this.gameObject);
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
