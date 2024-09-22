using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Values")]
    [SerializeField] private int rescueCount = 0;
    [SerializeField] private bool hasAlphaKey = false;
    [SerializeField] private bool hasBetaKey = false;
    [SerializeField] private bool hasGammaKey = false;
    [SerializeField] private bool hasDeltaKey = false;
    [SerializeField] private bool hasEpsilonKey = false;
    public void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void getKey(string keyName)
    {
        switch (keyName)
        {
            case "alpha":
                hasAlphaKey = true;
                break;
            case "beta":
                hasBetaKey = true;
                break;
            case "gamma":
                hasGammaKey = true;
                break;
            case "delta":
                hasDeltaKey = true;
                break;
            case "epsilon":
                hasEpsilonKey = true;
                break;
            default:
                break;
        }
    }


    public bool checkKey(string keyName)
    {
        switch (keyName)
        {
            case "alpha":
                return hasAlphaKey;
            case "beta":
                return hasBetaKey;
            case "gamma":
                return hasGammaKey;
            case "delta":
                return hasDeltaKey;
            case "epsilon":
                return hasEpsilonKey;
            case "omega":
                return rescueCount >= 4;
            default:
                return false;
        }
    }

    public int getRescueCount()
    {
        return rescueCount;
    }

    public void setRescueCount(int val)
    {
        rescueCount = val;
    }

    public void ResetGame()
    {
        hasAlphaKey = false;
        hasBetaKey = false;
        hasGammaKey = false;
        hasDeltaKey = false;
        hasEpsilonKey = false;
        rescueCount = 0;
    }
}
