using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] textObjects;

    static GameUI singleton = null;


    private GameUI()
    {
        if (singleton == null)
            singleton = this;
    }


    static void SetPlayerScore(int player, int value)
    {
        singleton.textObjects[player].GetComponent<Text>().text = value.ToString();
    }


    static void AddPlayerScore(int player, int amount)
    {
        Text obj = singleton.textObjects[player].GetComponent<Text>();
        int current = int.Parse(obj.text);
        current += amount;
        obj.text = current.ToString();
    }
}
