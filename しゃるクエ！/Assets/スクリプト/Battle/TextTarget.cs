using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTarget : MonoBehaviour
{
    private GameObject battleManager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetName()
    {
        battleManager = GameObject.Find("BattleManager");
        battleManager.GetComponent<command>().InitTarget();
        battleManager.GetComponent<command>().SetSkillName(gameObject.name);
    }
}
