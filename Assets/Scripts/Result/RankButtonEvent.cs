using UnityEngine;
using System.Collections;

public class RankButtonEvent : MonoBehaviour {
    private ResultManager resultManager;
    
    void Start()
    {
        resultManager = transform.parent.GetComponent<ResultManager>();
    }

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
    }
    void ButtonUP()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        resultManager.RanksActive();
    }
}
