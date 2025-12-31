using UnityEngine;

public class DeleteOnNotSee : MonoBehaviour
{
    void OnBecameInvisible()
    {
        // ทำลายวัตถุนี้ทิ้งทันที
        Destroy(gameObject);
    }
}
