using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apatossauro : Dinossauro {
    protected GroupController oldGroup;
	public Apatossauro() {
        oldGroup = null;
        base.custo = 35;
        base.abilityCost = 200;
        base.alcance_ataque =1;
		base.ataque=15;
		base.velocidadeAtaque=2.5;
		base.velocidade_deslocamento=2;
		base.vida=200;

        base.custoAttrAtaque = 1;
        base.custoAttrVelocidadeAtaque = 1;
        base.custoAttrVida = 1;
        base.custoAttrVelocidadeDeslocamento = 1;

        base.dinoType= Dinossauro.DinoTypes.APATOSSAURO;

        base.MAX_ALCANCE_ATAQUE=1;
		base.MAX_ATAQUE=25;
		base.MAX_VELOCIDADE_ATAQUE=2.25;
		base.MAX_VELOCIDADE_DESLOCAMENTO=4;
		base.MAX_VIDA=400;
        base.MAX_ATTR_VIDA = 5;
        base.MAX_ATTR_ATAQUE = 10;
        base.MAX_ATTR_VEL_ATQ = 10;
        base.MAX_ATTR_VEL_DES = 2;

        base.ataque_upg = 1;
        base.velocidadeAtaque_upg = -0.025;
        base.velocidade_deslocamento_upg = 1;
        base.vida_upg = 40;

        base.playerID=-1;
		base.nSlot = 2;

        
    }

    private void Start()
    {

    }



    #region implemented abstract members of Dinossauro
    public override void Habilidade()
    {
		//i really hope it works like that o.o'
		foreach(Dinossauro d in Gc.enemyTargetGroup.DinosDinossauro){
            if(d != null)
			    d.VelocidadeAtaque = d.VelocidadeAtaque * 1.5;
		}
			/*divide their attack speed by 2*/
		//when an apatassauro dies, the atk spd of the enemy should go back to the original value.
		//throw new System.NotImplementedException ();
	}

    public override bool Atacar(GroupController gp)
    {
        Gc = gp;
        Dinossauro dTarget = null;
        int menorVida = -1;


        foreach (Dinossauro d in gp.DinosDinossauro)
        {
            if (d != null && (d.Vida < menorVida || menorVida == -1))
            {
                //Apatasaur ability only will work one time per group;
                if(Gc != oldGroup)
                {
                    oldGroup = Gc;
                    if (habilidadeOn)
                        Habilidade();
                }
                dTarget = d;
                menorVida = d.Vida;
            }
        }
        if (menorVida != -1)
        {
            dTarget.Vida = dTarget.Vida - (ataque - Random.Range(0, ataque / 2) );
            Debug.Log(GetInstanceID() + "Attacked with " + ataque + " dmg. Target was " + dTarget + "which is now with " + dTarget.Vida + "life");
            return true;
        }
        else
        {
            Debug.Log(GetInstanceID() + "Attacked but there were no target");
            return false;
        }
    }
    #endregion
}
