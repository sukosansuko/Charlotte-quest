using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScene : MonoBehaviour
{
    Queue<Action> battleQueue;
    [SerializeField] private GameObject attackObj;
    [SerializeField] private GameObject receiveObj;

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
        if (Input.GetMouseButtonDown(0))
        {
            Action action1 = new Action()
            {
                damage = new Damage { attackChara = attackObj, receiveChara = receiveObj, mpCost = 1, atk = 1}
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
            Debug.Log(receiveChara.GetComponent<Status>().GetHP());
            //if (abnormalState != AbnormalState.None)
            //{
            //List<AbnormalState> temp = receiveChara.GetComponent<Status>().abnormalSet;
            //temp.Add(abnormalState);
            //receiveChara.GetComponent<Status>().abnormalSet = temp;
            //}
        }
    }

}
