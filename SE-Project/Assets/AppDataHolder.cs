using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppDataHolder : MonoBehaviour
{
    public static AppDataHolder instance { private set; get; }

    public string CSVRoute;

    public string Message;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SwitchErrorScene(string errorMessage)
    {
        Message = errorMessage;
        SceneManager.LoadScene("Error");
    }
    public void SwitchFinishScene(string finishMessage)
    {
        Message = finishMessage;
        SceneManager.LoadScene("Finish");
    }
}
