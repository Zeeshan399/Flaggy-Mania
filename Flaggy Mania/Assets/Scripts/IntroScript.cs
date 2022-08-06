using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float wait_timer = 6f;

    void Start()
    {
        StartCoroutine(wait_for_sec());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator wait_for_sec()
    {
        yield return new WaitForSeconds(wait_timer);
        SceneManager.LoadScene(1);

    }
}
