using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject humanPrefab;
    public Transform humansTransform;

    List<Human> humans = new List<Human>();

    void Start()
    {
        SpawnHumans(4);
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
                human.IsMarked = true;
            }
        }
    }

    public void ActivateVision()
    {
        foreach (var human in humans)
        {
            human.ShowMark();
        }
    }

    public void DeactivateVision()
    {
        foreach (var human in humans)
        {
            human.HideMark();
        }
    }

    public Vector3 GetHumanSpawnPos()
    {
        return new Vector3(Random.Range(-Util.XMax, Util.XMax), Random.Range(-Util.YMax, Util.YMax), 0);
    }
}
