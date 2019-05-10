using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Novel;

public class talkMass : MonoBehaviour
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
            if (stageMass.MassName == "talk1")      // 取得したﾏｽ名ごとに呼び出すｼﾅﾘｵを変える
            {
                NovelSingleton.StatusManager.callJoker("wide/scene1", "");
            }
            if (stageMass.MassName == "talk2")
            {
                NovelSingleton.StatusManager.callJoker("wide/scene2", "");
            }
            if (stageMass.MassName == "talk3")
            {
                NovelSingleton.StatusManager.callJoker("wide/scene3", "");
            }
        }
        if (mapChar.activScene == "W2")
        {
            if (stageMass.MassName == "talk1")
            {
                NovelSingleton.StatusManager.callJoker("wide/scene4", "");
            }
            if (stageMass.MassName == "talk2")
            {
                NovelSingleton.StatusManager.callJoker("wide/scene5", "");
            }
            if (stageMass.MassName == "talk3")
            {
                NovelSingleton.StatusManager.callJoker("wide/scene6", "");
            }
        }
        if (mapChar.activScene == "W3")
        {
            if (stageMass.MassName == "talk1")
            {
                NovelSingleton.StatusManager.callJoker("wide/scene7", "");
            }
            if (stageMass.MassName == "talk2")
            {
                NovelSingleton.StatusManager.callJoker("wide/scene8", "");
            }
            if (stageMass.MassName == "talk3")
            {
                NovelSingleton.StatusManager.callJoker("wide/scene9", "");
            }
        }
        if (mapChar.activScene == "W4")
        {
            if (stageMass.MassName == "talk1")
            {
                NovelSingleton.StatusManager.callJoker("wide/scene10", "");
            }
            if (stageMass.MassName == "talk2")
            {
                NovelSingleton.StatusManager.callJoker("wide/scene11", "");
            }
            if (stageMass.MassName == "talk3")
            {
                NovelSingleton.StatusManager.callJoker("wide/scene12", "");
            }
        }
    }
}
