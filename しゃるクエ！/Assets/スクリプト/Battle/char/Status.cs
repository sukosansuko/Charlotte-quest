using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Status : MonoBehaviour
{
    private string CharName;                    //  キャラクターの名前
    [SerializeField] private int LV;            //  レベル
    [SerializeField] private int HP;            //  体力
    [SerializeField] private int SP;            //  技の発動に必要
    [SerializeField] private int ATK;           //  物理攻撃力
    [SerializeField] private int DEF;           //  物理防御力
    [SerializeField] private int SPD;           //  素早さ
    [SerializeField] private int MAT;           //  魔法攻撃力
    [SerializeField] private int MDF;           //  魔法防御力
    [SerializeField] private int LUK;           //  幸運値
    private string AResistance;                 //  物理耐性(基本は敵のみ)
    private string MResistance;                 //  魔法耐性(基本は敵のみ)
    private string weakAttribute;               //  弱点属性(基本は敵のみ)

    public GameObject TLIcon;
    public GameObject turnPointer;

    enemy_charaList EC;

    //  TLのアイコン位置計算用
    private Vector3 tmp;

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
    private GameObject sceneNavigator;

    //  プレイヤーかどうか判断するために使用する
    private bool playerProof;

    //  アタッチしているオブジェクトの名前
    public string Name;
    public int charID;

    //  TL上の進行度
    public int TLProgress;
    private bool progressEnd;

    public STATE state;

    void Start()
    {
        battleManager = GameObject.Find("BattleManager");

        sceneNavigator = GameObject.Find("SceneNavigator");

        Name = this.gameObject.name;

        TLIcon.GetComponent<Image>().enabled = false;
        turnPointer.GetComponent<Image>().enabled = false;

        tmp = TLIcon.transform.position;
        TLIcon.transform.position = new Vector3(tmp.x,tmp.y,tmp.z);
        TLProgress = 0;
        progressEnd = false;
        SetChara();
    }

    void Update()
    {
        if (state == STATE.ST_DEAD)
        {
            Destroy(this.gameObject);
            Destroy(TLIcon);
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
        Vector3 pos = TLIcon.transform.position;
        pos = new Vector3(tmp.x + TLProgress,tmp.y,tmp.z);
        TLIcon.transform.position = pos;

        //  ActiveChooseがtrueの間は進行度は増えない
        if (battleManager.GetComponent<BattleScene>().GetComponent<BattleScene>().GetActiveChoose() == false)
        {
            int ProgressSPD = SPD / 20;
            if(ProgressSPD <= 0)
            {
                ProgressSPD = 1;
            }

            TLProgress += ProgressSPD;
            //  行動選択開始
            if (TLProgress >= 200 && !progressEnd)
            {
                battleManager.GetComponent<command>().SetCharID(charID - 1);
                battleManager.GetComponent<BattleScene>().SetActiveChoose(true);
                battleManager.GetComponent<command>().commandDisplay();

                if (playerProof)
                {
                    //  ポインターの表示
                    turnPointer.GetComponent<Image>().enabled = true;
                }
                progressEnd = true;
            }
            //  行動開始
            if (TLProgress >= 300)
            {
                Debug.Log("攻撃開始ィ！！");
                TLProgress = 0;
                progressEnd = false;
            }

            if(battleManager.GetComponent<command>().GetCommandEnd())
            {
                if (playerProof)
                {
                    //  ポインターの表示
                    turnPointer.GetComponent<Image>().enabled = false;
                }
            }
        }
    }

    public void SetChara()
    {
        Debug.Log(Name);
        //  データベースからステータスを取得
        if (Name.StartsWith("p"))
        {
            playerProof = true;
        }
        else
        {
            playerProof = false;
        }

        if (playerProof)
        {
            if (Name.Contains("1"))
            {
                charID = battleManager.GetComponent<BattleScene>().GetPID(1);
                TLIcon.GetComponent<Image>().enabled = true;
            }
            else if (Name.Contains("2"))
            {
                charID = battleManager.GetComponent<BattleScene>().GetPID(2);
                TLIcon.GetComponent<Image>().enabled = true;
            }
            else
            {
                charID = battleManager.GetComponent<BattleScene>().GetPID(3);
                TLIcon.GetComponent<Image>().enabled = true;
            }
            sceneNavigator.GetComponent<StatusControl>().SetStatus(charID - 1,ref CharName,ref LV,ref HP,ref SP,ref ATK,ref DEF,ref SPD,ref MAT,ref MDF,ref LUK);
        }
        else
        {
            if (Name.Contains("1"))
            {
                charID = battleManager.GetComponent<BattleScene>().GetEID(1);
                TLIcon.GetComponent<Image>().enabled = true;
            }
            else if (Name.Contains("2"))
            {
                charID = battleManager.GetComponent<BattleScene>().GetEID(2);
                TLIcon.GetComponent<Image>().enabled = true;
            }
            else
            {
                charID = battleManager.GetComponent<BattleScene>().GetEID(3);
                TLIcon.GetComponent<Image>().enabled = true;
            }
            SetEnemyStatus(charID - 1,ref CharName, ref LV, ref HP, ref SP, ref ATK, ref DEF, ref SPD, ref MAT, ref MDF, ref LUK);
        }
        Debug.Log("charIDは" + charID);
        if (charID != 0)
        {
            Debug.Log(CharName);
            Debug.Log("LV" + LV);
            Debug.Log("HP" + HP);
            Debug.Log("SP" + SP);
            Debug.Log("ATK" + ATK);
            Debug.Log("DEF" + DEF);
            Debug.Log("SPD" + SPD);
            Debug.Log("MAT" + MAT);
            Debug.Log("MDF" + MDF);
            Debug.Log("LUK" + LUK);
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

    public void SetState(STATE st)
    {
        this.state = st;
    }

    private void SetEnemyStatus(int id,ref string charName,ref int lv, ref int hp, ref int sp, ref int atk, ref int def, ref int spd, ref int mat, ref int mdf, ref int luk)
    {
        EC = Resources.Load("ExcelData/enemy_chara") as enemy_charaList;

        lv = 1;
        ////  レベルによってステータスを加算する
        charName = EC.sheets[0].list[id].Name;
        hp = (int)EC.sheets[0].list[id].HP;
        sp = (int)EC.sheets[0].list[id].SP;
        atk = (int)EC.sheets[0].list[id].ATK;
        def = (int)EC.sheets[0].list[id].DEF;
        spd = (int)EC.sheets[0].list[id].SPD;
        mat = (int)EC.sheets[0].list[id].MAT;
        mdf = (int)EC.sheets[0].list[id].MDF;
        luk = (int)EC.sheets[0].list[id].LUK;
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
