using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour {

    private PointManager pointManager;
    private TimeManager timeManager;
    private EnemySpowner enemySpowner;
    private GoDevice goDevice;
    private InfoUI infoUI;
    [SerializeField]
    public float holdTimeMax = 1f;
    private float holdTime;
    [SerializeField]
    private GameObject infoBoad;
    [SerializeField]
    private GameObject boadSpowner;
    private GameObject instantInfoBoad;

    public enum Phase
    {
        StartPhase, CountDown, GamePhase, ResultPhase
    }

    [SerializeField]
    private Phase nowPhase;

    public Phase _nowPhase
    {
        get
        {
            return nowPhase;
        }
    }

    // Use this for initialization
    void Start()
    {
        infoUI = GameObject.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor/InfoUI").GetComponent<InfoUI>();
        goDevice = GameObject.Find("OVRCameraRig/TrackingSpace").GetComponent<GoDevice>();
        pointManager = GetComponent<PointManager>();
        timeManager = GetComponent<TimeManager>();
        nowPhase = Phase.StartPhase;
        holdTime = holdTimeMax;
        instantInfoBoad = Instantiate(infoBoad, boadSpowner.transform.position, boadSpowner.transform.rotation);
        instantInfoBoad.name = "InfoBoad";
        instantInfoBoad.GetComponent<InfoBoad>().WriteInfo(InfoBoad.ruleText);
        enemySpowner = GameObject.Find("EnemySpowner").GetComponent<EnemySpowner>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((nowPhase == Phase.StartPhase || nowPhase == Phase.ResultPhase))
        {
            if (goDevice._isHoldTrigger)
            {
                holdTime -= Time.deltaTime;
                if (holdTime < 0)
                {
                    NextPhase();
                    holdTime = holdTimeMax;
                }
            }
            else
            {
                holdTime = holdTimeMax;
            }
        }
    }

    public void NextPhase()
    {
        int phaseCount = (int)nowPhase;
        if (phaseCount < (int)Phase.ResultPhase)
        {
            phaseCount++;

        }
        else
            phaseCount = 0;
        nowPhase = (Phase)phaseCount;

        switch (nowPhase)
        {
            case Phase.StartPhase:
                pointManager.PMInit();
                timeManager.TMInit();
                enemySpowner.SpownerInit();
                GameObject.Find("InfoBoad").GetComponent<InfoBoad>().WriteInfo(InfoBoad.ruleText);
                break;
            case Phase.CountDown:
                Destroy(GameObject.Find("InfoBoad").gameObject);
                infoUI.CountDown();
                break;
            case Phase.GamePhase:
                break;
            case Phase.ResultPhase:
                infoUI.SayEnd();
                pointManager.AppearResult();
                break;
        }
    }
}
