using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MessageScene : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Message;
    // Start is called before the first frame update
    void Start()
    {
        Message.text = AppDataHolder.instance.Message;
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main");
    }
}
