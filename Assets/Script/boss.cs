using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    #region Singleton
    private static boss _instance;
    public static boss Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {

        }
        else
        {
            _instance = this;
        }
        points = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.transform.childCount; i++)
        {
            points[i] = ways.transform.GetChild(i).transform;
        }
    }

    #endregion Singleton
    private Animator animator;
    [SerializeField] private float speed = 2f;
    private Vector3 pos;
    [SerializeField] GameObject ways;
    private Transform[] points;
    public int index;
    private int count,scale;
    // Start is called before the first frame update
   
    void Start()
    {
        animator = GetComponent<Animator>();
        count = points.Length;

        pos = points[index].position;
        scale = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime*scale;
        transform.position = Vector3.MoveTowards(transform.position, pos, step);
        if (transform.position == pos &&speed!=7) NextPoint();
        
    }
    void NextPoint()
    {
       
        index++;
        if (index == count) index = 0;
        Vector3 temp1 = pos - points[(index - 2 + count) % count].position;
        Vector3 temp2 = pos - points[index].position;

        pos = points[index].position;
        if (temp1.y < -1f)
        {
            if (temp2.x > 1f) Flip(-90);
            else Flip(90);
            return;
        }
        if (temp1.y > 1f)
        {
            if (temp2.x < -1f) Flip(-90);
            else Flip(90);
            return;
        }
        if (temp1.x < -1f)
        {
            if (temp2.y > 1f) Flip(90);
            else Flip(-90);
            return;
        }
        if (temp1.x > 1f)
        {
            if (temp2.y < -1f) Flip(90);
            else Flip(-90);
            return;
        }
    }
    public void PlayDeath()
    {
        
      if(index==2)  StartCoroutine(death());
    }
    IEnumerator death()
    {
        pos= new Vector3(transform.position.x, transform.position.y-2f, transform.position.z);
        speed = 7;
        yield return new WaitForSeconds(0.3f);
        AudioManager.Instance.GetAudioSourceReference().mute = true;
        AudioManager.Instance.PlaySFX("hurtOver");
        animator.SetTrigger("death");
        // Yield for 0.5 seconds
        yield return new WaitForSeconds(1.5f);
        yield return new WaitForSeconds(0.3f);

        bossvideo.Instance.PlayEnd();
        // Call the Check() function after the delay
        Destroy(gameObject);
       
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.Instance.StunHead();
   
        }
    }
    private void Flip(float angle)
    {

        transform.Rotate(0.0f, 0.0f, angle);
    }
    public void PlayAwake()
    {
        StartCoroutine(awake());
    }
    IEnumerator awake()
    {
        animator.SetTrigger("weakup");
        
        // Yield for 0.5 seconds
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("idle");
        // Call the Check() function after the delay
        scale = 1;
    }
}

