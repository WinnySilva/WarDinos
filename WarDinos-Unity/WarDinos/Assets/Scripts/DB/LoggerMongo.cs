using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MongoDB.Bson.Serialization.Attributes;
using UnityEngine.SceneManagement;


public class LoggerMongo {
	//public Object Id{get;set;}
	[BsonIgnoreIfNull]
	private int? nCena {get;set;}

	[BsonIgnoreIfNull]
	private string nomeCena{get;set;}

	[BsonIgnoreIfNull]
	public int? idButton{get;set;}

	[BsonIgnoreIfNull]
	public string PlayerNick{get;set;}

	[BsonIgnoreIfNull]
	public int? playerID{get;set;}

	[BsonIgnoreIfNull]
	public int? buttonPress{get;set;}

	[BsonIgnoreIfNull]
	public string _buttonPress{get;set;}

	[BsonIgnoreIfNull]
	public int? dinossauroID{get;set;}

	[BsonIgnoreIfNull]
	public string dinossauroName{get;set;}

	[BsonIgnoreIfNull]
	public string habilidade{get;set;}

	[BsonIgnoreIfNull]
	public int? habilidadeID{get;set;}

	[BsonIgnoreIfNull]
	public string acao{get;set;}

	[BsonIgnoreIfNull]
	public DateTime hora{get;set;}

	[BsonIgnoreIfNull]
	public String className {get;set;}

	[BsonIgnoreIfNull]
	public String exceptionName {get;set;}

	[BsonIgnoreIfNull]
	public String msg {get;set;}

	[BsonIgnoreIfNull]
	public System.Object attachedObj {get;set;}

	[BsonIgnoreIfNull]
	public int? grupoID {get;set;}

	[BsonIgnoreIfNull]
	public int? enemyGrupoID {get;set;}

	[BsonIgnoreIfNull]
	public string tempoDePartida {get;set;}

	[BsonIgnoreIfNull]
	public int? jogadorVencedor {get;set;}

	[BsonIgnoreIfNull]
	public int? classId {get;set;}

	[BsonIgnoreIfNull]
	public string modoJogo {get;set;}


	[BsonIgnoreIfNull]
	public int? LevelJogo {get;set;}

	public void writeLog(){
		this.nCena= SceneManager.GetActiveScene ().buildIndex;
		this.nomeCena = SceneManager.GetActiveScene ().name;
		this.hora = new DateTime();
		ConexaoMongo.writeLog(this);
		this.setAllNull ();

	}

	public LoggerMongo (Type cl){
		className = cl.FullName;
	}

/*	public Object[] attachedObj{
		get {
			return attachedObj;
		};
		set{
		}
	}
*/

	private void setAllNull(){
		nCena = null;

		nomeCena =null;

		idButton=null;

		PlayerNick=null;

		playerID =null;

		buttonPress =null;

		_buttonPress =null;

		dinossauroID=null;

		dinossauroName=null;

		habilidade=null;

		habilidadeID=null;

		acao=null;


		//className =null;

		exceptionName =null;

		msg =null;

		attachedObj =null;

		grupoID = null;

		enemyGrupoID = null;
	}
}
