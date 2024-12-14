using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject initialPage;
    [SerializeField] private GameObject mapPage;
    [SerializeField] private GameObject logPage;
    [SerializeField] private GameObject dataPage;
    [SerializeField] private GameObject optionsPage;

    //log info
    [SerializeField] private BoolSO agendaInfo;
    [SerializeField] private GameObject log0300PM;
    [SerializeField] private GameObject log0320PM;
 
    public void OpenInitialPage()
    {
        mapPage.SetActive(false);

        initialPage.SetActive(true);
    }

    public void OpenMap()
    {
        initialPage.SetActive(false);
        logPage.SetActive(false);
        dataPage.SetActive(false);
        optionsPage.SetActive(false);

        mapPage.SetActive(true);

    }

    public void OpenLog()
    {
        initialPage.SetActive(false);
        mapPage.SetActive(false);
        dataPage.SetActive(false);
        optionsPage.SetActive(false);

        logPage.SetActive(true);

        UpdateLog();
    }

    public void OpenData()
    {
        initialPage.SetActive(false);
        mapPage.SetActive(false);
        logPage.SetActive(false);
        optionsPage.SetActive(false);

        dataPage.SetActive(true);
    }

    public void OpenOptions()
    {
        initialPage.SetActive(false);
        mapPage.SetActive(false);
        logPage.SetActive(false);
        dataPage.SetActive(false);

        optionsPage.SetActive(true);
    }

    public void UpdateLog()
    {
        if (agendaInfo.Log0300PM)
        {
            log0300PM.SetActive(true);
        }
        else
        {
            log0300PM.SetActive(false);
        }
        if (agendaInfo.Log0320PM)
        {
            log0320PM.SetActive(true);
        } else
        {
            log0320PM.SetActive(false);
        }
    }
}
