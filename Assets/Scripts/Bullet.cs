using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{



    GameObject owner;

    protected float angle;

    bool ally;//PlayerBullet?

    [SerializeField]
    protected float speed;

    [SerializeField]
    int damage;

    [SerializeField]
    protected float lifetime;


    float timer;


    Vector2 velocity0;
    

    void OnTriggerEnter2D(Collider2D col)
    {
        BaseAvatar target = col.gameObject.GetComponent<BaseAvatar>();
        if(target && !(target.GetComponent<Engines>() && target.GetComponent<Engines>().Dashing))
        {
            if (target.tag == "Enemy" && ally)
            {
                target.TakeHit(damage, owner);
                Disappear();
            }

            if (target.tag == "Player" && !ally)
            {
                target.TakeHit(damage, owner);
                Disappear();
            }
        }
        
    }

    public void Init(GameObject owner, float angle, bool ally)
    {
        this.owner = owner;
        this.angle = angle;
        this.ally = ally;

    }

    // Use this for initialization
    void OnEnable()
    {
        //Invoke("Disappear", lifetime);
        velocity0 = new Vector2(speed * Mathf.Sin(angle), speed * Mathf.Cos(angle));
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle * Mathf.Rad2Deg));

        Invoke("Disappear", lifetime);
    }

    void Awake()
    {
        velocity0 = new Vector2(speed * Mathf.Sin(angle), speed * Mathf.Cos(angle));
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle * Mathf.Rad2Deg));
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        /*timer += Time.deltaTime;
        if (timer > lifetime)
        {
            Disappear();
        }*/
    }

    protected virtual void Move()
    {

        GetComponent<Rigidbody2D>().velocity = velocity0;
        //transform.Translate(new Vector2(speed * Mathf.Cos(angle)*Time.deltaTime, speed * Mathf.Sin(angle) * Time.deltaTime));
    }

    void Disappear()
    {
        timer = 0;
        gameObject.SetActive(false);
        CancelInvoke("Disappear");
    }

    public bool Ally
    {
        get
        {
            return ally;
        }
        private set
        {
            ally = value;
        }
    }


}

