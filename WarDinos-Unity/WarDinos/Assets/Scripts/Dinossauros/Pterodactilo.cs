using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pterodactilo : Dinossauro {
	public Pterodactilo (){

        base.custo = 10;
        base.abilityCost = 100;
        base.alcance_ataque =5;
		base.ataque=15;
		base.velocidadeAtaque=2;
		base.velocidade_deslocamento=5;
		base.vida=150;

        base.custoAttrAtaque = 1;
        base.custoAttrVelocidadeAtaque = 1;
        base.custoAttrVida = 1;
        base.custoAttrVelocidadeDeslocamento = 1;

        base.dinoType= Dinossauro.DinoTypes.PTERODACTILO;

        base.MAX_ALCANCE_ATAQUE=5;
		base.MAX_ATAQUE=30;
		base.MAX_VELOCIDADE_ATAQUE=1.75;
		base.MAX_VELOCIDADE_DESLOCAMENTO=10;
		base.MAX_VIDA=300;
        base.MAX_ATTR_VIDA = 10;
        base.MAX_ATTR_ATAQUE = 5;
        base.MAX_ATTR_VEL_ATQ = 10;
        base.MAX_ATTR_VEL_DES = 5;

        base.ataque_upg = 3;
        base.velocidadeAtaque_upg = -0.025;
        base.velocidade_deslocamento_upg = 1;
        base.vida_upg = 15;

        base.playerID=-1;
		base.nSlot=1;

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
        foreach (Dinossauro d in Gc.DinosDinossauro)
        {
            Velocidade_deslocamento++;
        }
	}

	#endregion
}
