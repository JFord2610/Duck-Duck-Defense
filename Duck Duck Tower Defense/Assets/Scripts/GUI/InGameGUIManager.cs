using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameGUIManager : MonoBehaviour
{
    GameObject canvas;
    GameObject inGameGUIHolder;
    GameObject optionsMenuHolder;
    GameObject optionsButton;

    private void Awake()
    {
        canvas = GameObject.Find("GameOverlayCanvas");
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            Transform child = canvas.transform.GetChild(i);
            if (child.name == "InGameGUIHolder")
            {
                inGameGUIHolder = child.gameObject;
                for (int k = 0; k < child.childCount; k++)
                {
                    GameObject obj = child.GetChild(i).gameObject;
                    switch (obj.name)
                    {
                        case "OptionsButton":
                            optionsButton = obj;
                            break;
                    }
                }
            }
            else if (child.name == "OptionsMenuHolder")
            {
                optionsMenuHolder = child.gameObject;
            }
        }
    }

    public void OptionsButton_OnClick()
    {
        inGameGUIHolder.SetActive(false);
        //To Do: Pause game??
        optionsMenuHolder.SetActive(true);
    }

    public void ReturnToGameButton_Click()
    {
        optionsMenuHolder.SetActive(false);
        //To Do: UnPause game??
        inGameGUIHolder.SetActive(true);
    }
}
