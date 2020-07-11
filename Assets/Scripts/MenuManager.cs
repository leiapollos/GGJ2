using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Canvas current;

    void Start(){
        current = GameObject.Find("MainCanvas").GetComponent<Canvas>();
    }

    public float ScaleSize = 1.1f;
    public float ScaleSpeed = 0.3f;

    public void Scale(GameObject obj){
        LeanTween.scale(obj, new Vector3(ScaleSize, ScaleSize, ScaleSize), ScaleSpeed);
    }

    public void StopScale(GameObject obj){
        LeanTween.scale(obj, new Vector3(1.0f, 1.0f, 1.0f), ScaleSpeed);
    }

    public void LoadGame(){
        SceneManager.LoadScene("Main");
    }

    public void LoadCanvas(Canvas canvas){
        current.gameObject.SetActive(false);
        current = canvas;
        current.gameObject.SetActive(true);
        ResetScale();
    }

    public void ResetScale(){
        GameObject[] btns = GameObject.FindGameObjectsWithTag("Button");
        foreach(GameObject bt in btns){
            bt.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}
