using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManage : MonoBehaviour
{
    public GameObject finalPanel;

    // Start is called before the first frame update
    void Start()
    {
        finalPanel.SetActive(false);
    }

    public void Credits()
    {
        SceneManager.LoadScene(2);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            finalPanel.SetActive(true);
            StartCoroutine(Waity());
        }
    }

    IEnumerator Waity()
    {
        yield return new WaitForSeconds(2);
        Credits();
    }
}
