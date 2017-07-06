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
		base.vida=600;

        base.custoAttrAtaque = 1;
        base.custoAttrVelocidadeAtaque = 1;
        base.custoAttrVida = 1;
        base.custoAttrVelocidadeDeslocamento = 1;

        base.dinoType= Dinossauro.DinoTypes.ESTEGOSSAURO;

        base.MAX_ALCANCE_ATAQUE=1;
		base.MAX_ATAQUE=20;
		base.MAX_VELOCIDADE_ATAQUE=1.75;
		base.MAX_VELOCIDADE_DESLOCAMENTO=4;
		base.MAX_VIDA=1200;
        base.MAX_ATTR_VIDA = 10;
        base.MAX_ATTR_ATAQUE = 10;
        base.MAX_ATTR_VEL_ATQ = 10;
        base.MAX_ATTR_VEL_DES = 2;
        base.ataque_upg = 1;
        base.velocidadeAtaque_upg = -0.025;
        base.velocidade_deslocamento_upg = 1;
        base.vida_upg = 60;

        base.playerID=-1;
		base.nSlot =2;

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region implemented abstract members of Dinossauro

    public override void Habilidade()
    {

        foreach (Dinossauro d in Gc.enemyTargetGroup.DinosDinossauro) {
            d.Vida = d.Vida - base.ataque;
        }


		//throw new System.NotImplementedException ();
	}

	#endregion
}
