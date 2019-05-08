using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCamera2 : MonoBehaviour
{
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    private Vector3 nowPos;
    private Vector3 afterPos;


   public int moveCnt;
    int moveMax = 2;
    // Start is called before the first frame update
    void Start()
    {
        moveCnt = -2;
    }

    // Update is called once per frame
    void Update()
    {
        Flick();
        CameraMove();
    }
    void Flick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }
        if(Input.GetMouseButtonUp(0))
        {
            touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            GetDir();
        }
    }
    void GetDir()
    {
        float FlickSize = 30;       // フリックを認知させる大きさ

        float dirX = touchEndPos.x - touchStartPos.x;
        if (moveCnt > -moveMax)
        {
            if (FlickSize < dirX)
            {
                //  右向きの時の処理
                Debug.Log("みぎ");
                moveCnt--;
            }
        }
        if (moveCnt < moveMax)
        {
            if (-FlickSize > dirX)
            {
                //  左向きの時の処理
                Debug.Log("ひだり");
                moveCnt++;
            }

        }
    }
    void CameraMove()
    {
        int movePos = 10;
        this.transform.position = nowPos;

        afterPos.x = moveCnt * movePos;
        if (nowPos.x > afterPos.x)
        {
            nowPos.x--;
        }
        else if(nowPos.x < afterPos.x)
        {
            nowPos.x++;
        }
    }
}
