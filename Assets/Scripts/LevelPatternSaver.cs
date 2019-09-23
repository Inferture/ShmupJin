using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;



[Serializable]
public struct ObjectStringCouple
{
    public string spawnName;
    public GameObject gameObject;
}

public class LevelPatternSaver : MonoBehaviour
{



    //[SerializeField]

    //Dictionary<string, GameObject> objectFromSpawnName = new Dictionary<string, GameObject>();

    [SerializeField]
    string levelName = "Level01";
    

    [Serializable]
    public struct SpawnEvent
    {
        public string spawned;
        public float time;
        public float x;
        public float y;

        public SpawnEvent(string spawned, float t, float x, float y)
        {
            this.spawned = spawned;
            time = t;
            this.x = x;
            this.y = y;
        }
    }

    [Serializable]
    public struct LevelPattern
    {
        [SerializeField]
        public List<SpawnEvent> spawns;
    }

    float time = 0;
    bool pause;
    [SerializeField]
    string currentSpawn;
    int currentSpawnNum;
    List<SpawnEvent> spawns = new List<SpawnEvent>();
    [SerializeField]

    List<ObjectStringCouple> objectsFromStrings;

    public static Dictionary<string, GameObject> spawnObjectsFromStrings = new Dictionary<string, GameObject>();

    void Awake()
    {
        time = 0;
        spawns = new List<SpawnEvent>();

        foreach(ObjectStringCouple objectString in objectsFromStrings)
        {
            if(!spawnObjectsFromStrings.ContainsKey(objectString.spawnName))
            {
                spawnObjectsFromStrings.Add(objectString.spawnName, objectString.gameObject);
            }
        }

        currentSpawnNum = 0;
        currentSpawn = objectsFromStrings[0].spawnName;



    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            if(pause)
            {
                GetComponent<AudioSource>().Pause();
            }
            else
            {
                GetComponent<AudioSource>().UnPause();
            }

        }
        if(!pause)
        {
            time += Time.deltaTime * GetComponent<AudioSource>().pitch ;
        }

        //GetComponent<Text>().text = "Time: " + time + "/Current spawn:" + currentSpawn;


        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mouse = Input.mousePosition;
            Vector2 pos = Camera.main.ScreenToWorldPoint(mouse);
            pos = new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));//////////////////
            spawns.Add(new SpawnEvent(currentSpawn,time-0.5f,pos.x,pos.y));
            GameObject obj = GameObject.Instantiate( spawnObjectsFromStrings[currentSpawn]);
            obj.transform.position = pos;
            obj.SetActive(true);
        }
        if(Input.GetMouseButtonDown(1))
        {
            int maxObj = 2;//objectsFromStrings.Count
            currentSpawnNum =(currentSpawnNum + 1)% maxObj;
            currentSpawn = objectsFromStrings[currentSpawnNum].spawnName;
        }
        if (Input.GetMouseButtonDown(2))
        {
            int maxObj = 2;//objectsFromStrings.Count
            currentSpawnNum = (currentSpawnNum - 1 + maxObj) % maxObj;
            currentSpawn = objectsFromStrings[currentSpawnNum].spawnName;
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Save();
        }
    }


    void Save()
    {
        LevelPattern pattern;
        pattern.spawns = spawns;
        
        XmlSerialization.WriteToXmlResource<LevelPattern>("Patterns/Levels/"+levelName + ".xml",pattern);
    }


}
