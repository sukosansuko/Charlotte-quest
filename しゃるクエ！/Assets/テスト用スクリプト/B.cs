using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B : MonoBehaviour
{
    A a;
    float pos = 0;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<A>();
    }

    // Update is called once per frame
    void Update()
    {
        //pos = A.posX;
        if (Input.GetKeyDown(KeyCode.A))
        {
            a.GetPos();
            Debug.Log(A.posX);
        }
    }
}
