using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField]
    string bulletName;

    [SerializeField]
    float cooldown = 0.2f;


    float angle;
    [SerializeField]
    bool ally = false;
    [SerializeField]
    float scale = 1;
    [SerializeField]
    public GameObject owner;

    float waitTime = 0;


    void Awake()
    {
        angle = transform.eulerAngles.z * Mathf.Deg2Rad;
    }
    // Use this for initialization
    void Start()
    {
        if(ally)
        {
            waitTime = cooldown;
        }
        if (transform.parent != null)
        {
            owner = transform.parent.gameObject;
        }
    }


    // Update is called once per frame
    void Update()
    {
        waitTime += Time.deltaTime;
    }

    virtual public GameObject Shoot()
    {

        if (waitTime >= cooldown)
        {
            waitTime = 0;

            GameObject bulletShotObject = Factory.Take(bulletName);

            Bullet bulletShot = bulletShotObject.GetComponent<Bullet>();
            
            bulletShot.Init(owner, angle, ally);


            Vector2 pos = GetComponent<Transform>().position;
            bulletShotObject.GetComponent<Transform>().position = pos;
            bulletShotObject.transform.localScale = new Vector3(scale, scale, scale);

            if (ally)
            {
                Sounds.Play("sound_player_shoot");
            }
            else
            {
                AudioSource source = GetComponent<AudioSource>();
                if (source)
                {
                    source.Play();
                }
                else
                {
                    Sounds.Play("sound_enemy_shoot");
                }
            }


            bulletShotObject.SetActive(true);
            return bulletShotObject;

        }

        return null;
    }

    public float Cooldown
    {
        get
        {
            return cooldown;
        }
        set
        {
            cooldown = value;
        }
    }

    
    public string BulletName
    {
        get
        {
            return bulletName;
        }
        set
        {
            bulletName = value;
        }
    }

}
