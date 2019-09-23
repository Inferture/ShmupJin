using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvatar : BaseAvatar
{

    [SerializeField]
    int points;
    [SerializeField]
    bool boss;
    [Serializable]
    public struct Drop
    {
        public string droppedObject;
        public float probability;
        public Drop(string drop, float proba)
        {
            droppedObject = drop;
            probability = proba;
        }
    }
    [SerializeField]
    List<Drop> drops;//Put the heart first
    // Start is called before the first frame update


    [SerializeField]
    Color hurtColor;
    [SerializeField]
    float timeColorationWhenHit;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Die()
    {
        
        BaseColoration();
        CancelInvoke("BaseColoration");
        base.Die();
        Sounds.Play("enemy_die");
        DeathDrop();

        GameObject scoreIncreaserObject = Factory.Take("exuprrooooosion");
        ScoreIncreaser scoreIncreaser = scoreIncreaserObject.GetComponentInChildren<ScoreIncreaser>(true);

        foreach(Shooter shooter in GetComponentsInChildren<Shooter>())
        {
            shooter.CancelInvoke("Shoot");
        }
        if (scoreIncreaser)
        {
            scoreIncreaser.Points = points;
            scoreIncreaser.transform.position = transform.position;
            scoreIncreaser.gameObject.SetActive(true);
            if (scoreIncreaser.GetComponent<ParticleSystem>())
            {
                scoreIncreaser.GetComponent<ParticleSystem>().startColor = GetComponent<SpriteRenderer>().color;
            }
        }
        else
        {
            Score.IncreaseScore(points);
        }
        if (boss)
        {
            Invoke("GameOver", 10);
        }
        gameObject.SetActive(false);

    }

    void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
    public override void TakeHit(int damage = 1, GameObject hitter = null)
    {
        HurtColoration();
        base.TakeHit(damage);
        Sounds.Play("enemy_hurt");
        if(Life<=0)
        {
            if(hitter && hitter.GetComponent<PlayerAvatar>() && drops.Count>0)
            {
                if(hitter.GetComponent<PlayerAvatar>().Life == 2)
                {
                    drops[0]= new Drop(drops[0].droppedObject, drops[0].probability*2);
                }
                if (hitter.GetComponent<PlayerAvatar>().Life == 1)
                {
                    drops[0] = new Drop(drops[0].droppedObject, drops[0].probability * 3);
                }
                if(hitter.GetComponent<PlayerAvatar>().UnlockedFireModes==2 && drops.Count>3)
                {
                    drops[2] = new Drop(drops[3].droppedObject, drops[2].probability);
                }
                if(hitter.GetComponent<PlayerAvatar>().UnlockedFireModes == 3 && drops.Count > 3)
                {
                    drops[2] = new Drop(drops[2].droppedObject, 0);
                    drops[3] = new Drop(drops[3].droppedObject, 0);
                }
            }
            Die();
        }
    }

    public int Points
    {
        get
        {
            return points;
        }
        private set
        {
            points = value;
        }
    }

    void DeathDrop()
    {
        float random = UnityEngine.Random.Range(0.0f,1.0f);

        float sum = 0;
        for (int i= 0;i < drops.Count;i++)
        {
            sum += drops[i].probability;
            if(random<sum)
            {
                GameObject drop = Factory.Take(drops[i].droppedObject);
                drop.transform.position = transform.position;
                drop.SetActive(true);
                return;
            }
        }
    }

    void HurtColoration()
    {
        GetComponent<SpriteRenderer>().color = hurtColor;
        Invoke("BaseColoration", timeColorationWhenHit);
    }
    void BaseColoration()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

}
