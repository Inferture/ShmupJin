using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerAvatar : BaseAvatar
{
    [SerializeField]
    int energy;
    [SerializeField]
    int maxEnergy;

    [SerializeField]
    int dashCost;

    [SerializeField]
    int shotCost;

    bool overheat;

    [SerializeField]
    float energyRecoveryOverTime;

    [Range(0,1)]
    [SerializeField]
    float overHeatRecoveryRatio = 0.75f;

    float recoveredEnergy;

    [SerializeField]
    GameObject overheatEffect;

    [SerializeField]
    GameObject dashEffect;

    [Range(0, 1)]
    [SerializeField]
    float maximumHorizontal = 0.2f;//According to screen ratio, 1 = all the screen
    [Range(0, 1)]
    [SerializeField]
    float minimumHorizontal = 0f;
    [Range(0, 1)]
    [SerializeField]
    float maximumVertical = 1f;
    [Range(0, 1)]
    [SerializeField]
    float minimumVertical = 0f;

    [SerializeField]
    GameObject diamond;

    [SerializeField]
    float invincibilityTimeWhenHurt;
    [SerializeField]
    float blinkDurationWhenHurt;
    [SerializeField]
    Color blinkColorWhenHurt;

    int unlockedFiremodes = 1;
    bool invincible;
    public override void TakeHit(int damage=1, GameObject hitter = null)
    {
        if(!invincible)
        {
            Life -= damage;
            Sounds.Play("player_hurt");

            EventManager.HitOrHeal(Life);
            if (Life <= 0)
            {
                Die();
            }
            invincible = true;
        }
        Invoke("StopInvincibility", invincibilityTimeWhenHurt);
        BlinkHurt();


        
    }

    protected override void Die()
    {
        base.Die();
        Sounds.Play("player_die");
        gameObject.SetActive(false);
        Invoke("GameOver", 4);
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public override bool Shoot()
    {
        if(overheat)
        {
            return false;
        }
        if(energy>0)
        {
            if(base.Shoot())
            {
                Energy = Mathf.Max(0, energy - shotCost);
                return true;
            }
        }
        return false;
    }
    public int Energy
    {
        get
        {
            return energy;
        }
        set
        {
            energy = value;
            if(energy<=0)
            {
                overheat = true;
                Sounds.Play("player_overheat");
            }
        }
    }
    public int MaxEnergy
    {
        get
        {
            return maxEnergy;
        }
        private set
        {
            maxEnergy = value;
        }
    }
    public int DashCost
    {
        get
        {
            return dashCost;
        }
        private set
        {
            dashCost = value;
        }
    }

    void Update()
    {
        
        dashEffect.SetActive(GetComponent<Engines>().Dashing);
        

        
        if (overheat)
        {
            overheatEffect.SetActive(true);
            if (energy>=maxEnergy)
            {
                overheat = false;
                overheatEffect.SetActive(false);
            }
            recoveredEnergy += Time.deltaTime * energyRecoveryOverTime*overHeatRecoveryRatio;
        }
        else
        {
            recoveredEnergy += Time.deltaTime * energyRecoveryOverTime;
        }

        energy = Mathf.Min(maxEnergy, energy + (int)Mathf.Floor(recoveredEnergy));
        recoveredEnergy -= (int)Mathf.Floor(recoveredEnergy);



        Vector2 limitDownLeft = Camera.main.ScreenToWorldPoint(new Vector2(minimumHorizontal * Screen.width, minimumVertical * Screen.height));
        Vector2 limitUpRight = Camera.main.ScreenToWorldPoint(new Vector2(maximumHorizontal * Screen.width, maximumVertical * Screen.height));

        transform.position = new Vector2(Mathf.Max(transform.position.x, limitDownLeft.x), Mathf.Max(transform.position.y, limitDownLeft.y));
        transform.position = new Vector2(Mathf.Min(transform.position.x, limitUpRight.x), Mathf.Min(transform.position.y, limitUpRight.y));


    }
    public void IncreaseLife()
    {
        Life++;
        EventManager.HitOrHeal(Life);
    }
    public void IncreaseEnergy()
    {
        maxEnergy += 20;
        energy += 20;
    }
    public void UnlockDouble()
    {
        if(unlockedFiremodes<2)
        {
            unlockedFiremodes = 2;
        }
    }

    public void UnlockZigZag()
    {
        if (unlockedFiremodes <= 3)
        {
            unlockedFiremodes=3;
        }
    }

    public bool Overheat
    {
        get
        {
            return overheat;
        }
        private set
        {
            overheat = value;
        }
    }

    public override void Switch()
    {
        activeFireMode = (activeFireMode + 1) % unlockedFiremodes;
        foreach (Shooter shooter in GetComponentsInChildren<Shooter>())
        {
            shooter.gameObject.SetActive(false);
        }
        foreach (Shooter shooter in firemodes[activeFireMode].shooters)
        {
            shooter.gameObject.SetActive(true);
        }
    }




    public int UnlockedFireModes
    {
        get
        {
            return unlockedFiremodes;
        }
        set
        {
            unlockedFiremodes = value;
        }
    }

    public void BlinkHurt()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        if(invincible)
        {
            if (new Color(blinkColorWhenHurt.r, blinkColorWhenHurt.g, blinkColorWhenHurt.b) == new Color(color.r, color.g, color.b))
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, color.a);
                Invoke("BlinkHurt", blinkDurationWhenHurt);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(blinkColorWhenHurt.r, blinkColorWhenHurt.g, blinkColorWhenHurt.b, color.a);
                Invoke("BlinkHurt", blinkDurationWhenHurt);
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, color.a);
        }
        
    }

    public void StopInvincibility()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, color.a);
        invincible = false;
    }
}
