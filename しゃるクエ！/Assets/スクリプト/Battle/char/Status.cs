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
    Image image;

    private GameObject battleManager;

    //  TL上の進行度
    public int TLProgress;
    private bool progressEnd;

    public STATE state;

    void Start()
    {
        battleManager = GameObject.Find("BattleManager");
        TLProgress = 0;
        progressEnd = false;
        SetChara();
    }

    void Update()
    {
        if (state == STATE.ST_DEAD)
        {
            Destroy(this.gameObject);
        }
        SetHP(GetHP());
        TLManager();
    }

    public void Dead()
    {
        SetState(STATE.ST_DEAD);
        //animator.SetTrigger("Dead");
    }

    private void TLManager()
    {
        //  ActiveChooseがtrueの間は進行度は増えない
        if (!battleManager.GetComponent<BattleScene>().GetComponent<BattleScene>().GetActiveChoose())
        {
            TLProgress++;
            //  行動選択開始
            if (TLProgress >= 100 && !progressEnd)
            {
                battleManager.GetComponent<BattleScene>().SetActiveChoose(true);
                progressEnd = true;
            }
            //  行動開始
            if (TLProgress >= 200)
            {
                Debug.Log("攻撃開始ィ！！");
                TLProgress = 0;
                progressEnd = false;
            }
        }
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

    public void SetChara()
    {
        var Name = this.gameObject.name;
        int charID;
        player_charaList PC;

        Debug.Log(Name);
        //  データベースからステータスを取得
        if (Name.StartsWith("p"))
        {
            PC = Resources.Load("ExcelData/player_chara") as player_charaList;
            if (Name.Contains("1"))
            {
                charID = GameObject.Find("BattleManager").GetComponent<BattleScene>().GetPID(1);
            }
            else if (Name.Contains("2"))
            {
                charID = GameObject.Find("BattleManager").GetComponent<BattleScene>().GetPID(2);
            }
            else
            {
                charID = GameObject.Find("BattleManager").GetComponent<BattleScene>().GetPID(3);
            }
            //Debug.Log("charIDは" + charID);
            //if(charID != 0)
            //{
            //    Debug.Log(PC.sheets[0].list[charID - 1].Name);
            //}
        }
        else
        {
            if (Name.Contains("1"))
            {
                charID = GameObject.Find("BattleManager").GetComponent<BattleScene>().GetEID(1);
            }
            else if (Name.Contains("2"))
            {
                charID = GameObject.Find("BattleManager").GetComponent<BattleScene>().GetEID(2);
            }
            else
            {
                charID = GameObject.Find("BattleManager").GetComponent<BattleScene>().GetEID(3);
            }
        }

        state = STATE.ST_ALIVE;
        SetHP(4);
    }

    //  ターゲットの切り替え用
    public void SetTarget()
    {
        battleManager.GetComponent<command>().SetTarget(gameObject.name);
        battleManager.GetComponent<command>().SkillDescription();
    }

    public void SetRayCast(bool flag)
    {
        image = GetComponent<Image>();
        image.raycastTarget = flag;
    }
}
