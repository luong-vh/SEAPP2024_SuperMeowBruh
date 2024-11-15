using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torotoki : MonoBehaviour
{
    public GameObject otherGameObject; // Đối tượng cần kiểm tra
    private void Start()
    {
       
    }
    void FixedUpdate()
    {
        // Lấy vị trí của đối tượng gốc và đối tượng khác
        Vector3 thisPosition = transform.position;
        Vector3 otherPosition = otherGameObject.transform.position;

        // Tính vector từ đối tượng gốc tới đối tượng khác
        Vector3 directionToOther = otherPosition - thisPosition;

        // Sử dụng tích chéo để xác định hướng
        float crossProduct = Vector3.Cross(transform.forward, directionToOther).y;
        if (crossProduct > 0 )
        {
            Flip(180);
        }
        
    }
    private void Flip(float angle)
    {

        transform.Rotate(0.0f, angle, 0.0f);
        
        
    }
}
