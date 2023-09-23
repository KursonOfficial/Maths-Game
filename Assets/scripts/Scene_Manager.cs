using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Manager : MonoBehaviour
{
    public void Menu(){
        SceneManager.LoadScene(0);
    }
        public void Multiplayer(){
        SceneManager.LoadScene(1);
    }
    public void MainGame(){
        SceneManager.LoadScene(2);
    }
    public void Option(){
        SceneManager.LoadScene(3);
    }
    public void Exit(){
        Application.Quit();
    }
}
