using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreIncreaser : MonoBehaviour
{


    Vector2 target;

    [SerializeField]
    float speed=1f;


    int points;
    // Start is called before the first frame update
    void Awake()
    {
        RectTransform rectTransform = Score.instance.GetComponent<RectTransform>();
        Vector3[] cornersPositions = new Vector3[4];
        rectTransform.GetWorldCorners(cornersPositions);
        target = Camera.main.ScreenToWorldPoint(0.5f*(cornersPositions[0] + cornersPositions[2]));//Camera.main.ScreenToWorldPoint();
        Debug.Log(target);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.SqrMagnitude(target- (Vector2)transform.position)>speed*speed*Time.deltaTime*Time.deltaTime)
        {
            transform.position += (Vector3)(target - new Vector2(transform.position.x, transform.position.y)).normalized*speed*Time.deltaTime ;
        }
        else
        {
            
            transform.position = target;
            Score.IncreaseScore(points);
            Score.instance.GetComponent<Text>().color = GetComponent<ParticleSystem>().main.startColor.color;
            gameObject.SetActive(false);
        }
        
    }

    public int Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }
}
