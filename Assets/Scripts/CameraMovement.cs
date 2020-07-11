using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Player player;
    Vector3 offset;
    public float FollowStrength = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int isPaused = PlayerPrefs.GetInt("isPaused");
        if (isPaused == 0)
        {
            Vector3 newPos = transform.position;
            if (player.velocity.x > 0.01)
                newPos.x = Mathf.Lerp(transform.position.x, player.transform.position.x + offset.x, FollowStrength * Time.deltaTime);
            else
                newPos.x += player.Speed * Time.deltaTime;
            newPos.y = Mathf.Lerp(transform.position.y, player.transform.position.y + offset.y, FollowStrength * Time.deltaTime);
            transform.position = newPos;
        }
    }
}
