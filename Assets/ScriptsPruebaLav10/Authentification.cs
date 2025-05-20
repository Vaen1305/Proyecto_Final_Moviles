using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using System.Threading.Tasks;
using UnityEngine.Events;

public class Authentification : MonoBehaviour
{
    [SerializeField] private string email;
    [SerializeField] private string password;

    [Header("Bool Actions")]
    [SerializeField] private bool signUp = false;
    [SerializeField] private bool signIn = false;

    private FirebaseAuth _authReference;

    public UnityEvent OnLogInSuccesful = new UnityEvent();
    public UnityEvent OnLogOutSuccessful = new UnityEvent();

    private void Awake()
    {
        _authReference = FirebaseAuth.GetAuth(FirebaseApp.DefaultInstance);
    }

    private void Start()
    {
        if (signUp)
        {
            Debug.Log("Start Register");
            StartCoroutine(RegisterUser(email, password));
        }

        if (signIn)
        {
            Debug.Log("Start Login");
            StartCoroutine(SignInWithEmail(email, password));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LogOut();
        }
    }
    public void LogIn()
    {
        StartCoroutine(SignInWithEmail(email, password));
    }
    public void SignUp()
    {
        StartCoroutine(RegisterUser(email, password));
    }
    public void RecoverPassword()
    {
        StartCoroutine(RecoverPassword(email));
    }


    private IEnumerator RecoverPassword(string email)
    {
        Debug.Log("Registering");
        var registerTask = _authReference.SendPasswordResetEmailAsync(email);
        yield return new WaitUntil(() => registerTask.IsCompleted);
    }


    private IEnumerator RegisterUser(string email, string password)
    {
        Debug.Log("Registering");
        var registerTask = _authReference.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register task with {registerTask.Exception}");
        }
        else
        {
            Debug.Log($"Succesfully registered user {registerTask.Result.User.Email}");
        }
    }

    private IEnumerator SignInWithEmail(string email, string password)
    {
        Debug.Log("Loggin In");

        var loginTask = _authReference.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogWarning($"Login failed with {loginTask.Exception}");
        }
        else
        {
            Debug.Log($"Login succeeded with {loginTask.Result.User.Email}");
            OnLogInSuccesful?.Invoke();
        }
    }

    public void LogOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        Debug.Log("User logged out successfully");
        OnLogOutSuccessful?.Invoke();
    }
}
