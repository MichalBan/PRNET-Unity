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
        }

        // Update is called once per frame
        void Update()
        {
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