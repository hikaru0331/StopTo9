using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI resultText;

    [SerializeField]
    private GameObject ButtonPanel;
    [SerializeField]
    private Button postOnXButton;
    [SerializeField]
    private Button titleButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ButtonPanel.SetActive(false);
        
        postOnXButton.onClick.AddListener(() =>
        {
            // ポストする
            Debug.Log("Post to X");
        });

        titleButton.onClick.AddListener(() =>
        {
            PlayerPrefs.DeleteAll();
            // タイトル画面に遷移
            SceneManager.LoadScene("Title");
        });
        
        PlayReslutAnimation();
    }
    
    private void PlayReslutAnimation()
    {
        int targetScore = PlayerPrefs.GetInt("NowScore");
        resultText.DOCounter(0, targetScore, 3.0f).OnUpdate(() =>
        {
            // 現在の値を取得して3桁形式にフォーマット
            if (int.TryParse(resultText.text, out int currentValue))
            {
                resultText.text = currentValue.ToString("D3");
            }
        }).OnComplete(() =>
        {
            // アニメーション終了時も最終値を3桁形式にフォーマット
            resultText.text = targetScore.ToString("D3");
            ButtonPanel.SetActive(true);
        });
    }
}