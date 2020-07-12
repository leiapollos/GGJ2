using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    protected Canvas current;

    public GameObject pauseScreen;

    void Start(){
        if(SceneManager.GetActiveScene().name.Equals("Menu"))
            current = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        
        if(SceneManager.GetActiveScene().name.Equals("Main")){
            PlayerPrefs.SetInt("isPaused", 0);
            pauseScreen.gameObject.SetActive(false);
        }
    }

    public float ScaleSize = 1.1f;
    public float ScaleSpeed = 0.3f;

    public void Scale(GameObject obj){
        LeanTween.scale(obj, new Vector3(ScaleSize, ScaleSize, ScaleSize), ScaleSpeed).setIgnoreTimeScale(true);
        LeanTween.scale(obj.GetComponentInChildren<Image>().gameObject, new Vector3(1,1,1), ScaleSpeed).setIgnoreTimeScale(true);
    }

    public void StopScale(GameObject obj){
        LeanTween.scale(obj, new Vector3(1.0f, 1.0f, 1.0f), ScaleSpeed).setIgnoreTimeScale(true);
        LeanTween.scale(obj.GetComponentInChildren<Image>().gameObject, new Vector3(0,0,0), ScaleSpeed).setIgnoreTimeScale(true);
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
        foreach(GameObject bt in btns)
        {
            bt.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            bt.GetComponentInChildren<Image>().gameObject.transform.localScale = new Vector3(0,0,0);
        }
    }

    public void Exit(){
        Application.Quit();
    }

    void Update(){
        if(SceneManager.GetActiveScene().name.Equals("Main")){
            if(Input.GetKeyDown(KeyCode.Escape)){
                HandlePause();
            }   
        }
    }

    public void HandlePause(){
        int isPaused = PlayerPrefs.GetInt("isPaused");
        if(isPaused == 1){
            PlayerPrefs.SetInt("isPaused", 0);
            ResetScale();
            pauseScreen.gameObject.SetActive(false);
            Time.timeScale = 1;
        }else{
            PlayerPrefs.SetInt("isPaused", 1);
            pauseScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}