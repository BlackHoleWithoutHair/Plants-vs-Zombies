using UnityEngine;
namespace BattleScene
{
    public class IBullet : Item
    {
        private int damage;
        private bool isHit;//是否命中
        public IBullet(GameObject obj, Vector2 pos) : base(obj, pos) { }
        protected override void Init()
        {
            base.Init();
            TriggerCenter.Instance.RegisterObserver(TriggerType.OnTriggerEnter, gameObject, "Zombie", (obj) =>
            {
                if (!isHit)
                {
                    isHit = true;
                    OnHitEnemy(obj);
                }
            });
        }
        protected virtual void OnHitEnemy(GameObject obj)
        {
            AudioUtility.Instance.PlayOneShot("splat" + Random.Range(2, 4));
            Remove();
            if (obj.GetComponent<Symbol>() != null)
            {
                obj.GetComponent<Symbol>().GetCharacter().UnderAttack(damage);
            }
        }
        public void SetDamage(int damage)
        {
            this.damage = damage;
        }
    }
}
