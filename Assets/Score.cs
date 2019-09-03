using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
