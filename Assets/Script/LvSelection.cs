using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvSelection : MonoBehaviour
{
    public List<GameObject> list;
    [SerializeField] private Sprite complete, completePressed, notcomplete, notcompletePressed;
    // Start is called before the first frame update
    void Start()
    {
        Button bt;
        SpriteState state;
        int n = GameManager.Instance.level();
        
        for(int i = 0; i < n; i++)
        {
            list[i].SetActive(true);
             bt = list[i].GetComponent<Button>();
            bt.GetComponent<Image>().sprite = complete;
            state = bt.spriteState;
            state.pressedSprite = completePressed;
            bt.spriteState = state;
        }
       if(n<7) {list[n].SetActive(true);
       bt = list[n].GetComponent<Button>();
        bt.GetComponent<Image>().sprite = notcomplete;
         state = bt.spriteState;
        state.pressedSprite = notcompletePressed;
            bt.spriteState = state;
        }
        AudioManager.Instance.PlayMusic("MusicMap");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
