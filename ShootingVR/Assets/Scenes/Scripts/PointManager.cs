using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class PointManager : MonoBehaviour {

    [SerializeField]
    private int removePoints=50;
    [SerializeField]
    private GameObject infoBoad;
    [SerializeField]
    private GameObject boadSpowner;
    [SerializeField]
    private int totalPoint;

    private string[] originRanking ={"1.","2.","3.","4.","5."};
    
    class PointInfo
    {
        public int point;
        public bool isNowPoint;

        public PointInfo(int point,bool isNowPoint)
        {
            this.point = point;
            this.isNowPoint = isNowPoint;
        }
    }

    // Use this for initialization
    void Start () {
        PMInit();
	}

    public void PMInit()
    {
        totalPoint = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddPoint(int point)
    {
        totalPoint += point;
    }

    public void RemovePoint()
    {
        totalPoint -= removePoints;
    }

    public void AppearResult()
    {
        var readRanking = ReadRanking();
        var sortRanking = SortRanking(readRanking);
        var rankingText = RankingText(sortRanking);
        WriteRanking(sortRanking);
        WriteBoad(rankingText);
    }

    private void WriteBoad(string rankingText)
    {
        var instantBoad = Instantiate(infoBoad, boadSpowner.transform.position, boadSpowner.transform.rotation);
        instantBoad.name = "InfoBoad";
        instantBoad.GetComponent<InfoBoad>().WriteInfo(rankingText);
    }

    private PointInfo[] ReadRanking()
    {
        int count = 0;
        PointInfo[] nextRanking = new PointInfo[6];
        for (; count < originRanking.Length; count++)
        {
            nextRanking[count] = new PointInfo(PlayerPrefs.GetInt(originRanking[count]), false);
        }
        nextRanking[count] = new PointInfo(totalPoint, true);
        return nextRanking;
    }

    private List<PointInfo> SortRanking(PointInfo[] ranking)
    {
        var rankingList = new List<PointInfo>();
        rankingList.AddRange(ranking);
        var c = new Comparison<PointInfo>(Compare);
        rankingList.Sort(c);

        return rankingList;
    }

    static int Compare(PointInfo a,PointInfo b)
    {
        return b.point - a.point;
    }

    private string RankingText(List<PointInfo> ranking)
    {
        string rankingText = "けっかはっぴょう";
        for(int count = 0; count < 5; count++)
        {
            if (ranking[count].isNowPoint)
            {
                rankingText += String.Format("<color=red>\n{0}あなた:{1:D4}てん</color>", originRanking[count], ranking[count].point);
            }
            else
            {
                rankingText += String.Format("\n{0}だれか:{1:D4}てん", originRanking[count], ranking[count].point);
            }
        }
        rankingText += String.Format("\n\nあなた:{0:D4}てん", totalPoint);

        return rankingText;
    }

    private void WriteRanking(List<PointInfo> ranking)
    {
        for(int count = 0; count < 5; count++)
        {
            PlayerPrefs.SetInt(originRanking[count], ranking[count].point);
        }
    }
}
