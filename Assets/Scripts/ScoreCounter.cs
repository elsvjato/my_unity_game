using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    private int score;
    private Text scoreUiDisplay;
    void Start()
    {
        score = 0;
        scoreUiDisplay = gameObject.GetComponent<Text>();
        scoreUiDisplay.text = "SCORE: " + score.ToString();
    }
    public void ScoreUpdate()
    {
        score++;
        scoreUiDisplay.text = "SCORE: " + score.ToString();
        SpecialEffects.specialEffects.CoinSFX();
    }
    public void ScorePrize()
    {
        score+=100;
        scoreUiDisplay.text = "SCORE: " + score.ToString();
        SpecialEffects.specialEffects.BonusSFX();
    }
    void Update()
    {
        
    }
}
