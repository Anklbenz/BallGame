using UnityEngine;

public sealed class BallSpawner
{
    private float _minScale;
    private float _maxScale;
    private float _minVelocity;
    private float _maxVelocity;
    private int _minBonusPoints;
    private int _maxBonusPoints;
    private PoolObjects<Ball> _poolBalls;
    private Vector3 _fieldSizeVector;
    private float _timer;


    public BallSpawner(Ball prefab, int poolAmount, Transform parent, float minScale, float maxScale, float minVelocity, float maxVelocity, int minBonusPoints, int maxBonusPoints)
    {
        _poolBalls = new PoolObjects<Ball>(prefab, poolAmount, true, parent);
        _minScale = minScale;
        _maxScale = maxScale;
        _minVelocity = minVelocity;
        _maxVelocity = maxVelocity;
        _minBonusPoints = minBonusPoints;
        _maxBonusPoints = maxBonusPoints;
        
    }

    public void SpawnBall(float timeNextSpawn)
    {
        _timer += Time.fixedDeltaTime;

        if (_timer > timeNextSpawn)
        {
            _timer = 0;
            float xPosition = Random.Range(-_fieldSizeVector.x, _fieldSizeVector.x);
            float velocity = Random.Range(_minVelocity, _maxVelocity);
            float scale = Random.Range(_minScale, _maxScale);
            int bonusPoints = Random.Range(_minBonusPoints, _maxBonusPoints);
            Color color = new Color(Random.value, Random.value, Random.value, 1);

            var ball = _poolBalls.GetFreeElement();
            ball.Initialize(xPosition, scale, velocity, bonusPoints, color);
        }
    }

    public void SetFiedSize(Vector3 fieldSize)
    {
        this._fieldSizeVector = fieldSize;
    }
}
