using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform centerPoint;
    public float radius = 5f;
    public float rotationSpeed = 1f;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        angle /= Mathf.Rad2Deg;
      
            float x = centerPoint.position.x + radius * Mathf.Cos(angle);
            float y = centerPoint.position.y + radius * Mathf.Sin(angle);
            transform.position = new Vector3(x, y, transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {
       
            angle += Time.deltaTime * rotationSpeed;
            float x = centerPoint.position.x + radius * Mathf.Cos(angle);
            float y = centerPoint.position.y + radius * Mathf.Sin(angle);
            transform.position = new Vector3(x, y, transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.gameObject.tag == "Player")
        {
            other.transform.SetParent(this.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other!=null && other.gameObject.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}
