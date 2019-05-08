using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapChar : MonoBehaviour {

    public static float posx, posy, posz;
    public SpriteRenderer charImage;
    // Use this for initialization
    void Start()
    {
        charImage = GetComponent<SpriteRenderer>();
        posz = 1;
    }
    
	// Update is called once per frame
	void Update () {
        charImage.transform.position = new Vector3(posx, posy, posz);
    }
}
