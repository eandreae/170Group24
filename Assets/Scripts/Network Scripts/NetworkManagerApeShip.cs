﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerApeShip : NetworkRoomManager
{

    private NetworkRoomManager networkManager;

    [Header("Start game button")]
    [SerializeField] private GameObject startbutton = null;
    
    [Header("Connections")]
    [SerializeField] private int maxconnections;
    public List<NetworkConnection> connections { get; } = new List<NetworkConnection>();


    //our own function for starting game
    private void ServerStartGame()
    {
        ServerChangeScene("game");
    }

    public override void OnStartServer()
    {
        networkManager = GetComponent<NetworkRoomManager>();
        maxconnections = networkManager.maxConnections;

        base.OnStartServer();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        networkManager = GetComponent<NetworkRoomManager>();

        /*
         * can join lobbies without using the weird join scene
         * joining client can't click ready, but can see host click ready
         * maybe move this connection code to a different method? maybe on client
        */
        connections.Add(conn);
        Debug.Log(conn);
        GameObject player = Instantiate(roomPlayerPrefab.gameObject, roomPlayerPrefab.gameObject.GetComponent<Transform>());
        NetworkServer.AddPlayerForConnection(conn, player);

        base.OnServerConnect(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        connections.Remove(conn); //potentially need to fix this

        base.OnServerDisconnect(conn);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn); 
        
        //GameObject player = Instantiate(playerPrefab, playerPrefab.GetComponent<Transform>());
        //player.GetComponent<Player>().playerNum = numPlayers;
        //NetworkServer.ReplacePlayerForConnection(conn, player);   
    }
    
    //[ClientRpc]
    public override void OnServerSceneChanged(string newSceneName)
    {
        //base.OnServerSceneChanged(newSceneName);
        Debug.Log("numplayers " + numPlayers);
        Debug.Log(newSceneName);
        if (newSceneName == "game"){
            Debug.Log(connections.Count);
            int i = 0;
            foreach (NetworkConnection nc in connections)
            {
                //GameObject cube  = nc.identity.gameObject;
                //Debug.Log(cube);

                GameObject player = Instantiate(playerPrefab, playerPrefab.GetComponent<Transform>());
                player.GetComponent<Player>().playerNum = i;
                NetworkServer.ReplacePlayerForConnection(nc, player);
                i++;

                //NetworkServer.Destroy(cube);
            }
        }
    }
    
    public override void OnClientConnect(NetworkConnection conn){
        //base.OnClientConnect(conn);
    }

    public override void OnRoomServerAddPlayer(NetworkConnection conn)
    {
        base.OnRoomServerAddPlayer(conn);
        Debug.Log("room server add player");
    }

    public override void OnRoomStartServer()
    {
        base.OnRoomStartServer();
        Debug.Log("room start server");
    }

    public override void OnRoomServerConnect(NetworkConnection conn)
    {
        base.OnRoomServerConnect(conn);
        Debug.Log("room server connect (conn: " + conn +")");
    }

    public override void OnRoomServerSceneChanged(string sceneName)
    {
        base.OnRoomServerSceneChanged(sceneName);
        Debug.Log("room server scene changed");
    }



    public override void OnRoomServerPlayersReady()
    {
        //base.OnRoomServerPlayersReady();
        Debug.Log("ready");
        startbutton.SetActive(true);
    }

    public override void OnRoomServerPlayersNotReady()
    {
        base.OnRoomServerPlayersNotReady();
        Debug.Log("not ready");
        
        startbutton.SetActive(false);
    }

    public override void OnRoomClientConnect(NetworkConnection conn)
    {
        base.OnRoomClientConnect(conn);
        Debug.Log("room client connect");
    }

    public override void OnRoomClientDisconnect(NetworkConnection conn)
    {
        base.OnRoomClientDisconnect(conn);
        Debug.Log("room client disconnect");
    }

    public override void OnRoomClientEnter()
    {
        base.OnRoomClientEnter();
        Debug.Log("room client enter");
    }


}
