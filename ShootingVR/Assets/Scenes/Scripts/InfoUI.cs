using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUI : MonoBehaviour {

    private TextMesh textMesh;
    private PhaseManager phaseManager;
    private bool beginCountDown;
    private bool canSayEnd;
    private float endCount;
    private float startCount;
    private float red, green, blue;

    // Use this for initialization
    void Start () {
        textMesh = GetComponent<TextMesh>();
        red = textMesh.color.r;
        green = textMesh.color.g;
        blue = textMesh.color.b;
        textMesh.color = new Color(red, green, blue, 0);
        phaseManager = GameObject.Find("GameMaster").GetComponent<PhaseManager>();
        UIInit();
	}

    private void UIInit()
    {
        beginCountDown = false;
        canSayEnd = false;
        endCount = 2;
        startCount = 4f;
    }
	
	// Update is called once per frame
	void Update () {
        if (beginCountDown)
        {
            startCount -= Time.deltaTime;
            if (startCount < 0)
            {
                textMesh.color = new Color(red, green, blue, 0);
                UIInit();
                phaseManager.NextPhase();
                
            }else if (startCount < 1)
            {
                textMesh.text = "スタート！";
            }
            else
            {
                textMesh.text = ((int)startCount).ToString();
            }
        }

        if (canSayEnd)
        {
            endCount -= Time.deltaTime;
            textMesh.text = "しゅうりょう！";
            if (endCount < 0)
            {
                textMesh.color = new Color(red, green, blue, 0);
                UIInit();
            }
        }
	}

    public void CountDown()
    {
        textMesh.color = new Color(red, green, blue, 1);
        beginCountDown = true;
    }

    public void SayEnd()
    {
        textMesh.color = new Color(red, green, blue, 1);
        canSayEnd = true;
    }
}
