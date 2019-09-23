using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public struct ActionEvent
{
    public string action;
    public float time;
    public float x;
    public float y;
    public int intParameter;
    public ActionEvent(string action, float t, float x, float y)
    {
        this.action = action;
        time = t;
        this.x = x;
        this.y = y;
        intParameter = 0;
    }
    public ActionEvent(string action, float t)
    {
        this.action = action;
        time = t;
        this.x = 0;
        this.y = 0;
        intParameter = 0;
    }
    public ActionEvent(string action, float t, int parameter)
    {
        this.action = action;
        time = t;
        this.x = 0;
        this.y = 0;
        intParameter = parameter;
    }
    public ActionEvent(string action, float t, float x, float y, int parameter)
    {
        this.action = action;
        time = t;
        this.x = x;
        this.y = y;
        intParameter = parameter;
    }
}

[Serializable]
public struct BossPattern
{
    [SerializeField]
    public List<ActionEvent> actions;
    public BossPattern(List<ActionEvent> actions)
    {
        this.actions = actions;
    }
}

public class Boss1PatternSaver : MonoBehaviour
{


    float time = 0;
    bool pause;
    int currentActionNum;
    List<ActionEvent> actions = new List<ActionEvent>();

    [SerializeField]
    List<string> actionsStrings;

    [SerializeField]
    float cooldown=0.1f;
    [SerializeField]
    int period = 1;
    [SerializeField]
    int offset = 0;
    AIBoss1 ai;

    [SerializeField]
    AudioSource source;
    [SerializeField]
    Text informations;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        actions = new List<ActionEvent>();
        currentActionNum = 0;

        ai = GetComponent<AIBoss1>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pause)
        {
            time += Time.deltaTime;
        }
        if(Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.Space))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            actions.Add(new ActionEvent("Go", time, pos.x, pos.y));
            ai.Go(pos);
        }
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.Space))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            actions.Add(new ActionEvent("GoAndShoot", time, pos.x, pos.y));
            ai.GoAndShoot(pos,cooldown);
        }
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            ai.SwitchShooters(0);
            actions.Add(new ActionEvent("SwitchShooters", time, 0));
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ai.SwitchShooters(1);
            actions.Add(new ActionEvent("SwitchShooters", time, 1));
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ai.SwitchShooters(2);
            actions.Add(new ActionEvent("SwitchShooters", time, 2));
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ai.SwitchShooters(3);
            actions.Add(new ActionEvent("SwitchShooters", time, 3));
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ai.SwitchShooters(4);
            actions.Add(new ActionEvent("SwitchShooters", time, 4));
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            ai.SwitchShooters(5);
            actions.Add(new ActionEvent("SwitchShooters", time, 5));
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            ai.SwitchShooters(6);
            actions.Add(new ActionEvent("SwitchShooters", time, 6));
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            ai.SwitchShooters(7);
            actions.Add(new ActionEvent("SwitchShooters", time, 7));
        }

        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            cooldown *= 1.1f;
            actions.Add(new ActionEvent("IncreaseCooldown",time));
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            cooldown /= 1.1f;
            actions.Add(new ActionEvent("DecreaseCooldown",time));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            period ++;
            actions.Add(new ActionEvent("IncreasePeriod", time));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            period --;
            actions.Add(new ActionEvent("DecreasePeriod", time));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            offset++;
            actions.Add(new ActionEvent("IncreaseOffset", time));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            offset--;
            actions.Add(new ActionEvent("DecreaseOffset", time));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ai.ShootRegular("enemyBullet", period, offset);
            actions.Add(new ActionEvent("ShootRegular", time));
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ai.ShootRegularOffsetTime("enemyGreenBullet", period, offset, cooldown);
            actions.Add(new ActionEvent("ShootRegularOffsetTime", time));
        }
        if (Input.GetKey(KeyCode.C))
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                ai.TargetPlayer(true);
                actions.Add(new ActionEvent("TargetPlayer", time,1));
            }
            else
            {
                ai.TargetPlayer(false);
                actions.Add(new ActionEvent("TargetPlayer", time, 0));
            }
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            ai.ReleaseTargetingBullets();
            actions.Add(new ActionEvent("ReleaseTargetingBullets", time));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ai.ReleaseTargetingBullets(cooldown);
            actions.Add(new ActionEvent("ReleaseTargetingBulletsOffsetTime", time));
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            ai.ShootRegularTargeting("targetingBullet", null, period, offset, 0,true);
            actions.Add(new ActionEvent("ShootRegularTargeting", time));
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if(pause)
            {
                pause = false;
                source.Play();
            }
            else
            {
                pause = true;
                source.Pause();
            }
        }
        informations.text = "time:" + time + "\ncooldown"+ cooldown + "\nperiod" + period + "\noffset" + offset;

        if(Input.GetKeyDown(KeyCode.F5))
        {
            BossPattern pattern = new BossPattern(actions);
            XmlSerialization.WriteToXmlResource("Patterns/Enemies/Boss1.xml", pattern);
        }
    }
}
