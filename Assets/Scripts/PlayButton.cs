using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayButton : MonoBehaviour
{
    private TMP_Text playText;

    private Color nonHoverColor = new Color(256, 256, 256);
    private Color hoverColor = new Color(185, 181, 181);

    // Start is called before the first frame update
    void Start()
    {
        playText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        playText.color = hoverColor;
    }

    private void OnMouseExit()
    {
        playText.color = nonHoverColor;
    }

    
}
