using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

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
        if(ActionFlag)
        {
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            Action action1 = new Action()
            {
                damage = new Damage { attackChara = attackObj, receiveChara = receiveObj, mpCost = 1, atk = 100 }
            };
            battleQueue.Enqueue(action1);

            Action action = battleQueue.Dequeue();

            action.damage.processing();
        }
        

    }

    public class Damage
    {
        public GameObject attackChara;
        public GameObject receiveChara;
        public int mpCost;
        public int atk;

        public void processing()
        {
            attackChara.GetComponent<Status>().SetSP(attackChara.GetComponent<Status>().GetSP() - mpCost);
            receiveChara.GetComponent<Status>().SetHP(receiveChara.GetComponent<Status>().GetHP() - atk);

            Debug.Log(receiveChara.gameObject.name + "に" + atk + "ダメージ！");
            Debug.Log("残りHP" + receiveChara.GetComponent<Status>().GetHP());

            //if (abnormalState != AbnormalState.None)
            //{
            //List<AbnormalState> temp = receiveChara.GetComponent<Status>().abnormalSet;
            //temp.Add(abnormalState);
            //receiveChara.GetComponent<Status>().abnormalSet = temp;
            //}
        }
    }

    public void ActionSelectEnd()
    {
        GetComponent<command>().TargetDecided(ActivePlayer);
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

    //  攻撃を受けるキャラをセット
    public void SetReceiveObj(GameObject obj)
    {
        receiveObj = obj;
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
