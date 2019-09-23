using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField]
    PlayerAvatar player;

    [SerializeField]
    Color overHeatedColor;

    [SerializeField]
    Color energyColor;

    float energyWidth;
    // Start is called before the first frame update
    void Awake()
    {
        energyWidth = transform.parent.parent.GetComponent<Image>().sprite.rect.size.x * transform.lossyScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float energy = player.Energy;
        float maxEnergy = player.MaxEnergy;
        RectTransform transform = GetComponent<RectTransform>();

        float right = ((maxEnergy - energy) / maxEnergy) * energyWidth;
        transform.offsetMax = new Vector2(-right, transform.offsetMax.y);

        if (player.Overheat)
        {
            GetComponent<Image>().color = overHeatedColor;
            transform.parent.parent.GetComponent<Image>().color = new Color(0.1f,0.1f,0.2f);
        }
        else
        {
            GetComponent<Image>().color = energyColor;
            transform.parent.parent.GetComponent<Image>().color = Color.white;
        }
    }
}
