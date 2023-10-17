using DG.Tweening;
using UnityEngine;
namespace BattleScene
{
    public class PlantCard : MonoBehaviour
    {
        public PanelRoot m_Panel;
        private GameObject targetPoint;
        private float MoveTime;
        private bool isClick;
        // Start is called before the first frame update
        void Start()
        {
            isClick = false;
            targetPoint = GameObject.Find("AwardCardPoint");
            MoveTime = 3;
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnMouseUp()
        {
            if (isClick == false)
            {
                isClick = true;
                if (m_Panel == null)
                {
                    Debug.Log("PlantCard m_Panel is null");
                }
                else
                {
                    transform.DOMove(targetPoint.transform.position, MoveTime).OnComplete(() =>
                    {
                        Debug.Log(123331);
                        m_Panel.SetCardClick();
                    });
                    transform.DOScale(new Vector3(1.6f, 1.6f), MoveTime);
                }
            }
        }
    }

}
