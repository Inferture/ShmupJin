using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    string levelName;

    LevelPatternSaver.LevelPattern spawnPattern;

    float time = 0;
    int numSpawn;
    int nbSpawns;

    [SerializeField]
    GameObject backgrounds;

    bool bossSpawned;

    [SerializeField]
    GameObject boss;

    [SerializeField]
    GameObject bossUI;

    // Start is called before the first frame update
    void Awake()
    {

        TextAsset textAsset = Resources.Load<TextAsset>("Patterns/Levels/"+levelName);
        spawnPattern = XmlSerialization.ReadFromXmlResource<LevelPatternSaver.LevelPattern>(textAsset);
        time = 0;
        nbSpawns = spawnPattern.spawns.Count;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Debug.Log("I exist !!");
        while (numSpawn<nbSpawns && spawnPattern.spawns[numSpawn].time<=time)
        {
            Debug.Log("ENEMYFACTORY!");
            //GameObject spawn = GameObject.Instantiate(LevelPatternSaver.spawnObjectsFromStrings[spawnPattern.spawns[numSpawn].spawned]);
            GameObject spawn = SpawnFactory.Take(spawnPattern.spawns[numSpawn].spawned);
            spawn.transform.position = new Vector2(spawnPattern.spawns[numSpawn].x, spawnPattern.spawns[numSpawn].y);
            spawn.SetActive(true);
            numSpawn++;
            IncreaseSpeed(1.01f);
        }
        if(!bossSpawned && numSpawn >= nbSpawns)
        {
            Invoke("SpawnBoss", 10);
            bossSpawned = true;
            IncreaseSpeed(2);
        }
    }

    void IncreaseSpeed(float speedRatio)
    {
        BackgroundScroll[] backGroudscrolls = backgrounds.GetComponentsInChildren<BackgroundScroll>();
        foreach (BackgroundScroll backGroudscroll in backGroudscrolls)
        {
            backGroudscroll.HorizontalSpeed *= speedRatio;
            backGroudscroll.VerticalSpeed *= speedRatio;
        }
    }

    void SpawnBoss()
    {
        boss.SetActive(true);
        bossUI.SetActive(true);
    }

}
