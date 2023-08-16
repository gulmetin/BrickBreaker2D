using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetScoreText : MonoBehaviour
{
    private TMP_Text _scoreText;
    void Start()
    {
        _scoreText = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = "SCORE :"+ PlayerPrefs.GetInt("score").ToString();
    }
}
