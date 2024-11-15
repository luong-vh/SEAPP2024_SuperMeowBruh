using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SenceController : MonoBehaviour
{
    #region Singleton
    private static SenceController _instance;
    public static SenceController Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
          
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion Singleton    
        public void LoadSence(string senceName)
    {
        SceneManager.LoadScene(senceName);
    }
}
