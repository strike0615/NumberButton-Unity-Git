using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ButtonManager : MonoBehaviour {

    public GameObject buttonPrefab;

    private List<GameObject> buttons;
    private int[] numberTable;
    private Vector3 buttonSize;

	void Awake () {
        //ボタンのサイズを獲得、ボタンを並べる時にSetButtons関数で使用
        buttonSize = buttonPrefab.GetComponent<SpriteRenderer>().bounds.size;
	}
    void Start()
    {

        CreateButtons(GameManager.instance.MaxArrySize());
        SetButtons(GameManager.instance.startRow, GameManager.instance.startColumn);
        
    }

    //すべてのステージで使う最大数分のボタンと番号テーブルを事前に作って本オブジェクトの子に指定する
    void CreateButtons(int size)
    {
        
        numberTable = new int[size];
        buttons = new List<GameObject>();
        for (int i = 0; i < size; i++)
        {
            numberTable[i] = i + 1;
            GameObject temp = Instantiate(buttonPrefab, transform.position, Quaternion.identity) as GameObject;
            temp.transform.SetParent(transform);
            temp.SetActive(false);
            buttons.Add(temp);
        }
    }

    //現在ステージで使用する分のボタンの番号テーブルを並べ替る、SetButtons関数で呼び出す
    void ShuffleTable(int size)
    {
        if(size > numberTable.Length)
        {
            size = numberTable.Length;
            Debug.Log("ShuffleSize > tableSize");
            Debug.Log(size);
        }
        for(int i =0 ;i<size;i++)
        {
            int randomIndex = Random.Range(0, size);
            int temp = numberTable[randomIndex];
            numberTable[randomIndex] = numberTable[i];
            numberTable[i] = temp;
        }
    }

    //ボタンをセットする。最初は本スクリプトのスタートで呼び出し、以後はGameManagerのUpDateStageLevel関すで呼び出す
    public void SetButtons( int row,int column)
    {
        //現在ステージで使用するボタン数
        int nowStageSize = column * row;
        //使用数分の番号を並べ替える
        ShuffleTable(nowStageSize);

        //使用する数分位置を並べアクティブにする
        for (int i = 0; i < nowStageSize; i++)
        {
            Vector3 newPosition = new Vector3(buttonSize.x * (i /row), buttonSize.y * (i %row), 0f);

            buttons[i].transform.localPosition = newPosition;
            //ボタンの番号を指定する
            buttons[i].GetComponent<ButtonController>().SetNumber(numberTable[i]);
            buttons[i].SetActive(true);
        }

        //本オブジェクトの位置を現在使用中のボタン数に合わせてボタンが画面中心に来るよう調整する
        Vector3 newButtonsPosition = new Vector3((-buttonSize.x/2f) * (column-1),(-buttonSize.y/2f) * (row-1),0f);
        transform.position = newButtonsPosition;


        GameManager.instance.StartTime();
    }

    
}
