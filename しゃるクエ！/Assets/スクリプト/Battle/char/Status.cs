using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

    public GameObject MyTarget;
    public GameObject TLIcon;
    public GameObject turnPointer;

    player_charaList PC;
    player_skillList PS;

    enemy_charaList EC;

    private List<int> SaveSkillIDList = new List<int>();
    private List<GameObject> SaveReceiveList = new List<GameObject>();

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

    //  行動選択時に攻撃または支援を与える相手格納用
    private GameObject TargetChar;

    //  プレイヤーかどうか判断するために使用する(プレイヤーならtrue)
    private bool playerProof;

    //  アタッチしているオブジェクトの名前
    public string Name;
    public int charID;

    //  TL上の進行度
    public float TLProgress;
    private bool progressEnd;

    public STATE state;

    private bool actionFlag = false;
    private int testTime = 0;
    private bool skillInputFlag = false;

    private int spCost;
    private int HPCtlFlag;
    private int AttackType;
    private double HealPercent;

    private GameObject ReceiveChara;

    void Start()
    {
        //  技リストと被ダメージキャラリストの初期化
        for (int count = 0; count < 6; count++)
        {
            SaveSkillIDList.Add(0);
            SaveReceiveList.Add(GameObject.Find("enemy1"));
        }

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
            //battleManager.GetComponent<command>().SetTargetStart(Name);
            Destroy(this.gameObject);
            Destroy(TLIcon);
            Destroy(MyTarget);
        }
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
        pos = new Vector3(tmp.x + TLProgress / 40,tmp.y,tmp.z);
        TLIcon.transform.position = pos;

        //  ActiveChooseがtrueの間は進行度は増えない
        if (battleManager.GetComponent<BattleScene>().GetComponent<BattleScene>().GetActiveChoose() == false)
        {
            if (battleManager.GetComponent<BattleScene>().GetComponent<BattleScene>().GetActionFlag() == false)
            {
                //  移動
                float ProgressSPD = SPD / 20;
                if (ProgressSPD <= 0)
                {
                    ProgressSPD = 1;
                }
                TLProgress += ProgressSPD;
            }

            //  行動選択開始
            if (TLProgress >= 200 && !progressEnd)
            {
                TLProgress = 200;
                battleManager.GetComponent<command>().SetCharID(charID - 1);
                battleManager.GetComponent<BattleScene>().SetActiveChoose(true);
                battleManager.GetComponent<command>().commandDisplay();

                if (playerProof)
                {
                    battleManager.GetComponent<BattleScene>().SetActivePlayer(this.gameObject);
                    //  ポインターの表示
                    turnPointer.GetComponent<Image>().enabled = true;
                }
                progressEnd = true;
            }

            //if(TLProgress > 200 && !skillInputFlag)
            //{
            //    SaveSkill();
            //    skillInputFlag = true;
            //}

            //  行動開始
            if (TLProgress >= 300)
            {
                testTime++;
                if (!actionFlag)
                {
                    battleManager.GetComponent<BattleScene>().SetAttackObj(this.gameObject);
                    LoadSkill();
                    battleManager.GetComponent<command>().ActionStart();


                    battleManager.GetComponent<BattleScene>().TakeAction(spCost,HPCtlFlag,AttackType,HealPercent);


                    battleManager.GetComponent<BattleScene>().SetActionFlag(true);

                    actionFlag = true;
                }

                if(testTime >= 100)
                {
                    battleManager.GetComponent<command>().ActionEnd();
                    TLProgress = 0;
                    actionFlag = false;
                    progressEnd = false;
                    battleManager.GetComponent<BattleScene>().SetActionFlag(false);
                    testTime = 0;
                }
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
        //Debug.Log(Name);
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

        if (charID != 0)
        {
            //Debug.Log(CharName);
            //Debug.Log("LV" + LV);
            //Debug.Log("HP" + HP);
            //Debug.Log("SP" + SP);
            //Debug.Log("ATK" + ATK);
            //Debug.Log("DEF" + DEF);
            //Debug.Log("SPD" + SPD);
            //Debug.Log("MAT" + MAT);
            //Debug.Log("MDF" + MDF);
            //Debug.Log("LUK" + LUK);
        }

        state = STATE.ST_ALIVE;
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

    //  行動選択が完了したら、行動するまで行動の内容を保存しておく
    public void SaveSkill(int SkillID)
    {
        if (playerProof)
        {
            if (Name.Contains("1"))
            {
                SaveSkillIDList.Insert(0, SkillID);
            }
            else if (Name.Contains("2"))
            {
                SaveSkillIDList.Insert(1, SkillID);
            }
            else
            {
                SaveSkillIDList.Insert(2, SkillID);
            }
        }
        else
        {
            if (Name.Contains("1"))
            {
                SaveSkillIDList.Insert(3, SkillID);
            }
            else if (Name.Contains("2"))
            {
                SaveSkillIDList.Insert(4, SkillID);
            }
            else
            {
                SaveSkillIDList.Insert(5, SkillID);
            }
        }
    }

    //  攻撃を受ける側を保存
    public void SaveReceive(GameObject receive)
    {
        int attackID;
        if (playerProof)
        {
            if (Name.Contains("1"))
            {
                attackID = 0;
            }
            else if (Name.Contains("2"))
            {
                attackID = 1;
            }
            else
            {
                attackID = 2;
            }
        }
        else
        {
            if (Name.Contains("1"))
            {
                attackID = 3;
            }
            else if (Name.Contains("2"))
            {
                attackID = 4;
            }
            else
            {
                attackID = 5;
            }
        }

        string ReceiveName = receive.name;
        GameObject target;
        if (ReceiveName.StartsWith("p"))
        {
            if (ReceiveName.Contains("1"))
            {
                target = GameObject.Find("player1");
            }
            else if (ReceiveName.Contains("2"))
            {
                target = GameObject.Find("player2");
            }
            else
            {
                target = GameObject.Find("player3");
            }
        }
        else
        {
            if (ReceiveName.Contains("1"))
            {
                target = GameObject.Find("enemy1");
            }
            else if (ReceiveName.Contains("2"))
            {
                target = GameObject.Find("enemy2");
            }
            else
            {
                target = GameObject.Find("enemy3");
            }
        }
        SaveReceiveList.Insert(attackID, target);
    }

    //  保存しておいた行動を呼び出す
    public void LoadSkill()
    {
        int skillID;
        if (playerProof)
        {
            if (Name.Contains("1"))
            {
                skillID = SaveSkillIDList[0];
                //ReceiveChara = SaveReceiveList[0];
                battleManager.GetComponent<BattleScene>().SetReceiveObj(SaveReceiveList[0]);
            }
            else if (Name.Contains("2"))
            {
                skillID = SaveSkillIDList[1];
                //ReceiveChara = SaveReceiveList[1];
                battleManager.GetComponent<BattleScene>().SetReceiveObj(SaveReceiveList[1]);
            }
            else
            {
                skillID = SaveSkillIDList[2];
                //ReceiveChara = SaveReceiveList[2];
                battleManager.GetComponent<BattleScene>().SetReceiveObj(SaveReceiveList[2]);
            }
        }
        else
        {
            if (Name.Contains("1"))
            {
                skillID = SaveSkillIDList[3];
                //ReceiveChara = SaveReceiveList[3];
                battleManager.GetComponent<BattleScene>().SetReceiveObj(SaveReceiveList[3]);
            }
            else if (Name.Contains("2"))
            {
                skillID = SaveSkillIDList[4];
                //ReceiveChara = SaveReceiveList[4];
                battleManager.GetComponent<BattleScene>().SetReceiveObj(SaveReceiveList[4]);
            }
            else
            {
                skillID = SaveSkillIDList[5];
                //ReceiveChara = SaveReceiveList[5];
                battleManager.GetComponent<BattleScene>().SetReceiveObj(SaveReceiveList[5]);
            }
        }

        SkillInfluence(skillID);

        spCost = (int)PS.sheets[0].list[skillID].sp;
    }

    //  スキルの効果の取得
    public void SkillInfluence(int skillID)
    {
        PC = Resources.Load("ExcelData/player_chara") as player_charaList;
        PS = Resources.Load("ExcelData/playerSkill") as player_skillList;

        GameObject AttackObj = battleManager.GetComponent<BattleScene>().GetAttackObj();
        GameObject ReceiveObj = battleManager.GetComponent<BattleScene>().GetReceiveObj();

        GameObject UseObj = AttackObj;

        battleManager.GetComponent<command>().SetActiveSkillText(PS.sheets[0].list[skillID].skillName);

        //  ステータスを弄るキャラ
        string charaStatus = PS.sheets[0].list[skillID].useChara;
        //  使用するステータス
        string useStatus = PS.sheets[0].list[skillID].influence1;
        string useStatus2 = PS.sheets[0].list[skillID].influence2;
        //  技の倍率
        double magnification = PS.sheets[0].list[skillID].power;
        //  HPに影響を与える技かどうか
        HPCtlFlag = (int)PS.sheets[0].list[skillID].hpCtl;
        //  物理攻撃か魔法攻撃か
        AttackType = (int)PS.sheets[0].list[skillID].attackType;
        HealPercent = 0;

        if (charaStatus == "Attack")
        {
            UseObj = AttackObj;
        }
        else if(charaStatus == "Receive")
        {
            UseObj = ReceiveObj;
        }
        else
        {
        }

        switch (useStatus)
        {
            case ("HP"):
                HealPercent = magnification;
                break;
            case ("ATK"):
                UseObj.GetComponent<Status>().SetATK((int)Math.Round(UseObj.GetComponent<Status>().GetATK() * magnification));
                break;
            case ("DEF"):
                UseObj.GetComponent<Status>().SetDEF((int)Math.Round(UseObj.GetComponent<Status>().GetDEF() * magnification));
                break;
            case ("SPD"):
                UseObj.GetComponent<Status>().SetSPD((int)Math.Round(UseObj.GetComponent<Status>().GetSPD() * magnification));
                break;
            case ("MAT"):
                UseObj.GetComponent<Status>().SetMAT((int)Math.Round(UseObj.GetComponent<Status>().GetMAT() * magnification));
                break;
            case ("MDF"):
                UseObj.GetComponent<Status>().SetMDF((int)Math.Round(UseObj.GetComponent<Status>().GetMDF() * magnification));
                break;
            case ("LUK"):
                UseObj.GetComponent<Status>().SetLUK((int)Math.Round(UseObj.GetComponent<Status>().GetLUK() * magnification));
                break;
            default:
                break;
        }

        switch (useStatus2)
        {
            case ("HP"):
                HealPercent = magnification;
                break;
            case ("ATK"):
                UseObj.GetComponent<Status>().SetATK((int)Math.Round(UseObj.GetComponent<Status>().GetATK() * magnification));
                break;
            case ("DEF"):
                UseObj.GetComponent<Status>().SetDEF((int)Math.Round(UseObj.GetComponent<Status>().GetDEF() * magnification));
                break;
            case ("SPD"):
                UseObj.GetComponent<Status>().SetSPD((int)Math.Round(UseObj.GetComponent<Status>().GetSPD() * magnification));
                break;
            case ("MAT"):
                UseObj.GetComponent<Status>().SetMAT((int)Math.Round(UseObj.GetComponent<Status>().GetMAT() * magnification));
                break;
            case ("MDF"):
                UseObj.GetComponent<Status>().SetMDF((int)Math.Round(UseObj.GetComponent<Status>().GetMDF() * magnification));
                break;
            case ("LUK"):
                UseObj.GetComponent<Status>().SetLUK((int)Math.Round(UseObj.GetComponent<Status>().GetLUK() * magnification));
                break;
            default:
                break;
        }
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

    public void SetSP(int sp)
    {
        this.SP = sp;
        if (SP <= 0)
        {
            SP = 0;
        }
    }

    public int GetSP()
    {
        return SP;
    }

    public void SetATK(int atk)
    {
        this.ATK = atk;
        if (ATK <= 0)
        {
            ATK = 1;
        }
    }

    public int GetATK()
    {
        return ATK;
    }

    public void SetDEF(int def)
    {
        this.DEF = def;
        if (DEF <= 0)
        {
            DEF = 1;
        }
    }

    public int GetDEF()
    {
        return DEF;
    }

    public void SetSPD(int spd)
    {
        this.SPD = spd;
        if (SPD <= 0)
        {
            SPD = 1;
        }
    }

    public int GetSPD()
    {
        return SPD;
    }

    public void SetMAT(int mat)
    {
        this.MAT = mat;
        if (MAT <= 0)
        {
            MAT = 1;
        }
    }

    public int GetMAT()
    {
        return MAT;
    }

    public void SetMDF(int mdf)
    {
        this.MDF = mdf;
        if (MDF <= 0)
        {
            MDF = 1;
        }
    }

    public int GetMDF()
    {
        return MDF;
    }

    public void SetLUK(int luk)
    {
        this.LUK = luk;
        if (LUK <= 0)
        {
           LUK = 1;
        }
    }

    public int GetLUK()
    {
        return LUK;
    }
}
