using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject humanPrefab;
    public Transform humansTransform;
    public Vision vision;

    public GameObject visionButton;
    public GameObject killButton;
    public GameObject endPanel;
    public GameObject killZone;
    public WhiteFade whiteFade;
    public LevelNumber levelNumber;
    public Texture2D cursorTexture;

    public AudioSource killAudio;
    public AudioSource visionAudio;
    public AudioSource nextAudio;

    public const float KillDistance = 1.25f;
    List<Human> humans = new List<Human>();
    int correctKillCount;
    public bool IsGameOver { get; private set; }
    const float endDelay = 2;
    int currentLevel;
    bool isTransitioning;

    void Awake()
    {
        IsGameOver = false;
    }

    void Start()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(10, 70), CursorMode.ForceSoftware);
        NextLevel();
    }

    void NextLevel()
    {
        correctKillCount = 0;
        visionButton.SetActive(true);
        killButton.SetActive(true);
        isTransitioning = false;
        currentLevel++;
        whiteFade.Flash();
        levelNumber.SetNumber(currentLevel);
        Clear();        
        SpawnHumans(currentLevel * 2);
        if (currentLevel > 1)
        {
            nextAudio.Play();
        }
    }

    void SpawnHumans(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Human human = Instantiate(humanPrefab, GetHumanSpawnPos(), Quaternion.identity, humansTransform).GetComponent<Human>();
            human.SetGameController(this);
            humans.Add(human);

            if (i < count/2)
            {
                human.Mark();
            }
        }
    }

    public void ActivateVision()
    {
        vision.Activate();
        if (!IsGameOver)
        {
            visionAudio.Play();
        }
        foreach (var human in humans)
        {
            if (human && human.gameObject)
            {
                human.ShowMark();
            }
        }
        visionButton.SetActive(false);
    }

    public void DeactivateVision()
    {
        vision.Deactivate();
        foreach (var human in humans)
        {
            if (human && human.gameObject && human.IsAlive)
            {
                human.HideMark();
            }
        }
    }

    void Clear()
    {
        foreach (var human in humans)
        {
            if (human && human.gameObject && human.IsAlive)
            {
                human.Die();
            }
        }
        humans.Clear();
    }

    public Vector3 GetHumanSpawnPos()
    {
        return new Vector3(Random.Range(-Util.XMax, Util.XMax), Random.Range(-Util.YMax, Util.YMax), 0);
    }

    public void Kill()
    {
        DeactivateVision();
        if (humans.Count > 0)
        {
            foreach (var human in humans)
            {
                if (human && human.gameObject && human.IsAlive && Vector2.Distance(human.GetFeetPosition(), Vector2.zero) <= KillDistance)
                {
                    if (human.IsMarked)
                    {
                        correctKillCount++;
                    }
                    else
                    {
                        BeginEnd();
                    }
                    human.Kill();
                }
            }
            killAudio.Play();
        }

        if (!isTransitioning && !IsGameOver && correctKillCount == currentLevel)
        {
            isTransitioning = true;
            killButton.SetActive(false);
            Invoke("NextLevel", endDelay);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }
    
    void BeginEnd()
    {
        if (IsGameOver)
        {
            return;
        }
        IsGameOver = true;
        killButton.SetActive(false);
        Invoke("End", endDelay);
    }

    void End()
    {
        killZone.SetActive(false);
        endPanel.SetActive(true);
        ActivateVision();
    }
}
