using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectList : MonoBehaviour
{
    public static int num;

    void Start()
    {
        num = 0;
    }

    void Update()
    {
        if(num == 1)
        {
            ATK();
        }
        else if(num == 2)
        {
            DEF();
        }
        else if(num == 3)
        {
            ITEM();
        }

        else
        {
            ;
        }
    }

    private void ATK()
    {
        Debug.Log("アタックチャンス");
    }

    private void DEF()
    {
        Debug.Log("ディフェンスチャンス");
    }

    private void ITEM()
    {
        Debug.Log("アイテムチャンス");
    }

    public void InitPush()
    {
        num = 0;
    }

    public void ATKPush()
    {
        num = 1;
    }

    public void DEFPush()
    {
        num = 2;
    }

    public void ITEMPush()
    {
        num = 3;
    }
}
