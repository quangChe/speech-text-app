using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Database;
using Newtonsoft.Json;

public class DatabaseHelper
{
    private UserTable users;
    private WordCategoryModel wordCategories;

    public UserModel Player { private set; get; }

    public DatabaseHelper()
    {
        users = new UserTable();
        
        GetOrCreateUser();
        GetOrCreateWordCategories();
    }

    private void GetOrCreateUser()
    {
        Player = users.GetById(1);
        if (Player == null) CreateDefaultPlayer();
        else UpdateLoginTime();
    }

    private void GetOrCreateWordCategories()
    {
        
    }

    private void CreateDefaultPlayer()
    {
        users.Create(new UserModel { name = "Main User" });
        Player = users.GetById(1);
    }


    public void UpdateUser(UserModel user)
    {
        users.UpdateUser(user);
    }

    private void UpdateLoginTime()
    {
        users.UpdateLoginDate(Player.id);
        Player = users.GetById(Player.id);
    }

    public void CloseConnections()
    {
        users.Close();
    }

}
