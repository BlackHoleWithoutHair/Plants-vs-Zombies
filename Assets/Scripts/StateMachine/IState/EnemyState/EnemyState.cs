using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState:IState
{
    protected new EnemyStateController m_Controller { get => base.m_Controller as EnemyStateController; set => base.m_Controller = value; }
    protected Animator m_Animator;
    protected IZombie enemy;
    protected GameObject gameObject;
    protected ZombieAttribute m_Attr;
    private GroundSystem m_GroundSystem;
    private float AttackDistance = 0.65f;
    public EnemyState(IStateController controller):base(controller)
    {
        enemy = m_Controller.GetEnemy();
        gameObject = enemy.gameObject;
        m_Animator = gameObject.GetComponent<Animator>();
        m_Attr = m_Controller.GetEnemy().GetAttr();
        m_GroundSystem = BattleScene.Mediator.Instance.GetSystem<GroundSystem>();
    }
    protected bool isFindPlant()
    {
        List<IPlant> plants = m_GroundSystem.GetPlantByRow((int)m_Attr.groundPosition.y);
        foreach(IPlant plant in plants)
        {
            float dis = gameObject.transform.position.x - plant.gameObject.transform.position.x;
            if (dis < AttackDistance && dis > 0.1)
            {
                m_Controller.BeFoundPlant = plant;
                return true;
            }
        }
        return false;
    }
    protected bool isFindPlant(IPlant plant)
    {
        if (plant.m_Attr.Hp <= 0) return false;
        float dis = gameObject.transform.position.x - plant.gameObject.transform.position.x;
        if (dis < AttackDistance && dis > 0.1)
        {
            return true;
        }
        return false;
    }
}
