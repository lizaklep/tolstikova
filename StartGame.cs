using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject[] _startGameObject; // массив объектов на сцене
    [SerializeField] private GameObject _panel;//самая первая панель с задачей

    public static bool ActiveTimer = false;// таймер выключен

    private bool men = false;// для проверки открыта ли меню

    public void Restart()//рестарт игры
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartsGame()// когда начинается игра все лишние элементы отключаем
    {
        for(int i = 0; i <_startGameObject.Length;i++)
        {
            _startGameObject[i].SetActive(false);
        }

        StartCoroutine(panelOpen());// вызываем корутину
    }

    private void Update()
    {
        if (men)//проверяем если мы вышли в меню то открываем все элементы которые закрыли
        {
            for (int i = 0; i < _startGameObject.Length; i++)
            {
                _startGameObject[i].SetActive(false);
            }
             men = false;

        }
    }

    private IEnumerator panelOpen()//корутина вызавающая таймер и панель с задачей через 1 секунду в начале игры
    {
        yield return new WaitForSeconds(1);
        _panel.SetActive(true);
        ActiveTimer = true;
    }
}
