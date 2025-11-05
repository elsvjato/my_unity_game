using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiMenu : MonoBehaviour
{

    public void ExitApp()
    {
        Application.Quit();
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
