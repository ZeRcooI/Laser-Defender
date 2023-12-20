using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Health _playerHealth;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    private ScoreKeeper _scoreKeeper;

    private void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        _healthSlider.maxValue = _playerHealth.GetHealth();
    }

    private void Update()
    {
        _healthSlider.value = _playerHealth.GetHealth();
        _scoreText.text = _scoreKeeper.GetScore().ToString("000000000");
    }
}