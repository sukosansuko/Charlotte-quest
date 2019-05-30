using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StatusWindow : MonoBehaviour
{
    public GameObject status;

    void Start()
    {
    }

    void Update()
    {
        if (status)
        {
            SetHP();
            SetSP();
        }
    }

    public void SetHP()
    {
        if (this.gameObject.name.Contains("charHP"))
        {
            this.gameObject.GetComponent<Text>().text = "HP:" + Convert.ToString(status.GetComponent<Status>().GetHP() + "/" + Convert.ToString(status.GetComponent<Status>().GetMAXHP()));
        }
    }

    public void SetSP()
    {
        if (this.gameObject.name.Contains("charSP"))
        {
            this.gameObject.GetComponent<Text>().text = "SP:" + Convert.ToString(status.GetComponent<Status>().GetSP() + "/" + Convert.ToString(status.GetComponent<Status>().GetMAXSP()));
        }
    }

    public void SetName()
    {
        if (this.gameObject.name.Contains("charName"))
        {
            this.gameObject.GetComponent<Text>().text = status.GetComponent<Status>().GetCharName();
        }
    }
}
