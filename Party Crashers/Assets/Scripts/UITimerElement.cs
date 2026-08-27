using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITimerElement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI m_textObj;
    [SerializeField] private Image m_fillBarImage;
    GameTimer timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameTimer.Instance != null)
        {
            timer = GameTimer.Instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!timer.IsRunning)
            return;

        timer.DisplayOnUIText(m_textObj);
        timer.DisplayOnUIFillBar(m_fillBarImage);
    }
}
