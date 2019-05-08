using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bmenu : MonoBehaviour
{

    public bool actFlag;
    public static bool flag;

    // Use this for initialization
    void Start()
    {
        actFlag = true;
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        flag = actFlag;
    }

    public void menuBotton()
    {
        if(actFlag)
        {
            actFlag = false;
        }
        else
        {
            actFlag = true;
        }
    }
}