using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletZigZag : Bullet
{

    [SerializeField]
    float frequency;

    // Start is called before the first frame update
    protected override void Move()
    {
        float sinAngle = Mathf.Sin(angle);
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Mathf.Abs(sinAngle), speed * Mathf.Cos(angle));
        angle += frequency*Time.deltaTime;

        if(sinAngle>0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle * Mathf.Rad2Deg));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.PI/2+angle * Mathf.Rad2Deg));
        }
    }
}
