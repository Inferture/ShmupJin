using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBasicEnemy : MonoBehaviour
{


    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Engines>().Direction = Vector2.left;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<BaseAvatar>().Shoot();
        GetComponent<Engines>().Direction = Vector2.left;
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
