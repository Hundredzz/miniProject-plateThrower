using UnityEngine;

public class DeleteChild : MonoBehaviour
{
    private Renderer[] childRenderers;
    private float canStartCheckingTime;
    public float safeDelay = 1.0f; // รอ 1 วินาทีก่อนเริ่มเช็ก

    void Start()
    {
        childRenderers = GetComponentsInChildren<Renderer>();
        canStartCheckingTime = Time.time + safeDelay; // ตั้งเวลาเริ่มเช็ก
    }

    void Update()
    {
        // ถ้ายังไม่ถึงเวลาที่กำหนด ให้ข้ามการเช็กไปก่อน
        if (Time.time < canStartCheckingTime) return;
        if (childRenderers == null || childRenderers.Length == 0) return;

        bool isAnyChildVisible = false;

        foreach (Renderer ren in childRenderers)
        {
            // เช็กว่า Renderer ยังมีตัวตนและถูกมองเห็นในกล้องหลักหรือไม่
            if (ren != null && ren.isVisible)
            {
                isAnyChildVisible = true;
                break; 
            }
        }

        if (!isAnyChildVisible)
        {
            // ตรวจสอบอีกครั้งว่าหน้าต่าง Scene View ไม่ได้เปิดดูอยู่
            Destroy(gameObject);
        }
    }
}
