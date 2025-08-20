using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum eUI_category
    {
        PlayTimeUI,
        Sys_Notification,
        Interact_Notification,
        Tutor_Notification,
        Panels
    }

    public enum Panels
    {
        PauseMenu,
        DeadMenu,
        ItemMenu
    }

    private string UI_GamePanelRoot = "Prefab/UI/";

    public GameObject m_CanvasRoot;

    public GameObject cameraController;

    public GameObject menuUI;
    public GameObject pauseMenu;
    public GameObject player;
    public GameObject deadmenu;
    public GameObject Endmenu;

    public Dictionary<string, GameObject> m_PanelList = new Dictionary<string, GameObject>();

    public List<GameObject> panelList;

    private bool isPaused = false;

    private void Start()
    {
        foreach (GameObject panel in panelList)
        {
            panel.SetActive(false);
            m_PanelList.Add(panel.name, panel);
            Debug.Log(m_PanelList[panel.name]);
        }
    }

    private bool CheckCanvasRootIsNull()
    {
        if (m_CanvasRoot == null)
        {
            Debug.LogError("m_CanvasRoot is Null, please add UIHandler");
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsPanelLive(string name)
    {
        return m_PanelList.ContainsKey(name);
    }

    public GameObject ShowPanel(string name)
    {
        if (CheckCanvasRootIsNull()) return null;

        if (IsPanelLive(name))
        {
            Debug.LogErrorFormat("[{0}] is already Showing", name);
            return null;
        }

        GameObject loadGo = Resources.Load<GameObject>(UI_GamePanelRoot + name);
        if (loadGo == null) { return null; }

        GameObject panel = GameObject.Instantiate(loadGo);
        panel.name = name;

        m_PanelList.Add(name, panel);

        return panel;
    }

    public void TogglePannel(string name, bool isOn)
    {
        //Show or Hide Panel
        if (IsPanelLive(name))
        {
            if (m_PanelList[name] != null)
                m_PanelList[name].SetActive(isOn);
        }
        else
        {
            Debug.LogErrorFormat("[{0}] not found", name);
        }
    }

    public void ClosePane(string name)
    {
        if (IsPanelLive(name))
        {
            if (m_PanelList[name] != null)
                Destroy(m_PanelList[name]);

            m_PanelList.Remove(name);
        }
        else
        {
            Debug.LogErrorFormat("[{0}] not found", name);
        }
    }

    public void CloseAllPanel()
    {
        foreach (GameObject panel in panelList)
        {
            panel.SetActive(false);
        }
    }

    public void CloseAllPanelForDIctionary()
    {
        foreach (KeyValuePair<string, GameObject> item in m_PanelList)
        {
            if(item.Value != null)
                Object.Destroy(item.Value);
        }
        m_PanelList.Clear();
    }

    public Vector2 GetCanvasSize()
    {
        if(CheckCanvasRootIsNull())
            return Vector2.zero * -1;

        RectTransform trans = m_CanvasRoot.transform as RectTransform;

        return trans.sizeDelta;
    }
    
    public void KeyGetPanel(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            
        }
    }
}
