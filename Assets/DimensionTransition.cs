using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DimensionTransition : MonoBehaviour
{
    public List<Transform> nums;
    public Image Glitch;
    Vector3[] scales = new Vector3[3];
    public Image whiteScreen;
    public float GrowMultiplier;
    public float TimerStart = 3;
    public float TransitionStart = 1, TransitionEnd = 0.2f;
    public float GlitchFrac = 0.2f;
    float timeSeg;
    bool transitioning;
    // Start is called before the first frame update
    void Start()
    {
        scales[0] = nums[0].localScale;
        scales[1] = nums[1].localScale;
        scales[2] = nums[2].localScale;
        timeSeg = TimerStart / 3;
    }

    // Update is called once per frame
    void Update()
    {
        var c = Glitch.color;
        float timer = LevelManager.main.curTimer;
        if (!transitioning)
        {
            if (timer <= TimerStart / 3)
            {
                /*
                nums[0].gameObject.SetActive(true);
                nums[1].gameObject.SetActive(false);
                nums[2].gameObject.SetActive(false);
                nums[0].localScale = EasingFunction.EaseInCubic(1, GrowMultiplier, timer / timeSeg) * scales[0];
                */
                c.a = 1 - timer / timeSeg < GlitchFrac ? 1 : 0;
            }
            else if (timer <= 2 * (TimerStart / 3))
            {
                /*
                nums[0].gameObject.SetActive(false);
                nums[1].gameObject.SetActive(true);
                nums[2].gameObject.SetActive(false);
                nums[1].localScale = EasingFunction.EaseInCubic(1, GrowMultiplier, timer / timeSeg - 1) * scales[1];
                */
                c.a = 1 - (timer / timeSeg - 1) < GlitchFrac ? 1 : 0;
            }
            else if (timer <= TimerStart)
            {
                /*
                nums[0].gameObject.SetActive(false);
                nums[1].gameObject.SetActive(false);
                nums[2].gameObject.SetActive(true);
                nums[2].localScale = EasingFunction.EaseInCubic(1, GrowMultiplier, timer / timeSeg - 2) * scales[2];
                */
                c.a = 1 - (timer / timeSeg - 2) < GlitchFrac ? 1 : 0;
            }
            else
            {
                /*
                nums[0].gameObject.SetActive(false);
                nums[1].gameObject.SetActive(false);
                nums[2].gameObject.SetActive(false);
                */
            }
            Glitch.color = c;
        }

        if (!transitioning && timer <= TransitionStart)
        {
            transitioning = true;
            StartCoroutine(FadeInOut());
        }
    }

    IEnumerator FadeInOut()
    {
        var c = Glitch.color;
        c.a = 1;
        Glitch.color = c;
        yield return new WaitForSeconds(TransitionStart);
        c.a = 0;
        Glitch.color = c;

        /*
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
        */
        transitioning = false;
    }
}
