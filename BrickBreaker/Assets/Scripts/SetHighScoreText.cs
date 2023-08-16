using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetHighScoreText : MonoBehaviour
{
    private TMP_Text _highscoreText;
    void Start()
    {
        _highscoreText = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _highscoreText.text = "HIGH SCORE :"+ PlayerPrefs.GetInt("highscore").ToString();
    }
}
