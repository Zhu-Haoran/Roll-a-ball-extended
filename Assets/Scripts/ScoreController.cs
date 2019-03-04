using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    public GameObject winBox;
    public int numberOfPickUpInLevel = 1;
    private int score;

    private List<GameObject> pickUpCollected;

    // Start is called before the first frame update
    void Start()
    {
        // Set the count to zero 
        score = 0;
        // Run the SetCountText function to update the UI (see below)
        SetScoreText();

        winBox.SetActive(false);

        pickUpCollected = new List<GameObject>();
    }
    
    // When this game object intersects a collider with 'is trigger' checked, 
    // store a reference to that collider in a variable named 'other'..
    void OnTriggerEnter(Collider other)
    {
        // ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
        if (other.gameObject.tag.Contains("Pick Up"))
        {
            pickUpCollected.Add(other.gameObject);
            
            // Make the other game object (the pick up) inactive, to make it disappear
            other.gameObject.SetActive(false);

            // Add one to the score variable 'count'
            score += 1;

            // Run the 'SetCountText()' function (see below)
            SetScoreText();
        }

        if (other.gameObject.CompareTag("Reset Zone"))
        {
            score = 0;

            SetScoreText();
            foreach (GameObject pickUp in pickUpCollected) {
                pickUp.SetActive(true);
            }
        }
    }

    // Create a standalone function that can update the 'scoreText' UI and check if the required amount to win has been achieved
    void SetScoreText()
    {
        // Update the text field of our 'scoreText' variable
        scoreText.text = "Count: " + score.ToString();

        // Check if our 'score' is equal to or exceeded the level's amount of pickups
        if (score >= numberOfPickUpInLevel)
        {
            winBox.SetActive(true);
            GetComponent<PauseController>().GameWon();
        }
    }
}
