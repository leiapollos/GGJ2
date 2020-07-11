using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DimensionTransition : MonoBehaviour
{
    Transform[] nums = new Transform[3];
    Vector3[] scales = new Vector3[3];
    Image whiteScreen;
    public float GrowMultiplier;
    public float TimerStart = 3;
    public float TransitionStart = 1, TransitionEnd = 0.2f;
    float timeSeg;
    bool transitioning;
    // Start is called before the first frame update
    void Start()
    {
        nums[0] = transform.Find("1");
        nums[1] = transform.Find("2");
        nums[2] = transform.Find("3");
        scales[0] = nums[0].localScale;
        scales[1] = nums[1].localScale;
        scales[2] = nums[2].localScale;
        timeSeg = TimerStart / 3;
        whiteScreen = transform.Find("white").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float timer = LevelManager.main.curTimer;
        if (timer <= TimerStart / 3)
        {
            nums[0].gameObject.SetActive(true);
            nums[1].gameObject.SetActive(false);
            nums[2].gameObject.SetActive(false);
            nums[0].localScale = EasingFunction.EaseInCubic(1, GrowMultiplier, timer / timeSeg) * scales[0];
        }
        else if (timer <= 2 * (TimerStart / 3))
        {
            nums[0].gameObject.SetActive(false);
            nums[1].gameObject.SetActive(true);
            nums[2].gameObject.SetActive(false);
            nums[1].localScale = EasingFunction.EaseInCubic(1, GrowMultiplier, timer / timeSeg - 1) * scales[1];
        }
        else if (timer <= TimerStart)
        {
            nums[0].gameObject.SetActive(false);
            nums[1].gameObject.SetActive(false);
            nums[2].gameObject.SetActive(true);
            nums[2].localScale = EasingFunction.EaseInCubic(1, GrowMultiplier, timer / timeSeg - 2) * scales[2];
        }
        else
        {
            nums[0].gameObject.SetActive(false);
            nums[1].gameObject.SetActive(false);
            nums[2].gameObject.SetActive(false);
        }
        if (!transitioning && timer <= TransitionStart)
        {
            transitioning = true;
            StartCoroutine(FadeInOut());
        }
    }

    IEnumerator FadeInOut()
    {
        float t = 0;
        while (t < TransitionStart)
        {
            var c = whiteScreen.color;
            c.a = EasingFunction.EaseInCubic(0, 1, t / TransitionStart);
            whiteScreen.color = c;
            yield return 0;
            t += Time.deltaTime;
        }
        t = 0;
        while (t < TransitionEnd)
        {
            var c = whiteScreen.color;
            c.a = EasingFunction.EaseOutCubic(1, 0, t / TransitionEnd);
            whiteScreen.color = c;
            yield return 0;
            t += Time.deltaTime;
        }
        transitioning = false;
    }
}
