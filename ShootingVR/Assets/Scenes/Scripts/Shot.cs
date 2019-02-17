using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    [SerializeField]
    private GameObject shotPoint;
    [SerializeField]
    private float shotIntervalMax = 0.5f;
    private float shotInterval;
    [SerializeField]
    GameObject bullet;
    private PhaseManager phaseManager;
    private GoDevice goDevice;

    // Use this for initialization
    void Start()
    {
        shotInterval = shotIntervalMax;
        phaseManager = GameObject.Find("GameMaster").GetComponent<PhaseManager>();
        goDevice = GetComponent<GoDevice>();
    }

    // Update is called once per frame
    void Update()
    {
        if (phaseManager._nowPhase == PhaseManager.Phase.GamePhase && goDevice._isHoldTrigger)
        {
            shotInterval -= Time.deltaTime;
            if (shotInterval < 0)
            {
                var instanceBullet = Instantiate(bullet, shotPoint.transform.position, shotPoint.transform.rotation);
                instanceBullet.name = "Bullet";
                shotInterval = shotIntervalMax;
            }
        }
    }
}
