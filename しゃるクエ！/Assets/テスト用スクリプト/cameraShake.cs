using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour {
    Vector3 InitPos;
    Vector3 ShakePos;

    int shakeCnt;
    bool shakeFlag;
	// Use this for initialization
	void Start () {
        InitPos = new Vector3(0, 0, -10);
        gameObject.transform.position = InitPos;
        shakeFlag = false;
        shakeCnt = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.S))
        {
            shakeFlag = true;
            shakeCnt = 0;
        }
        if(shakeFlag)
        {
            gameObject.transform.position = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), -10);
            shakeCnt++;
        }
        else if(!shakeFlag){
            gameObject.transform.position = InitPos;
        }
        if(shakeCnt >= 20)
        {
            shakeFlag = false;
        }
	}
}
