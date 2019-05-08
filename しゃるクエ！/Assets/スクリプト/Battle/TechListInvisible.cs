using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechListInvisible : MonoBehaviour
{
    public int actFlag;

    void Start()
    {
        actFlag = 0;
    }

    // テキストの表示・非表示の切り替え
    public void ATKEnabled(Text textComponent)
    {
        actFlag = SelectList.num;
        if (actFlag == 1)
        {
            textComponent.enabled = true;
        }
        else
        {
            textComponent.enabled = false;
        }
    }

    public void DEFEnabled(Text textComponent)
    {
        actFlag = SelectList.num;
        if (actFlag == 2)
        {
            textComponent.enabled = true;
        }
        else
        {
            textComponent.enabled = false;
        }
    }

    public void ITEMEnabled(Text textComponent)
    {
        actFlag = SelectList.num;
        if (actFlag == 3)
        {
            textComponent.enabled = true;
        }
        else
        {
            textComponent.enabled = false;
        }
    }
}
