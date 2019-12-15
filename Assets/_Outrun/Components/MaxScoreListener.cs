using Assets._Outrun.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxScoreListener : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        GameManager.OnMaxScoreChanged += OnMaxScoreChanged;
    }

    private void OnMaxScoreChanged(int newScore)
    {
        text.text = newScore.ToString();
    }
}
