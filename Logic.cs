using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
    [SerializeField] private Text _text; // текст самой задачи
    [SerializeField] private InputField _inputField; // поле ввода ответа
    [SerializeField] private Image _timer; // таймер

    [SerializeField] private GameObject _ok; // кнопка для подтверждения ответа
    [SerializeField] private GameObject _panel;// панель на коорой написана задача
    [SerializeField] private GameObject _Losepanel;// панель проигрыша
    [SerializeField] private GameObject _player;//лягушка
    [SerializeField] private GameObject[] _nextPos;// массив состоящий из кувшинок 

    [SerializeField] private Animator _anim;// аниматор для проигрывания анимаций лягушки

    private float _time = 10f;// время таймера
    private bool _check = false;// проверка на правильность ответа
    private bool _fale = false;// проверка на проигрыш

    private int i = 0;// переменная для считывания номера кувшинки в массиве

    private string[] _task1 = 
    { 
        "Колю угостили конфетами. Половину конфет он съел, а оставшиеся 5 конфет отнёс брату. Сколько конфет дали Коле?",
        "2+2=", 
        "3+3="
    };// массив задач
    private string[] _answer1 = 
    { 
       "10",
       "4",
       "6"
    };//массив ответов

    private void Start() // метод старт проигрывается 1 раз, в самом начале игры
    {
        i = 0; // инициализация индекса
        _text.text = _task1[i];// задаем текст задачи в зависимости от индекса
        StartGame.ActiveTimer = false; // указываем чтобы таймер не шел пока не открылась панель
        _time = 10f;// указаваем значение таймера
    }

    private void Update()// метод проигрывается постоянно
    {
        _timer.fillAmount = _time/10;// таймер сделан при помощи Image.FillAmount и т.к. его значение максимум = 1, то делим его на 10 

        if (_inputField.text.Length == 0) // проверка на то что если мы ввели хоть одну цифру, то тогда появляется кнопка подтверждения
        {
            _ok.SetActive(false);
        }
        else
        {
            _ok.SetActive(true);
        }

        if (StartGame.ActiveTimer)// проверка   запустился ли таймер
        {
            _time -=1f*Time.deltaTime; // уменьшаем значение времени
        }
        else
        {
            _time = 10f;
        }

        if (_time <=0)// если время закончилась, то открывается панель проигрыша
        {
            _panel.SetActive(false);
            _fale = true;
        }
        if (_fale)
        {
            _anim.Play("f");// проигрывается анимация проигрыша
            StartCoroutine(PanelActive());// запускаем корутину
        }

        if (_check) // если ответ правильный то
        {
            if (_player.transform.position.y <= _nextPos[i].transform.position.y) //передвигаем лягушку к следующей кувшинке
            {
                _player.transform.Translate(0, 3 * Time.deltaTime, 0);//движение лягушки
                _anim.Play("jump");// анимация прыжка             
            }
            else
            {
                _player.transform.Translate(0, 0, 0); // останавливаем лягушку
                _check = false;//правильность меняем на false т.к. перешли к следующей задаче
                _anim.Play("idle");// анимация покоя
            }
           
        }

         
    }

    public void Checkanswer() //проверка ответа
    {
        if (_inputField.text == _answer1[i])//если ответ правильный
        {
            _panel.SetActive(false);//закрываем панель
            i += 1;//увеличиваем значение для перехода к следующей задаче
            _inputField.text = "";// обнуляем значение в поле ввода
            StartGame.ActiveTimer = false;//останавливаем таймер
            _check = true;//указываем правильность ответа
            StartCoroutine(NextPanelActive());//вызываем корутину
        }
        else // иначе
        {
            _panel.SetActive(false);//закрываем панель
            _fale = true;// указываем проигрыш
        }
    }

    private IEnumerator PanelActive()// корутина вызывающая через 1 секунду панель проигрыша
    {
        yield return new WaitForSeconds(1);
        _Losepanel.SetActive(true);
    }

    private IEnumerator NextPanelActive()// корутина вызывающая следующую задачу после правильного ответа
    {
        yield return new WaitForSeconds(2);
        _panel.SetActive(true);
        StartGame.ActiveTimer = true;//вызываем таймер
        _text.text = _task1[i];// меняем текст задачи 
    }

}
