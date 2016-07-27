using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {

    
    public GameObject text;

    private int number;



    void OnMouseDown()
    {
        ButtonDown();
    }
    void OnMouseUp()
    {
        ButtonUP();
    }


    void ButtonDown()
    {
       
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        if (GameManager.instance.IsCanPushButton(number))
        {
            
            gameObject.SetActive(false);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            GameManager.instance.UpDateNowNumber();
        }
    }
    void ButtonUP()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    //番号設定、ButtonManagerのSetButtons関数で呼び出す
    public void SetNumber(int setNumber)
    {
        number = setNumber;
        text.GetComponent<TextMesh>().text = setNumber.ToString();
    }
}
