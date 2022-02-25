using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public bool isBomb = false;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        if (isBomb) //Bomb
        {
            button.onClick.AddListener(BombCellBehaviour);
        }
        else //Not bomb
        {
            button.onClick.AddListener(NormalCellBehaviour);
        }
    }

    private void NormalCellBehaviour()
    {
        //Color
        var cellColor = button.colors;
        cellColor.normalColor = Color.green;
        button.colors = cellColor;
        //Interaction
        button.interactable = false;
        //Event
        GameManager.Instance.NormalCellPressed();
    }

    private void BombCellBehaviour()
    {
        //Color
        var cellColor = button.colors;
        cellColor.normalColor = Color.red;
        button.colors = cellColor;
        //Event
        GameManager.Instance.BombCellPressed();
    }
}
