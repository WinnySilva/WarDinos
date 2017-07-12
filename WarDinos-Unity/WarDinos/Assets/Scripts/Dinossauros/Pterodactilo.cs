using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pterodactilo : Dinossauro {
	public Pterodactilo (){

        base.custo = 30;
        base.abilityCost = 100;
        base.alcance_ataque =5;
		base.ataque=15;
		base.velocidadeAtaque=2.0;
		base.velocidade_deslocamento=5;
		base.vida=50;

        base.custoAttrAtaque = 1;
        base.custoAttrVelocidadeAtaque = 1;
        base.custoAttrVida = 1;
        base.custoAttrVelocidadeDeslocamento = 1;

        base.dinoType= Dinossauro.DinoTypes.PTERODACTILO;

        base.MAX_ALCANCE_ATAQUE=5;
		base.MAX_ATAQUE=30;
		base.MAX_VELOCIDADE_ATAQUE=1.75;
		base.MAX_VELOCIDADE_DESLOCAMENTO=10;
		base.MAX_VIDA=80;
        base.MAX_ATTR_VIDA = 10;
        base.MAX_ATTR_ATAQUE = 5;
        base.MAX_ATTR_VEL_ATQ = 10;
        base.MAX_ATTR_VEL_DES = 5;

        base.ataque_upg = 3;
        base.velocidadeAtaque_upg = -0.025;
        base.velocidade_deslocamento_upg = 1;
        base.vida_upg = 3;

        base.playerID=-1;
		base.nSlot=1;

    }
    private void Start()
    {
        Habilidade();
    }
    #region implemented abstract members of Dinossauro

    public override void Habilidade()
    {
        foreach (Dinossauro d in Gc.DinosDinossauro)
        {
            if(d != null)
                Velocidade_deslocamento++;
        }
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
