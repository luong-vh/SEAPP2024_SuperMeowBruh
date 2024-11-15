using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int coin,level,bell;
    public bool isMuteMusic, isMuteSfx;
    public GameData()
    {
        
        Init();

    }
    public void Init()
    {
        coin = 0;
        bell = 0;
        level = 0;
        isMuteMusic = false;
        isMuteSfx = false;
    }
}
