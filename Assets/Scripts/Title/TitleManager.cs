using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    
    [SerializeField]
    private Button highScoreButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            // ゲームシーンに遷移
            SceneManager.LoadScene("InGame");
        });
        
        highScoreButton.onClick.AddListener(() =>
        {
            // ハイスコア画面に遷移
            SceneManager.LoadScene("HighScore");
        });
    }
}
