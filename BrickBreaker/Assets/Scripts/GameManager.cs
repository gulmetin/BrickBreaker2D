using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //events
    
    //prefabs
    public GameObject ballPrefab;
    public GameObject paddlePrefab;

    //texts
    public TMP_Text scoreText;
    public TMP_Text ballsText;
    public TMP_Text levelText;

    public TMP_Text highscoreText;

    //panels
    public GameObject panelStartMenu;
    public GameObject panelPlay;
    public GameObject panelPauseMenu;
    public GameObject panelGameOver;
    public GameObject panelCompleted;

    public GameObject[] levels;
    private GameObject _currentBall;
    private GameObject _currentLevel;
    private GameObject _currentPaddle;

    //enum
    public enum State{MENU, INIT, LOADLEVEL, PLAY, LEVELCOMPLETED, GAMEOVER}
    State _state;

    //getter,setter
    private int _score;
    public int Score
    {
        get { return _score; }
        set { _score = value; scoreText.text=_score.ToString();}
    }

    private int _balls;
    public int Balls
    {
        get { return _balls; }
        set { _balls = value; ballsText.text=_balls.ToString();}
    }

    private int _level;
    public int Level
    {
        get { return _level; }
        set { _level = value; levelText.text=(_level+1).ToString(); }
    }
    
    //clicked-functions

    public void  InitClicked() {
        SwitchState(State.INIT);
    }

    public void  PlayClicked() {
        SwitchState(State.LOADLEVEL);
    }

    public void  MainMenu() {
        SwitchState(State.MENU);
    }

    //event-functions

    public void  CheckLevelCompleted() {
        StartCoroutine(RaiseDelay());
    }

     IEnumerator RaiseDelay(){
        yield return new WaitForSeconds(0.5f);
        if( _currentLevel !=null && _currentLevel.transform.childCount == 0){
            SwitchState(State.LEVELCOMPLETED);
        }
    }

    public void ChangePlayerBall() {
        Balls--;  
        _currentBall=null;
        if(_currentBall==null){
            if(Balls>0){
            _currentBall=Instantiate(ballPrefab);
            }
            else{
                SwitchState(State.GAMEOVER);
            }
        }  
       
    }

    public void ChangeScore() {
        Score+=50;
    }


    private void Awake() {
        //DontDestroyOnLoad(this.gameObject);
    }
    private void Start() {
        SwitchState(State.MENU);
    }

    public void SwitchState(State newState, float delay=0){
       StartCoroutine(SwitchDelay(newState,delay));
    }

    IEnumerator SwitchDelay(State newState, float delay){
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
    }

    private void  BeginState(State newState) {
        switch (newState)
        {
            case State.MENU:
            highscoreText.text = "HIGH SCORE :"+ PlayerPrefs.GetInt("highscore").ToString();
                panelStartMenu.SetActive(true);
                break;
            case State.INIT:
                Score=0;
                Level=0;
                if(_currentPaddle != null){
                    Destroy(_currentPaddle);
                }
                _currentPaddle = Instantiate(paddlePrefab);
                SwitchState(State.LOADLEVEL);
                break;
            case State.LOADLEVEL:
                if(Level >= levels.Length){  
                    Destroy(_currentPaddle);
                    SwitchState(State.MENU);
                }else{
                    _currentLevel = Instantiate(levels[Level]);
                    SwitchState(State.PLAY);
                }
                break;
            case State.PLAY:
                panelPlay.SetActive(true);
                Cursor.visible = false;
                Balls=3;
                Instantiate(ballPrefab);
                break;
            case State.LEVELCOMPLETED:
                PlayerPrefs.SetInt("highscore",Score);
                Destroy(_currentBall);
                Destroy(_currentLevel);
                Level++;
                panelCompleted.SetActive(true);
                break;
            case State.GAMEOVER:
                Destroy(_currentBall);
                Destroy(_currentLevel);
                PlayerPrefs.SetInt("score",Score);
                if(Score>PlayerPrefs.GetInt("highscore")){
                    PlayerPrefs.SetInt("highscore",Score);
                }
                panelGameOver.SetActive(true);
                Score=0;
                break;

        }
    }

    private void  EndState() {
        switch (_state)
        {
            case State.MENU:
                panelStartMenu.SetActive(false);
                break;
            case State.INIT:
                
                break;
            case State.LOADLEVEL:
                break;
            case State.PLAY:
                Cursor.visible = true;
                panelPlay.SetActive(false);
                break;
            case State.LEVELCOMPLETED:
                panelCompleted.SetActive(false);
                break;
            case State.GAMEOVER:
                panelPlay.SetActive(false);
                panelGameOver.SetActive(false);
                break;

        }
    }


}

