using Assets._Outrun.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Outrun.System
{
    class GameManager
    {
        public static event ScoreChanged OnScoreChanged = (score) => { };
        public static event MaxScoreChanged OnMaxScoreChanged = (maxScore) => { };

        public delegate void ScoreChanged(int newScore);
        public delegate void MaxScoreChanged(int newMaxScore);

        private static Scene scene;
        private static List<IResetable> resetableComponents;
        private static List<ObjectFollower> followers;
        private static int score;
        private static int maxScore = 0;

        public static void Play(Scene scene)
        {
            GameManager.scene = scene;
            var rootObjects = scene.GetRootGameObjects();
            var track = rootObjects.First(gameObject => gameObject.CompareTag("Track"));
            var camera = rootObjects.First(gameObject => gameObject.CompareTag("MainCamera"));
            var light = rootObjects.First(gameObject => gameObject.CompareTag("MainLight"));

            resetableComponents = new List<IResetable>
            {
                track.GetComponentInChildren<PathSpawner>(),
                track.GetComponentInChildren<ObstacleCarSpawner>(),
                track.GetComponentInChildren<CharacterCollision>(),
                track.GetComponentInChildren<InputCharacterController>(),
                track.GetComponentInChildren<CarEngine>(),
            };

            followers = new List<ObjectFollower>
            {
                camera.GetComponent<ObjectFollower>(),
                light.GetComponent<ObjectFollower>()
            };

            ResetGameComponents();
            SetScore(0);
        }

        public static void AddScore()
        {
            SetScore(score + 1);
            if (score > maxScore)
            {
                maxScore = score;
                OnMaxScoreChanged(maxScore);
            }
        }

        private static void SetScore(int value)
        {
            score = value;
            OnScoreChanged(score);
        }

        public static void Reset()
        {
            ResetGameComponents();
            ToggleFollowers();
            SetScore(0);
        }

        public static void End()
        {
            ResetGameComponents();

            var canvas = scene.GetRootGameObjects().First(gameObject => gameObject.CompareTag("Canvas"));
            var gameOver = canvas.GetComponentInChildren<GameOverSequence>(true);
            gameOver.gameObject.SetActive(true);
            gameOver.ShowRestart();

            ToggleFollowers();
        }

        private static void ToggleFollowers()
        {
            followers.ForEach(followers => followers.Toggle());
        }

        private static void ResetGameComponents()
        {
            resetableComponents.ForEach(component => component.Reset());
        }

    }
}
