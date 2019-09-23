using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField]
    float horizontalSpeed;
    [SerializeField]
    float verticalSpeed;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Time.deltaTime * new Vector2(horizontalSpeed, verticalSpeed));
    }

    public float HorizontalSpeed
    {
        get
        {
            return horizontalSpeed;
        }
        set
        {
            horizontalSpeed = value;
        }
    }
    public float VerticalSpeed
    {
        get
        {
            return verticalSpeed;
        }
        set
        {
            verticalSpeed = value;
        }
    }
}
