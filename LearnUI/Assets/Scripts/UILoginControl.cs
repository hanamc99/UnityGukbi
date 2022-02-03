using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoginControl : MonoBehaviour
{
    [SerializeField] Button loginBtn;
    [SerializeField] Button closeBtn;
    [SerializeField] InputField inputEmail;
    [SerializeField] InputField inputPassword;
    [SerializeField] Toggle rememberTog;

    void Start()
    {
        loginBtn.onClick.AddListener(() =>
        {
            Debug.Log(this.inputEmail.text + " / " + this.inputPassword.text);
        });
        closeBtn.onClick.AddListener(() => Close());
        rememberTog.onValueChanged.AddListener((active) => Debug.Log(active));
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
