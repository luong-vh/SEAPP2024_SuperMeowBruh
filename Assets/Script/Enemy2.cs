using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float speed;
    private Vector3 pos;
    [SerializeField] GameObject ways;
    private Transform[] points;
    private int index, count;
    // Start is called before the first frame update
    private void Awake()
    {
        points = new Transform[ways.transform.childCount];
        for(int i = 0;i < ways.transform.childCount;i++)
        {
            points[i]=ways.transform.GetChild(i).transform;
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        count=points.Length;
        index = 1;
        pos = points[index].position;
    }

    // Update is called once per frame
    void Update()
    {
        var step= speed*Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pos, step);
        if (transform.position == pos) NextPoint();
    }
    void NextPoint()
    {
        
        index ++;
        if(index==count) index = 0;
        if ( points[index].position.x > pos.x)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else this.transform.eulerAngles = new Vector3(0, 0, 0);
        pos = points[index].position;
       
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
            Player.Instance.StunHead();
            animator.SetTrigger("death");
            StartCoroutine(death());

        }
    }
}
