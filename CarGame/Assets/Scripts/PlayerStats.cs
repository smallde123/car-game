using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CompleteProject
{

    public class PlayerStats : MonoBehaviour
    {
        [HideInInspector]
        public TouchEvents touchCar;
        [HideInInspector]
        public GameObject settingsBtn, garageBtn, storeBtn, storePage, popupPage, garagePage;
        [HideInInspector]
        public CashManager cashManager;
        [HideInInspector]
        public HealthManager healthManager;
        public SparkManager sparkManager;
        [HideInInspector]
        public GameObject carMesh;
        [HideInInspector]
        public Text cash;
        public Text speedText;
        public Text sparkText;
        [Space(10)]
        [Header("PLAYER VARIABLES")]
        public int currentVehicle = 0;
        public int numDelivered = 0;
        public int numRuined = 0;
        public int lowRange;
        public int highRange;
        [Space(10)]
        [Tooltip("Wood Barrier Damage")]
        public int wDamage = 25;
        [Tooltip("Cement Block Damage")]
        public int cDamage = 100;
        [Space(10)]
        public float speed;
        public float cleanDrifts = 0;
        public float maxDrifts;
        public float distTraveled;
        public float maxDistance;
        public float newDist;
        [Space(10)]
        public bool brokeDown = false;
        public bool canTakeDamage = true;
        public bool isBoosting = false;
        [Space(10)]
        public ParticleSystem carParticles;
        public SpriteRenderer spriteRend;
        public SpriteRenderer spriteSmoke;
        public Image boostImage;
        public GameObject boostBar;
        public Rigidbody rigidBod;
        [Space(10)]
        [HideInInspector]
        public Animator carAnim;
        [HideInInspector]
        public Animator garageAnim;
        [HideInInspector]
        public Animator buttonsAnim;
        public VehicleObject[] vehicles;
        public AudioSource woodAudio, cementAudio, collectAudio;

        private float carForce = 3;


        // Start is called before the first frame update
        void Start()
        {
            sparkManager.GetSparks();
            speed = 30;
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

        // Update is called once per frame
        void Update()
        {
            currentVehicle = Mathf.Clamp(currentVehicle, 0, 1000);
            cleanDrifts = Mathf.Clamp(cleanDrifts, 0, 5);
            distTraveled = Mathf.Clamp(distTraveled, 0, Mathf.Infinity);
            speed = Mathf.Clamp(speed, 20, 50);
            

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
            speedText.text = speed.ToString("f0");
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

                if (cleanDrifts >= maxDrifts)
                {
                    boostImage.fillAmount = 1.0f;
                }

                if (isBoosting == true)
                {
                    boostBar.SetActive(false);
                    canTakeDamage = false;
                    boostImage.fillAmount = 0.0f;
                }

            }

            if(PlayerPrefs.HasKey("sparks"))
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
                }
                else if(canTakeDamage == false)
                {
                    speed -= 2;
                    //cementAudio.Play();
                }
            }

            if (col.gameObject.tag == ("cement"))
            {
                if(canTakeDamage == true)
                {
                    healthManager.SubHealth(cDamage);
                    boostImage.fillAmount = 0.0f;
                    cementAudio.Play();
                }
                else if(canTakeDamage == false)
                {
                    speed -= 2;
                    //cementAudio.Play();
                }
            }

            if (col.gameObject.tag == ("wood"))
            {
                if(canTakeDamage == true)
                {
                    healthManager.SubHealth(wDamage);
                    boostImage.fillAmount = 0.0f;
                    woodAudio.Play();
                    speed -= 1;
                }
                else if(canTakeDamage == false)
                {
                    woodAudio.Play();
                    speed -= 1;
                }
            }

            if (col.gameObject.tag == ("plastic") && canTakeDamage == true)
            {
                healthManager.SubHealth(5);
                boostImage.fillAmount = 0.0f;
                //a sound
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == ("cashadd") && other.gameObject.tag != ("wood"))
            {
                cashManager.AddCash(vehicles[currentVehicle].cashEarned);
                boostBar.SetActive(true);
                boostImage.fillAmount += 0.2f;
                cash.text = "$" + cashManager.playerCash.ToString("f0");
                //a sound
            }

            if(other.gameObject.tag == ("spark"))
            {
                sparkManager.AddSparks(1.0f);
                sparkManager.SaveSparks();
                speed += 1;
                Destroy(other.gameObject);
                sparkText.text = sparkManager.sparks.ToString("f0");
                collectAudio.Play();

            }
            if (other.gameObject.tag == ("sparkb"))
            {
                sparkManager.AddSparks(5.0f);
                sparkManager.SaveSparks();
                speed += 5;
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
            if (speed >= 45)
            {
                isBoosting = true;
                spriteRend.enabled = true;
                canTakeDamage = false;
                //StartCoroutine(BoostTime(10.0f));
                print("Boost Active");
            }
            else if(speed <= 44)
            {
                isBoosting = false;
                spriteRend.enabled = false;
                canTakeDamage = true;
                print("Boost Inactive");
            }
        }

        IEnumerator BoostTime(float time)
        {
            yield return new WaitForSeconds(time);
        }

        public void Pause()
        {
            speed = 0f;
        }
        public void UnPause()
        {
            speed = vehicles[currentVehicle].vSpeed;

        }

        public void Crashed()
        {
            speed = 0f;
            carAnim.enabled = false;
            spriteRend.enabled = false;
            spriteSmoke.enabled = true;

            boostBar.SetActive(false);
            settingsBtn.SetActive(true);
            storeBtn.SetActive(true);
            garageBtn.SetActive(true);
            buttonsAnim.Play("ButtonsUp");
            sparkManager.SaveSparks();
        }

        public void AddDelivered()
        {
            numDelivered += 1;
        }
        public void AddRuined()
        {
            numRuined += 1;
        }

        public void OpenStore()
        {
            storePage.SetActive(true);
            Pause();
        }
        public void ExitStore()
        {
            storePage.SetActive(false);
            UnPause();
        }
        public void OpenGarage()
        {
            garagePage.SetActive(true);garageAnim.Play("GarageOpen");
            Pause();
        }
        public void CloseGarage()
        {
            garageAnim.Play("GarageClose");
            UnPause();
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

}
