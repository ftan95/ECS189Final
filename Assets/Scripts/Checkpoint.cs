using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject[] spikes;
    private Vector3 shift;

    private void Start()
    {
        spikes = GameObject.FindGameObjectsWithTag("Spikes");
        shift = this.transform.position;
        shift.y += 1.0f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       foreach(GameObject spike in spikes)
        {
            spike.GetComponent<Death>().InitialPosition = shift;
        }
        this.GetComponent<EdgeCollider2D>().isTrigger = false;
    }



}
