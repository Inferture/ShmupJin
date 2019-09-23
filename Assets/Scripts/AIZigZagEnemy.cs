using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIZigZagEnemy : MonoBehaviour
{
    [SerializeField]
    float frequency;

    [SerializeField]
    float angleStart;

    [SerializeField]
    float verticalRange=1;

    [SerializeField]
    float horizontalRange=1;


    float angle;
    // Start is called before the first frame update
    void Awake()
    {
        angle = angleStart;
        GetComponent<Engines>().Direction = new Vector2(-horizontalRange*Mathf.Abs(Mathf.Sin(angle)), verticalRange*Mathf.Cos(angle)); ;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<BaseAvatar>().Shoot();
        Vector2 direction = new Vector2(-horizontalRange*Mathf.Abs(Mathf.Sin(angle)), verticalRange*Mathf.Cos(angle));

        GetComponent<Engines>().Direction = direction;
        angle += frequency * Time.deltaTime;
    }


    void OnBecameInvisible()
    {
        gameObject.SetActive(false) ;
    }
}
