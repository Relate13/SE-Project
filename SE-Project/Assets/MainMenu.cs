using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField inputField;
    public void StartCompare()
    {
        string CSVRoute = inputField.text;
        if (File.Exists(CSVRoute))
        {
            AppDataHolder.instance.CSVRoute = CSVRoute;
            SceneManager.LoadScene("Compare");
        }
        else
        {
            AppDataHolder.instance.SwitchErrorScene("无法找到对应文件，请检查文件路径");
        }

    }
}
