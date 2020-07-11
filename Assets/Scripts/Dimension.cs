using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimension : MonoBehaviour
{
    [HideInInspector]
    public LevelSection[] sections;
    System.Random random = new System.Random();
    SpriteRenderer[,] backgrounds = new SpriteRenderer[2, 2];
    public float ScrollMultiplier = 1;
    Vector2 offset;
    float width, height;
    // Start is called before the first frame update
    void Start()
    {
        //setup Parallax
        var mainBackground = transform.Find("Background").GetComponent<SpriteRenderer>();
        var camBounds = Camera.main.OrthographicBounds();
        width = camBounds.size.x;
        height = camBounds.size.y;
        mainBackground.size = new Vector2(camBounds.size.x, camBounds.size.y);
        backgrounds[0, 0] = mainBackground;
        var mainBounds = mainBackground.bounds;
        backgrounds[0, 1] = Instantiate(mainBackground, transform.position, Quaternion.identity, transform);
        backgrounds[1, 0] = Instantiate(mainBackground, transform.position, Quaternion.identity, transform);
        backgrounds[1, 1] = Instantiate(mainBackground, transform.position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int isPaused = PlayerPrefs.GetInt("isPaused");
        if (isPaused == 0)
        {
            Vector3 camPos = Camera.main.transform.position;
            camPos.z = 0;
            offset = -camPos * ScrollMultiplier;
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    var localOffset = new Vector2(offset.x + x * width, offset.y + y * height);
                    localOffset.x = fmodcirc(localOffset.x, width * 2) - width;
                    localOffset.y = fmodcirc(localOffset.y, height * 2) - height;
                    backgrounds[x, y].transform.localPosition = localOffset;
                }
            }
            transform.position = camPos;
        }
    }

    private float fmodcirc(float f, float div)
    {
        while (f < 0)
        {
            f += div;
        }
        while (f >= div)
        {
            f -= div;
        }
        return f;
    }

    public void SetSections(LevelSection[] sections)
    {
        this.sections = sections;
    }

    public LevelSection NextSection()
    {
        return sections[random.Next(sections.Length)];
    }

    public Dimension MakeInstance()
    {
        var instance = Instantiate(this);
        instance.sections = sections;
        return instance;
    }
}
