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
        }

        ladderDown = true;
    }
}
