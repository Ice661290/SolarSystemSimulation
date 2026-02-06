using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;        // เป้าหมายที่กล้องจะโฟกัส
    public float zoomSpeed = 10f;   // ความเร็วในการซูม
    public float rotateSpeed = 100f; // ความเร็วในการหมุนกล้อง
    public float minDistance = 5f;  // ระยะใกล้ที่สุดที่กล้องสามารถเข้าได้
    public float maxDistance = 50f; // ระยะไกลที่สุดที่กล้องสามารถออกได้

    private float currentDistance;  // ระยะห่างปัจจุบันของกล้อง
    private Vector3 currentRotation; // การหมุนปัจจุบันของกล้อง

    // Start is called before the first frame update
    void Start()
    {
        currentDistance = Vector3.Distance(transform.position, target.position);
        currentRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // การซูมเข้า-ออกด้วย Scroll Wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentDistance -= scroll * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // การหมุนกล้องด้วยการลากเมาส์
        if (Input.GetMouseButton(1)) // คลิกขวาเพื่อหมุน
        {
            float rotateHorizontal = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            float rotateVertical = -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

            currentRotation.x += rotateVertical;
            currentRotation.y += rotateHorizontal;
        }

        // คำนวณตำแหน่งและการหมุนของกล้อง
        Quaternion rotation = Quaternion.Euler(currentRotation);
        Vector3 position = target.position - (rotation * Vector3.forward * currentDistance);

        transform.rotation = rotation;
        transform.position = position;
    
    }
}
