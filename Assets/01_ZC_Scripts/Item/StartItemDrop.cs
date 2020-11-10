using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartItemDrop : MonoBehaviour
{
    public float speed = 2f;

    private float y = 0f;

    public IEnumerator StartRotation(float time)
    {
        while (true)
        {
            y = speed * Time.deltaTime;

            transform.Rotate(0, y, 0);          

            yield return new WaitForSeconds(Time.deltaTime);
            time -= Time.deltaTime;

            if (time < 0f)
                break;
        }

        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Backpack")
        {
            Backpack.instance.itemCount--;
            gameObject.SetActive(false);
        }
    }
}
