using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public float Offset;
    public GameObject heart;
    Player player;
    
    int NumHearts;
    List<GameObject> CurrHearts = new List<GameObject>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        NumHearts = 0;
    }

    void AddLives()
    {
        NumHearts = player.lives;
        for (int i = 0; i < NumHearts; i++)
        {
            Vector3 pos = transform.position + Offset * i * Vector3.right;
            CurrHearts.Add(Instantiate(heart, pos, Quaternion.identity, this.transform));
        }
    }

    void RemoveLive()
    {
        NumHearts = player.lives;
        Destroy(CurrHearts[CurrHearts.Count - 1]);
        CurrHearts.RemoveAt(CurrHearts.Count - 1);
    }

    
    void Update()
    {
       if(player.lives > NumHearts)
        {
            AddLives();
        }
       else if(player.lives < NumHearts)
        {
            RemoveLive();
        }
    }
}
