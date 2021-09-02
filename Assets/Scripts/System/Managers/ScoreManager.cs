using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : ManagerService
{
    public int ScoreGainingModificator = 1;
    int CurrentScore;
    TextMeshProUGUI ScoreText;

    public ScoreManager(){
        SetScore(0);
    }

    public void AddScore(int _value){
        SetScore(CurrentScore + _value * ScoreGainingModificator);
    }

    public void RemoveScore(int _value){
        SetScore(CurrentScore - _value);
    }

    public void SetScore(int _value){
        CurrentScore = _value;
        SynchronizeScoreAndText();
    }

    void SynchronizeScoreAndText(){
        ScoreText.text = CurrentScore.ToString();
    }
}
