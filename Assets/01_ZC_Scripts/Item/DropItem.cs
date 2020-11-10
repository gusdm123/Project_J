using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private bool dropStart = true; // 드랍 시작
    private bool lootStart = false; // 루팅 단계 시작

    public GameObject dropItem;
    public float startSpeed;
    public float startDuringTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoroutineUpdate());
    }

    // Update is called once per frame
    IEnumerator CoroutineUpdate()
    {
        WaitForSeconds normalTime = new WaitForSeconds(Time.deltaTime);

        while (true)
        {
            

            if (dropStart == true)
            {
                dropStart = false;
                StartCoroutine(PlayDropDirector());
            }
            yield return normalTime;
        }
    }

    public void StartTest()
    {
        StartCoroutine(PlayDropDirector());
    }

    IEnumerator PlayDropDirector()
    {
        StartCoroutine(dropItem.GetComponent<StartItemDrop>().StartRotation(startDuringTime * 10));

        float duringTime = startDuringTime;
        float speed = startSpeed;

        while (true)
        {
            speed = Mathf.Lerp(speed,0f,Time.deltaTime * 3);
            //transform.Translate(Vector3.right * speed * Time.deltaTime);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
            duringTime -= Time.deltaTime;

            if (duringTime <= 0f)
                break;
        }

        yield return new WaitForSeconds(startDuringTime);

        duringTime = startDuringTime;
        speed = startSpeed;

        while (true)
        {
            speed = Mathf.Lerp(speed, 0f, Time.deltaTime * 3);
            transform.Translate(-Vector3.right * speed * Time.deltaTime);
            //transform.Translate(-Vector3.up * speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
            duringTime -= Time.deltaTime;

            if (duringTime <= 0f)
                break;
        }

        StartCoroutine(PlayLootDirector());
    }

    IEnumerator PlayLootDirector()
    {
        float duringTime = 0.3f;
        float speed = Time.deltaTime;

        while (true)
        {
            speed *= 1.02f; 
            transform.position = Vector3.Lerp(transform.position, UIManager.instance.lootBackPackWayPoint.transform.position, speed);
            yield return new WaitForSeconds(Time.deltaTime);

            duringTime -= Time.deltaTime;

            if (duringTime < 0f)
                break;
        }

        while (true)
        {
            speed *= 1.02f;
            transform.position = Vector3.Lerp(transform.position, UIManager.instance.lootBackPack.transform.position, speed);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
