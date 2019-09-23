using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{

    [SerializeField]
    int poolSize;
    [SerializeField]
    bool growing;

    static Factory instance;


    [SerializeField]
    List<ObjectStringCouple> objectFromStringList = new List<ObjectStringCouple>();
    Dictionary<string, GameObject> spawnObjectsFromStrings = new Dictionary<string, GameObject>();

    static Dictionary<string, Pool> poolFromName = new Dictionary<string, Pool>();

    // Start is called before the first frame update
    void Awake()
    {
        
        foreach (ObjectStringCouple objectString in objectFromStringList)
        {
            if (!spawnObjectsFromStrings.ContainsKey(objectString.spawnName))
            {
                spawnObjectsFromStrings.Add(objectString.spawnName, objectString.gameObject);
            }
        }


        instance = this;
        foreach (string spawnName in spawnObjectsFromStrings.Keys)
        {
            poolFromName[spawnName] = new Pool(spawnObjectsFromStrings[spawnName], poolSize, growing);
        }

        
        

    }
    public static GameObject Take(string spawnName)
    {

        GameObject go = poolFromName[spawnName].Take();
        if(!go)
        {
            Debug.LogError("Deficience in Factory: spawnName");
        }
        return go;
    }
    
}
