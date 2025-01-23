using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class DatabaseConnection : MonoBehaviour
{
    private readonly string dbfile = "URI=file:Brasal.db.db";
    private IDbConnection conexao;
    public bool CompletionMedalMataAtlantica, PreservationMedalMataAtlantica, ProtectionMedalMataAtlantica, 
        CompletionMedalCerrado, PreservationMedalCerrado, ProtectionMedalCerrado,
        CompletionMedalCaatinga, PreservationMedalCaatinga, ProtectionMedalCaatinga,
        CompletionMedalPantanal, PreservationMedalPantanal, ProtectionMedalPantanal,
        CompletionMedalAmazonia, PreservationMedalAmazonia, ProtectionMedalAmazonia; //bool temporários (?) para usar nas medalhas
    void Start()
    {
        Conectar();
        DatabaseMataAtlantica();
        DatabaseCerrado();
        DatabaseCaatinga();
        DatabasePantanal();
        DatabaseAmazonia();
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
        string query = "SELECT medalhaconclusao, medalhapreservacao, medalhaprotecao FROM medalhas WHERE fase = 'mataatlantica'";
        var comando = conexao.CreateCommand();
        comando.CommandText = query;
        var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            CompletionMedalMataAtlantica = reader.GetBoolean(0);
            PreservationMedalMataAtlantica = reader.GetBoolean(1);
            ProtectionMedalMataAtlantica = reader.GetBoolean(2);
            Debug.Log("Valores Mata Atlântica:" + " " + CompletionMedalMataAtlantica + " " + PreservationMedalMataAtlantica + " " + ProtectionMedalMataAtlantica); //lembrar de tirar depois, é só para observação
        }
    }
    private void DatabaseCerrado()
    {
        string query = "SELECT medalhaconclusao, medalhapreservacao, medalhaprotecao FROM medalhas WHERE fase = 'cerrado'";
        var comando = conexao.CreateCommand();
        comando.CommandText = query;
        var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            CompletionMedalCerrado = reader.GetBoolean(0);
            PreservationMedalCerrado = reader.GetBoolean(1);
            ProtectionMedalCerrado = reader.GetBoolean(2);
            Debug.Log("Valores Cerrado:" + " " + CompletionMedalCerrado + " " + PreservationMedalCerrado + " " + ProtectionMedalCerrado); //lembrar de tirar depois, é só para observação
        }
    }
    private void DatabaseCaatinga()
    {
        string query = "SELECT medalhaconclusao, medalhapreservacao, medalhaprotecao FROM medalhas WHERE fase = 'caatinga'";
        var comando = conexao.CreateCommand();
        comando.CommandText = query;
        var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            CompletionMedalCaatinga = reader.GetBoolean(0);
            PreservationMedalCaatinga = reader.GetBoolean(1);
            ProtectionMedalCaatinga = reader.GetBoolean(2);
            Debug.Log("Valores Caatinga:" + " " + CompletionMedalCaatinga + " " + PreservationMedalCaatinga + " " + ProtectionMedalCaatinga); //lembrar de tirar depois, é só para observação
        }
    }
    private void DatabasePantanal()
    {
        string query = "SELECT medalhaconclusao, medalhapreservacao, medalhaprotecao FROM medalhas WHERE fase = 'pantanal'";
        var comando = conexao.CreateCommand();
        comando.CommandText = query;
        var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            CompletionMedalPantanal = reader.GetBoolean(0);
            PreservationMedalPantanal = reader.GetBoolean(1);
            ProtectionMedalPantanal = reader.GetBoolean(2);
            Debug.Log("Valores Pantanal:" + " " + CompletionMedalPantanal + " " + PreservationMedalPantanal + " " + ProtectionMedalPantanal); //lembrar de tirar depois, é só para observação
        }
    }
    private void DatabaseAmazonia()
    {
        string query = "SELECT medalhaconclusao, medalhapreservacao, medalhaprotecao FROM medalhas WHERE fase = 'amazonia'";
        var comando = conexao.CreateCommand();
        comando.CommandText = query;
        var reader = comando.ExecuteReader();
        if (reader.Read())
        {
            CompletionMedalAmazonia = reader.GetBoolean(0);
            PreservationMedalAmazonia = reader.GetBoolean(1);
            ProtectionMedalAmazonia = reader.GetBoolean(2);
            Debug.Log("Valores Amazonia" + " " + CompletionMedalAmazonia + " " + PreservationMedalAmazonia + " " + ProtectionMedalAmazonia); //lembrar de tirar depois, é só para observação
        }
    }
}
