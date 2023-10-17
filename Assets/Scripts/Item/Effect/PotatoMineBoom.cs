using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PotatoMineBoom : Item
{ 
    public PotatoMineBoom(GameObject obj,Vector2 pos) : base(obj, pos) { }
    protected override void OnEnter()
    {
        base.OnEnter();
        Object.Instantiate(ResourcesFactory.GetEffect("PotatoMineParticles"), transform.position, Quaternion.Euler(-90,0,0));
        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOScale(Vector3.one, 0.3f).OnComplete(() =>
        {
            CoroutinePool.Instance.StartCoroutine(WaitForRemove());
        });
    }
    private IEnumerator WaitForRemove()
    {
        yield return new WaitForSeconds(1f);
        Remove();
    }
}