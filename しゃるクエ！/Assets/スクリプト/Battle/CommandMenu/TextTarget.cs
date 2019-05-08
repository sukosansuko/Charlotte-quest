using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTarget : MonoBehaviour
{
    public bool actFlag;

    void Start()
    {
        actFlag = false;
    }

    public void Enabled(Text textComponent)
    {
        if (actFlag)
        {
            textComponent.enabled = true;
        }
        else
        {
            textComponent.enabled = false;
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
