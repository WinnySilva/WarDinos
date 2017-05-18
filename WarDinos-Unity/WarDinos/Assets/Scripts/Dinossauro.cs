using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dinossauro : MonoBehaviour {

	protected int vida;
	protected int ataque;
	protected double velocidadeAtaque;
	protected int velocidade_deslocamento;
	protected int alcance_ataque;


	protected int custoVida;
	protected int custoAtaque;
	protected int custoVelocidadeAtaque;

	protected int MAX_VIDA;
	protected int MAX_ATAQUE;
	protected double MAX_VELOCIDADE_ATAQUE;
	protected int MAX_VELOCIDADE_DESLOCAMENTO;
	protected int MAX_ALCANCE_ATAQUE;

	public Transform agent;

	void Update(){
		if(agent != null){
			Vector3 p =  new Vector3(agent.position.x,1,agent.position.z+3);
		this.GetComponent<Transform>().position = p;
		}
	}


	public int Alcance_ataque {
		get {
			return alcance_ataque;
		}
		set{
			
			if(value<0){
				alcance_ataque=0;
			}
			else if(value>MAX_ALCANCE_ATAQUE){
				alcance_ataque = MAX_ALCANCE_ATAQUE;
			}else
			alcance_ataque=value;

		}
	}

	public int Velocidade_deslocamento {
		get {
			return velocidade_deslocamento;
		}
		set{
			
			if(value<1){
				velocidade_deslocamento = 1;
			}
			else if(value>MAX_VELOCIDADE_DESLOCAMENTO){
				velocidade_deslocamento = MAX_VELOCIDADE_DESLOCAMENTO;
			}else
				velocidade_deslocamento=value;
			
		}
	}

	public double VelocidadeAtaque {
		get {
			return velocidadeAtaque;
		}
		set{
			if(value<1){
				velocidadeAtaque= 1;
			}
			else if(value>MAX_VELOCIDADE_ATAQUE ){
				velocidadeAtaque = MAX_VELOCIDADE_ATAQUE;
			}else{
				velocidadeAtaque =value;
			}
		}
	}

	public int Ataque {
		get {
			return ataque;
		}
		set{
			if(value<1){
				ataque= 1;
			}
			else if(value>MAX_ATAQUE ){
				ataque = MAX_ATAQUE;
			}else{
				ataque =value;
			}
		}
	}

	public int Vida {
		get {
			return vida;
		}
		set{
			if(value<1){
				vida= 1;
			}
			else if(value>MAX_VIDA ){
				vida = MAX_VIDA;
			}else{
				ataque =value;
			}
		}
	}



}
