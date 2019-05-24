using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BattleScene : MonoBehaviour
{
    Queue<Action> battleQueue;
    [SerializeField] private GameObject attackObj;
    [SerializeField] private GameObject receiveObj;

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;


    [SerializeField] private int pID1;
    [SerializeField] private int pID2;
    [SerializeField] private int pID3;
    [SerializeField] private int eID1;
    [SerializeField] private int eID2;
    [SerializeField] private int eID3;

    private command co;

    //  コマンド選択開始用フラグ
    private bool ActiveChoose;

    //  行動中かどうかのフラグ
    private bool ActionFlag;

    public GameObject ActivePlayer;

    public Transform TL;

    public struct Action
    {
        //public Performance p;
        public Damage damage;
        public Heal heal;
    }

    void Start()
    {
        Player1 = GameObject.Find("player1");
        Player2 = GameObject.Find("player2");
        Player3 = GameObject.Find("player3");
        Enemy1 = GameObject.Find("enemy1");
        Enemy2 = GameObject.Find("enemy2");
        Enemy3 = GameObject.Find("enemy3");

        battleQueue = new Queue<Action>();
        foreach (Transform child in TL)
        {
            child.gameObject.SetActive(true);
        }
    }

    void Update()
    {

    }

    public void TakeAction(int spcost, int HpFlag, int AttackType, double healPercent)
    {
        //  1ならダメージを与える処理
        if (HpFlag == 1)
        {
            Action action1 = new Action()
            {
                damage = new Damage { attackChara = attackObj, receiveChara = receiveObj, spCost = spcost, attackType = AttackType}
            };
            battleQueue.Enqueue(action1);
        }
        //  2ならHPを回復する処理
        else if(HpFlag == 2)
        {
            Action action1 = new Action()
            {
                heal = new Heal { attackChara = attackObj, receiveChara = receiveObj, spCost = spcost, HealPercent = healPercent}
            };
            battleQueue.Enqueue(action1);
        }
        //  3ならダメージを与えつつ回復する処理
        else if(HpFlag == 3)
        {
            //Action action1 = new Action()
            //{
            //    damage = new Damage { attackChara = attackObj, receiveChara = receiveObj, spCost = spcost, atk = power }
            //};
            //battleQueue.Enqueue(action1);
        }
        else
        {
        }

        Action action = battleQueue.Dequeue();
        if (HpFlag == 1)
        {
            action.damage.processing();
        }
        else if (HpFlag == 2)
        {
            action.heal.processing();
        }
        else
        {
        }
    }

    public class Damage
    {
        public GameObject attackChara;
        public GameObject receiveChara;
        public int spCost;
        public int attackType;
        public int atk;
        public int def;
        public int TotalDamage;

        public void processing()
        {
            //  物理攻撃
            if(attackType == 1)
            {
                atk = attackChara.GetComponent<Status>().GetATK();
                def = receiveChara.GetComponent<Status>().GetDEF();
            }
            //  魔法攻撃
            else if(attackType == 2)
            {
                atk = attackChara.GetComponent<Status>().GetMAT();
                def = receiveChara.GetComponent<Status>().GetMDF();
            }
            else
            {
            }

            TotalDamage = atk - def;
            if(TotalDamage <= 0)
            {
                TotalDamage = 1;
            }
            attackChara.GetComponent<Status>().SetSP(attackChara.GetComponent<Status>().GetSP() - spCost);
            receiveChara.GetComponent<Status>().SetHP(receiveChara.GetComponent<Status>().GetHP() - TotalDamage);

            Debug.Log(attackChara.gameObject.name + "が" + receiveChara.gameObject.name + "に" + TotalDamage + "ダメージ！");
            Debug.Log("残りHP" + receiveChara.GetComponent<Status>().GetHP());
        }
    }

    public class Heal
    {
        public GameObject attackChara;
        public GameObject receiveChara;
        public int spCost;
        public double HealPercent;
        public int TotalHeal;

        public void processing()
        {
            TotalHeal = (int)Math.Round(receiveChara.GetComponent<Status>().GetHP() * HealPercent);
            if (TotalHeal <= 0)
            {
                TotalHeal = 1;
            }
            attackChara.GetComponent<Status>().SetSP(attackChara.GetComponent<Status>().GetSP() - spCost);
            receiveChara.GetComponent<Status>().SetHP(receiveChara.GetComponent<Status>().GetHP() + TotalHeal);

            Debug.Log(receiveChara.gameObject.name + "のHPが" + TotalHeal + "かいふく！");
            Debug.Log("残りHP" + receiveChara.GetComponent<Status>().GetHP());
        }
    }

    public void ActionSelectEnd(GameObject obj)
    {
        GetComponent<command>().TargetDecided(ActivePlayer,obj);
    }

    public int GetPID(int name)
    {
        if(name == 1)
        {
            return pID1;
        }
        else if (name == 2)
        {
            return pID2;
        }
        else
        {
            return pID3;
        }
    }

    public int GetEID(int name)
    {
        if (name == 1)
        {
            return eID1;
        }
        else if (name == 2)
        {
            return eID2;
        }
        else
        {
            return eID3;
        }
    }

    public void SetActivePlayer(GameObject obj)
    {
        ActivePlayer = obj;
    }

    public int GetActivePlayer()
    {
        return int.Parse(Regex.Replace(ActivePlayer.name, @"[^0-9]", ""));
    }

    public void SetActiveChoose(bool flag)
    {
        ActiveChoose = flag;
    }

    public bool GetActiveChoose()
    {
        return ActiveChoose;
    }

    //  攻撃するキャラをセット
    public void SetAttackObj(GameObject obj)
    {
        attackObj = obj;
    }

    public GameObject GetAttackObj()
    {
        return attackObj;
    }

    //  攻撃を受けるキャラをセット
    public void SetReceiveObj(GameObject obj)
    {
        receiveObj = obj;
    }

    public GameObject GetReceiveObj()
    {
        return receiveObj;
    }

    public void SetActionFlag(bool flag)
    {
        ActionFlag = flag;
    }

    public bool GetActionFlag()
    {
        return ActionFlag;
    }

}
