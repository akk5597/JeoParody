using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class Question {
    public string questionText;
    public List<string> answers;
    public string questionWord;
    public int difficulty;
    public const int VALUE = 200;

    public int GetScore(string submission) {
        int score = GetValue();
        return answers.Contains(submission.ToLower()) ? score : -score;
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
    public List<Topic> topics;

    public Repository() {
        string json = @"[
  {
    ""name"": ""The 70's"",
    ""questions"": [
      {
        ""questionText"": ""This famous director of the Caped Crusader trilogy was born in the 1970's"",
        ""answers"": [""christopher nolan""],
        ""questionWord"": ""Who is"",
        ""difficulty"": 1
      },
      {
        ""questionText"": ""The year the Vietnam War ended"",
        ""answers"": [""1975""],
        ""questionWord"": ""What is"",
        ""difficulty"": 2
      },
      {
        ""questionText"": ""This famous show features an evil race called the Cylons"",
        ""answers"": [""battlestar galactica""],
        ""questionWord"": ""What is"",
        ""difficulty"": 3
      },
      {
        ""questionText"": ""The programming language which first appeared in 1972"",
        ""answers"": [""c"", ""c language"", ""c programming language""],
        ""questionWord"": ""What is"",
        ""difficulty"": 4
      },
      {
        ""questionText"": ""The name of the second album by the British rock band Queen"",
        ""answers"": [""queen ii"", ""queen 2""],
        ""questionWord"": ""What is"",
        ""difficulty"": 5
      }
    ]
  },
  {
    ""name"": ""Literature"",
    ""questions"": [
      {
        ""questionText"": ""Her first draft of 'Pride and Prejudice' was called 'First Impressions.'"",
        ""answers"": [""jane austen""],
        ""questionWord"": ""Who is"",
        ""difficulty"": 1
      },
      {
        ""questionText"": ""This 'Sherlock' lead also starred in 'The Imitation Game.'"",
        ""answers"": [""benedict cumberbatch""],
        ""questionWord"": ""Who is"",
        ""difficulty"": 2
      },
      {
        ""questionText"": ""Your spellcheck isn't broken ... the title of this Stephen King novel is supposed to be spelled that way!"",
        ""answers"": [""pet semetary""],
        ""questionWord"": ""What is"",
        ""difficulty"": 3
      },
      {
        ""questionText"": ""The state where Mark Twain’s characters Tom Sawyer and Huckleberry Finn live"",
        ""answers"": [""missouri"", ""mo""],
        ""questionWord"": ""What is"",
        ""difficulty"": 4
      },
      {
        ""questionText"": ""I started a (fictional) revolution by just trying to save my sister."",
        ""answers"": [""katniss everdeen""],
        ""questionWord"": ""Who is"",
        ""difficulty"": 5
      }
    ]
  },
  {
    ""name"": ""Retro Gaming"",
    ""questions"": [
      {
        ""questionText"": ""The most expensive video game made to date"",
        ""answers"": [""gta v"", ""gtav"", ""gta5"", ""gta 5"", ""grand theft auto 5"", ""grand theft auto v""],
        ""questionWord"": ""What is"",
        ""difficulty"": 1
      },
      {
        ""questionText"": ""This gaming giant released the first ever flight simulator game"",
        ""answers"": [""microsoft""],
        ""questionWord"": ""What is"",
        ""difficulty"": 2
      },
      {
        ""questionText"": ""In the original arcade version of Donkey Kong, this was the name of the character that would later be known as Mario"",
        ""answers"": [""jump man""],
        ""questionWord"": ""What is"",
        ""difficulty"": 3
      },
      {
        ""questionText"": ""The year Nintendo was founded"",
        ""answers"": [""1889""],
        ""questionWord"": ""What is"",
        ""difficulty"": 4
      },
      {
        ""questionText"": ""This popular dining franchise is the founder of Atari also responsible for"",
        ""answers"": [""chuck e cheese"", ""chuck-e-cheese""],
        ""questionWord"": ""What is"",
        ""difficulty"": 5
      }
    ]
  },
  {
    ""name"": ""FRIENDS"",
    ""questions"": [
      {
        ""questionText"": ""The number of main characters in the TV Show FRIENDS"",
        ""answers"": [""6"", ""six""],
        ""questionWord"": ""What is"",
        ""difficulty"": 1
      },
      {
        ""questionText"": ""Joey Tribbiani played Dr. Drake Ramoray on this soap opera show"",
        ""answers"": [""days of our lives""],
        ""questionWord"": ""What is"",
        ""difficulty"": 2
      },
      {
        ""questionText"": ""The occupation of Rachel’s fiancé Barry Farber"",
        ""answers"": [""orthodontist""],
        ""questionWord"": ""What is"",
        ""difficulty"": 3
      },
      {
        ""questionText"": ""Middle name of Chandler Bing"",
        ""answers"": [""muriel""],
        ""questionWord"": ""What is"",
        ""difficulty"": 4
      },
      {
        ""questionText"": ""The number of sisters that Joey Tribbiani had"",
        ""answers"": [""7"", ""seven""],
        ""questionWord"": ""What is"",
        ""difficulty"": 5
      }
    ]
  },
  {
    ""name"": ""Apple Inc"",
    ""questions"": [
      {
        ""questionText"": ""Apple Inc became the first company to be valued at these many trillions"",
        ""answers"": [""3"", ""three""],
        ""questionWord"": ""What is"",
        ""difficulty"": 1
      },
      {
        ""questionText"": ""This person is the cofounder of Apple Inc along with Steve Jobs and Steve Wozniak"",
        ""answers"": [""ronald wayne""],
        ""questionWord"": ""Who is"",
        ""difficulty"": 2
      },
      {
        ""questionText"": ""In 2014, Apple Inc unveiled the Apple Watch which was this type of device"",
        ""answers"": [""wearable"", ""smartwatch""],
        ""questionWord"": ""What is"",
        ""difficulty"": 3
      },
      {
        ""questionText"": ""This product from Apple Inc was launched in March of 2007"",
        ""answers"": [""apple TV""],
        ""questionWord"": ""What is"",
        ""difficulty"": 4
      },
      {
        ""questionText"": ""Apple Inc bought this consumer audio products manufacturer in 2014"",
        ""answers"": [""beats"", ""beats by dre"", ""beats electronics""],
        ""questionWord"": ""What is"",
        ""difficulty"": 5
      }
    ]
  },
  {
    ""name"": ""North Carolina State University"",
    ""questions"": [
      {
        ""questionText"": ""The city that is home of the WolfPack"",
        ""answers"": [""raleigh""],
        ""questionWord"": ""What is"",
        ""difficulty"": 1
      },
      {
        ""questionText"": ""This is the total area of the campus"",
        ""answers"": [""2099"", ""2,099""],
        ""questionWord"": ""What is"",
        ""difficulty"": 2
      },
      {
        ""questionText"": ""This building was the first ever main building"",
        ""answers"": [""holloday hall""],
        ""questionWord"": ""What is"",
        ""difficulty"": 3
      },
      {
        ""questionText"": ""The year Tuffy became the new live mascot"",
        ""answers"": [""2010""],
        ""questionWord"": ""What is"",
        ""difficulty"": 4
      },
      {
        ""questionText"": ""This was the original school color along with Blue"",
        ""answers"": [""pink""],
        ""questionWord"": ""What is"",
        ""difficulty"": 5
      }
    ]
  }
]";
        topics = JsonConvert.DeserializeObject<List<Topic>>(json);
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
    public GameObject hostImage;
    public Button startGameButton;
    public GameObject baseGameObject;
    public GameObject tutorial;
    public GameObject answerResult;
    public Button continueAnswerResult;
    public Text answerResultText;
    public AudioSource soundEffect;

    public AudioClip buzzerSound;
    public AudioClip rightAnswerSound;
    public AudioClip wrongAnswerSound;
    public AudioClip victorySound;

    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

    int questionCount = 30;

    void Start() {
        // questionPanel = GameObject.Find("QuestionPanel");
        // answerField = GameObject.Find("AnswerField");
        // submitAnswer = GameObject.Find("SubmitAnswer");
        // questionWordText = GameObject.Find("QuestionWordText");
        // questionMark = GameObject.Find("QuestionMark");
        // playerList = GameObject.Find("PlayerList");

        players = new List<Player>();

        this.gameObject.SetActive(false);

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
        hostImage.SetActive(true);
        tutorial.SetActive(true);
        answerResult.SetActive(false);

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
        baseGameObject.SetActive(false);
        startGameButton.onClick.AddListener(delegate { StartGame(); });
        continueAnswerResult.onClick.AddListener(delegate { HideAnswerResult(); });
    }

    private void StartGame() {
        tutorial.SetActive(false);
        baseGameObject.SetActive(true);
    }

    private void HideAnswerResult() {
        answerResult.SetActive(false);
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
        hostImage.SetActive(false);
        soundEffect.PlayOneShot(buzzerSound);
    }

    private void CheckAnswer(Question question) {
        string answer = answerField.GetComponent<InputField>().text;
        answerField.GetComponent<InputField>().text = "";
        int score = question.GetScore(answer);
        players[currentPlayer].score += score;
        questionPanel.SetActive(false);
        answerField.SetActive(false);
        submitAnswer.SetActive(false);
        questionWordText.SetActive(false);
        questionMark.SetActive(false);
        questionCount--;
        if(questionCount == 0) {
            gameComplete = true;
        }
        hostImage.SetActive(true);
        answerResult.SetActive(true);
        if(score < 0) {
            soundEffect.PlayOneShot(wrongAnswerSound);
            answerResultText.text = "You got that WRONG! The correct answer was " + textInfo.ToTitleCase(question.answers[0]) + ".";
        } else {
            soundEffect.PlayOneShot(rightAnswerSound);
            answerResultText.text = "You got that RIGHT!";
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

        if (!shouldCheckBuzzer && !gameComplete) {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
                submitAnswer.GetComponent<Button>().onClick.Invoke();
            }
        }

        if (gameComplete && !victoryScreen.activeSelf) {
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
        soundEffect.PlayOneShot(victorySound);
        winnerPlayer.GetComponent<Text>().text = "Player" + winner;
        winnerScore.GetComponent<Text>().text = "Score: " + (players[winner - 1].score < 0 ? "-$" : "$") + Math.Abs(players[winner - 1].score);
        victoryScreen.SetActive(true);
        playAgain.onClick.AddListener(delegate { SceneManager.LoadScene("Game"); });
    }

}
