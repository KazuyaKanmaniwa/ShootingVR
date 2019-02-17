using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUI : MonoBehaviour {

    private enum PointCondition
    {
        Add, Remove
    }
    [SerializeField]
    PointCondition pointCondition;
    [SerializeField]
    private float fadeSpeed = 0.01f;
    private TextMesh textMesh;
    private float alpha;
    private bool canAction;
    private float red, green, blue;
    private Enemy enemy;

    // Use this for initialization
    void Start()
    {
        alpha = 0;
        canAction = false;
        textMesh = GetComponent<TextMesh>();
        red = textMesh.color.r;
        green = textMesh.color.g;
        blue = textMesh.color.b;
        textMesh.color = new Color(red, green, blue, alpha);
        enemy = transform.root.gameObject.GetComponent<Enemy>();
        if (pointCondition == PointCondition.Add)
            textMesh.text = "+" + enemy.enemyPoint + "てん";
    }

    // Update is called once per frame
    void Update()
    {
        if (canAction)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            textMesh.color = new Color(red, green, blue, alpha);
            if (alpha < 0)
            {
                alpha = 0;
                canAction = false;
                if (pointCondition == PointCondition.Add)
                    Destroy(this.gameObject);
            }
        }
    }

    public void ActionUI()
    {
        if(pointCondition == PointCondition.Add)
            transform.parent = null;
        alpha = 1;
        canAction = true;
    }
}
