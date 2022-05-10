using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    #region
    public static PointManager instance;
    private void Awake()
    {
        instance = this;
        if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    public int score1;
    public int score2;
}
