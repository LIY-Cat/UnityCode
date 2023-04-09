/*
*PlayerView.cs
*/
using UnityEngine;
using UnityEngine.UI;

public interface IPlayerView{
    Rigidbody PlayerRigidbody { get; }
    int Score { get; }
    void UpdateScore(int score);
    void UpdateGauge(float fillAmount);
    void UpdateScrollbar(float size);
}

public class PlayerView : MonoBehaviour, IPlayerView{
    [SerializeField] private Text scoreText;
    [SerializeField] private Image gaugeImage;
    [SerializeField] private Scrollbar scrollbar;

    public Rigidbody PlayerRigidbody { get; private set; }
    public int Score { get; private set; }

    private void Awake(){
        PlayerRigidbody = GetComponent<Rigidbody>();
    }

    public void UpdateScore(int score){
        Score = score;
        scoreText.text = "Score: " + Score.ToString();
    }

    public void UpdateGauge(float fillAmount){
        gaugeImage.fillAmount = fillAmount;
    }

    public void UpdateScrollbar(float size){
        scrollbar.size = size;
    }
}
