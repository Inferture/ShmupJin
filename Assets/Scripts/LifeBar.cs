using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeBar : MonoBehaviour
{

    [SerializeField]
    PlayerAvatar player;

    [SerializeField]
    GameObject heart;

    [SerializeField]
    int maxLife=10;

    List<GameObject> hearts = new List<GameObject>();


    // Start is called before the first frame update
    void Awake()
    {
        hearts = new List<GameObject>();
        Vector3 delta = new Vector3(-heart.GetComponent<RectTransform>().sizeDelta.x-5, 0, 0);
        for (int i=0;i<maxLife;i++)
        {
            GameObject newHeart = Instantiate(heart);
            newHeart.transform.parent = transform;
            newHeart.transform.localPosition = heart.transform.localPosition + i * delta;
            hearts.Add(newHeart);
        }
        for(int i=0;i<Mathf.Min(player.Life,maxLife);i++)
        {
            hearts[i].SetActive(true);
        }
        EventManager.OnHitOrHeal += RefreshLife;
        
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EventManager.OnHitOrHeal -= RefreshLife;
    }

    void RefreshLife(int life)
    {
        for (int i = 0; i < Mathf.Min(player.Life, maxLife); i++)
        {
            hearts[i].SetActive(true);
        }
        for (int i = Mathf.Min(Mathf.Max(player.Life, 0), maxLife); i < maxLife; i++)
        {
            hearts[i].SetActive(false);
        }
    }
}
