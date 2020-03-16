using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	[SerializeField]
	private GameObject[] textObjects;

	private void OnEnable()
	{
		RoundManager.PlayerScoreChanged += SetPlayerScore;
	}


	private void OnDisable()
	{
		RoundManager.PlayerScoreChanged -= SetPlayerScore;
	}


	private void SetPlayerScore(int player, int value)
	{
		textObjects[player].GetComponent<Text>().text = value.ToString();
	}


	private void AddPlayerScore(int player, int amount)
	{
		Text obj = textObjects[player].GetComponent<Text>();
		int current = int.Parse(obj.text);
		current += amount;
		obj.text = current.ToString();
	}
}
