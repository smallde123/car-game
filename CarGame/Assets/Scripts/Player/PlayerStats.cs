using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerStats : MonoBehaviour
{
        
    public TouchEvents touchCar;
    public GameObject settingsBtn, settingsPage, garageBtn, storeBtn, storePage, garagePage;
    public CashManager cashManager;
    public HealthManager healthManager;
    public SparkManager sparkManager;
    public GameObject carMesh;
    [Header("TEXT")]
    public Text cash;
    public Text speedText;
    public Text sparkText;
    [Header("PLAYER VARIABLES")]
    public int currentVehicle = 0;
    public int lowRange;
    public int highRange;
    public int wDamage = 25;
    public int cDamage = 100;
    public float speed;
    public float distTraveled;
    public float maxDistance;
    public float newDist;
    public bool brokeDown = false;
    public bool canTakeDamage = true;
    public bool isBoosting = false;
    public ParticleSystem carParticles;
    public SpriteRenderer spriteRend;
    public SpriteRenderer spriteSmoke;
    public Rigidbody rigidBod;
    public Animator carAnim;
    public Animator garageAnim;
    public Animator buttonsAnim;
    public VehicleObject[] vehicles;
    [Header("AUDIO")]
    public AudioSource woodAudio, cementAudio, collectAudio;

    private float carForce = 3;


        
    void Start()
    {
        sparkManager.GetSparks();
        speed = 40;
        GetDist();
        cashManager.GetCash();//Gets saved cash amount at start
        GetCar();//Gets random car type
        carMesh.GetComponent<MeshFilter>().mesh = vehicles[currentVehicle].vehicleMesh;//Stores vehicle mesh
        carMesh.GetComponent<MeshRenderer>().material = vehicles[currentVehicle].carColor;
        cash.text = "$" + cashManager.playerCash.ToString("f0");
        speedText.text = speed.ToString("f0");
        sparkText.text = sparkManager.sparks.ToString("f0");
        //speed = vehicles[currentVehicle].vSpeed;
        healthManager.health = vehicles[currentVehicle].vHealth;
        spriteSmoke.enabled = false;
    }

        
    void Update()
    {
        currentVehicle = Mathf.Clamp(currentVehicle, 0, 1000);
        distTraveled = Mathf.Clamp(distTraveled, 0, Mathf.Infinity);
        speed = Mathf.Clamp(speed, 0, 60);
            

        if (PlayerPrefs.HasKey("Dist"))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                PlayerPrefs.DeleteKey("Dist");
                maxDistance = 0;
                SaveDist();
                Debug.Log("RESET DISTANCE");
            }
        }

        cash.text = "$" + cashManager.playerCash.ToString("f0");//move outside of canPlay argument
        speedText.text = speed.ToString("f0") + " Mph";
        sparkText.text = sparkManager.sparks.ToString("f0");

        if (touchCar.canPlay == true)
        {

            BoostCar();

            if (speed >= 1)
            {
                distTraveled += 0.1f * Time.deltaTime;
            }

            if (healthManager.health <= 0f)
            {
                Crashed();
                brokeDown = true;
            }
            else if (healthManager.health >= 1)
            {
                brokeDown = false;
            }

            if (brokeDown == true)
            {
                cashManager.SaveCash();
                newDist = maxDistance;
                SaveDist();
            }
        }

        if(PlayerPrefs.HasKey("sparks"))//Debug: Spark Reset to 0
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                PlayerPrefs.DeleteKey("sparks");
                sparkManager.sparks = 0;
                sparkManager.SaveSparks();
                Debug.Log("RESET SPARKS");
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == ("car") || col.gameObject.tag == ("suv") || col.gameObject.tag == ("truck"))
        {
            if(canTakeDamage == true)
            {
                Handheld.Vibrate();
                Debug.Log("Vibrated");
                healthManager.SubHealth(cDamage);
                cementAudio.Play();
                Crashed();
            }
            else if(canTakeDamage == false)
            {
                //cementAudio.Play();
            }
        }

        if (col.gameObject.tag == ("cement"))
        {
            if(canTakeDamage == true)
            {
                healthManager.SubHealth(cDamage);
                cementAudio.Play();
                Crashed();
            }
            else if(canTakeDamage == false)
            {
                //crash audio
            }
        }

        if (col.gameObject.tag == ("wood") && canTakeDamage == true)
        {
            //if(canTakeDamage == true)
            //{
                healthManager.SubHealth(wDamage);
                woodAudio.Play();
                speed -= 5;
            //}
            //else if(canTakeDamage == false)
            //{
                //woodAudio.Play();
            //}
        }
        else if (col.gameObject.tag == ("wood") && canTakeDamage == true)
        {
            woodAudio.Play();
        }

            if (col.gameObject.tag == ("plastic") && canTakeDamage == true)
        {
            healthManager.SubHealth(5);
            //a sound
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("cashadd") && other.gameObject.tag != ("wood"))
        {
            cashManager.AddCash(vehicles[currentVehicle].cashEarned);
            cash.text = "$" + cashManager.playerCash.ToString("f0");
            //a sound
        }

        if(other.gameObject.tag == ("spark"))
        {
            sparkManager.AddSparks(0.25f);
            sparkManager.SaveSparks();
            speed += 0.25f;
            Destroy(other.gameObject);
            sparkText.text = sparkManager.sparks.ToString("f0");
            collectAudio.Play();

        }
        if (other.gameObject.tag == ("sparkb"))
        {
            sparkManager.AddSparks(1.0f);
            sparkManager.SaveSparks();
            speed += 1;
            Destroy(other.gameObject);
            sparkText.text = sparkManager.sparks.ToString("f0");
            collectAudio.Play();

        }

        if (other.gameObject.tag == ("hole"))
        {
                
        }
    }

    public void BoostCar()
    {
        if (speed >= 50)
        {
            //isBoosting = true;
            spriteRend.enabled = true;
            //canTakeDamage = false;
        }
        else if(speed <= 49)
        {
            //isBoosting = false;
            spriteRend.enabled = false;
            //canTakeDamage = true;
        }
    }
    
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
    }

    public void Crashed()
    {
        speed = 0f;
        carAnim.enabled = false;
        spriteRend.enabled = false;
        spriteSmoke.enabled = true;

        settingsBtn.SetActive(true);
        storeBtn.SetActive(true);
        garageBtn.SetActive(true);
        buttonsAnim.Play("ButtonsUp");
        sparkManager.SaveSparks();
    }

    public void OpenSettings()
    {
    settingsPage.SetActive(true);

    settingsBtn.SetActive(false);
    storeBtn.SetActive(false);
    garageBtn.SetActive(false);
        //Pause();
    }

    public void CloseSettingsPage()
    {
    settingsPage.SetActive(false);

    settingsBtn.SetActive(true);
    storeBtn.SetActive(true);
    garageBtn.SetActive(true);
    //UnPause();
    }

    public void CloseStorePage()
    {
        storePage.SetActive(false);

        settingsBtn.SetActive(true);
        storeBtn.SetActive(true);
        garageBtn.SetActive(true);
        //UnPause();
    }

    public void CloseGaragePage()
{
    garagePage.SetActive(false);
        

    settingsBtn.SetActive(true);
    storeBtn.SetActive(true);
    garageBtn.SetActive(true);
    //UnPause();
}

    public void OpenStore()
    {
    storePage.SetActive(true);

    settingsBtn.SetActive(false);
    storeBtn.SetActive(false);
    garageBtn.SetActive(false);
    //Pause();
    }

    public void OpenGarage()
    {
    garagePage.SetActive(true);

    settingsBtn.SetActive(false);
    storeBtn.SetActive(false);
    garageBtn.SetActive(false);
    //garageAnim.Play("GarageOpen");
    //Pause();
    }

    public void GameStart()
    {
        if(!brokeDown)
        {
            buttonsAnim.Play("ButtonsDown");
        }
    }

    public void GetCar()
    {
        int rand_num = Random.Range(lowRange, highRange);
        currentVehicle = rand_num;
    }
    public void SaveCar()
    {
        PlayerPrefs.SetInt("CarNum", currentVehicle);
    }

    public void GetDist()
    {
        maxDistance = PlayerPrefs.GetFloat("Dist");
        SaveDist();
            
    }
    public void SaveDist()
    {
        if(maxDistance <= PlayerPrefs.GetFloat("Dist"))
        {
            PlayerPrefs.SetFloat("Dist", distTraveled);
        }
        else
        {
            PlayerPrefs.SetFloat("Dist", newDist);
        }
    }
}
