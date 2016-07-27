using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayTimeTextManager : MonoBehaviour {


    public GameObject playTimeTextPrefab;
    public Vector3 playTimeStartPositon;
    public float TextInterval;
    public float delayTime = 1f;

    private ResultManager resultManager;
    private List<GameObject> playTimeTexts;
    private List<string> playTimeTextData;
    private int stageMax;

	
    void OnEnable()
    {
        resultManager = transform.parent.GetComponent<ResultManager>();
        playTimeTexts = new List<GameObject>();
        stageMax = GameManager.instance.maxLevel;
        
        CheckPlaytime();
        CreatePlayTimesTexts();
        StartCoroutine(ShowTexts());
        
    }


    //プレー時間のデータに応じてテキストに書くデータを保存する
    private void CheckPlaytime()
    {
        playTimeTextData = new List<string>();
        List<float> playtimeDatas = GameManager.instance.GetPlayTimes();

        if (playtimeDatas.Count <= 0)
        {
            for (int i = 0; i < stageMax + 1; i++)
            {
                if (i == stageMax)
                    playTimeTextData.Add( "Total Play Time: No Play");
                else
                    playTimeTextData.Add("Stage" + (i + 1) + ": No Play");
            }
        }
        else
        {
            float totalPlayTime = 0;
            foreach (float time in playtimeDatas)
            {
                totalPlayTime += time;
            }
            for (int i = 0; i < stageMax + 1; i++)
            {
                if (i == stageMax)
                    playTimeTextData.Add("Total Play Time: " + totalPlayTime);
                else
                    playTimeTextData.Add("Stage" + (i+1) + ": "+playtimeDatas[i]);
            }
        }
    }

    //テキストを表示するオブジェクトを作り、データを書き込む
    private void CreatePlayTimesTexts()
    {
        
        for (int i = 0; i < stageMax + 1; i++)
        {
            Vector3 position = new Vector3(playTimeStartPositon.x, playTimeStartPositon.y - TextInterval * i, 0f);
            GameObject temp = Instantiate(playTimeTextPrefab, position, Quaternion.identity) as GameObject;
            temp.SetActive(false);
            temp.GetComponent<TextMesh>().text = playTimeTextData[i];
            temp.transform.SetParent(transform);
            playTimeTexts.Add(temp);
        }
    }

    //テキストオブジェクトを順次に表示する
    private IEnumerator ShowTexts()
    {
        foreach(GameObject playTimeText in playTimeTexts)
        {
            playTimeText.SetActive(true);

            yield return new WaitForSeconds(delayTime);
        }
        yield return new WaitForSeconds(delayTime);
        resultManager.RankButtonActive();
    }


}
