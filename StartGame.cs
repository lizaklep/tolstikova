using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject[] _startGameObject; // ������ �������� �� �����
    [SerializeField] private GameObject _panel;//����� ������ ������ � �������

    public static bool ActiveTimer = false;// ������ ��������

    private bool men = false;// ��� �������� ������� �� ����

    public void Restart()//������� ����
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartsGame()// ����� ���������� ���� ��� ������ �������� ���������
    {
        for(int i = 0; i <_startGameObject.Length;i++)
        {
            _startGameObject[i].SetActive(false);
        }

        StartCoroutine(panelOpen());// �������� ��������
    }

    private void Update()
    {
        if (men)//��������� ���� �� ����� � ���� �� ��������� ��� �������� ������� �������
        {
            for (int i = 0; i < _startGameObject.Length; i++)
            {
                _startGameObject[i].SetActive(false);
            }
             men = false;

        }
    }

    private IEnumerator panelOpen()//�������� ���������� ������ � ������ � ������� ����� 1 ������� � ������ ����
    {
        yield return new WaitForSeconds(1);
        _panel.SetActive(true);
        ActiveTimer = true;
    }
}
