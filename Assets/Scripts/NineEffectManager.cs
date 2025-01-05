using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class NineEffectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject nineTextsParent;

    [SerializeField]
    private GameObject nineTextPrefab;

    [SerializeField]
    private TextMeshProUGUI achievementText; // "Achieved"メッセージ表示用
    [SerializeField]
    private Button returnButton;  // InGameへ戻るボタン

    private const int MaxColumns = 10; // 最大列数
    private const float CellWidth = 60f; // 横方向の間隔
    private const float CellHeight = 50f; // 縦方向の間隔
    private const float AnimationStartX = 600f; // スライドインの開始X位置 (画面外右)

    private void Start()
    {
        // 現在のスコアを取得
        int nowScore = 19;//PlayerPrefs.GetInt("NowScore", 0);

        // スコアに応じたnineTextPrefabの生成
        for (int i = 0; i < nowScore; i++)
        {
            // Prefabを生成し、親要素に設定
            GameObject newText = Instantiate(nineTextPrefab, nineTextsParent.transform);

            // 行・列を計算
            int row = i / MaxColumns;
            int column = i % MaxColumns;

            // 初期位置を設定 (画面外右に配置)
            RectTransform rectTransform = newText.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(AnimationStartX, -row * CellHeight);

            // 最終位置を計算
            Vector2 targetPosition = new Vector2(column * CellWidth, -row * CellHeight);

            // アニメーション: スライドイン
            rectTransform
                .DOAnchorPos(targetPosition, 0.5f)
                .SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    // 振動アニメーション
                    rectTransform.DOShakePosition(0.5f, strength: new Vector3(5, 0, 0), vibrato: 10);
                });
        }

        // 達成メッセージとボタンの表示
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(0.6f + (nowScore * 0.1f)) // 全てのスライドインが終わるタイミングを待つ
            .AppendCallback(() =>
            {
                achievementText.text = $"Achieved {nowScore} times!";
                achievementText.gameObject.SetActive(true);

                // アニメーション: フェードイン
                achievementText.DOFade(1, 1f).SetEase(Ease.InOutQuad);

                // ボタンを表示
                returnButton.gameObject.SetActive(true);
            });
    }
}