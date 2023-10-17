using System.Collections.Generic;
using UnityEngine;

public class IdleZombieManager
{
    private ZombieController m_System;
    private List<GameObject> zombies;
    private List<StageProperty> properties;
    private int row;
    public IdleZombieManager(ZombieController sys)
    {
        m_System = sys;
        zombies = new List<GameObject>();
    }
    public void BeforeGameStart()
    {
        properties = AttributeFactory.Instance.GetStage(ArchiveCommand.Instance.StageId).StageProperties;
        foreach (StageProperty stage in properties)
        {
            GameObject obj = Object.Instantiate(ResourcesFactory.GetZombie(stage.Type), GetPosition(), Quaternion.identity);
            obj.GetComponent<Animator>().enabled = false;
            zombies.Add(obj);
        }
    }
    public void GameStart()
    {
        foreach (GameObject obj in zombies)
        {
            Object.Destroy(obj);
        }
        zombies.Clear();
    }
    private Vector2 GetPosition()
    {
        Random.InitState(Random.Range(0, 100));
        row = Random.Range(0, 5);
        switch (row)
        {
            case 0:
                return new Vector2(Random.Range(8.39f, 11.7f), Random.Range(-3.5f, -2.9f));
            case 1:
                return new Vector2(Random.Range(8.19f, 11.7f), Random.Range(-2.23f, -0.23f));
            case 2:
                return new Vector2(Random.Range(8.2f, 11.7f), Random.Range(-0.7f, 1.3f));
            case 3:
                return new Vector2(Random.Range(8.0f, 10.8f), Random.Range(1.1f, 3.1f));
            case 4:
                return new Vector2(Random.Range(7.34f, 10.0f), Random.Range(1.5f, 2.1f));
        }
        return Vector2.zero;
    }
}
