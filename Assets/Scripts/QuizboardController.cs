using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class Question {
    public string questionText;
    public string answer;
    public string questionWord;
    public int difficulty;
    public const int VALUE = 200;

    public int GetScore(string submission) {
        int score = GetValue();
        return submission == answer ? score : -score;
    }

    public int GetValue() {
        return difficulty * VALUE;
    }

    public string GetQuestion() {
        return questionText;
    }

    public string GetQuestionWord() {
        return questionWord;
    }
}

class Topic {
    public string name;
    public List<Question> questions;
}

class Repository {
    const string filePath = "Assets/Data/repository.json";
    public List<Topic> topics;

    public Repository() {
        using (StreamReader r = new StreamReader(filePath)) {
            string json = r.ReadToEnd();
            topics = JsonConvert.DeserializeObject<List<Topic>>(json);
        }
    }

    static int[] GetUniqueRandomArray(int min, int max, int count) {
        int[] result = new int[count];
        List<int> numbersInOrder = new List<int>();
        for (var x = min; x < max; x++) {
            numbersInOrder.Add(x);
        }
        for (var x = 0; x < count; x++) {
            var randomIndex = UnityEngine.Random.Range(0, numbersInOrder.Count);
            result[x] = numbersInOrder[randomIndex];
            numbersInOrder.RemoveAt(randomIndex);
        }

        return result;
    }

    public List<Topic> GetTopics() {
        Debug.Log("Topics found: " + topics.Count);
        for (int i = 0; i < topics.Count; i++) {
            Debug.Log("Questions in Topic" + i + " found: " + topics[i].questions.Count);
            for (int j = 0; j < topics[i].questions.Count; j++) {
                Debug.Log("Question: " + topics[i].questions[j].questionText + ", Value: " + topics[i].questions[j].GetValue());
            }
        }
        return topics;
    }
}

class Player {
    public int score;
    public string name;
    public KeyCode buzzer;

    public Player(string _name, KeyCode _buzzer) {
        score = 0;
        name = _name;
        buzzer = _buzzer;
    }
}

public class QuizboardController : MonoBehaviour {

    public GameObject[] panels;
    private Repository repository;
    public GameObject questionPanel;
    public GameObject answerField;
    public GameObject submitAnswer;
    public GameObject questionWordText;
    public GameObject questionMark;
    private bool shouldCheckBuzzer;
    private int currentPlayer = 0;
    private List<Player> players;
    public Text[] playerScores;
    private KeyCode[] buzzers = { KeyCode.Q, KeyCode.C, KeyCode.M, KeyCode.P };
    public GameObject victoryScreen;
    public GameObject winnerPlayer;
    public GameObject winnerScore;
    public Button playAgain;
    private bool gameComplete;

    int questionCount = 30;

    void Start() {
        // questionPanel = GameObject.Find("QuestionPanel");
        // answerField = GameObject.Find("AnswerField");
        // submitAnswer = GameObject.Find("SubmitAnswer");
        // questionWordText = GameObject.Find("QuestionWordText");
        // questionMark = GameObject.Find("QuestionMark");
        // playerList = GameObject.Find("PlayerList");

        players = new List<Player>();

        for(int i = 0; i < 4; i++) {
            Player player = new Player("Player" + (i + 1), buzzers[i]);
            players.Add(player);
        }

        shouldCheckBuzzer = false;
        gameComplete = false;

        questionPanel.SetActive(false);
        answerField.SetActive(false);
        submitAnswer.SetActive(false);
        questionWordText.SetActive(false);
        questionMark.SetActive(false);

        victoryScreen.SetActive(false);

        repository = new Repository();
        List<Topic> topics = repository.GetTopics();

        for(int i = 0; i < panels.Length; i++) {
            panels[i].GetComponentInChildren<Text>().text = topics[i].name;
            Button[] buttons = panels[i].GetComponentsInChildren<Button>();
            for (int j = 0; j < topics[i].questions.Count; j++) {
                Question question = topics[i].questions[j];
                Button button = buttons[j];
                button.GetComponentInChildren<Text>().text = "$" + question.GetValue();
                button.onClick.AddListener(delegate { ShowQuestion(question, button); });
            }
        }
    }

    private void ShowQuestion(Question question, Button button) {
        button.interactable = false;
        questionPanel.GetComponentInChildren<Text>().text = question.questionText;
        shouldCheckBuzzer = true;
        submitAnswer.GetComponent<Button>().onClick.RemoveAllListeners();
        submitAnswer.GetComponent<Button>().onClick.AddListener(delegate { CheckAnswer(question); });
        questionWordText.GetComponent<Text>().text = question.questionWord;
        questionPanel.SetActive(true);
    }

    private void BuzzerPressed() {
        answerField.SetActive(true);
        submitAnswer.SetActive(true);
        questionWordText.SetActive(true);
        questionMark.SetActive(true);
    }

    private void CheckAnswer(Question question) {
        string answer = answerField.GetComponent<InputField>().text;
        // answerField.GetComponent<InputField>().text = "";
        players[currentPlayer].score += question.GetScore(answer);
        questionPanel.SetActive(false);
        answerField.SetActive(false);
        submitAnswer.SetActive(false);
        questionWordText.SetActive(false);
        questionMark.SetActive(false);
        questionCount--;
        if(questionCount == 0) {
            gameComplete = true;
        }
    }

    void Update() {
        if (shouldCheckBuzzer) {
            for (int i = 0; i < 4; i++) {
                if (Input.GetKeyDown(players[i].buzzer) && shouldCheckBuzzer) {
                    currentPlayer = i;
                    shouldCheckBuzzer = false;
                }
            }

            if(!shouldCheckBuzzer)
                BuzzerPressed();
        }

        if(gameComplete && !victoryScreen.activeSelf) {
            int max = players[0].score;
            int winner = 1;
            for (int i = 1; i < 4; i++) {
                if (players[i].score > max) {
                    winner = i + 1;
                }
            }
            ShowVictoryScreen(winner);
        }

        for (int i = 0; i < 4; i++) {
            if(i == currentPlayer) {
                playerScores[i].color = Color.green;
            } else {
                playerScores[i].color = Color.black;
            }
            playerScores[i].text = "Score: " + (players[i].score < 0 ? "-$" : "$") + Math.Abs(players[i].score);
        }
    }

    void ShowVictoryScreen(int winner) {
        winnerPlayer.GetComponent<Text>().text = "Player" + winner;
        winnerScore.GetComponent<Text>().text = "Score: " + (players[winner - 1].score < 0 ? "-$" : "$") + Math.Abs(players[winner - 1].score);
        victoryScreen.SetActive(true);
        playAgain.onClick.AddListener(delegate { SceneManager.LoadScene("Game"); });
    }

}
