using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy3 : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float speed=2f;
    private Vector3 pos;
    [SerializeField] GameObject ways;
    private Transform[] points;
    public int index ;
    private int count;
    private bool isStun;
    // Start is called before the first frame update
    private void Awake()
    {
        points = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.transform.childCount; i++)
        {
            points[i] = ways.transform.GetChild(i).transform;
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        count = points.Length;
        
        pos = points[index].position;
        animator.SetTrigger("idle");
        isStun = false;
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;
        if(isStun) { step = 0; }
        transform.position = Vector3.MoveTowards(transform.position, pos, step);
        if (transform.position == pos) NextPoint();
    }
    void NextPoint()
    {
        
        index++;
        if (index == count) index = 0;
        Vector3 temp1 = pos - points[(index-2+count)%count].position;
        Vector3 temp2 = pos - points[index].position;
        
        pos = points[index].position;
        if (temp1.y < -1f)
        {
            if (temp2.x > 1f) Flip(-90);
            else Flip(90);
            return;
        }
        if(temp1.y > 1f)
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
    IEnumerator death()
    {
        
        // Yield for 0.5 seconds
        yield return new WaitForSeconds(1f);

        // Call the Check() function after the delay
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isStun=true;
            animator.SetTrigger("death");
            Player.Instance.StunHead();
            
            StartCoroutine(death());

        }
    }
    private void Flip(float angle)
    {
        
        transform.Rotate(0.0f,0.0f, angle);
    }
    public void PlayStun()
    {
        StartCoroutine(stun());
    }
    IEnumerator stun()
    {
        animator.SetTrigger("stun");
        isStun=true;
        // Yield for 0.5 seconds
        yield return new WaitForSeconds(5f);
        animator.SetTrigger("idle");
        // Call the Check() function after the delay
        isStun = false;
    }
}
