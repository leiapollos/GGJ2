using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionTransition : MonoBehaviour
{
    Transform[] nums = new Transform[3];
    Vector3[] scales = new Vector3[3];
    public float GrowMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        nums[0] = transform.Find("1");
        nums[1] = transform.Find("2");
        nums[2] = transform.Find("3");
        scales[0] = nums[0].localScale;
        scales[1] = nums[1].localScale;
        scales[2] = nums[2].localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float timer = LevelManager.main.curTimer;
        if (timer <= 1)
        {
            nums[0].gameObject.SetActive(true);
            nums[1].gameObject.SetActive(false);
            nums[2].gameObject.SetActive(false);
            nums[0].localScale = EasingFunction.EaseInCubic(1, GrowMultiplier, timer) * scales[0];
        }
        else if (timer <= 2)
        {
            nums[0].gameObject.SetActive(false);
            nums[1].gameObject.SetActive(true);
            nums[2].gameObject.SetActive(false);
            nums[1].localScale = EasingFunction.EaseInCubic(1, GrowMultiplier, timer - 1) * scales[1];
        }
        else if (timer <= 3)
        {
            nums[0].gameObject.SetActive(false);
            nums[1].gameObject.SetActive(false);
            nums[2].gameObject.SetActive(true);
            nums[2].localScale = EasingFunction.EaseInCubic(1, GrowMultiplier, timer - 2) * scales[2];
        }
        else
        {
            nums[0].gameObject.SetActive(false);
            nums[1].gameObject.SetActive(false);
            nums[2].gameObject.SetActive(false);
        }
    }
}
