using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apatossauro : Dinossauro {

	public Apatossauro() {
        base.custo = 35;
        base.abilityCost = 200;
        base.alcance_ataque =1;
		base.ataque=5;
		base.velocidadeAtaque=2.5;
		base.velocidade_deslocamento=2;
		base.vida=1000;

        base.custoAttrAtaque = 1;
        base.custoAttrVelocidadeAtaque = 1;
        base.custoAttrVida = 1;
        base.custoAttrVelocidadeDeslocamento = 1;

        base.dinoType= Dinossauro.DinoTypes.APATOSSAURO;

        base.MAX_ALCANCE_ATAQUE=1;
		base.MAX_ATAQUE=15;
		base.MAX_VELOCIDADE_ATAQUE=2.25;
		base.MAX_VELOCIDADE_DESLOCAMENTO=4;
		base.MAX_VIDA=2000;
        base.MAX_ATTR_VIDA = 5;
        base.MAX_ATTR_ATAQUE = 10;
        base.MAX_ATTR_VEL_ATQ = 10;
        base.MAX_ATTR_VEL_DES = 2;

        base.ataque_upg = 1;
        base.velocidadeAtaque_upg = -0.025;
        base.velocidade_deslocamento_upg = 1;
        base.vida_upg = 200;

        base.playerID=-1;
		base.nSlot = 2;
	}





    #region implemented abstract members of Dinossauro
    public override void Habilidade()
    {
		//i really hope it works like that o.o'
		foreach(Dinossauro dino in Gc.enemyTargetGroup.DinosDinossauro){
			dino.VelocidadeAtaque = dino.VelocidadeAtaque * 0.5;
		}
			/*divide their attack speed by 2*/
		//when an apatassauro dies, the atk spd of the enemy should go back to the original value.
		//throw new System.NotImplementedException ();
	}
	#endregion
}
