using UnityEngine;

namespace BattleScene
{
    public class SunFlower : IBulletPlant
    {
        public SunFlower(GameObject obj) : base(obj)
        {
            m_Attr = AttributeFactory.Instance.GetPlantAttribute(PlantType.SunFlower);
        }
        protected override void OnCharacterStart()
        {
            base.OnCharacterStart();
            FireInterval = Random.Range(10f, 15f);
        }
        protected override void OnFire()
        {
            base.OnFire();
            FireInterval = Random.Range(10f, 15f);
            ItemFactory.Instance.GetBullet(BulletType.FlowerSun, m_FirePoint.transform.position).AddToController();
        }
    }

}
