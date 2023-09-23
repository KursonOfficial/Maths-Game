using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROTATO : MonoBehaviour
{

    public GameObject sus;
    // Start is called before the first frame update
 
    public bool swix;
    public bool swiy;
    public float speed;
    void Update()
    {
        if (swix){
            if (sus.transform.localScale.x <= -1f){
                swix = !swix;
            }
                sus.transform.localScale = sus.transform.localScale - new Vector3 (speed*Time.deltaTime,0f,0f);
        }else {
            if (sus.transform.localScale.x >= 1f){
                swix = !swix;
            }
                sus.transform.localScale = sus.transform.localScale + new Vector3 (speed*Time.deltaTime,0f,0f);
        }
    }

}
           /* if (swix){
            if (sus.transform.localScale.x <= -1f){
                swix = !swix;
            }
            if (sus.transform.localScale.x<= 0.2f)
                sus.transform.localScale = sus.transform.localScale - new Vector3 (0.003f,0f,0f);
            else 
                sus.transform.localScale = sus.transform.localScale - new Vector3 (0.001f,0f,0f);
        }else {
            if (sus.transform.localScale.x >= 1f){
                swix = !swix;
            }
            if (sus.transform.localScale.x<= 0.2f)
                sus.transform.localScale = sus.transform.localScale + new Vector3 (0.003f,0f,0f);
                else 
                sus.transform.localScale = sus.transform.localScale + new Vector3 (0.001f,0f,0f);
            */