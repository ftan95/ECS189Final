using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private float TimeToShowBanner = 3.0f;
    [SerializeField] private int Next;
    [SerializeField] private GameObject Banner;
    private float BannerTimer = 0.0f;
    private bool LevelComplete = false;
    private GameManager Gm;

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
        Banner.SetActive(true);
        LevelComplete = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelComplete)
        {
            if (BannerTimer >= TimeToShowBanner)
            {
                this.Gm.LoadNextLevel(Next);
            }
            else
            {
                BannerTimer += Time.deltaTime;
            }
        }
    }
}
