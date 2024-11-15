using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance => _instance;

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
   private int currentLevel;
    //bool isInStartChecker, isInFinishChecker;
   private GameData _gameData;
    public const string DATA_KEY = "DATA_KEY";
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(DATA_KEY))
        {
            //nếu có

            //JSON hóa từ string đã lưu thành class
            string savedJSON = PlayerPrefs.GetString(DATA_KEY);
           
            //gán nó cho _gameData
            this._gameData = JsonUtility.FromJson<GameData>(savedJSON);
        }
        else
        {
           Init();
        }
       
    }

    // Update is called once per frame
    void Update()
    {
  
    }
    public void increaseCoin(int c)
    {
        _gameData.coin += c;
        SaveData();
    }
    public void decreaseCoin(int c)
    {
        _gameData.coin -= c;
        SaveData();

    }
    public int getCoin()
    {
        return _gameData.coin;        
    }
    public void increaseBell(int b)
    {
        _gameData.bell += b;
        SaveData();
    }
    public void decreaseBell(int b)    
    {
        _gameData.bell -= b;
        SaveData();
    }  
    public int getBell() { return _gameData.bell; }
    public void Load(string name)
    {
        SenceController.Instance.LoadSence(name);
    }
    public void InStart()
    {
       // isInStartChecker = true;
    }
    public void OutStart()
    {
      //  isInStartChecker = false;
    }
    public void InFinish()
    {
        
        // isInFinishChecker = true;
        if (LvManager.Instance.getLv()== _gameData.level)
        {
            _gameData.level++;
            SaveData();
        }
        LvManager.Instance.End();
        SenceController.Instance.LoadSence("LevelSelectionMap");
    }
    public void OutFinish()
    {
      //  isInFinishChecker = false;
    }
    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
    }
    public int level() {  return _gameData.level; }
    public void Init()
    {
        this._gameData = new GameData();
        SaveData();

    }
    private void SaveData()
    {
        //JSON hóa data clas
        string dataJSON = JsonUtility.ToJson(this._gameData);
        //save JSON string
        
        PlayerPrefs.SetString(DATA_KEY, dataJSON);
    }
    public void Detele()
    {
        PlayerPrefs.DeleteAll();
        _instance._gameData = new GameData();
    }
    public bool ToggleMusic()
    {
        _gameData.isMuteMusic = !_gameData.isMuteMusic;
        SaveData();
        return _gameData.isMuteMusic;
    }
    public bool ToggleSfx()
    {
        _gameData.isMuteSfx = !_gameData.isMuteSfx;
        SaveData();
        return _gameData.isMuteSfx;
    }
    public bool isMuteMusic() {  return _gameData.isMuteMusic; }
    public bool isMuteSfx() {  return _gameData.isMuteSfx;}
    public void Wait(float t)
    {
        StartCoroutine(wait(t));
    }
    IEnumerator wait(float t)
    {
        yield return new WaitForSeconds(t);
    }
}
