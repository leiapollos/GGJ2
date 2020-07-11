using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<string> DimensionNames;
    public string StartingDimension;
    Dictionary<string, LevelSection[]> Dimensions = new Dictionary<string, LevelSection[]>();
    public float SpawnMargin = 1;
    List<LevelSection> spawned = new List<LevelSection>();
    public float InitPlayerHeight = 2;
    Player player;
    string dimension;
    System.Random rand = new System.Random();
    public float MinTimer, MaxTimer;
    float curTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        LoadSections();
        SpawnInit(StartingDimension);
        dimension = StartingDimension;
        curTimer = Random.Range(MinTimer, MaxTimer);
    }

    void LoadSections()
    {
        foreach (string name in DimensionNames)
        {
            Dimensions[name] = Resources.LoadAll<LevelSection>("Dimensions/" + name);
        }
    }

    void SpawnInit(string dimension)
    {
        Bounds camBounds = Camera.main.OrthographicBounds();
        Vector3 playerPos = player.transform.position;

        Vector3 lastSpawn = playerPos + (Vector3.down * InitPlayerHeight);
        LevelSection cur;
        do
        {
            var nextSection = Dimensions[dimension][rand.Next(Dimensions[dimension].Length)];
            cur = Instantiate(
                nextSection,
                lastSpawn + nextSection.StartOffset(),
                Quaternion.identity
                );
            lastSpawn = cur.endPos;
            spawned.Add(cur);
        } while (cur.endPos.x < camBounds.max.x + SpawnMargin);

        lastSpawn = playerPos + (Vector3.down * InitPlayerHeight);
        do
        {
            var nextSection = Dimensions[dimension][rand.Next(Dimensions[dimension].Length)];
            cur = Instantiate(
                nextSection,
                lastSpawn + nextSection.EndOffset(),
                Quaternion.identity
                );
            lastSpawn = cur.startPos;
            spawned.Insert(0, cur);
        } while (cur.startPos.x > camBounds.min.x - SpawnMargin);
    }

    // Update is called once per frame
    void Update()
    {
        GenerateAndClean(dimension);
        curTimer -= Time.deltaTime;
        if (curTimer <= 0)
        {
            curTimer = Random.Range(MinTimer, MaxTimer);
            SwitchDimension();
        }
    }

    void SwitchDimension()
    {
        foreach (var section in spawned)
        {
            Destroy(section.gameObject);
        }
        spawned.Clear();
        var others = new List<string>(DimensionNames);
        others.Remove(dimension);
        dimension = others[rand.Next(others.Count)];
        SpawnInit(dimension);
    }

    void GenerateAndClean(string dimension)
    {
        Bounds camBounds = Camera.main.OrthographicBounds();

        //right of camera
        while (spawned[spawned.Count - 1].endPos.x < camBounds.max.x)
        {
            var nextSection = Dimensions[dimension][rand.Next(Dimensions[dimension].Length)];
            var newSection = Instantiate(
                nextSection,
                spawned[spawned.Count - 1].endPos + nextSection.StartOffset(),
                Quaternion.identity
                ).GetComponent<LevelSection>();
            spawned.Add(newSection);
        }
        while (spawned[spawned.Count - 1].startPos.x > camBounds.max.x)
        {
            Destroy(spawned[spawned.Count - 1].gameObject);
            spawned.RemoveAt(spawned.Count - 1);
        }

        //left of camera
        while (spawned[0].startPos.x > camBounds.min.x)
        {
            var nextSection = Dimensions[dimension][rand.Next(Dimensions[dimension].Length)];
            var newSection = Instantiate(
                nextSection,
                spawned[0].startPos + nextSection.EndOffset(),
                Quaternion.identity
                ).GetComponent<LevelSection>();
            spawned.Insert(0, newSection);
        }
        while (spawned[0].endPos.x < camBounds.min.x)
        {
            Destroy(spawned[0].gameObject);
            spawned.RemoveAt(0);
        }
    }
}
