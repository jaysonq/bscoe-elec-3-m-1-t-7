using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

	enum Screen { MainMenu, Password};
	Screen currentScreen = Screen.MainMenu;
	string[,] Passwords = new string[,]{
		{"desk", "chalk", "chair", "study"},
		{"teller", "alarms", "window", "keypad"},
		{"bitcoins", "database", "encrypted", "ILOVEYOU"}
	};
	int level = 0;
	int lives = 3;
	string password = "";
	string shuffledPassword = "";

	// Use this for initialization
	void Start () {
		ShowMainMenu();
	}

	void ShowMainMenu(){
		currentScreen = Screen.MainMenu;
		lives = 3;
		Terminal.ClearScreen();
		Terminal.WriteLine("              #M1T7");
		Terminal.WriteLine("What would you like to hack into?");
		Terminal.WriteLine("");
		Terminal.WriteLine("Press 1 for the school");
		Terminal.WriteLine("Press 2 for the bank");
		Terminal.WriteLine("Press 3 for the Pentagon");
		Terminal.WriteLine("");
		Terminal.WriteLine("Enter your selection:");
	}

	void ShowGuessScreen(){
		Terminal.ClearScreen();
		Terminal.WriteLine("Lives: " + lives);
		Terminal.WriteLine("Level "+ (level + 1) + ": " + shuffledPassword);
		Terminal.WriteLine("Enter Password:");
	}

	void SelectRandomPassword(){
		password = Passwords[level, Random.Range(0,3)];
	}

	void ShufflePassword(){
		char[] array = password.ToCharArray();
		int n = password.Length;
        for (int i = 0; i < n; i++)
        {
            int r = Random.Range(0, n - 1);
            char temp = array[r];
            array[r] = array[i];
            array[i] = temp;
        }
		shuffledPassword = new string(array);
	}

	void RunGame(){
		currentScreen = Screen.Password;
		SelectRandomPassword();
		shuffledPassword = StringExtension.Anagram(password);
		ShowGuessScreen();
	}

	void ShowGameOver(){
		Terminal.ClearScreen();
		Terminal.WriteLine("Sorry you were caught!! :'(");
		Terminal.WriteLine("Type menu to try again.");
	}

	void ShowCongrats(){
		Terminal.ClearScreen();
		lives = 0;
		Terminal.WriteLine("Congrats! You beat the game!");
		Terminal.WriteLine("Type `menu to try again.`");
	}

	void OnUserInput(string input){
		if(input == "menu"){
			ShowMainMenu();
		} else if(currentScreen == Screen.MainMenu){
			if(input == "007"){
				Terminal.WriteLine("");
				Terminal.WriteLine("Oh! Welcome back Mr.Bond!");
				Terminal.WriteLine("Enter your selection:");
			} else{
				switch(input){
					case "1":
						level = 0;
						RunGame();
						break;
					case "2":
						level = 1;
						RunGame();
						break;
					case "3":
						level = 2;
						RunGame();
						break;
					default:
						Terminal.WriteLine("invalid");
						break;
				}
			}
		} else if(currentScreen == Screen.Password){
			lives--;
			if(input == password){
				ShowCongrats();
			} else if(lives > 0){
				RunGame();
			} else if (lives == 0){
				ShowGameOver();
			} else{
				Terminal.WriteLine("invalid.");
			}
		} 
	}
}
