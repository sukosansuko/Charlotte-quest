using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A : MonoBehaviour
{
   public static float posX;
    // Start is called before the first frame update
    void Start()
    {
       posX = this.gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.A))
        //{
        //    GetPos();
        //}
    }
    public void GetPos()
    {
        posX += 1;
        Debug.Log(posX);
    }
}
