using UnityEngine;
using System.IO;

public class PlayerCarInstantiator : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] Transform CarPrefabs;
    public GameObject[] PlayerCarPrefab;
    public GameObject NewPlayerCar;
    string path;

    void Start()
    {
        path = Application.persistentDataPath + "SelectedCar.bst";

        if (File.Exists(path))
        {
            BinaryReader BR = new BinaryReader(File.Open(path, FileMode.Open));
            NewPlayerCar = Instantiate(PlayerCarPrefab[BR.ReadInt32()], transform.position, transform.rotation);
            BR.Close();
        }
        else
        {
            NewPlayerCar = Instantiate(PlayerCarPrefab[0], transform.position, transform.rotation);
        }
    }
}
