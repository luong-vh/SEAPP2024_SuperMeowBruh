using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;
public class TextManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinName;
    [SerializeField] private TMP_Text bellName;
    // Start is called before the first frame update
    void Start()
    {

        coinName.text = GameManager.Instance.getCoin().ToString();
        bellName.text = GameManager.Instance.getBell().ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        coinName.text = GameManager.Instance.getCoin().ToString();
        bellName.text = GameManager.Instance.getBell().ToString();
    }
}
