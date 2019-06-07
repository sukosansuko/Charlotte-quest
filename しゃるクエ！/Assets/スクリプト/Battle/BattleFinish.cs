using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleFinish : MonoBehaviour
{
    Image image;
    int FlashingCnt;

    private GameObject sceneNavigator;

    Color color1 = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    Color color2 = new Color(1.0f, 1.0f, 1.0f, 0.0f);

    void Start()
    {
        sceneNavigator = GameObject.Find("SceneNavigator");
        WorldmapChar.SavePos = WorldmapChar.InitCharPos;
        FlashingCnt = 0;
        image = GetComponent<Image>();
    }

    void Update()
    {
        FlashingCnt++;

        if (FlashingCnt / 30 % 2 == 0)
        {
            image.color = color1;
        }
        else
        {
            image.color = color2;
        }
        if (gameObject.GetComponent<Image>().enabled == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.Instance.PlaySE("タイトルから進む時");
                switch (sceneNavigator.GetComponent<StatusControl>().GetStageID())
                {
                    case 1:
                        SceneNavigator.Instance.Change("W1");
                        break;
                    case 2:
                        SceneNavigator.Instance.Change("W2");
                        break;
                    case 3:
                        SceneNavigator.Instance.Change("W3");
                        break;
                    case 4:
                        SceneNavigator.Instance.Change("W4");
                        break;
                    case 5:
                        SceneNavigator.Instance.Change("W5");
                        break;
                    case 6:
                        SceneNavigator.Instance.Change("W6");
                        break;
                    case 7:
                        SceneNavigator.Instance.Change("W7");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
