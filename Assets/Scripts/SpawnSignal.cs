using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSignal : MonoBehaviour
{

    [SerializeField]
    string spawnName;
    [SerializeField]
    float lifetime;
    [SerializeField]
    float speed;
    float timer = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<ParticleSystem>().Clear();
        GetComponent<ParticleSystem>().Play();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(new Vector2(speed*Time.deltaTime, 0.03f*Mathf.Sin(20*timer)));
        if(timer>lifetime)
        {
            GameObject spawn = Factory.Take(spawnName);
            spawn.transform.position = transform.position;
            spawn.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
