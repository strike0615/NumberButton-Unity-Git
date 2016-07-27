using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour {

    public LayerMask touchInputMask;


    Camera camera;
    // Update is called once per frame
#if UNITY_ANDROID
    void Update () 
    {



        //if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        //{
  
        //        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit hit;
        //        if (Physics.Raycast(ray, out hit, touchInputMask))
        //        {
        //            GameObject recipient = hit.transform.gameObject;
        //            if (Input.GetMouseButtonDown(0))
        //            {
        //                recipient.SendMessage("ButtonDown", hit.point, SendMessageOptions.DontRequireReceiver);
        //            }
        //            if (Input.GetMouseButtonUp(0))
        //            {
        //                recipient.SendMessage("ButtonUP", hit.point, SendMessageOptions.DontRequireReceiver);
        //            }
        //        }
            
        //}


        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Ray ray = camera.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, touchInputMask))
                {
                    GameObject recipient = hit.transform.gameObject;
                    if (touch.phase == TouchPhase.Began)
                    {
                        recipient.SendMessage("ButtonDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Ended)
                    {
                        recipient.SendMessage("ButtonUP", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
    }
#endif
}
