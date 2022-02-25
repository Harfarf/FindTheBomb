using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BoolValue firstTimeBool;
    private int cellProgress = 0;

    [Header("BUTTONS")]
    public List<Button> cells;
    private Button bomb;

    [Header("UI")]
    public GameObject startPanel;
    public GameObject failPanel;
    public GameObject winPanel;
    public Button retryButton;

    [Header("AUDIO")]
    public AudioClip goodCellAudioClip;
    public AudioClip wrongCellAudioClip;
    public AudioClip winAudioClip;
    private AudioSource audioSource;

    //Singleton
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        //DontDestroyOnLoad(gameObject);
        #endregion

        //Audio
        audioSource = GetComponent<AudioSource>();

        //Asign cells
        bomb = cells[Random.Range(0, cells.Count)];
        cells.Remove(bomb);

        #region NormalCells
        foreach (Button cell in cells)
        {
            //color
            var cellColor = cell.colors;
            cellColor.pressedColor = Color.green;
            cellColor.selectedColor = Color.green;
            cell.colors = cellColor;
        }
        #endregion

        #region BombCell
        //color
        var bombColor = bomb.colors;
        //bombColor.normalColor = Color.red; //For testing
        bombColor.pressedColor = Color.red;
        bombColor.selectedColor = Color.red;
        bomb.colors = bombColor;
        //other
        bomb.GetComponent<Cell>().isBomb = true;
        #endregion

    }

    private void Start()
    {
        if (firstTimeBool.runTimeValue == true)
        {
            startPanel.SetActive(true);
            firstTimeBool.SetRunTimeValue(false);
        }
    }

    public void NormalCellPressed()
    {
        audioSource.clip = goodCellAudioClip;
        audioSource.Play();
        cellProgress += 1;
        CellReCount();
    }
    public void BombCellPressed()
    {
        audioSource.clip = wrongCellAudioClip;
        audioSource.Play();
        failPanel.SetActive(true);
        StartCoroutine(RetryGame());
    }
    private IEnumerator RetryGame()
    {
        yield return new WaitForSeconds(2f);
        retryButton.gameObject.SetActive(true);
    }

    public void CellReCount()
    {
        if(cellProgress >= cells.Count)
        {
            audioSource.clip = winAudioClip;
            audioSource.Play();
            winPanel.SetActive(true);
            StartCoroutine(RetryGame());
        }
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
