using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeated : MonoBehaviour
{
    [SerializeField]
    GameObject objectToRepeat;
    [SerializeField]
    int clonesRight;
    [SerializeField]
    int clonesLeft;
    [SerializeField]
    int clonesUp;
    [SerializeField]
    int clonesDown;

    float width;
    float height;


    // Start is called before the first frame update
    void Start()
    {
        
        width = objectToRepeat.GetComponent<SpriteRenderer>().bounds.size.x;
        height = objectToRepeat.GetComponent<SpriteRenderer>().bounds.size.y;
        
        Vector2 originalPos = objectToRepeat.transform.position;
        Transform originalParent = objectToRepeat.transform.parent;
        
        for (int i = 0; i < clonesRight; i++)
        {
            GameObject go = Instantiate(objectToRepeat);
            if (originalParent)
            {
                go.transform.SetParent(originalParent);
            }
            go.transform.position = new Vector2(originalPos.x + width * (i + 1), originalPos.y);
        }
        
        for (int i = 0; i < clonesLeft; i++)
        {
            GameObject go = Instantiate(objectToRepeat);
            if (originalParent)
            {
                go.transform.SetParent(originalParent);
            }
            go.transform.position = new Vector2(originalPos.x - width * (i + 1), originalPos.y);
        }

        for (int i = 0; i < clonesUp; i++)
        {
            GameObject go = Instantiate(objectToRepeat);
            if (originalParent)
            {
                go.transform.SetParent(originalParent);
            }
            go.transform.position = new Vector2(originalPos.x, originalPos.y + height * (i + 1));
        }

        for (int i = 0; i < clonesDown; i++)
        {
            GameObject go = Instantiate(objectToRepeat);
            if (originalParent)
            {
                go.transform.SetParent(originalParent);
            }
            go.transform.position = new Vector2(originalPos.x, originalPos.y - height * (i + 1));
        }
        
    }
}
