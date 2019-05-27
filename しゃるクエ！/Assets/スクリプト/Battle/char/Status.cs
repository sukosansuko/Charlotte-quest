﻿using System.Collections;
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

    //  プレイヤー用画像読み込み
    [SerializeField] private Sprite psp1;
    [SerializeField] private Sprite psp2;
    [SerializeField] private Sprite psp3;
    [SerializeField] private Sprite psp4;
    [SerializeField] private Sprite psp5;
    [SerializeField] private Sprite psp6;


    private int TURN;                           //  行動回数
    private string AResistance;                 //  物理耐性(基本は敵のみ)
    private string MResistance;                 //  魔法耐性(基本は敵のみ)
    private string weakAttribute;               //  弱点属性(基本は敵のみ)
    private int MAX_HP;                         //  HPの最大値
    private int MAX_SP;                         //  SPの最大値

    public GameObject MyTarget;
    public GameObject TLIcon;
    public GameObject turnPointer;
    public GameObject HPDisplay;

    player_charaList PC;
    player_skillList PS;

    enemy_charaList EC;
    enemy_skillList ES;

    private List<int> SaveSkillIDList = new List<int>();
    private List<GameObject> SaveReceiveList = new List<GameObject>();

    //  TLのアイコン位置計算用
    private Vector3 tmp;

    public enum STATE
    {
        ST_NON,
        ST_ALIVE,                   //  生きている
        ST_DEAD,                    //  死亡
        ST_MAX
    }

    //  バフ、デバフの管理用
    struct strData
    {
        public int TIME;            //  バフ、デバフが有効なターン数
        public int ATK;             //  物理攻撃力
        public int DEF;             //  物理防御力
        public int SPD;             //  素早さ
        public int MAT;             //  魔法攻撃力
        public int MDF;             //  魔法防御力
        public int LUK;             //  幸運値
    }

    //  バフ、デバフ管理用のリスト
    private List<strData> changeStatus = new List<strData>();

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
        progressEnd = false;
        SetChara();
    }

    void Update()
    {
        if (state == STATE.ST_DEAD)
        {
            //battleManager.GetComponent<command>().SetTargetStart(Name);
            Destroy(TLIcon);
            Destroy(MyTarget);
            if(!playerProof)
            {
                Destroy(HPDisplay);
            }
            Destroy(this.gameObject);
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
                    //ProgressSPD = 0;
                    ProgressSPD = 1;
                }
                TLProgress += ProgressSPD;
            }

            //  行動選択開始
            if (TLProgress >= 200 && !progressEnd)
            {
                TLProgress = 200;

                if (playerProof)
                {
                    battleManager.GetComponent<command>().SetCharID(charID - 1);
                    battleManager.GetComponent<BattleScene>().SetActiveChoose(true);
                    battleManager.GetComponent<command>().commandDisplay();
                    //  ポインターの表示
                    turnPointer.GetComponent<Image>().enabled = true;
                }
                else
                {
                    battleManager.GetComponent<EnemyAction>().SetCharID(charID - 1);
                    battleManager.GetComponent<EnemyAction>().CommandSelect(this.gameObject);
                }
                battleManager.GetComponent<BattleScene>().SetActivePlayer(this.gameObject);
                progressEnd = true;
            }

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

                    ReceiveChara = battleManager.GetComponent<BattleScene>().GetReceiveObj();
                    if (ReceiveChara.GetComponent<Status>().GetHP() <= 0)
                    {
                        ReceiveChara.GetComponent<Status>().Dead();
                    }

                    TLProgress = 0;
                    actionFlag = false;
                    progressEnd = false;
                    battleManager.GetComponent<BattleScene>().SetActionFlag(false);
                    testTime = 0;
                    CheckBuff();
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
                TLProgress = 40;
            }
            else if (Name.Contains("2"))
            {
                charID = battleManager.GetComponent<BattleScene>().GetPID(2);
                TLIcon.GetComponent<Image>().enabled = true;
                TLProgress = 20;
            }
            else
            {
                charID = battleManager.GetComponent<BattleScene>().GetPID(3);
                TLIcon.GetComponent<Image>().enabled = true;
                TLProgress = 0;
            }
            sceneNavigator.GetComponent<StatusControl>().SetStatus(charID - 1,ref CharName,ref LV,ref HP,ref SP,ref ATK,ref DEF,ref SPD,ref MAT,ref MDF,ref LUK);
            MAX_HP = HP;
            MAX_SP = SP;
            TURN = 0;

            switch (charID)
            {
                case 1:
                    GetComponent<Image>().sprite = psp1;
                    break;
                case 2:
                    GetComponent<Image>().sprite = psp2;
                    break;
                case 3:
                    GetComponent<Image>().sprite = psp3;
                    break;
                case 4:
                    GetComponent<Image>().sprite = psp4;
                    break;
                case 5:
                    GetComponent<Image>().sprite = psp5;
                    break;
                case 6:
                    GetComponent<Image>().sprite = psp6;
                    break;
                default:
                    break;
            }

        }
        else
        {
            if (Name.Contains("1"))
            {
                charID = battleManager.GetComponent<BattleScene>().GetEID(1);
                TLIcon.GetComponent<Image>().enabled = true;
                HPDisplay = GameObject.Find("enemyHP1");
                HPDisplay.GetComponent<Text>().enabled = true;
                TLProgress = 40;
            }
            else if (Name.Contains("2"))
            {
                charID = battleManager.GetComponent<BattleScene>().GetEID(2);
                TLIcon.GetComponent<Image>().enabled = true;
                HPDisplay = GameObject.Find("enemyHP2");
                HPDisplay.GetComponent<Text>().enabled = true;
                TLProgress = 20;
            }
            else
            {
                charID = battleManager.GetComponent<BattleScene>().GetEID(3);
                TLIcon.GetComponent<Image>().enabled = true;
                HPDisplay = GameObject.Find("enemyHP3");
                HPDisplay.GetComponent<Text>().enabled = true;
                TLProgress = 0;
            }
            SetEnemyStatus(charID - 1,ref CharName, ref LV, ref HP, ref SP, ref ATK, ref DEF, ref SPD, ref MAT, ref MDF, ref LUK);
            MAX_HP = HP;
            MAX_SP = SP;
            TURN = 0;

            battleManager.GetComponent<EnemyAction>().EnemyCharaChange(this.gameObject,charID);
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
                ReceiveChara = SaveReceiveList[0];
            }
            else if (Name.Contains("2"))
            {
                skillID = SaveSkillIDList[1];
                ReceiveChara = SaveReceiveList[1];
            }
            else
            {
                skillID = SaveSkillIDList[2];
                ReceiveChara = SaveReceiveList[2];
            }
        }
        else
        {
            if (Name.Contains("1"))
            {
                skillID = SaveSkillIDList[3];
                ReceiveChara = SaveReceiveList[3];
            }
            else if (Name.Contains("2"))
            {
                skillID = SaveSkillIDList[4];
                ReceiveChara = SaveReceiveList[4];
            }
            else
            {
                skillID = SaveSkillIDList[5];
                ReceiveChara = SaveReceiveList[5];
            }
        }

        battleManager.GetComponent<BattleScene>().SetReceiveObj(ReceiveChara);
        SkillInfluence(skillID);

        spCost = (int)PS.sheets[0].list[skillID].sp;
    }

    //  スキルの効果の取得
    public void SkillInfluence(int skillID)
    {
        PC = Resources.Load("ExcelData/player_chara") as player_charaList;
        PS = Resources.Load("ExcelData/playerSkill") as player_skillList;

        EC = Resources.Load("ExcelData/enemy_chara") as enemy_charaList;
        ES = Resources.Load("ExcelData/enemySkill") as enemy_skillList;

        GameObject AttackObj = battleManager.GetComponent<BattleScene>().GetAttackObj();

        //  前の行動で標的が死亡してたら標的を変える
        if(battleManager.GetComponent<BattleScene>().GetReceiveObj() == null)
        {
            if (playerProof)
            {
                battleManager.GetComponent<command>().changeTarget(playerProof,PS.sheets[0].list[skillID].target);
            }
            else
            {
                battleManager.GetComponent<command>().changeTarget(playerProof,ES.sheets[0].list[skillID].target);
            }
        }
        GameObject ReceiveObj = battleManager.GetComponent<BattleScene>().GetReceiveObj();

        GameObject UseObj = AttackObj;

        if(playerProof)
        {
            battleManager.GetComponent<command>().SetActiveSkillText(PS.sheets[0].list[skillID].skillName);
        }
        else
        {
            battleManager.GetComponent<command>().SetActiveSkillText(ES.sheets[0].list[skillID].skillName);
        }

        string charaStatus;
        string useStatus;
        string useStatus2;
        double magnification;
        int STime;

        //  ステータスを弄るキャラ
        charaStatus = PS.sheets[0].list[skillID].useChara;
        //  使用するステータス
        useStatus = PS.sheets[0].list[skillID].influence1;
        useStatus2 = PS.sheets[0].list[skillID].influence2;
        //  技の倍率
        magnification = PS.sheets[0].list[skillID].power;
        //  HPに影響を与える技かどうか
        HPCtlFlag = (int)PS.sheets[0].list[skillID].hpCtl;
        //  物理攻撃か魔法攻撃か
        AttackType = (int)PS.sheets[0].list[skillID].attackType;
        //  持続時間
        STime = (int)PS.sheets[0].list[skillID].period;

        //  行動するのが敵の場合は敵のステータスに変更
        if (!playerProof)
        {
            charaStatus = ES.sheets[0].list[skillID].useChara;
            useStatus = ES.sheets[0].list[skillID].influence1;
            useStatus2 = ES.sheets[0].list[skillID].influence2;
            magnification = ES.sheets[0].list[skillID].power;
            HPCtlFlag = (int)ES.sheets[0].list[skillID].hpCtl;
            AttackType = (int)ES.sheets[0].list[skillID].attackType;
            STime = (int)ES.sheets[0].list[skillID].period;
        }

        HealPercent = 0;

        DeleteBuff(AttackObj);
        DeleteBuff(ReceiveObj);

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

        strData str1 = new strData();
        switch (useStatus)
        {
            case ("HP"):
                HealPercent = magnification;
                break;
            case ("ATK"):
                str1.ATK = (int)Math.Round(UseObj.GetComponent<Status>().GetATK() * magnification);
                str1.TIME = STime + TURN;
                UseObj.GetComponent<Status>().changeStatus.Add(str1);
                break;
            case ("DEF"):
                str1.DEF = (int)Math.Round(UseObj.GetComponent<Status>().GetDEF() * magnification);
                str1.TIME = STime + TURN;
                UseObj.GetComponent<Status>().changeStatus.Add(str1);
                break;
            case ("SPD"):
                str1.SPD = (int)Math.Round(UseObj.GetComponent<Status>().GetSPD() * magnification);
                str1.TIME = STime + TURN;
                UseObj.GetComponent<Status>().changeStatus.Add(str1);
                break;
            case ("MAT"):
                str1.MAT = (int)Math.Round(UseObj.GetComponent<Status>().GetMAT() * magnification);
                str1.TIME = STime + TURN;
                UseObj.GetComponent<Status>().changeStatus.Add(str1);
                break;
            case ("MDF"):
                str1.MDF = (int)Math.Round(UseObj.GetComponent<Status>().GetMDF() * magnification);
                str1.TIME = STime + TURN;
                UseObj.GetComponent<Status>().changeStatus.Add(str1);
                break;
            case ("LUK"):
                str1.LUK= (int)Math.Round(UseObj.GetComponent<Status>().GetLUK() * magnification);
                str1.TIME = STime + TURN;
                UseObj.GetComponent<Status>().changeStatus.Add(str1);
                break;
            default:
                break;
        }

        strData str2 = new strData();
        switch (useStatus2)
        {
            case ("HP"):
                HealPercent = magnification;
                break;
            case ("ATK"):
                str2.ATK = (int)Math.Round(UseObj.GetComponent<Status>().GetATK() * magnification);
                str2.TIME = STime + TURN;
                UseObj.GetComponent<Status>().changeStatus.Add(str2);
                break;
            case ("DEF"):
                str2.DEF = (int)Math.Round(UseObj.GetComponent<Status>().GetDEF() * magnification);
                str2.TIME = STime + TURN;
                UseObj.GetComponent<Status>().changeStatus.Add(str2);
                break;
            case ("SPD"):
                str2.SPD = (int)Math.Round(UseObj.GetComponent<Status>().GetSPD() * magnification);
                str2.TIME = STime + TURN;
                UseObj.GetComponent<Status>().changeStatus.Add(str2);
                break;
            case ("MAT"):
                str2.MAT = (int)Math.Round(UseObj.GetComponent<Status>().GetMAT() * magnification);
                str2.TIME = STime + TURN;
                UseObj.GetComponent<Status>().changeStatus.Add(str2);
                break;
            case ("MDF"):
                str2.MDF = (int)Math.Round(UseObj.GetComponent<Status>().GetMDF() * magnification);
                str2.TIME = STime + TURN;
                UseObj.GetComponent<Status>().changeStatus.Add(str2);
                break;
            case ("LUK"):
                str2.LUK = (int)Math.Round(UseObj.GetComponent<Status>().GetLUK() * magnification);
                str2.TIME = STime + TURN;
                UseObj.GetComponent<Status>().changeStatus.Add(str2);
                break;
            default:
                break;
        }
    }

    //  バフ、デバフを反映させる
    public void SetBuff()
    {
        if (changeStatus != null)
        {
            for (int count = 0; count < changeStatus.Count; count++)
            {
                    SetATK(GetATK() + changeStatus[count].ATK);
                    SetDEF(GetDEF() + changeStatus[count].DEF);
                    SetSPD(GetSPD() + changeStatus[count].SPD);
                    SetMAT(GetMAT() + changeStatus[count].MAT);
                    SetMDF(GetMDF() + changeStatus[count].MDF);
                    SetLUK(GetLUK() + changeStatus[count].LUK);
            }
        }
    }

    //  バフ、デバフを解除する
    public void DeleteBuff(GameObject objchara)
    {
        if (objchara.GetComponent<Status>().changeStatus != null)
        {
            for (int count = 0; count < objchara.GetComponent<Status>().changeStatus.Count; count++)
            {
                objchara.GetComponent<Status>().SetATK(objchara.GetComponent<Status>().GetATK() - objchara.GetComponent<Status>().changeStatus[count].ATK);
                objchara.GetComponent<Status>().SetDEF(objchara.GetComponent<Status>().GetDEF() - objchara.GetComponent<Status>().changeStatus[count].DEF);
                objchara.GetComponent<Status>().SetSPD(objchara.GetComponent<Status>().GetSPD() - objchara.GetComponent<Status>().changeStatus[count].SPD);
                objchara.GetComponent<Status>().SetMAT(objchara.GetComponent<Status>().GetMAT() - objchara.GetComponent<Status>().changeStatus[count].MAT);
                objchara.GetComponent<Status>().SetMDF(objchara.GetComponent<Status>().GetMDF() - objchara.GetComponent<Status>().changeStatus[count].MDF);
                objchara.GetComponent<Status>().SetLUK(objchara.GetComponent<Status>().GetLUK() - objchara.GetComponent<Status>().changeStatus[count].LUK);
            }
        }
    }


    //  ターン終了時にバフ、デバフの効果時間が終わっていないか確認する
    public void CheckBuff()
    {
        this.TURN++;

        if (changeStatus != null)
        {
            //  バフ、デバフがかかっている数だけループ
            for (int count = 0; count < changeStatus.Count; count++)
            {
                //  もし効果時間が終わっていたらバフ、デバフを解除する
                if(changeStatus[count].TIME <= this.TURN)
                {
                    SetATK(GetATK() - changeStatus[count].ATK);
                    SetDEF(GetDEF() - changeStatus[count].DEF);
                    SetSPD(GetSPD() - changeStatus[count].SPD);
                    SetMAT(GetMAT() - changeStatus[count].MAT);
                    SetMDF(GetMDF() - changeStatus[count].MDF);
                    SetLUK(GetLUK() - changeStatus[count].LUK);

                    changeStatus.RemoveAt(count);
                }

                if(changeStatus == null)
                {
                    break;
                }
            }
        }
    }

    public void SetHP(int hp)
    {
        this.HP = hp;
        if(HP >= MAX_HP)
        {
            HP = MAX_HP;
        }
        if(HP <= 0)
        {
            HP = 0;
        }
    }

    public int GetHP()
    {
        return HP;
    }

    public void SetSP(int sp)
    {
        this.SP = sp;
        if (SP >= MAX_SP)
        {
            SP = MAX_SP;
        }
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

    public int GetTurn()
    {
        return TURN;
    }
}
