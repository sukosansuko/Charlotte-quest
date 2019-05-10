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
            AudioManager.Instance.PlayBGM("ゲームオーバー3");
        }
        //prevScene = activScene;
    }
}
