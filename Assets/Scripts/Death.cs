using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Vector3 InitialPosition;
    private List<Vector3> BlockList = new List<Vector3>();
    private Vector3 BlockPosition;
    private Vector3 BlockPosition2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Player")
        {
            player.transform.position = this.InitialPosition;
            GameObject.Find("Bridge 1").transform.position = this.BlockPosition;
            for (int i = 1; i < 11; i++)
            {
                GameObject.Find("Bridge " + i).transform.position = this.BlockPosition;
                GameObject.Find("Bridge " + i).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }

        }
        
        
        
        player.transform.position = this.InitialPosition;
        FindObjectOfType<AudioManager>().Play("GameOver");
    }
    // Start is called before the first frame update
    void Start()
    {

        this.InitialPosition = player.transform.position;
       // this.BlockPosition = GameObject.Find("Bridge 1").transform.position;
        for (int i = 1; i < 11; i++)
        {
            var index = "Bridge " + i;
            Debug.Log(index);
            this.BlockPosition = GameObject.Find(index).transform.position;
            this.BlockList.Add(this.BlockPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
