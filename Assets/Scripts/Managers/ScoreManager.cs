using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Difficulty difficulty = Difficulty.Easy;
    [SerializeField] private double restPointsPerLife = 50;
    private int missionTime;
    private bool victimIsDied;
    private double healthScore = 0;
    void Start()
    {
        ((GameManager)GameManager.Instance).OnMissionTimeStart += MissionStart;
        ((GameManager)GameManager.Instance).OnDecreaseLife += DecreaseLife;
        ((GameManager)GameManager.Instance).OnWinMission += MissionFinish;
        ((GameManager)GameManager.Instance).OnVictimIsDied += VictimIsDied;
    }

    private void MissionStart(int missionSecs)
    {
        missionTime = missionSecs;
    }

    private void VictimIsDied()
    {
        victimIsDied = true;
    }

    private void MissionFinish(int secs)
    {
        double score = 0;
        // difficulty score
        score += GetDifficultyScore();
        // health rest score
        score += GetHealthScore();
        // time score
        score += GetTimeScore(secs);
        // victim rescued score
        score += GetVictimScore();
        // TODO: flags score
    }

    private void DecreaseLife()
    {
        healthScore -= restPointsPerLife;
    }

    private double GetDifficultyScore()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return 100;
            case Difficulty.Normal:
                return 200;
            case Difficulty.Hard:
                return 300;
            default:
                return 0;
        }
    }

    private double GetHealthScore()
    {
        return healthScore == 0 ? 100 : healthScore;
    }

    private double GetTimeScore(int secs)
    {
        return secs >= missionTime ? 0 : missionTime - secs;
    }

    private double GetVictimScore()
    {
        return victimIsDied ? 0 : 500;
    }
}
