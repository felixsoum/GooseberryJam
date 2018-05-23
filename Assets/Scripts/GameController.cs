using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject humanPrefab;
    public Transform humansTransform;
    public Vision vision;
    const float killDistance = 1.25f;
    List<Human> humans = new List<Human>();
    int correctKillCount;

    void Start()
    {
        SpawnHumans(8);
    }

    void SpawnHumans(int count)
    {
        humans.Clear();
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
        foreach (var human in humans)
        {
            human.ShowMark();
        }
    }

    public void DeactivateVision()
    {
        vision.Deactivate();
        foreach (var human in humans)
        {
            human.HideMark();
        }
    }

    public Vector3 GetHumanSpawnPos()
    {
        return new Vector3(Random.Range(-Util.XMax, Util.XMax), Random.Range(-Util.YMax, Util.YMax), 0);
    }

    public void Kill()
    {
        if (humans.Count > 0)
        {
            foreach (var human in humans)
            {
                if (Vector3.Distance(human.transform.position, Vector3.zero) <= killDistance)
                {
                    if (human.IsMarked)
                    {
                        correctKillCount++;
                    }
                    human.Kill();
                }
            }
        }
    }

    public void Reset()
    {
        if (humans.Count > 0)
        {
            foreach (var human in humans)
            {
                human.Die();
            }
        }
        SpawnHumans(8);
    }
}
