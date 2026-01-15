using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float destroyTime = 1f;
    public TextMesh textMesh; // Dùng TextMesh cho đơn giản (không cần Canvas)

    private Vector3 offset = new Vector3(0, 2f, 0); // Xuất hiện cao hơn đầu một chút

    void Start()
    {
        // Tự động hủy sau 1 khoảng thời gian
        Destroy(gameObject, destroyTime);
        
        // Random nhẹ vị trí sang trái phải chút cho đỡ bị chồng lên nhau
        transform.position += new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
    }

    void Update()
    {
        // Bay lên từ từ
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }

    public void SetValue(int amount)
    {
        if (textMesh == null) textMesh = GetComponent<TextMesh>();

        if (amount >= 0)
        {
            textMesh.text = "+" + amount;
            textMesh.color = Color.green; // Màu xanh lá cho điểm cộng
        }
        else
        {
            textMesh.text = amount.ToString(); // Tự có dấu trừ rồi
            textMesh.color = Color.red;   // Màu đỏ cho điểm trừ
        }
    }
}