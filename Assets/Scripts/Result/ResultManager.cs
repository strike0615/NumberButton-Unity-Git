using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System; 
using System.Text;
public class ResultManager : MonoBehaviour 
{
    public GameObject gameClearText;
    public GameObject playTimeTexts;
    public GameObject ranks;
    public GameObject rankButton;
    public GameObject replayButton;

    public int rankMax = 5;

    private int nowRank = -1;
    private List<string> rankDataList;
    private float totalPlayTime;


	// Use this for initialization
    void Start()
    {
        LoadRanksData();
        CheckPlayTime();
        CheckRankIn();
        SaveRankData();

        gameClearText.SetActive(true);
    }


    private void LoadRanksData()
    {
        rankDataList = new List<string>();
        for(int i=0;i<rankMax;i++)
        {
            string key = "ranks" + i.ToString();
            if(PlayerPrefs.HasKey(key))
            {
                rankDataList.Add(PlayerPrefs.GetString(key));
            }
            else
            {
                rankDataList.Add("No Data");
            }
        }
    }
    private void CheckPlayTime()
    {
        List<float> playtimeDatas = GameManager.instance.GetPlayTimes();
        totalPlayTime = 0;
        if (playtimeDatas.Count > 0)
        {
            foreach (float time in playtimeDatas)
            {
                totalPlayTime += time;
            }
        }
    }
    private void CheckRankIn()
    {
        if (totalPlayTime == 0) return;

        for (int i = 0; i < rankDataList.Count;i++ )
        {
            if (rankDataList[i] == "No Data")
            {
                rankDataList.Insert(i, totalPlayTime.ToString());
                rankDataList.RemoveAt(rankDataList.Count - 1);
                nowRank = i;
                break;
            }
            else
            {
                if(float.Parse(rankDataList[i]) > totalPlayTime)
                {
                    rankDataList.Insert(i, totalPlayTime.ToString());
                    rankDataList.RemoveAt(rankDataList.Count - 1);
                    nowRank = i;
                    break;
                }
            }
        }
    }
    private void SaveRankData()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < rankMax; i++)
        {
            string key = "ranks" + i.ToString();
            PlayerPrefs.SetString(key, rankDataList[i]);
        }
        PlayerPrefs.Save();
    }
   


    public void PlayTimeActive()
    {
        playTimeTexts.SetActive(true);
    }
    public void RankButtonActive()
    {
        rankButton.SetActive(true);
    }
    public void RanksActive()
    {
        rankButton.SetActive(false);
        playTimeTexts.SetActive(false);
        ranks.SetActive(true);
    }
    public void ReplayButtonActive()
    {
        replayButton.SetActive(true);
    }

    public List<string> GetRankData()
    {
        return rankDataList;
    }
    public int GetNowRank()
    {
        return nowRank;
    }
}
