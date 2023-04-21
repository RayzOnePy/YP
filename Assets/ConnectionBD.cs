using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;

public class ConnectionBD : MonoBehaviour
{
    private string dbName = "URI=file:Inventoryy.db";
    private SqliteConnection connection;
    private SqliteCommand command;
    
    void Start()
    {
        connection = new SqliteConnection(dbName);
        connection.Open();
        command = connection.CreateCommand();

        CreateDB();

        connection.Close();
    }
    public void CreateDB()
    {
        command.CommandText = "CREATE TABLE IF NOT EXISTS users (\r\n  user_id SERIAL PRIMARY KEY,\r\n  username VARCHAR(255) UNIQUE NOT NULL,\r\n  password VARCHAR(255) NOT NULL,\r\n  full_name VARCHAR(255) NOT NULL\r\n);";
        command.ExecuteNonQuery();
        AddUser("pokersbullshit", "qwe", "EbaninEbaninovich");
        PrintUser();
    }
    public void AddUser(string username, string password, string full_name)
    {
        command.CommandText = $"SELECT EXISTS(SELECT username FROM users WHERE username = 'awwhrfguaygfisahfiuh')";
        var reader = command.ExecuteReader();
        var answer = reader.Read();
        Debug.Log(reader[0]);
        if (Convert.ToBoolean(reader[0]) == false)
        {
            reader.Close();
            command.CommandText = $"INSERT INTO users (username, password, full_name) VALUES ('{username}', '{password}', '{full_name}');";
            command.ExecuteNonQuery();
        }
        else
        {
            Debug.Log("Пользователь с таким логином уже зарегистрирован");
        }
        reader.Close();
    }
    public void PrintUser()
    {
        command.CommandText = $"SELECT * FROM users";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Debug.Log($"{reader["username"]} {reader["password"]}");
        }
    }

    public void AddWeapon(string weaponName, int weaponDamage)
    {
        var connection = new SqliteConnection(dbName);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"INSERT INTO weapons (name, damage) VALUES ('{weaponName}','{weaponDamage}');";
        command.ExecuteNonQuery();
        connection.Close();
    }
    public void DisplayWeapons()
    {
        var connection = new SqliteConnection(dbName);
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"SELECT * FROM weapons;";
        var reader = command.ExecuteReader();
        while(reader.Read())
        {
            Debug.Log($"Name - {reader["name"]} \n Damage - {reader["damage"]}");
        }
    }
}
