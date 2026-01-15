using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController thePlayer;
    private Vector3 lastPlayerPosition;

    [Header("Camera Settings")]
    public float yOffset = 1f;       
    public float ySmoothTime = 0.5f; 
    private float yVelocity = 0.0f;

    private float lastGroundY; 

    [Header("Jump Effect")]
    [Range(0f, 0.5f)]
    public float jumpInfluence = 1f;
    
    // [MỚI] Ngưỡng thay đổi để camera cập nhật (chống rung)
    // Nghĩa là: Nếu độ cao thay đổi nhỏ hơn 0.1 đơn vị thì coi như không đổi
    private float changeThreshold = 1f; 

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        lastPlayerPosition = thePlayer.transform.position;
        lastGroundY = thePlayer.transform.position.y;
    }

    void Update()
    {
        if (thePlayer == null) return;

        // --- 1. TRỤC X (Giữ nguyên) ---
        float distanceToMoveX = thePlayer.moveSpeed * Time.deltaTime;
        float newX = transform.position.x + distanceToMoveX;

        // --- 2. TRỤC Y (Logic chống rung) ---
        
        // Chỉ cập nhật độ cao đất KHI VÀ CHỈ KHI:
        // 1. Nhân vật đang chạm đất
        // 2. VÀ Độ cao mới khác biệt rõ rệt so với độ cao cũ (lớn hơn ngưỡng 0.1)
        if (thePlayer.grounded)
        {
            if (Mathf.Abs(thePlayer.transform.position.y - lastGroundY) > changeThreshold)
            {
                lastGroundY = thePlayer.transform.position.y;
            }
        }

        // Tính toán Target Y
        float targetY;

        if (thePlayer.grounded)
        {
            // [QUAN TRỌNG] Khi ở dưới đất, khóa cứng Camera vào lastGroundY
            // Bỏ qua mọi phép tính jumpInfluence để tránh nhiễu
            targetY = lastGroundY + yOffset;
        }
        else
        {
            // Khi đang nhảy mới tính thêm độ nhích
            float currentJumpHeight = thePlayer.transform.position.y - lastGroundY;
            targetY = lastGroundY + yOffset + (currentJumpHeight * jumpInfluence);
        }

        // Giới hạn đáy
        if(targetY < 0) targetY = 0; 

        // Di chuyển mượt
        float newY = Mathf.SmoothDamp(transform.position.y, targetY, ref yVelocity, ySmoothTime);

        // --- 3. GÁN VỊ TRÍ ---
        transform.position = new Vector3(newX, newY, transform.position.z);

        lastPlayerPosition = thePlayer.transform.position;
    }
}