using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engines : MonoBehaviour
{


    //Rigidbody2D rb;

    float speed;

    Vector2 position;
    bool dashing;

    Vector2 direction;

    [SerializeField]
    float dashSpeedMultiplier;

    [SerializeField]
    float dashTime;//Time the dash lasts

    [Range(0,1)]
    [SerializeField]
    float slowRatio=0.3f;

    [SerializeField]
    bool slowed;
    void Start()
    {
        speed = GetComponent<BaseAvatar>().MaxSpeed;
        //rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Direction= direction;
    }
    public bool Dashing
    {
        get
        {
            return dashing;
        }
        set
        {
            if (!dashing && value)
            {
                Invoke("StopDashing", dashTime);
                if(GetComponent<PlayerAvatar>())
                {
                    if(!GetComponent<PlayerAvatar>().Overheat)
                    {
                        if(GetComponent<PlayerAvatar>().Energy >GetComponent<PlayerAvatar>().DashCost)
                        {
                            GetComponent<PlayerAvatar>().Energy -= GetComponent<PlayerAvatar>().DashCost;
                        }
                        else
                        {
                            GetComponent<PlayerAvatar>().Energy = 0;
                        }
                        Sounds.Play("player_dash");
                        dashing = value;
                    }
                    
                }
                else
                {
                    dashing = value;
                }
            }
            else
            {
                dashing = value;
            }
        }
    }

    public Vector2 Direction
    {
        get
        {
            return direction;
        }
        set
        {

            direction = value;
            if (!dashing)
            {
                
                if(slowed)
                {
                    //GetComponent<Rigidbody2D>().velocity = speed * direction*slowRatio;
                    transform.Translate(speed * direction * slowRatio * Time.deltaTime);
                }
                else
                {
                    //GetComponent<Rigidbody2D>().velocity = speed * direction;
                    transform.Translate(speed * direction * Time.deltaTime);
                }

            }
            else
            {
                //rb.velocity = dashSpeedMultiplier*speed * direction;
                transform.Translate(speed * direction *dashSpeedMultiplier * Time.deltaTime);
            }
        }
        
    }
    public Vector2 Position
    {
        get
        {
            return position;
        }
        private set
        {
            position = value;
        }
    }

    void StopDashing()
    {
        dashing = false;
    }

    public bool Slowed
    {
        get
        {
            return slowed;
        }
        set
        {
            slowed = value;
        }
    }


}
