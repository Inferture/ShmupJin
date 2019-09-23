using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OptionRoom : Option
{
    public string scene;
    // Use this for initialization
    void Start()
    {
        if (parentMenu == null && transform.parent != null && transform.parent.GetComponent<Menu>() != null)
        {
            parentMenu = transform.parent.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    override public void Select()
    {
        SceneManager.LoadScene(scene);
    }
}