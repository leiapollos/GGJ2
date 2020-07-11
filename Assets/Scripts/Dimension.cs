using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Dimension : MonoBehaviour
{
    List<LevelSection> sections;
    System.Random random = new System.Random();
    public Sprite BackgroundSprite;
    SpriteRenderer background;
    // Start is called before the first frame update
    void Start()
    {
        background = transform.Find("Background").GetComponent<SpriteRenderer>();
        background.sprite = BackgroundSprite;
        var camBounds = Camera.main.OrthographicBounds();
        background.size = new Vector2(camBounds.size.x, camBounds.size.y);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
