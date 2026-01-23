using System.Collections;
using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    public TMP_Text text; 
    public float blinkSpeed = 0.6f;

    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            text.enabled = !text.enabled;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    //void Awake()
    //{
    //    text = GetComponent<TMP_Text>(); 
    //}

}
