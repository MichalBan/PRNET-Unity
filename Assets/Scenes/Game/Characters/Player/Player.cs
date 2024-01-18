using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scenes.Game
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Weapon))]
    public class Player : Character
    {
        private GameObject _healthBar;
        private float _maxHealth;
        private Animator _anim;
        private Movement _movement;
        private Weapon _weapon;
        private Rigidbody2D _body;
        public float qCooldown;
        public float eCooldown;
        public float rCooldown;
        private float qCooldown_T;
        private float eCooldown_T;
        private float rCooldown_T;
        private bool qReady = true;
        private bool eReady = true;
        private bool rReady = true;
        public TextMeshProUGUI healthText;

        // Start is called before the first frame update
        void Start()
        {
            _maxHealth = HealthPoints;
            _healthBar = GameObject.Find("HealthBar");
            _healthBar.GetComponent<Slider>().value = HealthPoints;
            _anim = GetComponent<Animator>();
            _movement = GetComponent<Movement>();
            _weapon = GetComponent<Weapon>();
            _body = GetComponent<Rigidbody2D>();

            _weapon.InvokeRepeating("DoFireBolt", 0.0f, _weapon.FireRate);
            InvokeRepeating("UpdateQ", 1f, 1f);
            InvokeRepeating("UpdateE", 1f, 1f);
            InvokeRepeating("UpdateR", 1f, 1f);


        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown("q") && qReady)
            {
                _weapon.Invoke("DoShuriken", 0.0f);
                qReady = false;
                qCooldown_T = qCooldown;

            }

            if (Input.GetKeyDown("e") && eReady)
            {
                _weapon.Invoke("DoRocketBolt", 0.0f);
                eReady = false;
                eCooldown_T = eCooldown;
            }

            if (Input.GetKeyDown("r") && rReady)
            {
                _weapon.Invoke("DoFog", 0.0f);
                rReady = false;
                rCooldown_T = rCooldown;
            }

            if (HealthPoints > 0)
            {
                _movement.UpdateMovement();
            }

            healthText.text = Mathf.Round(HealthPoints) + "/" + Mathf.Round(_maxHealth);
        }

        public override void OnDamage()
        {
            _healthBar.GetComponent<Slider>().value = HealthPoints / _maxHealth;
            if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
            {
                _anim.SetTrigger("hurt");
            }
        }

        public override void OnDeath()
        {
            _body.bodyType = RigidbodyType2D.Static;
            _anim.SetTrigger("die");
            Invoke("Die", 0.3f);
        }


        public void UpdateQ()
        {
            if (qReady==false)
            {
                if(qCooldown_T<= 0)
                {
                    qReady = true;
                }
                else
                {
                    qCooldown_T--;
                }
            }
            else
            {
                return;
            }
        }

        public void UpdateE()
        {
            if (eReady == false)
            {
                if (eCooldown_T <= 0)
                {
                    eReady = true;
                }
                else
                {
                    eCooldown_T--;
                }
            }
            else
            {
                return;
            }
        }

        public void UpdateR()
        {
            if (rReady == false)
            {
                if (rCooldown_T <= 0)
                {
                    rReady = true;
                }
                else
                {
                    rCooldown_T--;
                }
            }
            else
            {
                return;
            }
        }

        public void Die()
        {
            SceneManager.LoadScene("DeathScene");
        }

        public void IncreaseHealth()
        {
            HealthPoints = _maxHealth;
            _healthBar.GetComponent<Slider>().value = _maxHealth;
        }
    }
}