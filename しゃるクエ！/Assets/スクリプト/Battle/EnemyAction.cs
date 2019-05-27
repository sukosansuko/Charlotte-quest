using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyAction : MonoBehaviour
{
    //  敵用画像読み込み
    [SerializeField] private Sprite esp1;
    [SerializeField] private Sprite esp2;
    [SerializeField] private Sprite esp3;
    [SerializeField] private Sprite esp4;
    [SerializeField] private Sprite esp5;
    [SerializeField] private Sprite esp6;
    [SerializeField] private Sprite esp7;
    [SerializeField] private Sprite esp8;
    [SerializeField] private Sprite esp9;
    [SerializeField] private Sprite esp10;
    [SerializeField] private Sprite esp11;
    [SerializeField] private Sprite esp12;

    private int charID;

    enemy_charaList EC;
    enemy_skillList ES;

    private int skillID;
    private int skillCount;

    private GameObject battleManager;

    void Start()
    {
        battleManager = GameObject.Find("BattleManager");
    }

    void Update()
    {
        
    }

    public void SetCharID(int id)
    {
        charID = id;
    }

    public void CommandSelect(GameObject charaObj)
    {
        System.Random rnd = new System.Random();
        skillCount = 0;

        EC = Resources.Load("ExcelData/enemy_chara") as enemy_charaList;
        ES = Resources.Load("ExcelData/enemySkill") as enemy_skillList;

        //  スキルをいくつ持っているかカウントする
        if(EC.sheets[0].list[charID].Skill1 != 0)
        {
            skillCount++;
        }
        if (EC.sheets[0].list[charID].Skill2 != 0)
        {
            skillCount++;
        }
        if (EC.sheets[0].list[charID].Skill3 != 0)
        {
            skillCount++;
        }
        if (EC.sheets[0].list[charID].Skill4 != 0)
        {
            skillCount++;
        }
        //  持っているスキルの中からランダムでスキルを選ぶ
        int skillNumber = rnd.Next(1, skillCount + 1);

        switch(skillNumber)
        {
            case 1:
                skillID = EC.sheets[0].list[charID].Skill1;
                break;
            case 2:
                skillID = EC.sheets[0].list[charID].Skill2;
                break;
            case 3:
                skillID = EC.sheets[0].list[charID].Skill3;
                break;
            case 4:
                skillID = EC.sheets[0].list[charID].Skill4;
                break;
            default:
                break;
        }
        //  スキルを決定する
        charaObj.GetComponent<Status>().SaveSkill(skillID - 1);

        string target = ES.sheets[0].list[skillID].target;
        //  ランダムで標的を決める時用
        int targetID;
        GameObject receive;
        if (target.StartsWith("P"))
        {
            if (target.Contains("0"))
            {
                battleManager.GetComponent<Status>().SaveReceive(charaObj);
            }
            else if (target.Contains("1"))
            {
                targetID = rnd.Next(1, battleManager.GetComponent<command>().GetEnemyCount() + 1);
                receive = battleManager.GetComponent<command>().GetEnemyChild(targetID - 1);
                charaObj.GetComponent<Status>().SaveReceive(receive);
            }
            else
            {
            }
        }
        else
        {
            if (target.Contains("1"))
            {
                targetID = rnd.Next(1, battleManager.GetComponent<command>().GetPlayerCount() + 1);
                receive = battleManager.GetComponent<command>().GetPlayerChild(targetID - 1);
                charaObj.GetComponent<Status>().SaveReceive(receive);
            }
            else
            {
            }
        }
    }

    public void EnemyCharaChange(GameObject obj, int id)
    {
        switch (id)
        {
            case 1:
                obj.GetComponent<Image>().sprite = esp1;
                break;
            case 2:
                obj.GetComponent<Image>().sprite = esp2;
                break;
            case 3:
                obj.GetComponent<Image>().sprite = esp3;
                break;
            case 4:
                obj.GetComponent<Image>().sprite = esp4;
                break;
            case 5:
                obj.GetComponent<Image>().sprite = esp5;
                break;
            case 6:
                obj.GetComponent<Image>().sprite = esp6;
                break;
            case 7:
                obj.GetComponent<Image>().sprite = esp7;
                break;
            case 8:
                obj.GetComponent<Image>().sprite = esp8;
                break;
            case 9:
                obj.GetComponent<Image>().sprite = esp9;
                break;
            case 10:
                obj.GetComponent<Image>().sprite = esp10;
                break;
            case 11:
                obj.GetComponent<Image>().sprite = esp11;
                break;
            case 12:
                obj.GetComponent<Image>().sprite = esp12;
                break;
            default:
                break;
        }
    }
}
