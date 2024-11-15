using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class LvManager : MonoBehaviour
{
    #region Singleton
    private static LvManager _instance;
    public static LvManager Instance => _instance;

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
    
    [SerializeField] private string DATA_KEY;
    [SerializeField] private int num,lv;
    [SerializeField] private TMP_Text timeTextStart;
    [SerializeField] private TMP_Text timePause;
    [SerializeField] private TMP_Text timeTextFinish;
    [SerializeField] private TMP_Text coinTextStart;
    [SerializeField] private TMP_Text coinTextFinish;
    [SerializeField] private List<GameObject> bells;
    [SerializeField] private string musicName;
    private LvData _lvData;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic(musicName);
        currentTime = 0;
        
        if (PlayerPrefs.HasKey(DATA_KEY))
        {
            //nếu có

            //JSON hóa từ string đã lưu thành class
            string savedJSON = PlayerPrefs.GetString(DATA_KEY);

            //gán nó cho _gameData
            this._lvData = JsonUtility.FromJson<LvData>(savedJSON);
        }
        else
        {
            Init(num);
        }
        if (_lvData.time == -1)
        {
            timeTextStart.text = "Besttime ___";
        }
        else
        {
            timeTextStart.text = "Besttime "+_lvData.time.ToString("F2");
        }
        coinTextStart.text = GameManager.Instance.getCoin().ToString();
        for (int i = 0;i< bells.Count; i++)  
            if (_lvData.isBell[i] && bells[i] != null)
            {
                Renderer renderer = bells[i].GetComponent<Renderer>();
                renderer.material.color = new Color(1,1,1,0.7f);
            }  

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        timePause.text="time "+currentTime.ToString("F2")+"\n"+timeTextStart.text;
    }
    public void Init(int n)
    {

        this._lvData = new LvData();
        _lvData.num = n;
        for(int i = 0;i<n;i++) { _lvData.isBell.Add(false); }
        SaveData();

    }
    public bool setBell(int n)
    {
       if( _lvData.isBell[n] == true) return false;
        _lvData.isBell[n] = true;
        SaveData();
        return true;
    }
    public void setTime(float time)
    {
        _lvData.time = time;
        SaveData();
    }
    private void SaveData()
    {
        //JSON hóa data clas
        string dataJSON = JsonUtility.ToJson(this._lvData);
        //save JSON string
        PlayerPrefs.SetString(DATA_KEY, dataJSON);
    }
    public void End()
    {
        if(_lvData.time<0 ||  _lvData.time>currentTime)
        {
        _lvData.time = currentTime;
        }
        SaveData() ;
        coinTextFinish.text = GameManager.Instance.getCoin().ToString();
        timeTextFinish.text = "Time " + currentTime.ToString("F2");
    }
    public int getLv() { return lv; }
    public void penalty() { currentTime += 5f; }
}
