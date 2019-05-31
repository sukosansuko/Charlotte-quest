using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleMass : MonoBehaviour {

    string activScene;      // 現在のｼｰﾝ名

    // Use this for initialization
    void Start()
    {
        activScene = mapChar.activScene;
    }

    // Update is called once per frame
    void Update () {
        if (mapChar.activScene == "W1")
        {
            if (stageMass.MassName == "battle1")
            {

                SceneNavigator.Instance.Change("タイトル");
            }
            if (stageMass.MassName == "battle2")
            {
                SceneNavigator.Instance.Change("ホーム");
            }
        }
        if (mapChar.activScene == "W2")
        {
            if (stageMass.MassName == "battle1")
            {

            }
            if (stageMass.MassName == "battle2")
            {

            }
        }
        if (mapChar.activScene == "W3")
        {
            if (stageMass.MassName == "battle1")
            {

            }
            if (stageMass.MassName == "battle2")
            {

            }
        }
        if (mapChar.activScene == "W4")
        {
            if (stageMass.MassName == "battle1")
            {

            }
            if (stageMass.MassName == "battle2")
            {

            }
        }
    }
}
