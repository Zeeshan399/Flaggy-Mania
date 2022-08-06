using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    void update()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
    }


}
