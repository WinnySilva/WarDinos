using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoggerMongo {
	//public Object Id{get;set;}
	public int nCena {get;set;}
	public string nomeCena{get;set;}
	public string idButton{get;set;}
	public string PlayerNick{get;set;}
	public int playerID{get;set;}
	public int buttonPress{get;set;}
	public string _buttonPress{get;set;}
	public int dinossauroID{get;set;}
	public string dinossauroName{get;set;}
	public string habilidade{get;set;}
	public int habilidadeID{get;set;}
	public string acao{get;set;}
	public string hora{get;set;}
	public void writeLog(){
		ConexaoMongo.writeLog(this);
	}



}
