using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Sprite onMusic, offMusic, onSfx, offSfx, onMusicPressed, offMusicPressed, onSfxPressed, offSfxPressed;
    [SerializeField] private Button music, sfx;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        UpdateUI();
    }
    public void pauseButton()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    public void resumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Load(string name)
    {
        SenceController.Instance.LoadSence(name);
    }
    public void PlayMusic(string name)
    {
        AudioManager.Instance.PlayMusic(name);
    }
    public void PlaySfx(string name)
    {
        AudioManager.Instance.PlaySFX(name);
    }
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
        UpdateUI();
    }
    public void ToggleSfx()
    {
        AudioManager.Instance.ToggleSfx();
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (GameManager.Instance.isMuteMusic())
        {
            music.GetComponent<Image>().sprite = offMusic;
            SpriteState state = music.spriteState;
            state.pressedSprite = offMusicPressed;
            music.spriteState = state;
        }
        else
        {
            music.GetComponent<Image>().sprite = onMusic;
            SpriteState state = music.spriteState;
            state.pressedSprite = onMusicPressed;
            music.spriteState = state;
        }
        if (GameManager.Instance.isMuteSfx())
        {
            sfx.GetComponent<Image>().sprite = offSfx;
            SpriteState state = sfx.spriteState;
            state.pressedSprite = offSfxPressed;
            sfx.spriteState = state;
        }
        else
        {
            sfx.GetComponent<Image>().sprite = onSfx;
            SpriteState state = sfx.spriteState;
            state.pressedSprite = onSfxPressed;
            sfx.spriteState = state;
        }
    }
}
