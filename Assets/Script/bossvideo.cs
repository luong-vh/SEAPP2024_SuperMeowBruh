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
    public GameObject objectToFollow;
    public GameObject[] above, below;
    public BoxCollider2D triggerCollider;
    public GameObject pos;
    public Transform  a,b;
    Transform taget;
    float speed = 0;
    bool isEnd = false;
    
    // Start is called before the first frame update
    void Start()
    {
       
        

    }

    // Update is called once per frame
    void Update()
    {
        if(speed!=0) 
       { var step = speed * Time.deltaTime ;
            cinemachineCamera.transform.position = Vector3.MoveTowards(cinemachineCamera.transform.position, taget.position, step);
        if (cinemachineCamera.transform.position == b.position) if(!isEnd) StartCoroutine(NextPoint()); else StartCoroutine(end());
            if (cinemachineCamera.transform.position == a.position) 
            {
                cinemachineCamera.Follow = objectToFollow.transform;
                speed = 0;
                if (isEnd)
                {
                    AudioManager.Instance.PlayMusic("Level7");
                    AudioManager.Instance.GetAudioSourceReference().mute = false;
                }
            }
        }
    }
    IEnumerator NextPoint()
    {
        for (int i = 0; i < below.Length; i++) {
            AudioManager.Instance.PlaySFX("hurt"); 
            yield return new WaitForSeconds(0.2f);

            below[i].SetActive(true); 
        }
        
        taget =a;
        
    }
    IEnumerator start()
    {
        triggerCollider.enabled = false;
        for (int i = 0; i < above.Length; i++) { AudioManager.Instance.PlaySFX("hurt"); yield return new WaitForSeconds(0.2f); above[i].SetActive(true); }
        boss.Instance.PlayAwake();
        cinemachineCamera.Follow = null;
        taget = b;
        speed = 5;   
    }
    IEnumerator end()
    {
        
        for (int i = 0; i < below.Length; i++) { AudioManager.Instance.PlaySFX("hurt"); yield return new WaitForSeconds(0.2f);  below[i].SetActive(false); }

        taget = a;
    }
    public void PlayEnd()
    {
        isEnd=true;
        cinemachineCamera.Follow = null;
        taget = b;
        speed = 5;
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        a.position = cinemachineCamera.transform.position;
        b.position = new Vector3(a.position.x, a.position.y+15f, a.position.z);
        AudioManager.Instance.PlayMusic("MusicBoss");
        StartCoroutine(start());
    }


}
