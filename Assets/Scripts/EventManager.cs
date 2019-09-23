using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{

    public delegate void RefreshUILife(int life);

    public static event RefreshUILife OnHitOrHeal;


    public static void HitOrHeal(int life)
    {
        OnHitOrHeal(life);
    }

 


}
