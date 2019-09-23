using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoss1 : MonoBehaviour
{

    [SerializeField]
    int numberShooters;
    [SerializeField]
    float distanceShooters;
    [SerializeField]
    GameObject player;

    [SerializeField]
    List<string> bulletNames = new List<string>();

    List<Shooter> shooters = new List<Shooter>();

    bool moving;
    Vector2 targetMoving;

    bool regularShootWhenMoving;
    float shotCooldownWhenMoving;
    float shotTimer;

    GameObject lastTargetedSpot;

    List<List<Boss1TargetingBullet>> inactiveBulletsStacks= new List<List<Boss1TargetingBullet>>();



    List<ActionEvent> actions = new List<ActionEvent>();

    float timer = 0;
    int currentAction = 0;


    [SerializeField]
    float cooldown = 0.1f;
    [SerializeField]
    int period = 1;
    [SerializeField]
    int offset = 0;

    // Start is called before the first frame update
    void Start()
    {
        GenerateShooters();

        TextAsset patternAsset = Resources.Load<TextAsset>("Patterns/Enemies/Boss1");

        BossPattern pattern = XmlSerialization.ReadFromXmlResource<BossPattern>(patternAsset);
        actions = pattern.actions;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        while(currentAction<actions.Count && actions[currentAction].time<=timer)
        {
            ActionEvent actionEvent = actions[currentAction];
            //do action
            if(actionEvent.action == "Go")
            {
                Go(new Vector2(actionEvent.x, actionEvent.y));
            }
            else if(actionEvent.action=="GoAndShoot")
            {
                GoAndShoot(new Vector2(actionEvent.x,actionEvent.y), cooldown);
            }
            else if(actionEvent.action== "SwitchShooters")
            {
                SwitchShooters(actionEvent.intParameter);
            }
            else if(actionEvent.action == "IncreaseCooldown")
            {
                cooldown *= 1.1f;
            }
            else if(actionEvent.action=="DecreaseCooldown")
            {
                cooldown /= 1.1f;
            }
            else if(actionEvent.action == "IncreasePeriod")
            {
                period++;
            }
            else if (actionEvent.action == "DecreasePeriod")
            {
                period--;
            }
            else if (actionEvent.action == "IncreaseOffset")
            {
                offset++;
            }
            else if (actionEvent.action == "DecreaseOffset")
            {
                offset--;
            }
            else if (actionEvent.action == "ShootRegular")
            {
                ShootRegular("enemyBullet", period, offset);
            }
            else if (actionEvent.action == "ShootRegularOffsetTime")
            {
                ShootRegularOffsetTime("enemyGreenBullet", period, offset, cooldown);
            }
            else if(actionEvent.action == "TargetPlayer")
            {
                if(actionEvent.intParameter==0)
                {
                    TargetPlayer(false);
                }
                else
                {
                    TargetPlayer(true);
                }
            }
            else if(actionEvent.action == "ReleaseTargetingBullets")
            {
                ReleaseTargetingBullets();
            }
            else if (actionEvent.action == "ReleaseTargetingBulletsOffsetTime")
            {
                ReleaseTargetingBullets(cooldown);
            }
            else if(actionEvent.action == "ShootRegularTargeting")
            {
                ShootRegularTargeting("targetingBullet", null, period, offset, 0, true);
            }
            currentAction++;
        }

        if(currentAction>=actions.Count)
        {
            currentAction = 0;
            timer = 0;
        }

        if(moving)
        {
            if(Vector2.SqrMagnitude((targetMoving-(Vector2)transform.position))>2*GetComponent<EnemyAvatar>().MaxSpeed* GetComponent<EnemyAvatar>().MaxSpeed*Time.deltaTime*Time.deltaTime)
            {
                GetComponent<Engines>().Direction = (targetMoving - (Vector2)transform.position).normalized;
            }
            else
            {
                GetComponent<Engines>().Direction = Vector2.zero;
                transform.position = targetMoving;
                moving = false;
            }
            if(regularShootWhenMoving)
            {

                shotTimer += Time.deltaTime;
                if(shotTimer>shotCooldownWhenMoving)
                {
                    ShootRegularTargeting(bulletNames[2], lastTargetedSpot,24, 12, 0, false);
                    shotTimer = 0;
                }
            }
        }
    }

    void GenerateShooters()
    {
        for(int i=0;i<numberShooters;i++)
        {
            GameObject shooterObject = Instantiate(GetComponentInChildren<Shooter>(true).gameObject);
            shooterObject.transform.parent = transform;
            shooterObject.transform.rotation = Quaternion.Euler( new Vector3(0,0,i*360f/numberShooters));
            shooterObject.transform.Translate(shooterObject.transform.forward * distanceShooters);
            shooterObject.transform.position = (Vector2)transform.position + distanceShooters * (new Vector2(Mathf.Cos(Mathf.Deg2Rad*i * 360f / numberShooters), Mathf.Sin(Mathf.Deg2Rad*i * 360f / numberShooters)));
            shooters.Add(shooterObject.GetComponent<Shooter>());
        }
    }

    public void ShootRegular(string bulletName, int period=1, int offset=0)
    {
        for(int i=0;i<numberShooters;i++)
        {
            shooters[i].gameObject.SetActive(false);
            
        }
        int k = 0;
        for(int i=offset;i<numberShooters;i+=period)
        {
            shooters[i].gameObject.SetActive(true);
            shooters[i].BulletName = bulletName;

            shooters[i].Shoot();
            k++;
        }
    }

    public void ShootRegularOffsetTime(string bulletName, int period = 1, int offset = 0, float offsetTime = 0)
    {
        for (int i = 0; i < numberShooters; i++)
        {
            shooters[i].gameObject.SetActive(false);

        }
        int k = 0;
        for (int i = offset; i < numberShooters; i += period)
        {
            shooters[i].gameObject.SetActive(true);
            shooters[i].BulletName = bulletName;

            shooters[i].Invoke("Shoot",k*offsetTime);
            k++;
        }
    }

    public void ShootRegularTargeting(string bulletName, GameObject target=null, int period = 1, int offset = 0, float offsetTime = 0, bool active = true)
    {
        if(!target)
        {
            target = lastTargetedSpot;
        }
        for (int i = 0; i < numberShooters; i++)
        {
            shooters[i].gameObject.SetActive(false);

        }
        int k = 0;
        for (int i = offset; i < numberShooters; i += period)
        {
            shooters[i].gameObject.SetActive(true);
            shooters[i].BulletName = bulletName;
            k++;
        }
        foreach (Shooter shooter in GetComponentsInChildren<Shooter>())
        {
            Boss1TargetingBullet bullet = shooter.Shoot().GetComponent< Boss1TargetingBullet>();
            bullet.Target = target;
            bullet.Active = active;
            if (!active)
            {
                if(inactiveBulletsStacks.Count>0)
                {
                    inactiveBulletsStacks[inactiveBulletsStacks.Count - 1].Add(bullet);
                }
                else
                {
                    inactiveBulletsStacks.Add(new List<Boss1TargetingBullet>());
                    inactiveBulletsStacks[0].Add(bullet);
                }
            }
        }
        
    }

    public void SwitchShooters(int mode)
    {
        if(mode==0)
        {
            for (int i = 0; i < numberShooters; i++)
            {
                shooters[i].gameObject.SetActive(false);
                shooters[i].transform.position = (Vector2)transform.position + distanceShooters * (new Vector2(Mathf.Cos(Mathf.Deg2Rad * i * 360f / numberShooters), Mathf.Sin(Mathf.Deg2Rad * i * 360f / numberShooters)));
            }
        }
        if (mode == 1)
        {
            for (int i = 0; i < numberShooters; i++)
            {
                shooters[i].gameObject.SetActive(false);
                shooters[i].transform.position = (Vector2)transform.position + distanceShooters * (new Vector2(-Mathf.Cos(Mathf.Deg2Rad * i * 360f / numberShooters), Mathf.Sin(Mathf.Deg2Rad * i * 360f / numberShooters)));
            }
        }
        if (mode == 2)
        {
            for (int i = 0; i < numberShooters; i++)
            {
                shooters[i].gameObject.SetActive(false);
                shooters[i].transform.position = (Vector2)transform.position + distanceShooters * (new Vector2(Mathf.Cos(Mathf.Deg2Rad * i * 360f / numberShooters), -Mathf.Sin(Mathf.Deg2Rad * i * 360f / numberShooters)));
            }
        }
        if (mode == 3)
        {
            for (int i = 0; i < numberShooters; i++)
            {
                shooters[i].gameObject.SetActive(false);
                shooters[i].transform.position = (Vector2)transform.position + distanceShooters * (new Vector2(-Mathf.Cos(Mathf.Deg2Rad * i * 360f / numberShooters), -Mathf.Sin(Mathf.Deg2Rad * i * 360f / numberShooters)));
            }
        }
        if (mode == 4)
        {
            for (int i = 0; i < numberShooters; i++)
            {
                shooters[i].gameObject.SetActive(false);
                shooters[i].transform.position = (Vector2)transform.position + distanceShooters * (new Vector2(Mathf.Sin(Mathf.Deg2Rad * i * 360f / numberShooters), Mathf.Cos(Mathf.Deg2Rad * i * 360f / numberShooters)));
            }
        }
        if (mode == 5)
        {
            for (int i = 0; i < numberShooters; i++)
            {
                shooters[i].gameObject.SetActive(false);
                shooters[i].transform.position = (Vector2)transform.position + distanceShooters * (new Vector2(-Mathf.Sin(Mathf.Deg2Rad * i * 360f / numberShooters), Mathf.Cos(Mathf.Deg2Rad * i * 360f / numberShooters)));
            }
        }
        if (mode == 6)
        {
            for (int i = 0; i < numberShooters; i++)
            {
                shooters[i].gameObject.SetActive(false);
                shooters[i].transform.position = (Vector2)transform.position + distanceShooters * (new Vector2(Mathf.Sin(Mathf.Deg2Rad * i * 360f / numberShooters), -Mathf.Cos(Mathf.Deg2Rad * i * 360f / numberShooters)));
            }
        }
        if (mode == 7)
        {
            for (int i = 0; i < numberShooters; i++)
            {
                shooters[i].gameObject.SetActive(false);
                shooters[i].transform.position = (Vector2)transform.position + distanceShooters * (new Vector2(-Mathf.Sin(Mathf.Deg2Rad * i * 360f / numberShooters), -Mathf.Cos(Mathf.Deg2Rad * i * 360f / numberShooters)));
            }
        }

    }

    public GameObject TargetPlayer(bool createNewTarget = true)
    {
        if(createNewTarget)
        {
            GameObject targetedSpot = Factory.Take("targetedSpot");
            targetedSpot.transform.position = player.transform.position;
            targetedSpot.SetActive(true);
            lastTargetedSpot = targetedSpot;

            return targetedSpot;
        }
        else
        {
            lastTargetedSpot.transform.position = player.transform.position;
            lastTargetedSpot.SetActive(true);
            return lastTargetedSpot;
        }
    }
    public GameObject TargetPlayer(GameObject targetedSpot)
    {
        if(!targetedSpot)
        {
            targetedSpot = Factory.Take("targetedSpot");
            lastTargetedSpot = targetedSpot;
        }
        targetedSpot.transform.position = player.transform.position;
        targetedSpot.SetActive(true);
        return targetedSpot;
    }
    public void Go(Vector2 pos)
    {
        regularShootWhenMoving = false;
        targetMoving = pos;
        moving = true;
    }
    public void GoAndShoot(Vector2 pos, float cooldown)
    {
        targetMoving = pos;
        moving = true;
        regularShootWhenMoving = true;
        shotCooldownWhenMoving = cooldown;
        shotTimer = 0;

        inactiveBulletsStacks.Add(new List<Boss1TargetingBullet>());
    }

    public void ReleaseTargetingBullets(float offsetTime=0)
    {
        if(inactiveBulletsStacks.Count>0)
        {
            List<Boss1TargetingBullet> bullets = inactiveBulletsStacks[0];
            inactiveBulletsStacks.RemoveAt(0);
            if (offsetTime > 0)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    bullets[i].Invoke("Activate", i * offsetTime);
                }
            }
            else
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    bullets[i].Activate();
                }
            }
        }
        
    }
}
