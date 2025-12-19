using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Gmae Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public int playerId; //  케릭터 변경
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;

   
    void Awake()
    {
        instance = this; // 인스턴스는 이거다(초기화)
    }

    public void GameStart(int id) // 인스펙터 창에서 직접 호출 설정
    {
        playerId = id; //캐릭터 변경
        health = maxHealth; // 플레이어 체력  100으로 초기화
        player.gameObject.SetActive(true); // 캐릭터 활성화
        uiLevelUp.Select(playerId % 2); // 캐릭터 선택 및 무기활성화
        Resume(); // 시간을 다시흐르게 하고, islive 까지 true로 활성화
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(1f); // 캐릭터가 죽는 애니매이션 기다리기

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }

    public void GameVictroy()
    {
        StartCoroutine(GameVictroyRoutine());
    }
    IEnumerator GameVictroyRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true); // 몬스터 전부 정리

        yield return new WaitForSeconds(1f); // 몬스터가 죽는 애니매이션 기다리기

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0); // 시작 씬 불러오기
    }

    void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictroy();

        }
    }

    public void GetExp()
    {
        if (!isLive)
            return;

        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0; // 시간 멈춤

    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1; // 시간이 다시 흐름 # 만약 2라면 배속
    }

}

