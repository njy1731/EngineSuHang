using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public static ScoreManager GetInstance()
    {
        if(Instance == null)
        {
            Instance = FindObjectOfType<ScoreManager>();
            if (Instance == null)
            {
                GameObject container = new GameObject("Single");
                Instance = container.AddComponent<ScoreManager>();
            }
        }
        return Instance;
    }

    private void Awake()
    {
        Debug.Log(ScoreManager.GetInstance().GetScore());
    }

    private void Start()
    {
        if(Instance != null)
        { 
            if(Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private int score = 0;

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int newScore)
    {
        score += newScore;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ScoreManager.Instance.AddScore(5);
            Debug.Log(ScoreManager.Instance.GetScore());
        }
    }
}
