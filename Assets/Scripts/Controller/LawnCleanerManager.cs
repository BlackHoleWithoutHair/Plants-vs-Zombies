using System.Collections.Generic;
using UnityEngine;
namespace BattleScene
{
    public class LawnCleanerManager
    {
        private class LawnCleaner
        {
            public LawnCleaner(GameObject obj)
            {
                this.obj = obj;
            }
            public GameObject obj;
            public bool isRun;
        }
        private PlantController m_System;
        private List<LawnCleaner> LawnCleaners;
        private List<IZombie> zombies;
        private float Speed = 2f;
        public LawnCleanerManager(PlantController sys)
        {
            m_System = sys;
            LawnCleaners = new List<LawnCleaner>();
            foreach (Transform obj in GameObject.Find("LawnCleaners").GetComponentsInChildren<Transform>())
            {
                if (obj.name != "LawnCleaners")
                {
                    LawnCleaners.Add(new LawnCleaner(obj.gameObject));
                }
            }
        }
        public void GameStart()
        {
            zombies = Mediator.Instance.GetController<ZombieController>().GetZombies();
        }
        public void GameUpdate()
        {
            for (int i = 0; i < LawnCleaners.Count; i++)
            {
                if (LawnCleaners[i].isRun == true)
                {
                    LawnCleaners[i].obj.GetComponent<Animator>().enabled = true;
                    LawnCleaners[i].obj.transform.position += Vector3.right * Speed * Time.deltaTime;
                    if (LawnCleaners[i].obj.transform.position.x > 6.5f)
                    {
                        Object.Destroy(LawnCleaners[i].obj);
                        LawnCleaners.RemoveAt(i);
                    }
                }
            }
            foreach (IZombie zombie in zombies)
            {
                foreach (LawnCleaner cleaner in LawnCleaners)
                {
                    if (Vector2.Distance(zombie.transform.position, cleaner.obj.transform.position) < 1f
                        && zombie.transform.position.y > cleaner.obj.transform.position.y)
                    {
                        cleaner.isRun = true;
                        zombie.UnderAttack(5000);
                    }
                }
            }
        }
    }
}

