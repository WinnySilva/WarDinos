using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRex : Dinossauro {
    protected int life;
    public TRex(){

        base.custo = 100;
        base.abilityCost = 100;
        base.alcance_ataque =1;
		base.ataque=50;
		base.velocidadeAtaque=2;
		base.velocidade_deslocamento=3;
		base.vida=100;
        life = base.vida;
        base.custoAttrAtaque = 1;
        base.custoAttrVelocidadeAtaque = 1;
        base.custoAttrVida = 1;
        base.custoAttrVelocidadeDeslocamento = 1;

        base.dinoType= Dinossauro.DinoTypes.TREX;

        base.MAX_ALCANCE_ATAQUE=1;
		base.MAX_ATAQUE=80;
		base.MAX_VELOCIDADE_ATAQUE=1.25;
		base.MAX_VELOCIDADE_DESLOCAMENTO=6;
		base.MAX_VIDA=200;
        base.MAX_ATTR_VIDA = 10;
        base.MAX_ATTR_ATAQUE = 10;
        base.MAX_ATTR_VEL_ATQ = 10;
        base.MAX_ATTR_VEL_DES = 3;

        base.vida_upg = 10;
        base.ataque_upg = 3;
        base.velocidadeAtaque_upg = -0.025;
        base.velocidade_deslocamento_upg = 1;

        base.playerID=-1;
		base.nSlot=4;
	}
    private void Start()
    {

    }


    #region implemented abstract members of Dinossauro

    public override void Habilidade()
    {
        /**
		 * life steal.
		*/
        if (base.vida + base.ataque < life)
        {
            base.vida = base.vida + (int) base.ataque/2;
        }
        
		

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
                dTarget = d;
                menorVida = d.Vida;
            }
        }
        if (menorVida != -1)
        {
            dTarget.Vida = dTarget.Vida - (ataque - Random.Range(0,ataque/2) ) ;
            if (habilidadeOn) {
                Habilidade();
            }
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
