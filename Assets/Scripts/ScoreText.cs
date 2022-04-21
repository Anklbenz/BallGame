using UnityEngine;
using UnityEngine.UI;

public sealed class ScoreText : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _aliveTime;
    private float timer;
    private Text _scoreTextLabel;

    private void Awake()
    {
        _scoreTextLabel = GetComponent<Text>();
    }

    private void OnEnable()
    {
        timer = _aliveTime;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
            gameObject.SetActive(false);
    }

    public void Text(string str)
    {
        _scoreTextLabel.text = str;
    }
}
