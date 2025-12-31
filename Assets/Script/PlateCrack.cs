using UnityEngine;

public class PlateCrack : MonoBehaviour
{
    public float lifeTime = 5f;
    private bool hasCracked = false; // ตัวล็อกป้องกันการทำงานซ้ำ
    public GameObject crackedPlate;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // ฟังก์ชันนี้จะทำงานเมื่อ Rigidbody ของกระสุนชนกับวัตถุอื่น
    void OnCollisionEnter(Collision collision)
    {
        if (hasCracked) return;
        hasCracked = true;
        Destroy(gameObject);
        Instantiate(crackedPlate,transform.position, transform.rotation);
    }
}
