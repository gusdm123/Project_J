using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<UIManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 static 변수

    public GameObject gameOverCanvas; // 게임 오버시 띄울 캔버스

    public void GameOverActive() // 게임 오버 캔버스를 활성화
    {
        gameOverCanvas.SetActive(true);
    }

    public void  GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
