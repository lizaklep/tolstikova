using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
    [SerializeField] private Text _text; // ����� ����� ������
    [SerializeField] private InputField _inputField; // ���� ����� ������
    [SerializeField] private Image _timer; // ������

    [SerializeField] private GameObject _ok; // ������ ��� ������������� ������
    [SerializeField] private GameObject _panel;// ������ �� ������ �������� ������
    [SerializeField] private GameObject _Losepanel;// ������ ���������
    [SerializeField] private GameObject _player;//�������
    [SerializeField] private GameObject[] _nextPos;// ������ ��������� �� �������� 

    [SerializeField] private Animator _anim;// �������� ��� ������������ �������� �������

    private float _time = 10f;// ����� �������
    private bool _check = false;// �������� �� ������������ ������
    private bool _fale = false;// �������� �� ��������

    private int i = 0;// ���������� ��� ���������� ������ �������� � �������

    private string[] _task1 = 
    { 
        "���� �������� ���������. �������� ������ �� ����, � ���������� 5 ������ ���� �����. ������� ������ ���� ����?",
        "2+2=", 
        "3+3="
    };// ������ �����
    private string[] _answer1 = 
    { 
       "10",
       "4",
       "6"
    };//������ �������

    private void Start() // ����� ����� ������������� 1 ���, � ����� ������ ����
    {
        i = 0; // ������������� �������
        _text.text = _task1[i];// ������ ����� ������ � ����������� �� �������
        StartGame.ActiveTimer = false; // ��������� ����� ������ �� ��� ���� �� ��������� ������
        _time = 10f;// ��������� �������� �������
    }

    private void Update()// ����� ������������� ���������
    {
        _timer.fillAmount = _time/10;// ������ ������ ��� ������ Image.FillAmount � �.�. ��� �������� �������� = 1, �� ����� ��� �� 10 

        if (_inputField.text.Length == 0) // �������� �� �� ��� ���� �� ����� ���� ���� �����, �� ����� ���������� ������ �������������
        {
            _ok.SetActive(false);
        }
        else
        {
            _ok.SetActive(true);
        }

        if (StartGame.ActiveTimer)// ��������   ���������� �� ������
        {
            _time -=1f*Time.deltaTime; // ��������� �������� �������
        }
        else
        {
            _time = 10f;
        }

        if (_time <=0)// ���� ����� �����������, �� ����������� ������ ���������
        {
            _panel.SetActive(false);
            _fale = true;
        }
        if (_fale)
        {
            _anim.Play("f");// ������������� �������� ���������
            StartCoroutine(PanelActive());// ��������� ��������
        }

        if (_check) // ���� ����� ���������� ��
        {
            if (_player.transform.position.y <= _nextPos[i].transform.position.y) //����������� ������� � ��������� ��������
            {
                _player.transform.Translate(0, 3 * Time.deltaTime, 0);//�������� �������
                _anim.Play("jump");// �������� ������             
            }
            else
            {
                _player.transform.Translate(0, 0, 0); // ������������� �������
                _check = false;//������������ ������ �� false �.�. ������� � ��������� ������
                _anim.Play("idle");// �������� �����
            }
           
        }

         
    }

    public void Checkanswer() //�������� ������
    {
        if (_inputField.text == _answer1[i])//���� ����� ����������
        {
            _panel.SetActive(false);//��������� ������
            i += 1;//����������� �������� ��� �������� � ��������� ������
            _inputField.text = "";// �������� �������� � ���� �����
            StartGame.ActiveTimer = false;//������������� ������
            _check = true;//��������� ������������ ������
            StartCoroutine(NextPanelActive());//�������� ��������
        }
        else // �����
        {
            _panel.SetActive(false);//��������� ������
            _fale = true;// ��������� ��������
        }
    }

    private IEnumerator PanelActive()// �������� ���������� ����� 1 ������� ������ ���������
    {
        yield return new WaitForSeconds(1);
        _Losepanel.SetActive(true);
    }

    private IEnumerator NextPanelActive()// �������� ���������� ��������� ������ ����� ����������� ������
    {
        yield return new WaitForSeconds(2);
        _panel.SetActive(true);
        StartGame.ActiveTimer = true;//�������� ������
        _text.text = _task1[i];// ������ ����� ������ 
    }

}
