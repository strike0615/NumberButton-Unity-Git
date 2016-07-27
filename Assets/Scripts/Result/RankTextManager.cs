using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankTextManager : MonoBehaviour {

    public GameObject rankPrefab;

    public Vector3 rankStartPosition;
    public float TextInterval = 3;
    public float delayTime = 1f;

    private ResultManager resultManager;
    private List<GameObject> rankTexts;
    private List<string> rankDataList;
    private int rankMax;

	

    void OnEnable()
    {
        resultManager = transform.parent.GetComponent<ResultManager>();
        rankDataList = resultManager.GetRankData();
        rankMax = resultManager.rankMax;
        CreateRanks();
        StartCoroutine(RankCoroutine());
    }

    private void CreateRanks()
    {
        rankTexts = new List<GameObject>();

        for (int i = 0; i < rankMax; i++)
        {
            Vector3 position = new Vector3(rankStartPosition.x, rankStartPosition.y - TextInterval * i, 0f);
            GameObject temp = Instantiate(rankPrefab, position, Quaternion.identity) as GameObject;
            temp.SetActive(false);
            temp.GetComponent<TextMesh>().text = "Rank"+ (i + 1) + ": " +rankDataList[i];
            temp.transform.SetParent(transform);
            rankTexts.Add(temp);
        }
        int nowRank = resultManager.GetNowRank();
        if (nowRank != -1)
        {
            rankTexts[nowRank].transform.localScale = new Vector3(2f,2f,1f);
        }
    }

    private IEnumerator RankCoroutine()
    {
        foreach (GameObject rank in rankTexts)
        {
            rank.SetActive(true);
            yield return new WaitForSeconds(delayTime);
        }
        resultManager.ReplayButtonActive();
    }
}
