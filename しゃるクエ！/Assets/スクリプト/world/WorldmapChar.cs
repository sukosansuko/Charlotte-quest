using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldmapChar : MonoBehaviour
{
    string activScene;

    // Start is called before the first frame update
    public static Vector3 InitCharPos;        // ﾜｰﾙﾄﾞﾏｯﾌﾟ上のｷｬﾗｸﾀｰ初期位置(W1の場所)
    public static Vector3 SavePos = new Vector3(0, 0, 0);  // ｷｬﾗｸﾀｰ位置保存用
    public SpriteRenderer charImage;
    // Use this for initialization
    void Start()
    {
        charImage = GetComponent<SpriteRenderer>();
        InitCharPos = new Vector3(GameObject.Find("W1").transform.position.x, GameObject.Find("W1").transform.position.y + 1.4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (SavePos == new Vector3(0, 0, 0))
        {
            SavePos = InitCharPos;
        }
        charImage.transform.position = SavePos;
        activScene = SceneManager.GetActiveScene().name;
        if (activScene == "ワールドマップ")
        {
            mapChar.SavePos = new Vector3(0, 0, 0);
        }
    }
}
