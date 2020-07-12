using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityDimensionManager : MonoBehaviour
{
    public static UnityDimensionManager Instance;
    public RectTransform rectTransform;

    public Camera renderTextureCam;
    public Camera normalCamera;
    protected bool isUnity = false;

    public void Switch()
    {
        if(!isUnity)
        {
            rectTransform.localScale*=0.615f;
            rectTransform.localPosition = new Vector3(-39, 92, 0);
            renderTextureCam.gameObject.SetActive(true);
            normalCamera.gameObject.SetActive(false);
        }
        else
        {
            rectTransform.localScale = new Vector3(1,1,1);
            rectTransform.localPosition = new Vector3(0, 0, 0);
            normalCamera.gameObject.SetActive(true);
            renderTextureCam.gameObject.SetActive(false);
            normalCamera.transform.position = GameObject.Find("Main Camera Render Texture").transform.position;
        }

        isUnity = !isUnity;
    }

    void Awake(){
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            Switch();
        }
    }
}
