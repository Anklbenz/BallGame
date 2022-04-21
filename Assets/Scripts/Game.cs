using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class Game : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int _health;
    [SerializeField] private Text _healthLabel;
    [Header("Scene Elements")]
    [SerializeField] private Deadline _deadline;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private Text _scoreLabel;
    [SerializeField] private UIGameOverScreen _gameOverScreen;
    [Header("Ball Settings")]
    [SerializeField] private Ball _prefab;
    [SerializeField] private int _ballPoolAmount;
    [SerializeField] private BallDestroyer _ballDestroyer;
    [SerializeField] private float _minScale = 1;
    [SerializeField] private float _maxScale = 1;
    [SerializeField] private float _minVelocity = 1;
    [SerializeField] private float _maxVelocity = 1;
    [SerializeField] private int _minBonusPoints = 1;
    [SerializeField] private int _maxBonusPoints = 5;
    [SerializeField] private float _timeNextSpawn = 1;
    [SerializeField] private float _spawnSpeedChagleTime = 0.1f;
    [SerializeField] private float _spawnSpeedChagleStep = 0.1f;
    [Header("Score Label Text")]
    [SerializeField] private ScoreText _textPrefab;
    [SerializeField] private int _labelPoolAmount;
    [SerializeField] private Transform _parentCanvas;
    [Header("Particale Settings")]
    [SerializeField] private ParticalePlay _particalePrefab;
    [SerializeField] private int _particalePoolAmount;
    [SerializeField] private Transform _particaleParent;

    private PoolObjects<ParticalePlay> _particalePool;
    private PoolObjects<ScoreText> _scoreLabelPool;
    private Vector3 fieldSizeVector;
    private BallSpawner _ballSpawner;
    private int _totalPoints;

    private void Awake()
    {
        this.DefineFildSize();
        _particalePool = new PoolObjects<ParticalePlay>(_particalePrefab, _particalePoolAmount, true, _particaleParent);
        _scoreLabelPool = new PoolObjects<ScoreText>(_textPrefab, _labelPoolAmount, true, _parentCanvas);
        _ballSpawner = new BallSpawner(_prefab, _ballPoolAmount, _spawnPoint.transform, _minScale, _maxScale, _minVelocity, _maxVelocity, _minBonusPoints, _maxBonusPoints);
        _ballSpawner.SetFiedSize(fieldSizeVector);
        _ballDestroyer.OnBallDestroyEvent += OnBallDestroy;
        _deadline.OnBallHitDedlineEvent += OnBallLost;
        InvokeRepeating("SpawnSpeedIncrese", 0, _spawnSpeedChagleTime);
    }

    private void SpawnSpeedIncrese()
    {
        _timeNextSpawn -= _spawnSpeedChagleStep;
    }

    private void DefineFildSize()
    {
        fieldSizeVector = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, -Camera.main.transform.position.z));
    }

    private void FixedUpdate()
    {
        _ballSpawner.SpawnBall(_timeNextSpawn);
    }

    private void OnBallDestroy(Ball obj)
    {
        AddPoints(obj.BonusPoints);
        DrawScoreLabel(obj.transform.position, obj.BonusPoints);
        DrawParicales(obj.transform.position, obj.BallColor);
    }

    private void AddPoints(int points)
    {
        _totalPoints += points;
        _scoreLabel.text = _totalPoints.ToString();
    }

    private void DrawScoreLabel(Vector2 position, int bonusPoints)
    {
        var scoreLabel = _scoreLabelPool.GetFreeElement();
        scoreLabel.transform.position = position;
        scoreLabel.Text("+" + bonusPoints.ToString());
    }

    private void DrawParicales(Vector2 position, Color color)
    {
        var particales = _particalePool.GetFreeElement();
        particales.transform.position = position;
        particales.SetColor(color);
        particales.Play();
    }

    private void OnBallLost(Ball obj)
    {
        Damage(obj.BonusPoints);
        DrawHealthLabel();
        obj.Deactivate();
        DrawParicales(obj.transform.position, obj.BallColor);
    }

    private void Damage(int points)
    {
        _health -= points * Random.Range(1, 10);

        if(_health <=0)
        {
            _health = 0;
            Time.timeScale = 0f;
            _gameOverScreen.gameObject.SetActive(true);
            _gameOverScreen.SetScores(_totalPoints);
        }
    }

    private void DrawHealthLabel()
    {
        _healthLabel.text = _health.ToString();
    }

    private void OnDestroy()
    {
        _ballDestroyer.OnBallDestroyEvent -= OnBallDestroy;
        _deadline.OnBallHitDedlineEvent -= OnBallLost;
    }
}
