using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoints : MonoBehaviour
{

    public int scoreToGive;

    private ScoreManager theScoreManager;

    private AudioSource coinSound;
    public GameObject floatingTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();

        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" || other.gameObject.tag == "Player")
        {
            // Cộng điểm
            theScoreManager.AddScore(scoreToGive);
            
            // Tắt đồng xu đi (hoặc Destroy(gameObject))
            gameObject.SetActive(false); 
            
            if(floatingTextPrefab != null)
            {
                // Tạo ra text tại vị trí của đồng tiền
                GameObject textObj = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
                // Set giá trị và màu sắc
                textObj.GetComponent<FloatingText>().SetValue(scoreToGive);
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
        }
    }
}
