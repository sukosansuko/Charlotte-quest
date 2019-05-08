using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public bool actFlag;
    public Image image;
    Color color1 = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    Color color2 = new Color(1.0f, 1.0f, 1.0f, 0.0f);

    void Start()
    {
        actFlag = false;
        image = GetComponent<Image>();
    }

    void Update()
    {
        if(actFlag)
        {
            image.color = color1;
        }
        else
        {
            image.color = color2;
        }
    }

    public void TruePush()
    {
        actFlag = true;
    }

    public void FalsePush()
    {
        actFlag = false;
    }
}
