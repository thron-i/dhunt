using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    // Player score
    public int score = 0;
    public int highScore = 0;
  
    void Awake()
    {
        // if it doesn't exist
        if (instance == null)
        {
            // Set the instance to the current object (this)
            instance = this;
        }
        // There can only be a single instance of the game manager
        else if (instance != this)
        {
            // Destroy the current object, so there is just one manager
            Destroy(gameObject);
        }
        // Don't destroy this object when loading scenes
        DontDestroyOnLoad(gameObject);
    }
    public void IncreaseScore(int amount)
    {
        // Increase the score by the given amount
        score += amount;
        // Show the new score in the console
        print("New Score: " + score.ToString());
        if (score > highScore)
        {
            highScore = score;
            print("New high score: " + highScore);
        }
    }
}