using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animTest : MonoBehaviour
{
    Animator anim;
    CharacterController con;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        con = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("isAttack", true);
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isDefence", true);
        }
        else
        {
            anim.SetBool("isDefence", false);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("isDeath", true);
        }
        else
        {
            anim.SetBool("isDeath", false);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("isDamage", true);
        }
        else
        {
            anim.SetBool("isDamage", false);
        }
        if(Input.GetKey(KeyCode.None))
        {
            anim.SetBool("isIdol", true);
        }
        else
        {
            anim.SetBool("isIdol", false);
        }
    }
}
