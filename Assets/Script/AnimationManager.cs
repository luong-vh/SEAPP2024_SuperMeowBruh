using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    #region Singleton
    private static AnimationManager _instance;
    public static AnimationManager Instance => _instance;

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
    [SerializeField] Animator sceneAnim;
    // Start is called before the first frame update
    void Start()
    {
        PlayStartAnim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayStartAnim()
    {
       
        StartCoroutine(StartAnim());
    }
    IEnumerator StartAnim()
    {
        sceneAnim.SetTrigger("start");
        yield return new WaitForSeconds(2f);
    }
    public void PlayEndAnim()
    {
      
        StartCoroutine(EndAnim());
    }
    IEnumerator EndAnim()
    {
       
        sceneAnim.SetTrigger("end");
        yield return new WaitForSeconds(2f);
     
        GameManager.Instance.InFinish();
    }
}
