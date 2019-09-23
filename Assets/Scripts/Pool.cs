using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{


    List<GameObject> pool = new List<GameObject>();

    [SerializeField]
    int poolNumber;
    [SerializeField]
    bool growing;
    [SerializeField]
    GameObject pooledObject;

    int nextObject=0;



    // If used as a MonoBehavior in a world object
    /*void Awake()
    {
        for (int i = 0; i < poolNumber; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pool.Add(obj);
            obj.transform.SetParent(transform);
        }
    }*/



    public Pool(GameObject pooledObject, int poolNumber, bool growing)
    {
        this.pooledObject = pooledObject;
        this.poolNumber = poolNumber;
        this.growing = growing;

        for (int i = 0; i < poolNumber; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject Take()
    {
        //On parcourt en partant de nextObject et en faisant le tour
        for (int i = 0; i < pool.Count; i++)
        {
            int next = (nextObject + i) % pool.Count;
            if (!pool[next].activeInHierarchy)
            {
                nextObject = (next + 1) % pool.Count;
                return pool[next];
            }
        }

        if (growing)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pool.Add(obj);
            return obj;
        }
        return null;
    }

    
}
