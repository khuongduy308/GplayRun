using UnityEngine;

public class MoneyTrap : MonoBehaviour
{
    public int penaltyAmount = 50; // Số điểm bị trừ (bạn chỉnh số này trong Unity)

    private ScoreManager theScoreManager;
    private AudioSource coinSound;

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