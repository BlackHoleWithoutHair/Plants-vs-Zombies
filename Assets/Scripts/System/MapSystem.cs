using UnityEngine;

public class MapSystem
{
    GameObject UnSolderBK;
    GameObject DayTimeBK;
    public MapSystem()
    {
        UnSolderBK = GameObject.Find("GameBG1");
        DayTimeBK = GameObject.Find("GameBG");
    }
    public void GameStart()
    {
        int stageId = ArchiveCommand.Instance.StageId;
        if (stageId == 1)
        {
            DayTimeBK.GetComponent<SpriteRenderer>().enabled = false;
            UnSolderBK.GetComponent<SpriteRenderer>().enabled = true;
            UnSolderBK.transform.Find("sod1row").gameObject.SetActive(true);
            UnSolderBK.transform.Find("sod3row").gameObject.SetActive(false);
        }
        else if (stageId <= 3)
        {
            DayTimeBK.GetComponent<SpriteRenderer>().enabled = false;
            UnSolderBK.GetComponent<SpriteRenderer>().enabled = true;
            UnSolderBK.transform.Find("sod1row").gameObject.SetActive(false);
            UnSolderBK.transform.Find("sod3row").gameObject.SetActive(true);
        }
        else
        {
            DayTimeBK.GetComponent<SpriteRenderer>().enabled = true;
            UnSolderBK.GetComponent<SpriteRenderer>().enabled = false;
            UnSolderBK.transform.Find("sod1row").gameObject.SetActive(false);
            UnSolderBK.transform.Find("sod3row").gameObject.SetActive(false);
        }
    }
}
