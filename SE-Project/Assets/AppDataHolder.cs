using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppDataHolder : MonoBehaviour
{
    public static AppDataHolder instance { private set; get; }

    public string CSVRoute;


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
}
