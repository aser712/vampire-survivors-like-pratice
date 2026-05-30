using UnityEngine;
using UnityEngine.UI;

public class PlayerHPbar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform player;
    public Vector3 offset = new Vector3(0, 0.06f, 0);

    public PlayerStatus playerStatus;
    public Slider slider;

    void Start()
    {
        slider.maxValue = playerStatus.maxhp;
    }
        
    // Update is called once per frame
    void Update()
    {
        slider.value = playerStatus.hp;
    }
}
