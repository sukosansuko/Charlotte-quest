﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMass : MonoBehaviour
{
    string activScene;      // 現在のｼｰﾝ名

    // Use this for initialization
    void Start()
    {
        activScene = mapChar.activScene;
    }

    // Update is called once per frame
    void Update()
    {
        if (mapChar.activScene == "W1")
        {
            if (stageMass.MassName == "boss")
            {

            }
        }
        if (mapChar.activScene == "W2")
        {
            if (stageMass.MassName == "boss")
            {

            }
        }
        if (mapChar.activScene == "W3")
        {
            if (stageMass.MassName == "boss")
            {

            }
        }
        if (mapChar.activScene == "W4")
        {
            if (stageMass.MassName == "boss")
            {

            }
        }
    }
}
