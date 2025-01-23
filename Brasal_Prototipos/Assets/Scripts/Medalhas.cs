using UnityEditor.Experimental.GraphView;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Medalhas
{
    public string Level { get; set; }
    public bool Completionmedal { get; set; }
    public bool PreservationMedal { get; set; }
    public bool ProtectionMedal { get; set; }

    // Construtor para facilitar a criação de objetos a partir do DataReader
    public Medalhas(string level, bool completionmedal, bool preservationMedal, bool protectionMedal) 
    {
        Level = level;
        Completionmedal = completionmedal;
        PreservationMedal = preservationMedal;
        ProtectionMedal = protectionMedal;
    }
    public Medalhas() { }

    // usado apenas para verificação, apagar depois
    public override string ToString()
    {
        return $"Level: {Level}, Medalhaconclusao: {Completionmedal}, PreservationMedal: {PreservationMedal}, ProtectionMedal: {ProtectionMedal}";
    }

}
