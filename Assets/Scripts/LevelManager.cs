﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public List<string> DimensionNames;
    public string StartingDimension;
    Dictionary<string, Dimension> Dimensions = new Dictionary<string, Dimension>();
    public float SpawnMargin = 1;
    List<LevelSection> spawned = new List<LevelSection>();
    public float InitPlayerHeight = 2;
    Player player;
    public string dimensionName;
    Dimension dimension;
    System.Random rand = new System.Random();
    public float MinTimer, MaxTimer;
    [HideInInspector]
    public float curTimer;

    int sequencePos = 0;

    [System.Serializable]
    public class MainSequenceEntry
    {
        public string dimensionName;
        public float duration;
    }

    public MainSequenceEntry[] MainSequence;

    protected bool isPaused = false;

    protected Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = cam = GameObject.Find("Main Camera Render Texture").GetComponent<Camera>();
        main = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        LoadSections();
        if (MainSequence.Length > 0)
        {
            dimensionName = MainSequence[0].dimensionName;
            curTimer = MainSequence[0].duration;
        }
        else
        {
            dimensionName = StartingDimension;
            curTimer = Random.Range(MinTimer, MaxTimer);
        }
        SpawnInit();
    }

    void LoadSections()
    {
        foreach (string name in DimensionNames)
        {
            Dimensions[name] = Resources.LoadAll<Dimension>("Dimensions/" + name)[0];
            Dimensions[name].SetSections(Resources.LoadAll<LevelSection>("Dimensions/" + name + "/LevelSections"));
        }
    }

    void SpawnInit()
    {
        Bounds camBounds = cam.OrthographicBounds();
        Vector3 playerPos = player.transform.position;
        dimension = Dimensions[dimensionName].MakeInstance();
        Vector3 lastSpawn = playerPos + (Vector3.down * InitPlayerHeight);
        LevelSection cur;
        do
        {
            var nextSection = dimension.NextSection();
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
            var nextSection = dimension.NextSection();
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
        int isPaused = PlayerPrefs.GetInt("isPaused");
        if (isPaused == 0)
        {
            GenerateAndClean();
            curTimer -= Time.deltaTime;
            if (curTimer <= 0)
            {
                sequencePos++;
                if (sequencePos < MainSequence.Length)
                {
                    curTimer = MainSequence[sequencePos].duration;
                    SwitchDimension(MainSequence[sequencePos].dimensionName);
                }
                else
                {
                    curTimer = Random.Range(MinTimer, MaxTimer);
                    SwitchDimension();
                }
            }
        }
    }

    void SwitchDimension()
    {
        var others = new List<string>(DimensionNames);
        others.Remove(dimensionName);
        SwitchDimension(others[rand.Next(others.Count)]);
    }

    void SwitchDimension(string dimName)
    {
        foreach (var section in spawned)
        {
            Destroy(section.gameObject);
        }
        spawned.Clear();
        Destroy(dimension.gameObject);
        dimensionName = dimName;
        SpawnInit();

        //Displayes the dialogue
        this.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    void GenerateAndClean()
    {
        Bounds camBounds = cam.OrthographicBounds();

        //right of camera
        while (spawned[spawned.Count - 1].endPos.x < camBounds.max.x)
        {
            var nextSection = dimension.NextSection();
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
            var nextSection = dimension.NextSection();
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
