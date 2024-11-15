using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] int value;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // start check point
            if (value == 0)
            {
                
                GameManager.Instance.InStart();
            }
            // finish check point
            else
            {
                Player.Instance.DisControlable();
                LvManager.Instance.End();
                AnimationManager.Instance.PlayEndAnim();
                

            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // start check point
            if (value == 0)
            {
                GameManager.Instance.OutStart();
            }
            //finish check point
            else
            {
                GameManager.Instance.OutFinish();
            }
        }
    }
}
