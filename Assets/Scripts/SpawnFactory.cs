using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFactory : MonoBehaviour
{

    [SerializeField]
    int poolSize;
    [SerializeField]
    bool growing;

    static SpawnFactory instance;


    static Dictionary<string, Pool> poolFromName = new Dictionary<string, Pool>();

    static bool initialized = false;
    
    void Awake()
    {

        
        foreach (string spawnName in LevelPatternSaver.spawnObjectsFromStrings.Keys)
        {
            Pool pool = new Pool(LevelPatternSaver.spawnObjectsFromStrings[spawnName], poolSize, growing);
            //poolFromName.Add(spawnName, pool);
            poolFromName[spawnName] = pool;
        }
            

        instance = this;
    }
    public static GameObject Take(string spawnName)
    {

        return poolFromName[spawnName].Take();
    }
   
}
