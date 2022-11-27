using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            print("quit");
            Application.Quit(); // this function is ignored in editor, see documentation
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }
}
