using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceManager : MonoBehaviour
{
    private KeywordRecognizer _keywordRecognizer;
    Dictionary<string, Action> _actions = new Dictionary<string, Action>();

    private void Start() {
          _actions.Add("forward", Forward);
          _actions.Add("up", Up);
          _actions.Add("down", Down);
          _actions.Add("back", Back);

          _keywordRecognizer = new KeywordRecognizer(_actions.Keys.ToArray());
          _keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
          
          // Listening the keywords
          _keywordRecognizer.Start();
    }

    private void OnApplicationQuit() {
          if (_keywordRecognizer != null && _keywordRecognizer.IsRunning) {
                _keywordRecognizer.Stop();
                _keywordRecognizer.Dispose();
          }
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech){
          Debug.Log(speech.text);
          if (_actions.ContainsKey(speech.text)) {
                _actions[speech.text].Invoke();
          } else {
                Debug.LogWarning("Command not recognized: " + speech.text);
          }
    }

    private void Forward(){
          transform.Translate(2, 0, 0);
    }

    private void Up(){
          transform.Translate(0, 0, 2);
    }

    private void Down(){
          transform.Translate(0, 0, -2);
    }

    private void Back(){
          transform.Translate(-2, 0 ,0);
    }
}
