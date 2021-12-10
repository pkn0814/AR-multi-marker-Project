using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timerTxt;
    public Text End;
    public Image image;
    float image_time = 0f;
    float F_time = 1f;
    public float time = 20f;
    private float selectCountdown;

    void Start()
    {
        selectCountdown = time;
    }

    void Update()
    {
        if (Mathf.Floor(selectCountdown) <= 0)
        {
            Fade();
            End.text = "Merry\nChristmas";
            Invoke("retrygame", 3f); // 3초 후 재시작
        }
        else
        {
            selectCountdown -= Time.deltaTime;
            timerTxt.text = Mathf.Floor(selectCountdown).ToString() + "초";
        }
    }

    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }
    IEnumerator FadeFlow()
    {
        image.gameObject.SetActive(true);
        Color alpha = image.color; // 이미지의 투명도를 가져온다
        while (alpha.a < 1f)
        {
            image_time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, image_time);
            image.color = alpha;
            yield return null;
        }
        yield return null;
    }

    public void retrygame()
    {
        SceneManager.LoadScene("PresentScene"); // 씬 재시작
    }
}