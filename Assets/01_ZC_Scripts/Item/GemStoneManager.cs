using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemStoneManager : MonoBehaviour
{
    public GameObject sprite;

    private int gemstoneHeight = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Backpack.instance.gemStoneCount; i++)
        {
            GameObject gem = Instantiate(sprite, transform);

            if (i != 0)
            {
                if (i % 8 == 0)
                    gemstoneHeight++;
            }

            if (i != 0)
            {
                gem.transform.localPosition += new Vector3(50f * (i % 8), gemstoneHeight * -100f, 0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
