using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int _CoinCount;
    [SerializeField] private TextMeshProUGUI _countCountText;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private bool _fadeIn, _fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        _CoinCount = 0;
        //_countCountText.text = "Coins:" + _CoinCount.ToString();
        FadeOutUi();
    }

    // Update is called once per frame
    void Update()
    {
        if(_fadeIn)
        {
            if(_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += Time.deltaTime;
                if(_canvasGroup.alpha >= 1)
                {
                    _fadeIn = false;
                }
            }
        }
        if(_fadeOut)
        {
            if(_canvasGroup.alpha >= 0)
            {
                _canvasGroup.alpha -= Time.deltaTime;
                if(_canvasGroup.alpha == 0)
                {
                    _fadeOut = false;
                }
            }
        }
    }

    public void UpdateCoinCount(int amount)
    {
        _CoinCount += amount;
        _countCountText.text = "Coins: " + _CoinCount.ToString();
    }

    public void FadeInUi()
    {
        //_canvasGroup.alpha = 0;
        _fadeIn = true;
    }

    public void FadeOutUi()
    {
        //_canvasGroup.alpha = 1;
        _fadeOut = true;
    }
}
