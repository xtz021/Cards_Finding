using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void QuitApp()
    {
        Application.Quit();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

}
