using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UIModel:AbstractModel
{
    public int SunNum;
    public int CoinNum;
    protected override void OnInit()
    {
        base.OnInit();
        SunNum = 150;
        CoinNum = 0;
    }
}