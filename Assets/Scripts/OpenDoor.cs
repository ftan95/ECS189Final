using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    void OnTriggerEnter2D(Collider2D col)
    {
        this.prefab.SetActive(true);
        this.GetComponent<BoxCollider2D>().isTrigger = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
