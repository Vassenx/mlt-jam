using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    bool ladderDown = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ladderDown)
        {
            this.GetComponent<Animator>().SetBool("pullLadder", true);
            this.GetComponent<AudioSource>().Play();

            StartCoroutine(Waity(collision));

        }

        ladderDown = true;
    }

    IEnumerator Waity(Collider2D collision)
    {
        yield return new WaitForSeconds(2);
        collision.gameObject.transform.localPosition = new Vector3(16.6f, 10.29f, -11f);
    }
}
