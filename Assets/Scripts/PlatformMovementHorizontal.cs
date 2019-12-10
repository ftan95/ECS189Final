using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementHorizontal : MonoBehaviour
{
    [SerializeField] private float Speed = 10.0f;
    [SerializeField] private float RightBound;
    [SerializeField] private float LeftBound;
    private float BasePosition;
    // Platform moves -> when Direction is true and <- when Direction is false
    [SerializeField] private bool Direction;
    void Start()
    {
        BasePosition = this.gameObject.transform.position.x;
        RightBound = BasePosition + RightBound;
        LeftBound = BasePosition - LeftBound;
    }

    // Update is called once per frame
    void Update()
    {
        var position = this.gameObject.transform.position;
        if (Direction == true)
        {
            position.x += Speed * Time.deltaTime;

            if (position.x > RightBound)
            {
                Direction = false;
            }
        }
        else
        {
            position.x -= Speed * Time.deltaTime;

            if (position.x < LeftBound)
            {
                Direction = true;
            }
        }

        this.gameObject.transform.position = position;
    }
}
