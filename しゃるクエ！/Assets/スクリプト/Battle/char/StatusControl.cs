using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class StatusControl : MonoBehaviour
{
    struct statusData
    {
        public string CharName;    //  キャラクターの名前
        public int LV;             //  レベル
        public int HP;             //  体力
        public int SP;             //  技の発動に必要
        public int ATK;            //  物理攻撃力
        public int DEF;            //  物理防御力
        public int SPD;            //  素早さ
        public int MAT;            //  魔法攻撃力
        public int MDF;            //  魔法防御力
        public int LUK;            //  幸運値
        public int EXP;            //  経験値
    }

    private player_charaList PC;
    private playerGrow PG;
    private EXP_List EL;

    //  ステータス格納用
    statusData[] StatusList = new statusData[6];

    //  レベルアップ時の上昇ステータス格納用リスト
    statusData[] StatusGrowList = new statusData[6];

    void Start()
    {
        StatusInit();
    }

    void StatusInit()
    {
        PC = Resources.Load("ExcelData/player_chara") as player_charaList;
        PG = Resources.Load("ExcelData/playerGrow") as playerGrow;

        //  レベル1時の初期ステータスを取得
        for (int charID = 0; charID < 6; charID++)
        {
            StatusList[charID] = new statusData(){
                                                    CharName = PC.sheets[0].list[charID].Name, LV = 1,   HP = (int)PC.sheets[0].list[charID].HP,        SP = (int)PC.sheets[0].list[charID].SP,
                                                    ATK = (int)PC.sheets[0].list[charID].ATK,            DEF = (int)PC.sheets[0].list[charID].DEF,
                                                    SPD = (int)PC.sheets[0].list[charID].SPD,            MAT = (int)PC.sheets[0].list[charID].MAT,
                                                    MDF = (int)PC.sheets[0].list[charID].MDF,            LUK = (int)PC.sheets[0].list[charID].LUK,      EXP = 0
                                                 };

            StatusGrowList[charID] = new statusData(){
                                                    LV = 1,HP = (int)PG.sheets[0].list[charID].GROWHP,  SP = (int)PG.sheets[0].list[charID].GROWSP,
                                                    ATK = (int)PG.sheets[0].list[charID].GROWATK,       DEF = (int)PG.sheets[0].list[charID].GROWDEF,
                                                    SPD = (int)PG.sheets[0].list[charID].GROWSPD,       MAT = (int)PG.sheets[0].list[charID].GROWMAT,
                                                    MDF = (int)PG.sheets[0].list[charID].GROWMDF,       LUK = (int)PG.sheets[0].list[charID].GROWLUK, EXP = 0
                                                 };
        }

        //Debug.Log(StatusList[1].LV);
        //Debug.Log(StatusList[1].HP);
        //Debug.Log(StatusList[1].SP);
        //Debug.Log(StatusList[1].ATK);
        //Debug.Log(StatusList[1].DEF);
        //Debug.Log(StatusList[1].SPD);
        //Debug.Log(StatusList[1].MAT);
        //Debug.Log(StatusList[1].MDF);
        //Debug.Log(StatusList[1].LUK);
        //Debug.Log(StatusList[1].EXP);

        //Debug.Log("StatusGrow   " + StatusGrowList[1].LV);
        //Debug.Log(StatusGrowList[1].HP);
        //Debug.Log(StatusGrowList[1].SP);
        //Debug.Log(StatusGrowList[1].ATK);
        //Debug.Log(StatusGrowList[1].DEF);
        //Debug.Log(StatusGrowList[1].SPD);
        //Debug.Log(StatusGrowList[1].MAT);
        //Debug.Log(StatusGrowList[1].MDF);
        //Debug.Log(StatusGrowList[1].LUK);
        //Debug.Log(StatusGrowList[1].EXP);
    }

    void Update()
    {

    }

    //  外部にステータスを渡す時用
    public void SetStatus(int id,ref string charName,ref int lv, ref int hp, ref int sp, ref int atk, ref int def, ref int spd, ref int mat,ref int mdf, ref int luk)
    {
        charName = StatusList[id].CharName;
        lv = StatusList[id].LV;
        ////  レベルによってステータスを加算する
        hp = StatusList[id].HP + StatusGrowList[id].HP * (lv - 1);
        sp = StatusList[id].SP + StatusGrowList[id].SP * (lv - 1);
        atk = StatusList[id].ATK + StatusGrowList[id].ATK * (lv - 1);
        def = StatusList[id].DEF + StatusGrowList[id].DEF * (lv - 1);
        spd = StatusList[id].SPD + StatusGrowList[id].SPD * (lv - 1);
        mat = StatusList[id].MAT + StatusGrowList[id].MAT * (lv - 1);
        mdf = StatusList[id].MDF + StatusGrowList[id].MDF * (lv - 1);
        luk = StatusList[id].LUK + StatusGrowList[id].LUK * (lv - 1);
    }

    public void GetLvExp(int id,int exp)
    {
        exp = StatusList[id].EXP;
    }

    //  経験値の獲得&レベルアップ処理
    private bool SetExp(int id,int exp)
    {
        bool LevelUpFlag = false;
        StatusList[id].EXP += exp;

        //  レベルアップするために必要な総経験値
        int levelUpExp = EL.sheets[0].list[StatusList[id].LV - 1].TotalExp;

        while (StatusList[id].EXP >= levelUpExp)
        {
            StatusList[id].LV++;
            levelUpExp = EL.sheets[0].list[StatusList[id].LV - 1].TotalExp;
            
            //  レベルアップしたらtrueを返してレベルアップしたことを伝える
            LevelUpFlag = true;
        }
        return LevelUpFlag;
    }
}
