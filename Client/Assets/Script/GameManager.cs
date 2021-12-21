using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public static Dictionary<int, Player> players = new Dictionary<int, Player>();
    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;

    public Camera mainCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    

    public void SpawnPlayer(int _id, string _username, Vector3 _position , Quaternion _rotation, int _mapID , Stats stats)
    {        
        GameObject _player;

        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
            GAME.MAP_START = _mapID;
            _player.GetComponent<Player>().id = _id;
            _player.GetComponent<Player>().username = _username;
            _player.GetComponent<Player>().mapID = _mapID;
            _player.GetComponent<Player>().stat = stats;
            players.Add(_id, _player.GetComponent<Player>());

            CameraFollow.playerPrepab = _player;
            mainCamera.GetComponent<CameraFollow>().enabled = true;
            BarHealth.instance.SetPlayer(_username, stats.level);
        }
        else
        {
            if (_mapID == GAME.MAP_START)
            {
                _player = Instantiate(playerPrefab, _position, _rotation);
                _player.GetComponent<Player>().id = _id;
                _player.GetComponent<Player>().username = _username;
                _player.GetComponent<Player>().mapID = _mapID;
                _player.GetComponent<Player>().stat = stats;

                players.Remove(_id);
                players.Add(_id, _player.GetComponent<Player>());
            }
            else
            {
                Player playerAnotherMap = new Player(_id, _username, _mapID);
                playerAnotherMap.stat = stats;

                players.Remove(_id);
                players.Add(_id, playerAnotherMap);
            }
                
        }
        try
        {
            GameManager.players[_id].gameObject.transform.localPosition = new Vector3(_position.x, _position.y, _position.y);
            GameManager.players[_id].position = new Vector3(_position.x, _position.y, _position.y);
            PlayerSet(_id, new Vector3(_position.x, _position.y, _position.y), GameManager.players[_id].spriteStop, GameManager.players[_id].speedAnim, GameManager.players[_id].mapID, GameManager.players[_id].IsPunching, GameManager.players[_id].IsDead, GameManager.players[_id].IsKick);
               
        }
        catch{ }
        //Map.instance.UpdateMap();
    }

    public void PlayerSet(int _id, Vector3 _position, int _spriteStop, int _speedAnim, int _mapID, bool IsPunching, bool IsDead, bool IsKick)
    {
        if (players.Count == 0) return;
        players[_id].mapID = _mapID;
        if (players[_id].mapID != GAME.MAP_START)
        {
            Player pFind = players.SingleOrDefault(x => x.Key == _id).Value;
            if (pFind)
                Destroy(players[_id].gameObject);

            return;
        }

        Player pNotSpawnFind = players.SingleOrDefault(x => x.Key == _id).Value;
        if (pNotSpawnFind == null)
        {
            GameObject _player;
            _player = Instantiate(playerPrefab, _position, new Quaternion());
            _player.GetComponent<Player>().id = _id;
            _player.GetComponent<Player>().username = players[_id].username;
            _player.GetComponent<Player>().mapID = players[_id].mapID;
            _player.GetComponent<Player>().Equipment = pNotSpawnFind.Equipment;
            players[_id] = _player.GetComponent<Player>();
        }
       
        players[_id].transform.position = _position;
        players[_id].spriteStop = _spriteStop;
        players[_id].speedAnim = _speedAnim;
        players[_id].IsPunching = IsPunching;
        players[_id].IsDead = IsDead;
        players[_id].IsKick = IsKick;
    }

}
