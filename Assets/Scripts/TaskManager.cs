using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;

public class Task<T> where T : MonoBehaviour
{
    public T Goal { get; set; }
    public int Count { get; set; }

    public Task(T targetType, int count)
    {
        Goal = targetType;
        Count = count;
    }
}

public class TaskManager : MonoBehaviour
{
    public Task<Fruit> Task { get; set; }

    [SerializeField] private UnityEvent _wonEvent;

    private int pickedCount;
    [SerializeField] private int maxTaskCapacity;

    [SerializeField] private TMP_Text _fruitCounter, _addOne, _levelPassed;

    [SerializeField] private FruitPool _fruitPool;

    private void Awake()
    {
        _addOne.alpha = 0f;
        var element = _fruitPool.FruitsData.GetRandElement();
        var count = Random.Range(1, maxTaskCapacity + 1);

        Task = new Task<Fruit>(element, count);

        _fruitPool.PoolStart(Task);
    }

    private void Update() => UpdateFruitCounterText();

    public void PutFruit(Fruit fruit)
    {
        if (fruit?.Kind == Task.Goal.Kind && pickedCount < Task.Count)
            MoveText();

        if (pickedCount == Task.Count)
        {
            _levelPassed.DOFade(1f, 1f);
            _wonEvent.Invoke();
        }
            
        return;
    }

    private void MoveText()
    {
        pickedCount++;
        _addOne.alpha = 1f;
        _addOne.rectTransform.DOAnchorPos(new Vector3(206f, 636f, 0f), 1f)
            .SetEase(Ease.OutQuad).OnComplete(() => FadeText());
    }

    private void FadeText()
    {
        _addOne.DOFade(0f, 1f).SetDelay(0.5f)
            .OnComplete(() => _addOne.rectTransform.DOAnchorPos(new Vector3(-344f, 245.5f, 0f), 1f));
    }

    private void UpdateFruitCounterText()
    {
        _fruitCounter.text = pickedCount + " / " + Task.Count + " " + Task.Goal.gameObject.name + "s";
    }
}