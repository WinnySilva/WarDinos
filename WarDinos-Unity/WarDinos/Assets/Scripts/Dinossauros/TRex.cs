using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRex : Dinossauro {
    protected int life;
    public TRex(){

        base.custo = 100;
        base.abilityCost = 100;
        base.alcance_ataque =1;
		base.ataque=30;
		base.velocidadeAtaque=1.5;
		base.velocidade_deslocamento=3;
		base.vida=750;
        life = base.vida;
        base.custoAttrAtaque = 1;
        base.custoAttrVelocidadeAtaque = 1;
        base.custoAttrVida = 1;
        base.custoAttrVelocidadeDeslocamento = 1;

        base.dinoType= Dinossauro.DinoTypes.TREX;

        base.MAX_ALCANCE_ATAQUE=1;
		base.MAX_ATAQUE=60;
		base.MAX_VELOCIDADE_ATAQUE=1.25;
		base.MAX_VELOCIDADE_DESLOCAMENTO=6;
		base.MAX_VIDA=1500;
        base.MAX_ATTR_VIDA = 10;
        base.MAX_ATTR_ATAQUE = 10;
        base.MAX_ATTR_VEL_ATQ = 10;
        base.MAX_ATTR_VEL_DES = 3;

        base.vida_upg = 75;
        base.ataque_upg = 3;
        base.velocidadeAtaque_upg = -0.025;
        base.velocidade_deslocamento_upg = 1;

        base.playerID=-1;
		base.nSlot=4;
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
        /**
		 * life steal.
		*/
        if (base.vida + base.ataque < life)
        {
            base.vida = base.vida + base.ataque;
        }
        else {
            base.vida = life;
        }
		

		//throw new System.NotImplementedException ();
	}

	#endregion
}
