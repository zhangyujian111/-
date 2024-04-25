using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Brid
{
    //÷ÿ–¥ShowSkill
    public override void ShowSkill()
    {
        base.ShowSkill();
        rb.velocity *= 2;
    }
}
