using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct FireMode
{
    public List<Shooter> shooters;
}

public abstract class BaseAvatar : MonoBehaviour
{
    

    [SerializeField]
    protected float maxSpeed;
    [SerializeField]
    protected int life;

    [SerializeField]
    protected List<FireMode> firemodes;

    protected int activeFireMode;
   
    public float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }
        set
        {
            maxSpeed = value;
        }
    }

    public int Life
    {
        get
        {
            return life;
        }
        protected set
        {
            life = value;
        }
    }

    public virtual void TakeHit(int damage=1, GameObject hitter=null)
    {
        Life -= damage;
    }

    protected virtual void Die()
    {

    }

    public virtual bool Shoot()
    {
        bool shot = false;
        Shooter[] shooters = GetComponentsInChildren<Shooter>();

        foreach(Shooter shooter in shooters)
        {
            if(shooter.Shoot())
            {
                shot = true;
            }
        }
        return shot;
    }

    public virtual void Switch()
    {
        activeFireMode = (activeFireMode + 1) % firemodes.Count;
        foreach(Shooter shooter in GetComponentsInChildren<Shooter>())
        {
            shooter.gameObject.SetActive(false);
        }
        foreach (Shooter shooter in firemodes[activeFireMode].shooters)
        {
            shooter.gameObject.SetActive(true);
        }
    }

}
