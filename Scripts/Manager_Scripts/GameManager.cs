using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public DataController DataController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRestart()
    {
        DataController.SaveGameData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DataController.LoadGameData();
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
