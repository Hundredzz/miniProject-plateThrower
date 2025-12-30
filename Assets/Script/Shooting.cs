using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
public GameObject bulletPrefab; // ลาก Prefab กระสุนมาใส่
    public Transform firePoint;     // จุดที่กระสุนออกจากปืน
    public float bulletSpeed = 20f;
    public float xSensitivity = -0.1f; 

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) 
        {
            ShootTowardTarget();
        }
    }

    void ShootTowardTarget()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 shootDirection = (targetPosition - firePoint.position).normalized;

            // 1. คำนวณหาตำแหน่งเมาส์เทียบกับจุดกึ่งกลางจอ (Y-axis)
            // ผลลัพธ์: กลางจอ = 0, บนจอ = ค่าบวก, ล่างจอ = ค่าลบ
            float distanceFromCenterY = mousePos.y - (Screen.height / 2f);

            // 2. แปลงค่าตำแหน่งเป็นองศาแกน Z (ยิ่งเมาส์สูง ค่ายิ่งต่ำ)
            // เราจะใช้ตัวคูณ (Sensitivity) เพื่อกำหนดความแรงในการหมุน
            // เช่น ถ้าคูณด้วย -0.1: เมาส์สูงขึ้น 100 pixel มุม Z จะลดลง -10 องศา
            
            float xRotation = distanceFromCenterY * xSensitivity - 90f;

            // 3. คำนวณการหมุนหันไปหาเป้าหมาย
            Quaternion targetRotation = Quaternion.LookRotation(shootDirection);

            // 4. ผสมมุม (X = -90, Y = 0, Z = คำนวณจากเมาส์)
            Quaternion finalRotation = targetRotation * Quaternion.Euler(xRotation, 0f, 0f);

            // 5. สร้างกระสุน
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, finalRotation);

            // 6. ใส่ความเร็ว (Unity 6)
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = shootDirection * bulletSpeed;
            }
        }
    } 
}
