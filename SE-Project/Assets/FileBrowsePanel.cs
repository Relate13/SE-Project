using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AnotherFileBrowser.Windows;
public class FileBrowsePanel : MonoBehaviour
{
    public TMP_InputField inputField;
    [SerializeField]
    private string FileFilter;
    // Start is called before the first frame update
    public void SelectFile()
    {
        var bp = new BrowserProperties();
        bp.filter = FileFilter;
        bp.filterIndex = 0;
        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            inputField.text= path;
            //Debug.LogWarning(path);
        });
    }
}
