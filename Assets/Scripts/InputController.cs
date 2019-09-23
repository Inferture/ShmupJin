using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    [Range(0,1)]
    float axisThreshold;

    [SerializeField]
    [Range(0, 1)]
    float doubleTapThreshold; //in seconds

    float timeSinceLeft;
    float timeSinceRight;
    float timeSinceUp;
    float timeSinceDown;



    float Horizontal()
    {
        float right = Controls.GetActionAxisOrButtonValue(KeyStrings.key_right);
        float left = Controls.GetActionAxisOrButtonValue(KeyStrings.key_left);
        float horizontal = Mathf.Clamp(right - left,-1,1);
        if(Mathf.Abs(horizontal)< axisThreshold)
        {
            horizontal = 0;
        }
        return horizontal;
    }
    float Vertical()
    {
        float up = Controls.GetActionAxisOrButtonValue(KeyStrings.key_up);
        float down = Controls.GetActionAxisOrButtonValue(KeyStrings.key_down);
        float vertical = Mathf.Clamp(up - down,-1,1);
        

        if (Mathf.Abs(vertical) < axisThreshold)
        {
            vertical = 0;
        }
        return vertical;
    }
    bool Shoot()
    {
        return Controls.GetAction(KeyStrings.key_shoot);
    }

    bool Switch()
    {
        return Controls.GetActionDown(KeyStrings.key_switch);
    }

    bool Slow()
    {
        return Controls.GetAction(KeyStrings.key_slow);
    }
    bool Dash()
    {
        float horizontal = Horizontal();
        float vertical = Vertical();
        if(timeSinceLeft>0 && timeSinceLeft<doubleTapThreshold && horizontal<0)
        {
            return true;
        }
        if (timeSinceRight > 0 && timeSinceRight < doubleTapThreshold && horizontal > 0)
        {
            return true;
        }
        if (timeSinceDown > 0 && timeSinceDown < doubleTapThreshold && vertical < 0)
        {
            return true;
        }
        if (timeSinceUp > 0 && timeSinceUp < doubleTapThreshold && vertical > 0)
        {
            return true;
        }
        return false;
    }

    void Awake()
    {
        timeSinceDown = 2 * doubleTapThreshold;
        timeSinceRight = 2 * doubleTapThreshold;
        timeSinceUp = 2 * doubleTapThreshold;
        timeSinceLeft = 2 * doubleTapThreshold;
    }

    void Update()
    {



        bool dashing = Dash();

        if (dashing)
        {
            GetComponent<Engines>().Dashing = true;
        }


        timeSinceDown += Time.deltaTime;
        timeSinceRight += Time.deltaTime;
        timeSinceUp += Time.deltaTime;
        timeSinceLeft += Time.deltaTime;

        

        float horizontal = Horizontal();
        float vertical = Vertical();

        

        Vector2 movement = new Vector2(horizontal, vertical);

        

        if (horizontal<0)
        {
            timeSinceLeft = 0;
        }
        if (horizontal > 0)
        {
            timeSinceRight = 0;
        }
        if (vertical < 0)
        {
            timeSinceDown = 0;
        }
        if (vertical > 0)
        {
            timeSinceUp = 0;
        }

        

        if (Shoot())
        {
            GetComponent<BaseAvatar>().Shoot();
        }
        if (Switch())
        {
            GetComponent<BaseAvatar>().Switch();
        }
        GetComponent<Engines>().Slowed = Slow();

        GetComponent<Engines>().Direction = movement;
    }
}
