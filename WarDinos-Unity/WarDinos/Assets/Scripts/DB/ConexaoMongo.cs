using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;
using System;

public class ConexaoMongo : MonoBehaviour {
	protected static MongoClient _client;
	protected static MongoDatabase _database;

	static string host = "mongodb://localhost:27017";
	public static string NUMERO_CENA= "NUMERO_CENA";
	public static string NOME_CENA = "NOME_CENA";
	public static string PLAYER = "PLAYER_NICK" ;
	public static string PLAYER_ID = "PLAYER_ID" ;
	public static string BUTTON_PRESS_ID = "BUTTON_PRESS_ID" ;
	public static string BUTTON_PRESS  = "BUTTON_PRESS";
	public static string DINOSSAURO_ID = "DINO_ID";
	public static string DINOSSAURO = "DINO_NAME";
	public static string HABILIDADE = "HABILIDADE";
	public static string HABILIDADE_ID = "HABILIDADE_ID";
	public static string ACAO = "ACAO";
	public static string MSG  = "MENSAGEM";
	public static bool ativo = true;

	void Awake(){
		_client = new MongoClient();
		_database =  _client.GetServer().GetDatabase("Wardinos") ; //(host, _client.Settings);
		var document = new BsonDocument
		{
			{ "street", "2 Avenue" },
			{ "zipcode", "10075" },
			{ "building", "1480" },
			{ "coord", new BsonArray { 73.9557413, 40.7720266 } }
		};

		var collection = _database.GetCollection<BsonDocument>("Logs");

		collection.Insert(document);

	}

	public static MongoCollection getLogCollection(){
		_client = new MongoClient();
		_database =  _client.GetServer().GetDatabase("Wardinos") ; //(host, _client.Settings);
		return _database.GetCollection<BsonDocument>("Logs");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static BsonDocument createLog( ){
		BsonDocument document = new BsonDocument
		{
			{ "street", "2 Avenue" },
			{ "zipcode", "10075" },
			{ "building", "1480" },
			{ "coord", new BsonArray { 73.9557413, 40.7720266 } }
		};
		return document;
	}
		
	public static void writeLog(LoggerMongo log){
		if (!ConexaoMongo.ativo)
			return;
		try{
			MongoCollection col = ConexaoMongo.getLogCollection();
			col.Insert(log);
		}
		catch(BsonSerializationException e){
			Debug.LogError("NÃO FOI POSSÍVEL GRAVAR LOG " +
				"\n classe: "+log.className +"" +
				"\n msg: " +e.Message+"" +
				"\n obg: "+log.attachedObj+"" +
				"\n acao:"+log.acao
				+"\n tipo excecao: "+e.GetType() );
		//Desativa o write log
			//	ConexaoMongo.ativo = false;
			return;
		}
		catch(MongoConnectionException e ){
			ConexaoMongo.ativo = false;
		}
	}

	public static void writeLog(System.Object ob){
		MongoCollection col = ConexaoMongo.getLogCollection();
		col.Insert(ob);
	}
}
