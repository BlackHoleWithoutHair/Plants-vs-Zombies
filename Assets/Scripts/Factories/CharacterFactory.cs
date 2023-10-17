using BattleScene;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterFactory:Singleton<CharacterFactory>
{
    private CharacterFactory() { }
    public IPlant GetPlant(PlantType type, Vector3 position)
    {
        GameObject obj = Object.Instantiate(ResourcesFactory.GetPlant(type));
        GameObject soil = ResourcesFactory.GetEffect("SoilParticles");
        IPlant plant = null;
        GameObject effect = null;
        AudioUtility.Instance.PlayOneShot("plant");
        switch (type)
        {
            case PlantType.SunFlower:
                plant = new SunFlower(obj);
                effect = Object.Instantiate(soil);
                effect.SetActive(true);
                break;
            case PlantType.PeaShooter:
                plant = new PeaShooter(obj);
                effect = Object.Instantiate(soil);
                effect.SetActive(true);
                break;
            case PlantType.WallNut:
                plant = new WallNut(obj);
                effect = Object.Instantiate(soil);
                effect.SetActive(true);
                break;
            case PlantType.CherryBomb:
                plant = new Cherry(obj);
                obj.SetActive(true);
                break;
            case PlantType.PotatoMine:
                plant = new PotatoMine(obj);
                effect = Object.Instantiate(soil);
                effect.SetActive(true);
                break;
        }
        Mediator.Instance.GetSystem<GroundSystem>().Plant(position, plant);
        plant.transform.position = position;
        plant.m_Attr.groundPosition = Mediator.Instance.GetSystem<GroundSystem>().WorldPositionToGroundPosition(position);
        if(effect!=null)
        {
            effect.transform.position = plant.transform.Find("Root").position;
            effect.transform.rotation = Quaternion.Euler(-90, 0, 0);
        }
        if(plant==null)
        {
            Debug.Log("CharacterFactory GetPlant·µ»Ønull");
        }
        return plant;
    }
    public static GameObject GetPlantImage(PlantType type)
    {
        GameObject obj = null;
        obj = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Plants/" + type.ToString()));
        if (obj == null)
        {
            Debug.Log("CharacterFactory·µ»Ønull");
            return null;
        }
        foreach(Animator anim in obj.transform.GetComponentsInChildren<Animator>())
        {
            anim.enabled = false;
        }
        foreach(SpriteRenderer render in obj.transform.GetComponentsInChildren<SpriteRenderer>())
        {
            render.color = new Color(1, 1, 1, 0.5f);
        }
        return obj;
    }
    public IZombie GetZombie(ZombieType type)
    {
        int index;
        if (ArchiveCommand.Instance.StageId == 1)
        {
            index = 2;
        }
        else if (ArchiveCommand.Instance.StageId == 2 || ArchiveCommand.Instance.StageId == 3)
        {
            index = Random.Range(1, 4);
        }
        else
        {
            index = Random.Range(0, 5);
        }
        Vector3 position = new Vector3(8f, Mediator.Instance.GetSystem<GroundSystem>().GetWorldOffsetYByRowIndex(index), 0);
        GameObject obj = Object.Instantiate(ResourcesFactory.GetZombie(type), position, Quaternion.identity);
        IZombie zombie = null;
        switch (type)
        {
            case ZombieType.Zombie:
                zombie = new Zombie(obj);
                break;
            case ZombieType.FlagZombie:
                zombie = new Zombie(obj);
                break;
        }
        zombie.m_Attr.groundPosition.y = index;
        obj.transform.Find("BulletCheckBox").GetComponent<Symbol>().SetCharacter(zombie);
        if(zombie==null)
        {
            Debug.Log("CharacterFactory GetZombie·µ»Ønull");
        }
        return zombie;
    }
}
