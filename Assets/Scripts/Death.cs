using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector3 InitialPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.transform.position = this.InitialPosition;
        FindObjectOfType<AudioManager>().Play("GameOver");
    }
    // Start is called before the first frame update
    void Start()
    {
        this.InitialPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
