﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1TargetingBullet : Bullet
{

    [SerializeField]
    float angleSpeed;
   
    [SerializeField]
    GameObject target;

    bool active=true;
    override protected void Move()
    {
        if(active)
        {
            if (target && target.activeInHierarchy)
            {

                Vector2 targetPos = target.transform.position;
                Vector2 pos = transform.position;
                float targetAngle = Mathf.Atan2(targetPos.y - pos.y, targetPos.x - pos.x);
                while (Mathf.Abs(targetAngle - angle) > Mathf.PI)
                {
                    if (angle > targetAngle)
                    {
                        targetAngle += 2 * Mathf.PI;
                    }
                    else
                    {
                        targetAngle -= 2 * Mathf.PI;
                    }
                }
                if (angle > targetAngle)
                {
                    angle -= angleSpeed * Time.deltaTime;
                }
                else
                {
                    angle += angleSpeed * Time.deltaTime;
                }
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle * Mathf.Rad2Deg));
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle * Mathf.Rad2Deg));
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Mathf.Cos(angle), speed * Mathf.Sin(angle));
            }

        }
        

    }

    public GameObject Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }

    public bool Active
    {
        get
        {
            return active;
        }
        set
        {
            active = value;
            if(!active)
            {
                CancelInvoke("Disappear");
            }
            else
            {
                Invoke("Disappear", lifetime);
            }
        }
    }

    public void Activate()
    {
        Active = true;
    }

}

