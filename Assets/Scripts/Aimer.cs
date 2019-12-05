using UnityEngine;
using System.Collections;

// Class that moves the Aimer arrow on the screen based on the position of the mouse. 
// FIXME: Will need to add code for controller behavior.
public class Aimer : MonoBehaviour {
    [SerializeField] private GameObject GrappleProjectilePrefab;
    private float Angle;
    private Transform GrappleSpawnPoint;

    void Start () 
    {
        this.GrappleSpawnPoint = this.transform.Find("GrappleSpawn");
    }

    void Update () {
        Vector3 mouse_pos = Input.mousePosition;
        Vector3 player_pos = Camera.main.WorldToScreenPoint(this.transform.position);

        mouse_pos.x = mouse_pos.x - player_pos.x;
        mouse_pos.y = mouse_pos.y - player_pos.y;

        this.Angle = Mathf.Atan2 (mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler (new Vector3(0, 0, this.Angle));
    }

    public GameObject Fire()
    {
        var grappleProjectile = Instantiate(GrappleProjectilePrefab, this.GrappleSpawnPoint.position, this.transform.localRotation * Quaternion.AngleAxis(90, Vector3.forward));
        return grappleProjectile;
    }

    public Transform GetGrappleSpawnPoint()
    {
        return GrappleSpawnPoint;
    }
}