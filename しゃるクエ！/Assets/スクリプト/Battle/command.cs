using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class command : MonoBehaviour
{
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

    player_charaList PC;
    player_skillList PS;

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    private void Init()
    {
        Attack.GetComponent<Image>().enabled = true;
        Support.GetComponent<Image>().enabled = true;
        Item.GetComponent<Image>().enabled = true;

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
    }

    private void getAttackList()
    {
        PC = Resources.Load("ExcelData/player_chara") as player_charaList;
        PS = Resources.Load("ExcelData/playerSkill") as player_skillList;
        var IDList = new List<int>();
        IDList.Add((int)PC.sheets[0].list[0].AttackSkill1);
        IDList.Add((int)PC.sheets[0].list[0].AttackSkill2);
        IDList.Add((int)PC.sheets[0].list[0].AttackSkill3);
        IDList.Add((int)PC.sheets[0].list[0].AttackSkill4);
        IDList.Add((int)PC.sheets[0].list[0].AttackSkill5);
        IDList.Add((int)PC.sheets[0].list[0].AttackSkill6);
        IDList.Add((int)PC.sheets[0].list[0].AttackSkill7);
        IDList.Add((int)PC.sheets[0].list[0].AttackSkill8);
        for (int count = 0; count < IDList.Count - 1; count++)
        {
            if (IDList[count] != 0)
            {
                attackText.GetChild(count).gameObject.SetActive(true);
                attackText.GetChild(count).gameObject.GetComponent<Text>().text = PS.sheets[0].list[IDList[count] - 1].skillName;
            }
        }
    }

    private void getSupportList()
    {
        PC = Resources.Load("ExcelData/player_chara") as player_charaList;
        PS = Resources.Load("ExcelData/playerSkill") as player_skillList;
        var IDList = new List<int>();
        IDList.Add((int)PC.sheets[0].list[0].SupportSkill1);
        IDList.Add((int)PC.sheets[0].list[0].SupportSkill2);
        IDList.Add((int)PC.sheets[0].list[0].SupportSkill3);
        IDList.Add((int)PC.sheets[0].list[0].SupportSkill4);
        IDList.Add((int)PC.sheets[0].list[0].SupportSkill5);
        IDList.Add((int)PC.sheets[0].list[0].SupportSkill6);
        IDList.Add((int)PC.sheets[0].list[0].SupportSkill7);
        IDList.Add((int)PC.sheets[0].list[0].SupportSkill8);
        for (int count = 0; count < IDList.Count - 1; count++)
        {
            if (IDList[count] != 0)
            {
                supportText.GetChild(count).gameObject.SetActive(true);
                supportText.GetChild(count).gameObject.GetComponent<Text>().text = PS.sheets[0].list[IDList[count] - 1].skillName;
            }
        }
    }

    private void getItemList()
    {
        //PC = Resources.Load("ExcelData/player_chara") as player_charaList;
        //PS = Resources.Load("ExcelData/playerSkill") as player_skillList;
        //var IDList = new List<int>();
        //IDList.Add((int)PC.sheets[0].list[0].AttackSkill1);
        //IDList.Add((int)PC.sheets[0].list[0].AttackSkill2);
        //IDList.Add((int)PC.sheets[0].list[0].AttackSkill3);
        //IDList.Add((int)PC.sheets[0].list[0].AttackSkill4);
        //IDList.Add((int)PC.sheets[0].list[0].AttackSkill5);
        //IDList.Add((int)PC.sheets[0].list[0].AttackSkill6);
        //IDList.Add((int)PC.sheets[0].list[0].AttackSkill7);
        //IDList.Add((int)PC.sheets[0].list[0].AttackSkill8);
        //for (int count = 0; count < IDList.Count - 1; count++)
        //{
        //    if (IDList[count] != 0)
        //    {
        //        itemText.GetChild(count).gameObject.SetActive(true);
        //        itemText.GetChild(count).gameObject.GetComponent<Text>().text = PS.sheets[0].list[IDList[count] - 1].skillName;
        //    }
        //}
    }

    public void BackInput()
    {
        Attack.GetComponent<Image>().enabled = true;
        Support.GetComponent<Image>().enabled = true;
        Item.GetComponent<Image>().enabled = true;

        Description.GetComponent<Image>().enabled = false;
        DescriptionText.GetComponent<Text>().enabled = false;
        SkillList.GetComponent<Image>().enabled = false;
        Back.GetComponent<Image>().enabled = false;
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
    }

    public void SkillDescription()
    {
        Description.GetComponent<Image>().enabled = false;
        DescriptionText.GetComponent<Text>().enabled = false;

        Description.GetComponent<Image>().enabled = true;
        DescriptionText.GetComponent<Text>().enabled = true;
    }
}
