using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvData 
{
    public float time, num;
    public List<bool> isBell;
    public LvData() {
        time = -1;
        isBell = new List<bool>();
    }
}
