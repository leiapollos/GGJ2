using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Player player;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        int isPaused = PlayerPrefs.GetInt("isPaused");
        if(isPaused == 0)
            transform.position = player.transform.position + offset;
    }
}
