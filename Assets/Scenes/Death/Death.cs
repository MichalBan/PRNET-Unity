using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scenes.Death
{
    public class DeathMenu : MonoBehaviour
    {
        void Start()
        {
            Invoke("Ok", 3f);
        }
    
        void Ok()
        {
            Debug.Log("ok");
            SceneManager.LoadScene("MenuScene");
        }
    }
}
