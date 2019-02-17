using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    [SerializeField]
    GameObject timerText;
    [SerializeField]
    private float gameTimeLimit = 60f;
    private float gameTimer;
    private TextMesh textMesh;
    private Color textRed;
    private Color textBlack;
    private PhaseManager phaseManager;

    private bool inTime
    {
        get
        {
            if (gameTimer > 0f)
                return true;
            else
                return false;
        }
    }

    // Use this for initialization
    void Start()
    {
        TMInit();
    }

    public void TMInit()
    {
        phaseManager = GetComponent<PhaseManager>();
        gameTimer = gameTimeLimit;
        textMesh = timerText.GetComponent<TextMesh>();
        textRed = new Color(255f / 255f, 0f / 255f, 0f / 255f);
        textBlack = new Color(0f / 255f, 0f / 255f, 0f / 255f);
    }

    // Update is called once per frame
    void Update()
    {
        if (inTime && phaseManager._nowPhase == PhaseManager.Phase.GamePhase)
        {
            CountDown();
            WriteTime();
        }
    }

    void CountDown()
    {
        gameTimer -= Time.deltaTime;
    }

    void WriteTime()
    {
        if (!inTime)
        {
            gameTimer = 0.0f;
            phaseManager.NextPhase();
        }
        string textTime = gameTimer.ToString("00.0");
        if (gameTimer < 10.0f)
            textMesh.color = textRed;
        else
            textMesh.color = textBlack;
        textMesh.text = textTime;
    }
}
