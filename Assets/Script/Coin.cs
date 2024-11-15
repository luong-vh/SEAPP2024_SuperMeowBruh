using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value;
    public bool isRotate;
    public Transform centerPoint;
    private float radius = 1.5f;
    private float rotationSpeed=2f;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        angle/=Mathf.Rad2Deg;
        if (isRotate)
            {
            float x = centerPoint.position.x + radius * Mathf.Cos(angle);
            float y = centerPoint.position.y + radius * Mathf.Sin(angle);
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotate)
        {
            angle += Time.deltaTime * rotationSpeed;
            float x = centerPoint.position.x + radius * Mathf.Cos(angle);
            float y = centerPoint.position.y + radius * Mathf.Sin(angle);

            // Set the coin's position
            transform.position = new Vector3(x, y, transform.position.z);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.increaseCoin(value);
            AudioManager.Instance.PlaySFX("coinCollected");
            Destroy(gameObject);
        }
    }
  
}
