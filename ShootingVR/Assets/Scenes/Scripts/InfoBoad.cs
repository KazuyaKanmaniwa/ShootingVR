using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBoad : MonoBehaviour {

    [SerializeField]
    private GameObject infoText;
    private TextMesh textMesh;
    private static string _ruleText = "ルールせつめい\n60びょうかんで\nテキをうて！\nトリガーボタン\nながおしでスタート";
    public static string ruleText
    {
        get
        {
            return _ruleText;
        }
    }
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WriteInfo(string text)
    {
        textMesh = infoText.GetComponent<TextMesh>();
        if (textMesh != null)
            Debug.Log("test");
        textMesh.text = text;
    }
}
