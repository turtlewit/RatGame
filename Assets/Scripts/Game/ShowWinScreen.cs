using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowWinScreen : MonoBehaviour
{
    [SerializeField]
    Text winText;

    [SerializeField]
    GameObject button;

    [SerializeField]
    TeamColors colors;

    // Start is called before the first frame update
    void Start()
    {
        RoundManager.GameWon += OnWin;
        gameObject.SetActive(false);
    }

    void OnWin(int winner)
    {
        gameObject.SetActive(true);
        winText.text = $"Player {winner + 1} wins";
        winText.color = colors.colors[winner];
        if (winner != NetworkPlayer.LocalPlayer.PlayerNumber)
            button.SetActive(false);
    }

}
