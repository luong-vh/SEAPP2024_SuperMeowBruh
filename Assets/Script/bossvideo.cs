using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class bossvideo : MonoBehaviour
{
    #region Singleton
    private static bossvideo _instance;
    public static bossvideo Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {

        }
        else
        {
            _instance = this;

        }
    }

    #endregion Singleton    
    public CinemachineVirtualCamera cinemachineCamera;
    public CinemachineVirtualCamera target;
    public GameObject objectToFollow;
    public GameObject[] above, below;
    public bool isPlay =false;
    bool isEnd = false;
    
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

        if (isEnd)
        {
            AudioManager.Instance.PlayMusic("Level7");
            AudioManager.Instance.GetAudioSourceReference().mute = false;
        }

    }
    IEnumerator start()
    {
        isPlay = true;
        Player.Instance.isAbleControl =false;
        for (int i = 0; i < above.Length; i++)
        {
            AudioManager.Instance.PlaySFX("hurt"); 
            yield return new WaitForSeconds(0.2f); 
            above[i].SetActive(true);
        }
        target.Priority = 11;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < below.Length; i++)
        {
            AudioManager.Instance.PlaySFX("hurt");
            yield return new WaitForSeconds(0.2f);
            below[i].SetActive(true);
        }
        target.Priority = 0;
        yield return new WaitForSeconds(2f);
        boss.Instance.PlayAwake();
        Player.Instance.isAbleControl = true;
    }
    IEnumerator end()
    {
        Player.Instance.isAbleControl = false;
        yield return new WaitForSeconds(2f);
        target.Priority = 11;
        
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < below.Length; i++) 
        { 
            AudioManager.Instance.PlaySFX("hurt");
            yield return new WaitForSeconds(0.2f); 
            below[i].SetActive(false); 
        }
        target.Priority = 0;
        yield return new WaitForSeconds(2f);
        Player.Instance.isAbleControl = true;
    }
    public void PlayEnd()
    {
        isEnd=true;
        StartCoroutine(end());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isPlay)
        {
            AudioManager.Instance.PlayMusic("MusicBoss");
            StartCoroutine(start());
        }   
    }


}
