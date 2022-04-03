using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Newtonsoft.Json;

class Question {
    public string questionText;
    private string answer;
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
        return topics;
    }
}

public class QuizboardController : MonoBehaviour {

    public GameObject[] panels;
    private Repository repository;
    private GameObject questionPanel;
    private GameObject answerField;
    private GameObject submitAnswer;
    private GameObject questionWordText;
    private GameObject questionMark;


    void Start() {
        questionPanel = GameObject.Find("QuestionPanel");
        answerField = GameObject.Find("AnswerField");
        submitAnswer = GameObject.Find("SubmitAnswer");
        questionWordText = GameObject.Find("QuestionWordText");
        questionMark = GameObject.Find("QuestionMark");

        questionPanel.SetActive(false);
        answerField.SetActive(false);
        submitAnswer.SetActive(false);
        questionWordText.SetActive(false);
        questionMark.SetActive(false);

        repository = new Repository();
        List<Topic> topics = repository.GetTopics();

        for(int i = 0; i < panels.Length; i++) {
            panels[i].GetComponentInChildren<Text>().text = topics[i].name;
            Button[] buttons = panels[i].GetComponentsInChildren<Button>();
            for (int j = 0; j < topics[i].questions.Count; j++) {
                buttons[i].GetComponentInChildren<Text>().text = "$" + topics[i].questions[j].GetValue();
                buttons[i].onClick.AddListener(delegate { ShowQuestion(topics[i].questions[j], buttons[i]); });
            }
        }
    }

    private void ShowQuestion(Question question, Button button) {
        button.gameObject.SetActive(false);
        questionPanel.GetComponentInChildren<Text>().text = question.questionText;
        questionPanel.SetActive(true);
    }
}
