using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Enemy : MonoBehaviour
{
    public enum STATE
    {
        ST_NON,
        ST_ALIVE,                   //  生きている
        ST_ACT,                     //  行動中
        ST_DAMAGE,                  //  ダメージ
        ST_DEAD,                    //  死亡
        ST_MAX
    }

    [SerializeField]int EnemyNumber = 1;
    public STATE state;
    GameObject enemy;
    EnemyStatus status;

    void Start()
    {
        enemy = GameObject.Find("enemy" + EnemyNumber);
        status = enemy.GetComponent<EnemyStatus>();

        state = STATE.ST_ALIVE;
        status.SetHP(4);
    }

    void Update()
    {
        if (state == STATE.ST_DEAD)
        {
            Destroy(this.gameObject);
        }
        status.SetHP(status.GetHP());
        if (Input.GetMouseButtonDown(0))
        {
            status.Damage(1);
            Debug.Log(status.GetHP());
        }
    }

    public void Dead()
    {
        SetState(STATE.ST_DEAD);
        //animator.SetTrigger("Dead");
    }

    public void SetState(STATE st)
    {
        this.state = st;
    }
}
