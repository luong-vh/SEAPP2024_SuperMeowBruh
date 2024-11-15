using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1 : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
