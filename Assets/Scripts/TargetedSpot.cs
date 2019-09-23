using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedSpot : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Boss1TargetingBullet>() && collider.GetComponent<Boss1TargetingBullet>().Target==gameObject)
        {
            gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
