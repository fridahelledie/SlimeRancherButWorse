using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour
{
    public GameObject slimePrefab; //reference to the prefab
    public GameObject gemPrefab;
    public GameObject carrotPrefab;
   // private GameObject slimeInstance; //will store the instance of the prefab
    public Transform slimeSpawnPoint;
    public float shootSpeed = 2;
    public float gemSpeed = 2;
    public float shootUp = 2;
    private Rigidbody rb;

    private int numberOfSlimes = 0;
    private int numberOfGems = 0;
    private int numberOfCarrots = 0;
    

    public TextMeshProUGUI textNumberSlimes;
    public TextMeshProUGUI textNumberGems;
    public TextMeshProUGUI textNumberCarrot;
    

    private void Start()
    {
        textNumberSlimes.text = "0";
        textNumberGems.text = "0";
        textNumberCarrot.text = "0";
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Gem") {
            // Destroy the object that this script collided with
            Destroy(collision.gameObject);
            numberOfGems++;
            textNumberGems.text = numberOfGems.ToString();
        }
        if (collision.gameObject.tag == "Slime")
        {
            Destroy(collision.gameObject);
            numberOfSlimes++;
            textNumberSlimes.text = numberOfSlimes.ToString();
        }
        if(collision.gameObject.tag == "Carrot")
        {
            Destroy (collision.gameObject);
            numberOfCarrots++;
            textNumberCarrot.text= numberOfCarrots.ToString();
        }

    }

    private void Update()
    {
        ShootSlime();
        ShootGem();
        ShootCarrot();
    }

    private void ShootSlime()
    {
        if (Input.GetKeyDown(KeyCode.Q) & numberOfSlimes > 0)
        {
            var slime = Instantiate(slimePrefab, slimeSpawnPoint.position, slimeSpawnPoint.rotation);
            rb = slime.GetComponent<Rigidbody>();

            //apply force in the direction the player is looking
            Vector3 shootDirection = transform.GetChild(1).forward; //players forward direction
            rb.AddForce(shootDirection* shootSpeed + Vector3.up * shootUp, ForceMode.Impulse);

            numberOfSlimes--;
            textNumberSlimes.text = numberOfSlimes.ToString();

            
        }
    }

    private void ShootGem()
    {
        if (Input.GetKeyDown(KeyCode.E) & numberOfGems > 0)
        {
            var gem = Instantiate(gemPrefab, slimeSpawnPoint.position, slimeSpawnPoint.rotation);
            rb = gem.GetComponent<Rigidbody>();

            //apply force in the direction the player is looking
            Vector3 shootDirection = transform.GetChild(1).forward; //players forward direction
            rb.AddForce(shootDirection * gemSpeed + Vector3.up, ForceMode.Impulse);

            numberOfGems--;
            textNumberGems.text = numberOfGems.ToString();


        }
    }

    private void ShootCarrot()
    {
        if (Input.GetKeyDown(KeyCode.R) & numberOfCarrots > 0)
        {
            var gem = Instantiate(carrotPrefab, slimeSpawnPoint.position, slimeSpawnPoint.rotation);
            rb = gem.GetComponent<Rigidbody>();

            //apply force in the direction the player is looking
            Vector3 shootDirection = transform.GetChild(1).forward; //players forward direction
            rb.AddForce(shootDirection * gemSpeed + Vector3.up, ForceMode.Impulse);

            numberOfCarrots--;
            textNumberCarrot.text = numberOfCarrots.ToString();
            


        }
    }
}
