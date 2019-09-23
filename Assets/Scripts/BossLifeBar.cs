using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossLifeBar : MonoBehaviour
{
    [SerializeField]
    EnemyAvatar bossAvatar;

    Slider lifebar;
    [SerializeField]
    Image lifebarFill;
    int maxLife;

    [SerializeField]
    Color fullHealthColor;
    [SerializeField]
    Color emptyHealthColor;
    void Awake()
    {

        lifebar = GetComponent<Slider>();
        maxLife = bossAvatar.Life;
    }

    // Update is called once per frame
    void Update()
    {

        int life = bossAvatar.Life;
        lifebar.value = life / (float)maxLife;

        lifebarFill.color = (life * fullHealthColor + (maxLife - life) * emptyHealthColor) / (float)maxLife;
        
    }
}
