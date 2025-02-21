using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class Hud : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timer = null;

    private float time = 0.0f;
    public bool isTimerRunning = true;

    public GameObject player;
    public GameObject timerStopCube;

    void Start()
    {
        RectTransform rectTransform = transform as RectTransform;
        rectTransform.anchoredPosition = new Vector2(0.0f, 300.0f);
        rectTransform.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.InOutQuad);
    }

    public void OnButtonClick()
    {
        GameController.Instance.PlayerDied(player);
    }

    void Update()
    {
        if (isTimerRunning)
        {
            time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(time);
            timer.text = span.ToString(@"mm\:ss\.ff");
        }
    }
}