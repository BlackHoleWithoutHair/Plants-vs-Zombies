using BattleScene;
using UnityEngine;
public enum BulletType
{
    Pea,
    FlowerSun,
    DropSun,
}
public enum EffectType
{
    PotatoMineBoom,
    PeaSplats,
}

public class ItemFactory:Singleton<ItemFactory>
{
    private ItemFactory()
    {

    }
    public Item GetEffect(EffectType type,Vector2 pos )
    {
        GameObject obj = Object.Instantiate(ResourcesFactory.GetEffect(type.ToString()));
        obj.SetActive(false);
        Item item = null;
        switch(type)
        {
            case EffectType.PotatoMineBoom:
                item = new PotatoMineBoom(obj, pos);
                break;
        }
        if (item == null)
        {
            Debug.Log("ItemFactory GetEffect(" + type + ") return null");
        }
        return item;
    }
    public Item GetBullet(BulletType type,Vector2 pos)
    {
        GameObject obj=Object.Instantiate(ResourcesFactory.GetBullet(type));
        obj.SetActive(false);
        Item item = null;
        switch(type)
        {
            case BulletType.Pea:
                item = new Pea(obj, pos);
                break;
            case BulletType.FlowerSun:
                item=new FlowerSun(obj, pos);
                break;
            case BulletType.DropSun:
                item=new DropSun(obj, pos);
                break;
        }
        if(item==null)
        {
            Debug.Log("ItemFactory GetBullet(" + type + ") return null");
        }
        return item;
    }
}
