using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backButton : MonoBehaviour
{
    string activScene;      // 現在のｼｰﾝ名
    public static string prevScene; // ひとつ前のｼｰﾝ名
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Push()
    {
        if(activScene != prevScene)
        {
            //SceneNavigator.Instance.Change("");
        }
    }
}
