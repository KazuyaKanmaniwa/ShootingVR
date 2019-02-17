using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private int _enemyPoint;
    private GameObject target;
    [SerializeField]
    private float moveSpeed = 5f;
    private EnemySpowner enemySpowner;
    private PointManager pointManager;
    private GameObject gameMaster;
    private PhaseManager phaseManager;
    private PointUI removePointUI;
    private PointUI addPointUI;

    public Transform targetPosition
    {
        get
        {
            return target.transform;
        }
    }

    public int enemyPoint
    {
        get
        {
            return _enemyPoint;
        }
    }

    // Use this for initialization
    void Start()
    {
        removePointUI = GameObject.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor/RemovePointText").GetComponent<PointUI>();
        addPointUI = transform.Find("AddPointText").GetComponent<PointUI>();
        gameMaster = GameObject.Find("GameMaster");
        pointManager = gameMaster.GetComponent<PointManager>();
        phaseManager = gameMaster.GetComponent<PhaseManager>();
        enemySpowner = GameObject.Find("EnemySpowner").GetComponent<EnemySpowner>();
        target = GameObject.Find("OVRCameraRig");
        transform.LookAt(targetPosition);
    }

    // Update is called once per frame
    void Update()
    {

        EnemyMove();

        if (phaseManager._nowPhase == PhaseManager.Phase.ResultPhase)
            DeleteEnemy();
    }

    void EnemyMove()
    {
        var velocity = transform.rotation * new Vector3(0, 0, moveSpeed);
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pointManager.RemovePoint();
            removePointUI.ActionUI();
            DeleteEnemy();
        }

        if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            pointManager.AddPoint(enemyPoint);
            addPointUI.ActionUI();
            DeleteEnemy();
        }
        
    }

    void DeleteEnemy()
    {
        enemySpowner.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }
}
