using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    private int currentCar;

    string path;

    private void Awake()
    {
        path = Application.persistentDataPath + "SelectedCar.bst";

        if(File.Exists(path))
        {
            BinaryReader BR = new BinaryReader(File.Open(path, FileMode.Open));
            currentCar = BR.ReadInt32();
            BR.Close();
        }
        else
        {
            currentCar = 0;
        }    

        ActivateCar(currentCar);
    }

    private void ActivateCar(int _index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(i == currentCar)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    public void SelectCar()
    {
        //nextButton.interactable = (_index != transform.childCount - 1);
        //previousButton.interactable = (_index != 0);

        //for (int i = 0; i < transform.childCount; i++)
        //{
            //transform.GetChild(i).gameObject.SetActive(i == _index);
        //}

        path = Application.persistentDataPath + "SelectedCar.bst";

        if(File.Exists(path))
        {
            using(BinaryWriter BW = new BinaryWriter(File.Open(path, FileMode.Open)))
            {
                BW.Write(currentCar);
            }
        }
        else
        {
            using (BinaryWriter BW = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                BW.Write(currentCar);
            }
        }

        ActivateCar(currentCar);

        SceneManager.LoadScene("Menu");
    }
    public void ChangeCar(int _change)
    {
        if (currentCar >= transform.childCount - 1)
        {
            currentCar = 0;
        }
        else if (currentCar < 1)
        {
            currentCar = transform.childCount - 1;
        }
        else
        {
            currentCar += _change;
        }

        //currentCar += _change;
        ActivateCar(currentCar);
    }
}
