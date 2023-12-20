using TMPro;
using UnityEngine;

public class UiGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private ScoreKeeper _scoreKeeper;
    private string _userScoreText = "You scored: ";

    private void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        _scoreText.text = _userScoreText + _scoreKeeper.GetScore();
    }
}