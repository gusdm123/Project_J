using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachineManager : MonoBehaviour
{
    public GameObject[] slotObject;
    public GameObject[] slotObjectParent;
    // Start is called before the first frame update

    private int slotValue;
    private int slotNum;
    void Start()
    {
        slotValue = Backpack.instance.gold;
        slotNum = slotObject.Length;

        for (int j = 0; j < slotObject.Length; j++)
        {
            if (slotValue / (int)Mathf.Pow(10, (slotObject.Length - j) - 1) % 10 == 0)
            {
                slotObjectParent[(slotObject.Length - j) - 1].SetActive(false);
                slotNum--;
            }
            else
                break;
        }

        for (int i = 0; i < slotNum; i++)
        {
            StartCoroutine(PlaySlot01(i,slotValue));
        }
    } 

    IEnumerator PlaySlot01(int num,int target)
    {
        slotObject[num].transform.Translate(0f, 50f * Random.Range(1,19), 0f);

        float slotTime = 2f + num * 0.5f;

        while (true)
        {
            slotObject[num].transform.Translate(0f, 50f, 0f);

            if (slotObject[num].transform.localPosition.y >= 1000f)
                slotObject[num].transform.localPosition += new Vector3(0f, -1000f, 0f);

            yield return new WaitForSeconds(0.02f);
            slotTime -= 0.02f;

            if (slotTime <= 0)
            {
                break;
            }
        }

        slotObject[num].transform.localPosition = new Vector3(slotObject[num].transform.localPosition.x, target / (int)Mathf.Pow(10, (slotNum - num) - 1) % 10 * 100, slotObject[num].transform.localPosition.z);
    }
}
