using NFTPort;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Web3Manager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Login();

    // From NFTPort
    //[SerializeField] Text addressInput;
    //[SerializeField] Text ContractInput;
    //[SerializeField] private TextMeshProUGUI chaininput;
    //[SerializeField] private TextMeshProUGUI NFTNames;

    //[SerializeField] private Text Minturl;
    //[SerializeField] private Text Mintto;
    //[SerializeField] private Text MintName;


    //[SerializeField] private NFTs_ownedbyanAccount _nfTsOwnedbyanAccount;
    //[SerializeField] private NFTs_fromAContract _nfTsFromAContract;


    private string web3Account;


    public string testAccount;
    public TextMeshProUGUI accountTxt;

    // Start is called before the first frame update
    void Start()
    {
        //Account(testAccount);
    }

    public void Web3Login()
    {
        Login();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Account(string account)
    {
        web3Account = account;
        accountTxt.text = account;
        //NFTPort_Interface.Instance.NFTsOfAccount(account);
    }

    public void AccountSuccess()
    {
        //NFTNames.text = "";
        ////shows how to refrence the data model
        //for (int i = 0; i < _nfTsOwnedbyanAccount._ownedbyAddreddModel.nfts.Count; i++)
        //{
        //    NFTNames.text = NFTNames.text + '\n' +
        //                    _nfTsOwnedbyanAccount._ownedbyAddreddModel.nfts[i].name;
        //}
    }
}
