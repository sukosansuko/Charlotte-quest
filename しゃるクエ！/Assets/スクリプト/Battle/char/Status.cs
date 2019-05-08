using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Status : MonoBehaviour
{
    [SerializeField] private int LV;             //  レベル
    [SerializeField] private int HP;             //  体力
    [SerializeField] private int SP;             //  技の発動に必要
    [SerializeField] private int ATK;            //  物理攻撃力
    [SerializeField] private int DEF;            //  物理防御力
    [SerializeField] private int SPD;            //  素早さ
    [SerializeField] private int MAT;            //  魔法攻撃力
    [SerializeField] private int MDF;            //  魔法防御力
    [SerializeField] private int LUK;            //  幸運値

    public enum STATE
    {
        ST_NON,
        ST_ALIVE,                   //  生きている
        ST_ACT,                     //  行動中
        ST_DEAD,                    //  死亡
        ST_MAX
    }

    public STATE state;
    public GameObject chara;

    void Start()
    {
        state = STATE.ST_ALIVE;
        SetHP(4);
    }

    void Update()
    {
        if (state == STATE.ST_DEAD)
        {
            Destroy(this.gameObject);
        }
        SetHP(GetHP());
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

    public void SetHP(int hp)
    {
        this.HP = hp;
        if (HP <= 0)
        {
            Dead();
        }
    }

    public int GetHP()
    {
        return HP;
    }

    public void Damage(int Damage)
    {
        HP -= Damage;
    }

    public void SetSP(int sp)
    {
        this.SP = sp;
    }

    public int GetSP()
    {
        return SP;
    }

    public void SetATK(int atk)
    {
        this.ATK = atk;
    }

    public int GetATK()
    {
        return ATK;
    }

    public void SetDEF(int def)
    {
        this.DEF = def;
    }

    public int GetDEF()
    {
        return DEF;
    }

    public void SetSPD(int spd)
    {
        this.SPD = spd;
    }

    public int GetSPD()
    {
        return SPD;
    }

    public void SetMAT(int mat)
    {
        this.MAT = mat;
    }

    public int GetMAT()
    {
        return MAT;
    }

    public void SetMDF(int mdf)
    {
        this.MDF = mdf;
    }

    public int GetMDF()
    {
        return MDF;
    }

    public void SetLUK(int luk)
    {
        this.LUK = luk;
    }

    public int GetLUK()
    {
        return LUK;
    }
}
