using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementVertical : MonoBehaviour
{
    [SerializeField] private float Speed = 10.0f;
    [SerializeField] private float TopBound;
    [SerializeField] private float BottomBound;
    private float BasePosition;
    // Platform moves up when Direction is true and down when Direction is false
    [SerializeField] private bool Direction;

    // Start is called before the first frame update
    void Start()
    {
        BasePosition = this.gameObject.transform.position.y;
        TopBound = BasePosition + TopBound;
        BottomBound = BasePosition - BottomBound;

    }

    // Update is called once per frame
    void Update()
    {
        var position = this.gameObject.transform.position;
        if (Direction == true)
        {
            position.y += Speed * Time.deltaTime;

            if (position.y > TopBound)
            {
                Direction = false;
            }
        }
        else
        {
            position.y -= Speed * Time.deltaTime;

            if (position.y < BottomBound)
            {
                Direction = true;
            }
        }

        this.gameObject.transform.position = position;

    }
}
