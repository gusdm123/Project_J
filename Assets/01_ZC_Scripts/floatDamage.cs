using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floatDamage : MonoBehaviour
{
    public float moveSpeed;
    public float destroyTime;
    public Text FloatTextPrint;
    private Vector3 vector = new Vector3(0f,0f,0f);

    public void print(string Text)
    {
        FloatTextPrint.text = string.Format(" {0}", Text);
    }

    void Update()
    {       
        vector.Set(FloatTextPrint.transform.position.x, FloatTextPrint.transform.position.y + (moveSpeed + Time.deltaTime), FloatTextPrint.transform.position.z);

        FloatTextPrint.transform.position = vector;
        destroyTime -= Time.deltaTime;

        if (destroyTime <= 0)
        {
           Destroy(this.gameObject);
        }
    }
}
