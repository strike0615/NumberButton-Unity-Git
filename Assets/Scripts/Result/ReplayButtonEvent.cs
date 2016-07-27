using UnityEngine;
using System.Collections;

public class ReplayButtonEvent : MonoBehaviour {

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
        GameManager.instance.LoadTitle();
    }
}
