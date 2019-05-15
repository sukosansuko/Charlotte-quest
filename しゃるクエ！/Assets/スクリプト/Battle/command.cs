﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using System;

public class command : MonoBehaviour
{

    public enum TYPE
    {
        TYPE_NON,
        TYPE_ATTACK,            //  攻撃
        TYPE_SUPPORT,           //  サポート
        TYPE_ITEM,              //  アイテム
        TYPE_MAX
    }

    public GameObject Attack;
    public GameObject Support;
    public GameObject Item;
    public GameObject SkillList;
    public GameObject Back;
    public GameObject Description;
    public GameObject DescriptionText;

    public Transform attackText;
    public Transform supportText;
    public Transform itemText;

    public Transform playerTarget;
    public Transform enemyTarget;

    public Transform player;
    public Transform enemy;

    private int charID;
    private int target;
    private TYPE skillType;
    private int SkillName;

    player_charaList PC;
    player_skillList PS;

    private GameObject TextTarget;
    private GameObject battleManager;

    void Start()
    {
        battleManager = GameObject.Find("BattleManager");
        Init();
    }

    void Update()
    {
        
    }

    private void Init()
    {
        Attack.GetComponent<Image>().enabled = false;
        Support.GetComponent<Image>().enabled = false;
        Item.GetComponent<Image>().enabled = false;

        SkillList.GetComponent<Image>().enabled = false;
        Back.GetComponent<Image>().enabled = false;
        Description.GetComponent<Image>().enabled = false;
        DescriptionText.GetComponent<Text>().enabled = false;

        foreach (Transform child in attackText)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in supportText)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in itemText)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in playerTarget)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in enemyTarget)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in player)
        {
            child.gameObject.GetComponent<Status>().SetRayCast(false);
        }
        foreach (Transform child in enemy)
        {
            child.gameObject.GetComponent<Status>().SetRayCast(false);
        }

    }

    //  行動選択時に表示
    public void commandDisplay()
    {
        Attack.GetComponent<Image>().enabled = false;
        Support.GetComponent<Image>().enabled = false;
        Item.GetComponent<Image>().enabled = false;

        Attack.GetComponent<Image>().enabled = true;
        Support.GetComponent<Image>().enabled = true;
        Item.GetComponent<Image>().enabled = true;
    }

    //  攻撃テキストの表示
    public void SetAttackText()
    {
        Attack.GetComponent<Image>().enabled = false;
        Support.GetComponent<Image>().enabled = false;
        Item.GetComponent<Image>().enabled = false;

        SkillList.GetComponent<Image>().enabled = true;
        Back.GetComponent<Image>().enabled = true;

        getAttackList();
        skillType = TYPE.TYPE_ATTACK;
    }

    //  支援テキストの表示
    public void SetSupportText()
    {
        Attack.GetComponent<Image>().enabled = false;
        Support.GetComponent<Image>().enabled = false;
        Item.GetComponent<Image>().enabled = false;

        SkillList.GetComponent<Image>().enabled = true;
        Back.GetComponent<Image>().enabled = true;

        getSupportList();
        skillType = TYPE.TYPE_SUPPORT;
    }

    //   アイテムテキストの表示
    public void SetItemText()
    {
        Attack.GetComponent<Image>().enabled = false;
        Support.GetComponent<Image>().enabled = false;
        Item.GetComponent<Image>().enabled = false;

        SkillList.GetComponent<Image>().enabled = true;
        Back.GetComponent<Image>().enabled = true;

        getItemList();
        skillType = TYPE.TYPE_ITEM;
    }

    //  攻撃テキストの読み込み
    private void getAttackList()
    {
        PC = Resources.Load("ExcelData/player_chara") as player_charaList;
        PS = Resources.Load("ExcelData/playerSkill") as player_skillList;
        var IDList = new List<int>();
        IDList.Add((int)PC.sheets[0].list[charID].AttackSkill1);
        IDList.Add((int)PC.sheets[0].list[charID].AttackSkill2);
        IDList.Add((int)PC.sheets[0].list[charID].AttackSkill3);
        IDList.Add((int)PC.sheets[0].list[charID].AttackSkill4);
        IDList.Add((int)PC.sheets[0].list[charID].AttackSkill5);
        IDList.Add((int)PC.sheets[0].list[charID].AttackSkill6);
        IDList.Add((int)PC.sheets[0].list[charID].AttackSkill7);
        IDList.Add((int)PC.sheets[0].list[charID].AttackSkill8);
        for (int count = 0; count < IDList.Count - 1; count++)
        {
            if (IDList[count] != 0)
            {
                attackText.GetChild(count).gameObject.SetActive(true);
                attackText.GetChild(count).gameObject.GetComponent<Text>().text = PS.sheets[0].list[IDList[count] - 1].skillName;
            }
        }
    }

    //  支援テキストの読み込み
    private void getSupportList()
    {
        PC = Resources.Load("ExcelData/player_chara") as player_charaList;
        PS = Resources.Load("ExcelData/playerSkill") as player_skillList;
        var IDList = new List<int>();
        IDList.Add((int)PC.sheets[0].list[charID].SupportSkill1);
        IDList.Add((int)PC.sheets[0].list[charID].SupportSkill2);
        IDList.Add((int)PC.sheets[0].list[charID].SupportSkill3);
        IDList.Add((int)PC.sheets[0].list[charID].SupportSkill4);
        IDList.Add((int)PC.sheets[0].list[charID].SupportSkill5);
        IDList.Add((int)PC.sheets[0].list[charID].SupportSkill6);
        IDList.Add((int)PC.sheets[0].list[charID].SupportSkill7);
        IDList.Add((int)PC.sheets[0].list[charID].SupportSkill8);
        for (int count = 0; count < IDList.Count - 1; count++)
        {
            if (IDList[count] != 0)
            {
                supportText.GetChild(count).gameObject.SetActive(true);
                supportText.GetChild(count).gameObject.GetComponent<Text>().text = PS.sheets[0].list[IDList[count] - 1].skillName;
            }
        }
    }

    //  アイテムテキストの読み込み
    private void getItemList()
    {
        //PC = Resources.Load("ExcelData/player_chara") as player_charaList;
        //PS = Resources.Load("ExcelData/playerSkill") as player_skillList;
        //var IDList = new List<int>();
        //IDList.Add((int)PC.sheets[0].list[charID].AttackSkill1);
        //IDList.Add((int)PC.sheets[0].list[charID].AttackSkill2);
        //IDList.Add((int)PC.sheets[0].list[charID].AttackSkill3);
        //IDList.Add((int)PC.sheets[0].list[charID].AttackSkill4);
        //IDList.Add((int)PC.sheets[0].list[charID].AttackSkill5);
        //IDList.Add((int)PC.sheets[0].list[charID].AttackSkill6);
        //IDList.Add((int)PC.sheets[0].list[charID].AttackSkill7);
        //IDList.Add((int)PC.sheets[0].list[charID].AttackSkill8);
        //for (int count = 0; count < IDList.Count - 1; count++)
        //{
        //    if (IDList[count] != 0)
        //    {
        //        itemText.GetChild(count).gameObject.SetActive(true);
        //        itemText.GetChild(count).gameObject.GetComponent<Text>().text = PS.sheets[0].list[IDList[count] - 1].skillName;
        //    }
        //}
    }

    //  戻るボタンを押したときの処理
    public void BackInput()
    {
        Init();
        Attack.GetComponent<Image>().enabled = true;
        Support.GetComponent<Image>().enabled = true;
        Item.GetComponent<Image>().enabled = true;

        skillType = TYPE.TYPE_NON;
    }

    //  技の説明&ターゲットの表示
    public void SkillDescription()
    {
        //  技の説明表示
        Description.GetComponent<Image>().enabled = false;
        DescriptionText.GetComponent<Text>().enabled = false;
        foreach (Transform child in playerTarget)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in enemyTarget)
        {
            child.gameObject.SetActive(false);
        }

        Description.GetComponent<Image>().enabled = true;
        DescriptionText.GetComponent<Text>().enabled = true;

        int skillNumber = 0;

        //  技ごとにターゲット表示
        PC = Resources.Load("ExcelData/player_chara") as player_charaList;
        PS = Resources.Load("ExcelData/playerSkill") as player_skillList;
        if (skillType == TYPE.TYPE_ATTACK)
        {
            switch(SkillName)
            {
                case 1:
                    skillNumber = (int)PC.sheets[0].list[charID].AttackSkill1;
                    break;
                case 2:
                    skillNumber = (int)PC.sheets[0].list[charID].AttackSkill2;
                    break;
                case 3:
                    skillNumber = (int)PC.sheets[0].list[charID].AttackSkill3;
                    break;
                case 4:
                    skillNumber = (int)PC.sheets[0].list[charID].AttackSkill4;
                    break;
                case 5:
                    skillNumber = (int)PC.sheets[0].list[charID].AttackSkill5;
                    break;
                case 6:
                    skillNumber = (int)PC.sheets[0].list[charID].AttackSkill6;
                    break;
                case 7:
                    skillNumber = (int)PC.sheets[0].list[charID].AttackSkill7;
                    break;
                case 8:
                    skillNumber = (int)PC.sheets[0].list[charID].AttackSkill8;
                    break;
                default:
                    break;
            }
        }
        else if (skillType == TYPE.TYPE_SUPPORT)
        {
            switch(SkillName)
            {
                case 1:
                    skillNumber = (int)PC.sheets[0].list[charID].SupportSkill1;
                    break;
                case 2:
                    skillNumber = (int)PC.sheets[0].list[charID].SupportSkill2;
                    break;
                case 3:
                    skillNumber = (int)PC.sheets[0].list[charID].SupportSkill3;
                    break;
                case 4:
                    skillNumber = (int)PC.sheets[0].list[charID].SupportSkill4;
                    break;
                case 5:
                    skillNumber = (int)PC.sheets[0].list[charID].SupportSkill5;
                    break;
                case 6:
                    skillNumber = (int)PC.sheets[0].list[charID].SupportSkill6;
                    break;
                case 7:
                    skillNumber = (int)PC.sheets[0].list[charID].SupportSkill7;
                    break;
                case 8:
                    skillNumber = (int)PC.sheets[0].list[charID].SupportSkill8;
                    break;
                default:
                    break;
            }
        }
        else
        {

        }

        //  playerSkillのtargetから、ターゲットを取得
        string Name = PS.sheets[0].list[skillNumber - 1].target;
        int ActivePlayer = GetComponent<BattleScene>().GetActivePlayer() - 1;

        //  味方に対する技
        if (Name.StartsWith("P"))
        {
            if (Name.Contains("0"))
            {
                playerTarget.GetChild(ActivePlayer).gameObject.SetActive(true);
            }
            else if(Name.Contains("1"))
            {
                playerTarget.GetChild(target).gameObject.SetActive(true);
                foreach (Transform child in player)
                {
                    child.gameObject.GetComponent<Status>().SetRayCast(true);
                }
            }
            else
            {
                foreach (Transform child in playerTarget)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        //  敵に対する技  
        else
        {
            if (Name.Contains("1"))
            {
                enemyTarget.GetChild(target).gameObject.SetActive(true);
                foreach (Transform child in enemy)
                {
                    child.gameObject.GetComponent<Status>().SetRayCast(true);
                }
            }
            else
            {
                foreach (Transform child in enemyTarget)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        DescriptionText.GetComponent<Text>().text = PS.sheets[0].list[skillNumber - 1].effect;
    }

    public void SetSkillName(string name)
    {
        //  文字列中の数字を取得
        SkillName = int.Parse(Regex.Replace(name, @"[^0-9]", ""));
    }

    public void SetTarget(string id)
    {
        target = int.Parse(Regex.Replace(id, @"[^0-9]", "")) - 1;
    }

    public void SetCharID(int id)
    {
        charID = id;
    }

    public void InitTarget()
    {
        target = 0;
    }

    //  ターゲット確定時の処理
    public void TargetDecided()
    {
        Init();
        battleManager.GetComponent<BattleScene>().SetActiveChoose(false);
    }
}
