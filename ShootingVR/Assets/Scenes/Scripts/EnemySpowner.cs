using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpowner : MonoBehaviour {

    [SerializeField]
    private float timeOut = 2f;
    private float timeElapsed;
    private float fildRadius = 20f;
    private ArrayList enemyList;
    [SerializeField]
    private int enemyListMax = 5;
    private PhaseManager phaseManager;

    private System.Random randomSeed
    {
        get
        {
            return new System.Random((int)Time.realtimeSinceStartup);
        }
    }

    [SerializeField]
    GameObject enemyGreen;
    [SerializeField]
    GameObject enemyRed;
    [SerializeField]
    GameObject enemyPink;
    private int instantCount;

    // Use this for initialization
    void Start()
    {
        enemyList = new ArrayList();
        phaseManager = GameObject.Find("GameMaster").GetComponent<PhaseManager>();
        SpownerInit();
    }

    // Update is called once per frame
    void Update()
    {
        if(phaseManager._nowPhase==PhaseManager.Phase.GamePhase)
            RandomSpown();
    }

    public void SpownerInit()
    {
        instantCount = 1;
    }

    void RandomSpown()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeOut)
        {
            var random_angle = randomSeed.Next(360);
            var random_height = randomSeed.Next(5, 15);
            var spown_coordinate = new Vector3(fildRadius * Mathf.Cos(random_angle), random_height, fildRadius * Mathf.Sin(random_angle));
            if (enemyList.Count < enemyListMax)
            {
                GameObject instantiate_enemy=null;
                if (instantCount % 25 == 0)
                {
                    instantiate_enemy = Instantiate(enemyRed, spown_coordinate, Quaternion.identity);
                }else if (instantCount % 10 == 0)
                {
                    instantiate_enemy = Instantiate(enemyPink, spown_coordinate, Quaternion.identity);
                }
                else
                {
                    instantiate_enemy = Instantiate(enemyGreen, spown_coordinate, Quaternion.identity);
                }

                instantCount++;

                if(instantiate_enemy!=null)
                    enemyList.Add(instantiate_enemy);
            }
            timeElapsed = 0f;
        }
    }

    public void RemoveEnemy(GameObject remove_enemy)
    {
        var enemy_list_index = enemyList.IndexOf(remove_enemy);
        if (enemy_list_index < 0) return;
        enemyList.RemoveAt(enemy_list_index);
    }
}
