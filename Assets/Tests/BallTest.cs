using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class BallTest
    {
        private PlayerController player;
        private GameController gameController;
        private Rigidbody rb;

        // Objects are loaded 1 frame after the scene is loaded. SetUp needs to be public void,
        // so we need to call InitComponents() from tests.
        private void InitComponents()
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            rb = player.GetComponent<Rigidbody>();
        }

        private IEnumerator WaitUntilBallStopsMoving()
        {
            while (true)
            {
                if (rb.velocity.sqrMagnitude < 0.00001f)
                    yield break;
                yield return null;
            }
        }

        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("SampleScene");

        }



        [UnityTest]
        public IEnumerator BallXMovementTest()
        {
            InitComponents();

            // Move Right
            rb.AddForce(player.movement.Calculate(1, 0, 1.0f));

            for (int i = 0; i < 50; i++)
            {
                float previousXPosition = rb.transform.position.x;
                yield return null;
                Assert.LessOrEqual(previousXPosition, rb.transform.position.x);
            }

            while (true)
            {
                if (rb.velocity.sqrMagnitude < 0.00001f)
                    break;
                yield return null;
            }

            // Move Left
            rb.AddForce(player.movement.Calculate(-1, 0, 1.0f));

            for (int i = 0; i < 50; i++)
            {
                float previousXPosition = rb.transform.position.x;
                yield return null;
                Assert.GreaterOrEqual(previousXPosition, rb.transform.position.x);
            }
        }


        [UnityTest]
        public IEnumerator BallZMovementTest()
        {
            InitComponents();

            // Move Forward
            rb.AddForce(player.movement.Calculate(0, 1, 1.0f));

            for (int i = 0; i < 50; i++)
            {
                float previousZPosition = rb.transform.position.z;
                yield return null;
                Assert.LessOrEqual(previousZPosition, rb.transform.position.z);
            }

            while (true)
            {
                if (rb.velocity.sqrMagnitude < 0.00001f)
                    break;
                yield return null;
            }

            // Move Backward
            rb.AddForce(player.movement.Calculate(0, -1, 1.0f));

            for (int i = 0; i < 50; i++)
            {
                float previousZPosition = rb.transform.position.z;
                yield return null;
                Assert.GreaterOrEqual(previousZPosition, rb.transform.position.z);
            }
        }


        [UnityTest]
        public IEnumerator BlobSpawnWithinRadiusTest()
        {
            InitComponents();
            Random.InitState(1);

            for (int i = 0; i < 100; i++)
            {
                gameController.SpawnBlob();
            }

            GameObject[] blobs = GameObject.FindGameObjectsWithTag("blob");
            foreach (GameObject blob in blobs)
            {
                float distance = Vector3.Distance(rb.transform.position, blob.transform.position);
                Assert.LessOrEqual(distance, gameController.spawnRadius);
            }
            yield return null;
        }


        [UnityTest]
        public IEnumerator BlobTriggerTest()
        {
            InitComponents();
            gameController.blobSpawnSpeed = 1000000f;   // Avoid spawning additional blobs
            Random.InitState(1);

            // Destroy all blobs that were created when creating the gameController
            GameObject[] blobs = GameObject.FindGameObjectsWithTag("blob");
            foreach (GameObject b in blobs)
                Object.Destroy(b);

            yield return null;

            gameController.SpawnBlob();
            GameObject blob = GameObject.FindGameObjectWithTag("blob");

            rb.AddForce(player.movement.Calculate(0, -1, 1.0f));
            yield return new WaitForSeconds(2);

            Assert.AreEqual(1, player.GetPoints());

            blob = GameObject.FindGameObjectWithTag("blob");
            Assert.IsNull(blob);


        }
    }
}
