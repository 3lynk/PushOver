using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {
    Title,
    Menu,
    Tutorial,
    ChapterOne,
    ChapterTwo,
    Ending
}

//게임의 전체적인 흐름과 중요 정보를 저장한다
//SINGLETON 클래스
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void Start() {
        UpdateGameState(GameState.Title);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            M1Start();
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            M2Start();
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            M3Start();
        }
    }

    public void UpdateGameState(GameState newState) {
        State = newState;
        switch (newState) {
            case GameState.Title:
                //SceneManager.LoadScene("Start");
                break;
            case GameState.Menu:
                break;
            case GameState.Tutorial:
                break;
            case GameState.ChapterOne:
                break;
            case GameState.ChapterTwo:
                break;
            case GameState.Ending:
                break;
            default:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }

    public void GameStart() {
        SceneManager.LoadScene("Prologue");
    }

    public void Next() {
        SceneManager.LoadScene("SejongUniv");
    }

    public void Credit() {
        SceneManager.LoadScene("Credit");
    }

    public void Quit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    public void ToTitile() {
        SceneManager.LoadScene("Title");
    }

    public void M1Start() {
        SceneManager.LoadScene("JumpMinigame");
    }
    public void M1Fail() {
        SceneManager.LoadScene("SejongUniv");
    }
    public void M1Clear() {
        SceneManager.LoadScene("SejongUniv");
    }
    public void M2Start() {
        SceneManager.LoadScene("Run&JumpMinigame");
    }
    public void M2Fail() {
        SceneManager.LoadScene("SejongUniv");
    }
    public void M2Clear() {
        SceneManager.LoadScene("SejongUniv");
    }
    public void M3Start() {
        SceneManager.LoadScene("StealthMinigame");
    }
    public void M3Fail() {
        SceneManager.LoadScene("SejongUniv");
    }
    public void M3Clear() {
        SceneManager.LoadScene("SejongUniv");
    }

}
