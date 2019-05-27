using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMselect : SingletonMonoBehaviour<BGMselect>
{
    public static BGMselect instance = null;


    string activScene;
    string prevScene;
    string worldNum;

    // Start is called before the first frame update

    private void Awake()
    {
        // インスタンスが存在するとき
        if (instance != null)
        {
            // これを破棄する
            Destroy(this.gameObject);
            return;
        }
        // インスタンスが存在しない時
        else if (instance == null)
        {
            // これをインスタンスとする
            instance = this;
        }
        // シーンをまたいでも消去されないようにする
        DontDestroyOnLoad(this.gameObject);

    }
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        activScene = SceneManager.GetActiveScene().name;
        worldNum = WorldMass.stageName;
        if(activScene != prevScene)
        {
            AudioManager.Instance.StopBGM();
        }
        if (activScene == "タイトル")
        {
            AudioManager.Instance.PlayBGM("高槻ワンサイドラバー");
        }
        if (activScene == "ホーム")
        {
            AudioManager.Instance.PlayBGM("");
        }
        if (activScene == "編成")
        {
            AudioManager.Instance.PlayBGM("");
        }
        if (activScene == "ワールドマップ")
        {
            AudioManager.Instance.PlayBGM("");
        }
        if (activScene == "W1")
        {
            AudioManager.Instance.PlayBGM("");
        }
        if (activScene == "W2")
        {
            AudioManager.Instance.PlayBGM("");
        }
        if (activScene == "W3")
        {
            AudioManager.Instance.PlayBGM("");
        }
        if (activScene == "W4")
        {
            AudioManager.Instance.PlayBGM("");
        }
        if (worldNum == "W1")
        {
            if (activScene == "BattleScene")
            {
                AudioManager.Instance.PlayBGM("");
            }
        }
        if (worldNum == "W2")
        {
            if (activScene == "BattleScene")
            {
                AudioManager.Instance.PlayBGM("");
            }
        }
        if (worldNum == "W3")
        {
            if (activScene == "BattleScene")
            {
                AudioManager.Instance.PlayBGM("");
            }
        }
        if (worldNum == "W4")
        {
            if (activScene == "BattleScene")
            {
                AudioManager.Instance.PlayBGM("");
            }
        }
        prevScene = activScene;
    }
}
