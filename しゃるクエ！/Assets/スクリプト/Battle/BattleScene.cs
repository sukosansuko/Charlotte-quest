using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScene : MonoBehaviour
{
    Queue<Action> battleQueue;
    [SerializeField] private GameObject attackObj;
    [SerializeField] private GameObject receiveObj;

    [SerializeField] private int pID1;
    [SerializeField] private int pID2;
    [SerializeField] private int pID3;
    [SerializeField] private int eID1;
    [SerializeField] private int eID2;
    [SerializeField] private int eID3;

    private command co;

    //  コマンド選択開始用フラグ
    private bool ActionChoose;

    public struct Action
    {
        //public Performance p;
        public Damage damage;
    }

    void Start()
    {
        battleQueue = new Queue<Action>();
    }

    void Update()
    {
        if(ActionChoose)
        {
            GetComponent<command>().commandDisplay();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Action action1 = new Action()
            {
                damage = new Damage { attackChara = attackObj, receiveChara = receiveObj, mpCost = 1, atk = 1 }
            };
            battleQueue.Enqueue(action1);

            Action action = battleQueue.Dequeue();

            action.damage.processing();
            //StartCoroutine(ActionCoroutine());
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

    public int GetActivePlayer()
    {
        return 1;
    }

    public void SetActiveChoose(bool flag)
    {
        ActionChoose = flag;
    }

    public bool GetActiveChoose()
    {
        return ActionChoose;
    }
}
