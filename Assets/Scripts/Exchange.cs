using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Exchange : MonoBehaviour
{   
    private int numberOfCoin = 0;

    public TextMeshProUGUI textNumberCoin;

    private void Start()
    {
        
        textNumberCoin.text = "0";
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Gem")
        {
            // Destroy the object that this script collided with
            Destroy(collision.gameObject);
            numberOfCoin++;
            
            textNumberCoin.text = numberOfCoin.ToString();
        }

    }

}
