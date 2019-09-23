using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondCollider : MonoBehaviour
{



    [SerializeField]
    float alphaPlayerWhenDiamond = 0.5f;
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponent<Bullet>() && !collider.GetComponent<Bullet>().Ally)
        {
            HideCollider();
        }

        
    }
    void OnTriggerStay2D(Collider2D collider)
    {

        if (collider.GetComponent<Bullet>() && !collider.GetComponent<Bullet>().Ally)
        {

            ShowCollider();
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "LifeBonus")
        {
            transform.parent.GetComponent<PlayerAvatar>().IncreaseLife();
            Sounds.Play("player_heal");
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag == "EnergyBonus")
        {
            transform.parent.GetComponent<PlayerAvatar>().IncreaseEnergy();
            Sounds.Play("player_energy");
            Destroy(collider.gameObject);
        }
        else if(collider.gameObject.tag == "DoubleBonus")
        {
            transform.parent.GetComponent<PlayerAvatar>().UnlockDouble();
            Sounds.Play("double_shot");
            Destroy(collider.gameObject);
        }
        else if(collider.gameObject.tag == "ZigZagBonus")
        {
            transform.parent.GetComponent<PlayerAvatar>().UnlockZigZag();
            Sounds.Play("spiral_shot");
            Destroy(collider.gameObject);
        }
    }


    void ShowCollider()
    {
        Color color = transform.parent.GetComponent<SpriteRenderer>().color;
        transform.parent.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, alphaPlayerWhenDiamond);
    }
    void HideCollider()
    {
        Color color = transform.parent.GetComponent<SpriteRenderer>().color;
        transform.parent.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1);
    }

}
