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
            //for (int i=0; i < 11; i++)
            //{
            //    var position = GameObject.Find("Bridge " + i);
            //    position.transform.position = this.BlockList[i];
            //}

        }
        
        
        
        player.transform.position = this.InitialPosition;
        FindObjectOfType<AudioManager>().Play("GameOver");
    }
    // Start is called before the first frame update
    void Start()
    {

        this.InitialPosition = player.transform.position;
        for (int i = 0; i < 11; i++)
        {
            var test = GameObject.Find("Bridge " + i);
            //Debug.Log("Block position " + test.transform);
            //this.BlockPosition = test.transform.position;
            //this.BlockList.Add(this.BlockPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
