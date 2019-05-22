using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapChar : MonoBehaviour {

    public static string activScene;

    public SpriteRenderer charImage;
    public static Vector3 InitCharPos;        // ｽﾃｰｼﾞﾏｯﾌﾟ上のｷｬﾗｸﾀｰ初期位置(talk1の場所)
    public static Vector3 SavePos = new Vector3(0, 0, 10);
    // Use this for initialization
    void Start()
    {
        charImage = GetComponent<SpriteRenderer>();
        InitCharPos = new Vector3(GameObject.Find("talk1").transform.position.x, GameObject.Find("talk1").transform.position.y + 1.4f, 10);
        charImage.transform.position = InitCharPos;
        activScene = SceneManager.GetActiveScene().name;
    }
    
	// Update is called once per frame
	void Update ()
    {
        if (SavePos == new Vector3(0, 0, 10))
        {
            SavePos = InitCharPos;
        }
        charImage.transform.position = SavePos;
    }
}
