using UnityEngine;

public class MoneyTrap : MonoBehaviour
{
    public int penaltyAmount; // Số điểm bị trừ (bạn chỉnh số này trong Unity)

    private ScoreManager theScoreManager;
    private AudioSource coinSound;

    public GameObject floatingTextPrefab;

    void Start()
    {
        // Tìm ScoreManager trong Scene
        theScoreManager = FindObjectOfType<ScoreManager>();
        coinSound = GameObject.Find("DeathSound").GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu người chơi chạm vào
        if (other.gameObject.name == "Player" || other.gameObject.CompareTag("Player"))
        {
            if (theScoreManager != null)
            {
                // MẸO: Gọi hàm AddScore nhưng truyền số ÂM để trừ điểm
                theScoreManager.AddScore(-penaltyAmount); 

                if(floatingTextPrefab != null)
                {
                    // Tạo text ngay trên đầu nhân vật (other.transform.position)
                    Vector3 popPos = new Vector3(other.transform.position.x, other.transform.position.y + 1f, 0);
                    GameObject textObj = Instantiate(floatingTextPrefab, popPos, Quaternion.identity);
                    
                    // Truyền số ÂM để nó hiện màu đỏ
                    textObj.GetComponent<FloatingText>().SetValue(-penaltyAmount);
                }
                
                // (Tùy chọn) In ra log để kiểm tra
                Debug.Log("Đã dẫm phải bẫy! Bị trừ " + penaltyAmount + " điểm.");
            }

            if(coinSound.isPlaying)
            {
                coinSound.Stop();
                coinSound.Play();
            }
            else
            {
                coinSound.Play();
            }

            // Tắt cái bẫy đi để không bị trừ điểm liên tục khi đứng yên trong đó
            // Hoặc nếu muốn bẫy nổ/biến mất thì dùng dòng dưới:
            gameObject.SetActive(false); 
        }
    }
}