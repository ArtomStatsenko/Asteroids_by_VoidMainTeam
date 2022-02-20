using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BackgroundStars
{
    private Transform _starParentGO;
    private GameObject _starPrefab;
    private Dictionary<Border, float> _screenBorder;
    private List<Transform> _starList;
    private float _minStarScale;
    private float _maxStarScale;
    private float _time;
    private float _yPosition = -5f;
    private const string STAR = "Star";

    public BackgroundStars(int starCount, float minStarScale = 0.1f, float maxStarScale = 1.2f)
    {
        _minStarScale = minStarScale;
        _maxStarScale = maxStarScale;
        _time = Random.Range(0, 0.2f);
        _starList = new List<Transform>();
        _starPrefab = Resources.Load<GameObject>(STAR);
        _starParentGO = new GameObject($"[{STAR}]").transform;
        _screenBorder = GameModel.ScreenBorder;

        GenerateStars(starCount);
    }

    private Transform InitStar()
    {
        var position = new Vector3(
                Random.Range(_screenBorder[Border.Left], _screenBorder[Border.Right]), _yPosition,
                Random.Range(_screenBorder[Border.Top], _screenBorder[Border.Bottom]));

        var star = Object.Instantiate(_starPrefab, position, Quaternion.AngleAxis(90, Vector3.right)).transform;
        star.localScale = Vector3.one * star.localScale.x * Random.Range(_minStarScale, _maxStarScale);
        star.SetParent(_starParentGO);

        return star;
    }

    private void GenerateStars(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            var star = InitStar();
            _starList.Add(star);
        }
    }

    public void MoveStars(float speed)
    {
        foreach (var star in _starList)
        {
            star.Translate(Vector3.down * Time.deltaTime * speed);

            if (star.position.z <= _screenBorder[Border.Bottom])
            {
                ChangeStarPosition(star);
            }
        }
    }

    private void ChangeStarPosition(Transform star)
    {
        float x = Random.Range(_screenBorder[Border.Left], _screenBorder[Border.Right]);
        star.position = new Vector3(x, star.position.y, _screenBorder[Border.Top]);
    }
}
