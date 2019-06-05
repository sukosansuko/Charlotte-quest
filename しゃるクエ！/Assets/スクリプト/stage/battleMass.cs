using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleMass : MonoBehaviour {

    string activScene;      // 現在のｼｰﾝ名
    private GameObject sceneNavigator;
    stageData SD;

    // Use this for initialization
    void Start()
    {
        activScene = mapChar.activScene;
        sceneNavigator = GameObject.Find("SceneNavigator");
    }

    // Update is called once per frame
    void Update () {
        int enemy1 = 0;
        int enemy2 = 0;
        int enemy3 = 0;

        SD = Resources.Load("ExcelData/stageData") as stageData;

        if (mapChar.activScene == "W1")
        {
            if (stageMass.MassName == "battle1")
            {
                if (SD.sheets[0].list[0].enemy_count == 1)
                {
                    enemy1 = 0;
                    enemy2 = SD.sheets[0].list[0].enemy1;
                    enemy3 = 0;
                }
                if(SD.sheets[0].list[0].enemy_count == 3)
                {
                    enemy1 = SD.sheets[0].list[0].enemy1;
                    enemy2 = SD.sheets[0].list[0].enemy2;
                    enemy3 = SD.sheets[0].list[0].enemy3;
                }
                sceneNavigator.GetComponent<StatusControl>().SetEnemyList(enemy1,enemy2,enemy3);
                sceneNavigator.GetComponent<StatusControl>().SetStageID(1);
                SceneNavigator.Instance.Change("BattleScene");
            }
            if (stageMass.MassName == "battle2")
            {
                if (SD.sheets[0].list[1].enemy_count == 1)
                {
                    enemy1 = 0;
                    enemy2 = SD.sheets[0].list[1].enemy1;
                    enemy3 = 0;
                }
                if (SD.sheets[0].list[1].enemy_count == 3)
                {
                    enemy1 = SD.sheets[0].list[1].enemy1;
                    enemy2 = SD.sheets[0].list[1].enemy2;
                    enemy3 = SD.sheets[0].list[1].enemy3;
                }
                sceneNavigator.GetComponent<StatusControl>().SetEnemyList(enemy1, enemy2, enemy3);
                sceneNavigator.GetComponent<StatusControl>().SetStageID(1);
                SceneNavigator.Instance.Change("BattleScene");
            }
        }
        if (mapChar.activScene == "W2")
        {
            if (stageMass.MassName == "battle1")
            {
                if (SD.sheets[0].list[3].enemy_count == 1)
                {
                    enemy1 = 0;
                    enemy2 = SD.sheets[0].list[3].enemy1;
                    enemy3 = 0;
                }
                if (SD.sheets[0].list[3].enemy_count == 3)
                {
                    enemy1 = SD.sheets[0].list[3].enemy1;
                    enemy2 = SD.sheets[0].list[3].enemy2;
                    enemy3 = SD.sheets[0].list[3].enemy3;
                }
                sceneNavigator.GetComponent<StatusControl>().SetEnemyList(enemy1, enemy2, enemy3);
                sceneNavigator.GetComponent<StatusControl>().SetStageID(2);
                SceneNavigator.Instance.Change("BattleScene");
            }
            if (stageMass.MassName == "battle2")
            {
                if (SD.sheets[0].list[4].enemy_count == 1)
                {
                    enemy1 = 0;
                    enemy2 = SD.sheets[0].list[4].enemy1;
                    enemy3 = 0;
                }
                if (SD.sheets[0].list[4].enemy_count == 3)
                {
                    enemy1 = SD.sheets[0].list[4].enemy1;
                    enemy2 = SD.sheets[0].list[4].enemy2;
                    enemy3 = SD.sheets[0].list[4].enemy3;
                }
                sceneNavigator.GetComponent<StatusControl>().SetEnemyList(enemy1, enemy2, enemy3);
                sceneNavigator.GetComponent<StatusControl>().SetStageID(2);
                SceneNavigator.Instance.Change("BattleScene");
            }
        }
        if (mapChar.activScene == "W3")
        {
            if (stageMass.MassName == "battle1")
            {
                if (SD.sheets[0].list[7].enemy_count == 1)
                {
                    enemy1 = 0;
                    enemy2 = SD.sheets[0].list[7].enemy1;
                    enemy3 = 0;
                }
                if (SD.sheets[0].list[7].enemy_count == 3)
                {
                    enemy1 = SD.sheets[0].list[7].enemy1;
                    enemy2 = SD.sheets[0].list[7].enemy2;
                    enemy3 = SD.sheets[0].list[7].enemy3;
                }
                sceneNavigator.GetComponent<StatusControl>().SetEnemyList(enemy1, enemy2, enemy3);
                sceneNavigator.GetComponent<StatusControl>().SetStageID(3);
                SceneNavigator.Instance.Change("BattleScene");
            }
            if (stageMass.MassName == "battle2")
            {
                if (SD.sheets[0].list[8].enemy_count == 1)
                {
                    enemy1 = 0;
                    enemy2 = SD.sheets[0].list[8].enemy1;
                    enemy3 = 0;
                }
                if (SD.sheets[0].list[8].enemy_count == 3)
                {
                    enemy1 = SD.sheets[0].list[8].enemy1;
                    enemy2 = SD.sheets[0].list[8].enemy2;
                    enemy3 = SD.sheets[0].list[8].enemy3;
                }
                sceneNavigator.GetComponent<StatusControl>().SetEnemyList(enemy1, enemy2, enemy3);
                sceneNavigator.GetComponent<StatusControl>().SetStageID(3);
                SceneNavigator.Instance.Change("BattleScene");
            }
        }
        if (mapChar.activScene == "W4")
        {
            if (stageMass.MassName == "battle1")
            {
                if (SD.sheets[0].list[12].enemy_count == 1)
                {
                    enemy1 = 0;
                    enemy2 = SD.sheets[0].list[12].enemy1;
                    enemy3 = 0;
                }
                if (SD.sheets[0].list[12].enemy_count == 3)
                {
                    enemy1 = SD.sheets[0].list[12].enemy1;
                    enemy2 = SD.sheets[0].list[12].enemy2;
                    enemy3 = SD.sheets[0].list[12].enemy3;
                }
                sceneNavigator.GetComponent<StatusControl>().SetEnemyList(enemy1, enemy2, enemy3);
                sceneNavigator.GetComponent<StatusControl>().SetStageID(4);
                SceneNavigator.Instance.Change("BattleScene");
            }
            if (stageMass.MassName == "battle2")
            {
                if (SD.sheets[0].list[13].enemy_count == 1)
                {
                    enemy1 = 0;
                    enemy2 = SD.sheets[0].list[13].enemy1;
                    enemy3 = 0;
                }
                if (SD.sheets[0].list[13].enemy_count == 3)
                {
                    enemy1 = SD.sheets[0].list[13].enemy1;
                    enemy2 = SD.sheets[0].list[13].enemy2;
                    enemy3 = SD.sheets[0].list[13].enemy3;
                }
                sceneNavigator.GetComponent<StatusControl>().SetEnemyList(enemy1, enemy2, enemy3);
                sceneNavigator.GetComponent<StatusControl>().SetStageID(4);
                SceneNavigator.Instance.Change("BattleScene");
            }
        }
    }
}
