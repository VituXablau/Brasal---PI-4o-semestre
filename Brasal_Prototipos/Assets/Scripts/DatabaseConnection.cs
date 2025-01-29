using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class DatabaseConnection : MonoBehaviour
{
    private readonly string dbfile = "URI=file:Brasal.db.db";
    private IDbConnection conexao;
    void Start()
    {
        Conectar();
        DatabaseMataAtlantica();
        DatabaseCerrado();
        DatabaseCaatinga();
        DatabasePantanal();
        DatabaseAmazonia();
        DatabaseFirstTime();
        //NewGame();
    }
    private void Conectar()
    {
        conexao = new SqliteConnection(dbfile);
        try
        {
            conexao.Open();
            Debug.Log("conexao ok");
        }
        catch { Debug.Log("erro"); }
    }
    private void DatabaseMataAtlantica()
    {
        string query = "SELECT medalhaconclusao, medalhapreservacao, medalhaprotecao, maxpercentage, maxAnimals FROM medalhas WHERE fase = 'mataatlantica'";
        var comando = conexao.CreateCommand();
        comando.CommandText = query;
        var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            GameManager.medals_MataAtlantica[0] = reader.GetBoolean(0);
            GameManager.medals_MataAtlantica[1] = reader.GetBoolean(1);
            GameManager.medals_MataAtlantica[2] = reader.GetBoolean(2);
            GameManager.maxPercentage[0] = reader.GetFloat(3);
            GameManager.maxAnimals[0] = reader.GetInt32(4);
            Debug.Log("Valores Mata Atlântica:" + " " + GameManager.medals_MataAtlantica[0] + " " + GameManager.medals_MataAtlantica[1] + " " + GameManager.medals_MataAtlantica[2] + " " + GameManager.maxPercentage[0] + GameManager.maxAnimals[0]); //lembrar de tirar depois, é só para observação
        }
    }
    private void DatabaseCerrado()
    {
        string query = "SELECT medalhaconclusao, medalhapreservacao, medalhaprotecao, maxpercentage, maxAnimals FROM medalhas WHERE fase = 'cerrado'";
        var comando = conexao.CreateCommand();
        comando.CommandText = query;
        var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            GameManager.medals_Cerrado[0] = reader.GetBoolean(0);
            GameManager.medals_Cerrado[1] = reader.GetBoolean(1);
            GameManager.medals_Cerrado[2] = reader.GetBoolean(2);
            GameManager.maxPercentage[3] = reader.GetFloat(3);
            GameManager.maxAnimals[3] = reader.GetInt32(4);
        }
    }
    private void DatabaseCaatinga()
    {
        string query = "SELECT medalhaconclusao, medalhapreservacao, medalhaprotecao, maxpercentage, maxAnimals FROM medalhas WHERE fase = 'caatinga'";
        var comando = conexao.CreateCommand();
        comando.CommandText = query;
        var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            GameManager.medals_Caatinga[0] = reader.GetBoolean(0);
            GameManager.medals_Caatinga[1] = reader.GetBoolean(1);
            GameManager.medals_Caatinga[2] = reader.GetBoolean(2);
            GameManager.maxPercentage[4] = reader.GetFloat(3);
            GameManager.maxAnimals[4] = reader.GetInt32(4);
        }
    }
    private void DatabasePantanal()
    {
        string query = "SELECT medalhaconclusao, medalhapreservacao, medalhaprotecao, maxpercentage, maxAnimals FROM medalhas WHERE fase = 'pantanal'";
        var comando = conexao.CreateCommand();
        comando.CommandText = query;
        var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            GameManager.medals_Pantanal[0] = reader.GetBoolean(0);
            GameManager.medals_Pantanal[1] = reader.GetBoolean(1);
            GameManager.medals_Pantanal[2] = reader.GetBoolean(2);
            GameManager.maxPercentage[1] = reader.GetFloat(3);
            GameManager.maxAnimals[1] = reader.GetInt32(4);
        }
        
    }
    private void DatabaseAmazonia()
    {
        string query = "SELECT medalhaconclusao, medalhapreservacao, medalhaprotecao, maxpercentage, maxAnimals FROM medalhas WHERE fase = 'amazonia'";
        var comando = conexao.CreateCommand();
        comando.CommandText = query;
        var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            GameManager.medals_Amazonia[0] = reader.GetBoolean(0);
            GameManager.medals_Amazonia[1] = reader.GetBoolean(1);
            GameManager.medals_Amazonia[2] = reader.GetBoolean(2);
            GameManager.maxPercentage[3] = reader.GetFloat(3);
            GameManager.maxAnimals[4] = reader.GetInt32(4);
        }
    }
    public void NewGame()
    {
        string query = "UPDATE medalhas set medalhaconclusao = FALSE, medalhapreservacao = FALSE, medalhaprotecao = false, maxpercentage = 0, maxAnimals = 0 where fase IS NOT NULL; ";
        var comando = conexao.CreateCommand();
        comando.CommandText = query;
        comando.ExecuteNonQuery();

        string query2 = "UPDATE firsttime set firstTimePlaying = true, firstTimePA = true, firstTimeAM = true, firstTimeCE = true, firstTimeCA = true where id = 1;";
        var comando2 = conexao.CreateCommand();
        comando2.CommandText = query2;
        comando2.ExecuteNonQuery();
    }

    private void DatabaseFirstTime()
    {
        string query = "SELECT firstTimePlaying, firstTimePA, firstTimeAM, firstTimeCE, firstTimeCA FROM firsttime WHERE id = 1";
        var comando = conexao.CreateCommand();
        comando.CommandText = query;
        var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            MenuManager.firstTimePlaying = reader.GetBoolean(0);
            MenuManager.firstTimePA = reader.GetBoolean(1);
            MenuManager.firstTimeAM = reader.GetBoolean(2);
            MenuManager.firstTimeCE = reader.GetBoolean(3);
            MenuManager.firstTimeCA= reader.GetBoolean(4);
        }
    }
}
