using UnityEngine;
using System.Collections;

public class ShowClear : MonoBehaviour {

    public float delayTime = 0.1f;
    public float scaleSpeed = 0.1f;
    private ResultManager resultManager;


    void OnEnable()
    {
        resultManager = transform.parent.GetComponent<ResultManager>();
        StartCoroutine(Scaling());
        
    }
	
    //テキストを拡大する
    private IEnumerator Scaling()
    {
        for (float scale = 0f; scale <= 1f; scale += scaleSpeed)
        {
            transform.localScale = new Vector3(scale, scale, 1f);
            yield return new WaitForSeconds(delayTime);
        }
        yield return new WaitForSeconds(delayTime*10);
        resultManager.PlayTimeActive();
        gameObject.SetActive(false);
    }

}
