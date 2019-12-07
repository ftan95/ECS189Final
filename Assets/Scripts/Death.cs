using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.transform.position = new Vector3(-20.1f, -5.3f, 2.82f);
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
