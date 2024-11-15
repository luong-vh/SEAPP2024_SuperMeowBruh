using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject setting,reset,quit;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("startMenu");
        AudioManager.Instance.PlayMusic("StartMenu");
        setting.SetActive(false);
        reset.SetActive(false);
        quit.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void settingButton()
    {
        setting.SetActive(true);
    }
    public void backButton()
    {
        setting.SetActive(false);
    }
    public void resetButton()
    {
        reset.SetActive(true);
        quit.SetActive(false);
    }
    public void noResetButton()
    {
        reset.SetActive(false);
    }
    public void quitButton()
    {
        quit.SetActive(true);
        reset.SetActive(false);
    }
    public void noQuitButton()
    {
        quit.SetActive(false);
    }
    public void Load(string name)
    {
        SenceController.Instance.LoadSence(name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
