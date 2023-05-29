using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("Global");
    }
   
}
