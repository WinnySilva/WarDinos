using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estegossauro : Dinossauro {

	public Estegossauro () {
        base.custo = 25;
        base.abilityCost = 100;
        base.alcance_ataque =1;
		base.ataque=10;
		base.velocidadeAtaque=2.0;
		base.velocidade_deslocamento=2;
		base.vida=100;

        base.custoAttrAtaque = 1;
        base.custoAttrVelocidadeAtaque = 1;
        base.custoAttrVida = 1;
        base.custoAttrVelocidadeDeslocamento = 1;

        base.dinoType= Dinossauro.DinoTypes.ESTEGOSSAURO;

        base.MAX_ALCANCE_ATAQUE=1;
		base.MAX_ATAQUE=20;
		base.MAX_VELOCIDADE_ATAQUE=1.75;
		base.MAX_VELOCIDADE_DESLOCAMENTO=4;
		base.MAX_VIDA=200;
        base.MAX_ATTR_VIDA = 10;
        base.MAX_ATTR_ATAQUE = 10;
        base.MAX_ATTR_VEL_ATQ = 10;
        base.MAX_ATTR_VEL_DES = 2;
        base.ataque_upg = 1;
        base.velocidadeAtaque_upg = -0.025;
        base.velocidade_deslocamento_upg = 1;
        base.vida_upg = 10;

        base.playerID=-1;
		base.nSlot =2;

	}
    private void Start()
    {

    }

    #region implemented abstract members of Dinossauro
    
    /**
     * Habilidade implementada direto no ataque 
     */
    public override void Habilidade()
    {

		//throw new System.NotImplementedException ();
	}

    public override bool Atacar(GroupController gp)
    {
        Gc = gp;
        Dinossauro dTarget = null;
        int menorVida = -1;
        //Habilidade on = Ataque em área
        if (habilidadeOn)
        {
            foreach (Dinossauro d in gp.DinosDinossauro) {
                d.Vida = d.Vida - (ataque - Random.Range(1, ataque / 2)) ;
            }
            return true;
        }
        //se não, single
        else
        {
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
                dTarget.Vida = dTarget.Vida - (ataque - Random.Range(1, ataque / 2) );
                Debug.Log(GetInstanceID() + "Attacked with " + ataque + " dmg. Target was " + dTarget + "which is now with " + dTarget.Vida + "life");
                return true;
            }
            else
            {
                Debug.Log(GetInstanceID() + "Attacked but there were no target");
                return false;
            }
        }

    }

    #endregion
}
