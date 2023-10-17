using UnityEngine;
namespace BattleScene
{
    public class FlowerSun : ISun
    {
        private Vector2 SpeedDir;

        private float Speed = 1f;
        private float Gravity = 1f;
        private bool isStop;
        private float CumulativeTime;
        public FlowerSun(GameObject obj,Vector2 pos):base(obj,pos)
        {

        }
        protected override void Init()
        {
            base.Init();

            SpeedDir = (Vector2.up + Vector2.right) * Speed;
        }
        protected override void BeforeClickUpdate()
        {
            base.BeforeClickUpdate();
            if (!isStop)
            {
                SpeedDir = SpeedDir + Vector2.down * Gravity * Time.deltaTime;
                transform.Translate(SpeedDir * Time.deltaTime);
                CumulativeTime += Time.deltaTime;
                if (CumulativeTime > 3f)
                {
                    isStop = true;
                    CumulativeTime = 0f;
                }
            }
            else
            {
                CumulativeTime += Time.deltaTime;
                if (CumulativeTime > 4f)
                {
                    Remove();
                }
            }
        }
    }
}

