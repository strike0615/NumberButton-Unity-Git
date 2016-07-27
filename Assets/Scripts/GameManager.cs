using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public int startRow = 2;
    public int startColumn = 2;
    public int maxLevel = 3;

    public static List<float> playTimes;
    private float startTime;
    private float endTime;

    private int nowStageLevel = 0;
    private int nowNumber = 1;
    private ButtonManager buttonmanager;

	// Use this for initialization
	void Awake () {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        playTimes = new List<float>();

        CheckStartStatus();
	}

    //インスペクターからの正常範囲外入力をチェックする
    private void CheckStartStatus()
    {
        if (startRow < 1) startRow = 1;
        if (startColumn < 1) startColumn = 1;
        if (maxLevel < 1) maxLevel = 1;
    }

    //必要なボタン数を返すBottonManagerスクリプトのCreateButtons関数で呼び出す
    public int MaxArrySize()
    {
        return (startColumn + maxLevel - 1) * (startRow + maxLevel - 1);
    }
    


    public void UpDateStageLevel()
    {
        nowStageLevel++;
        if(nowStageLevel >= maxLevel)
        {
            LoadResult();
        }
        else
        {
            int nowRow = startRow + nowStageLevel;
            int nowColumn = startColumn + nowStageLevel;
            buttonmanager.SetButtons(nowRow, nowColumn);
            nowNumber = 1;
        }
    }

    //正しいボタンが押された時ButtonControllerのButtonDown関数で呼び出す
    public void UpDateNowNumber()
    {
        nowNumber++;
        int nowNumberMax = (startRow + nowStageLevel)*(startColumn + nowStageLevel);
        if(nowNumber > nowNumberMax)
        {
            EndTime();
            UpDateStageLevel();
        }
    }
    //public int NowStageLevel()
    //{
    //    return nowStageLevel;
    //}

    //正しい順番のボタンかを判断する、ButtonControllerのButtonDown関数で呼び出す
    public bool IsCanPushButton(int number)
    {
        if (number == nowNumber) return true;
        return false;
    }

    //BottonManagerスクリプトのSetButtons関数で呼び出す
    public void StartTime()
    {
        startTime = Time.time;
    }

    //UpDateNowNumber関数でUpDateStageLevel関数前に呼びさす
    public void EndTime()
    {
        endTime = Time.time;
        playTimes.Add(endTime - startTime);
    }

    
    public List<float> GetPlayTimes()
    {
        return playTimes;
    }


    //InGameシーンに入る時だけButtonManagerを獲得
    void OnLevelWasLoaded(int level)
    {
        if(level==1)
        {
            buttonmanager = GameObject.Find("ButtonManager").GetComponent<ButtonManager>();
        }
    }


    private void ResetGame()
    {
        nowStageLevel = 0;
        nowNumber = 1;
        playTimes.Clear();
    }
    public void LoadTitle()
    {
        ResetGame();
        Application.LoadLevel("Title");
        
    }
    public void LoadInGame()
    {
        Application.LoadLevel("InGame");
    }

    public void LoadResult()
    {
        Application.LoadLevel("Result");
    }
}
