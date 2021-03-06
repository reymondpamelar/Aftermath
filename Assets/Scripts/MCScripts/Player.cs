﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float playerHealth;
    public barSlideHandler b;

    public int playerKills;
    public TMP_Text killText;

    public int killCombo = 0;

    private AttackHandler AH;

    private Animator a;
    public ParticleSystem HP;
    // Start is called before the first frame update
    void Start()
    {
        b.SetMaxCD(playerHealth);

        playerKills = 0;

        AH = gameObject.GetComponent<AttackHandler>();

        a = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        b.SetCD(playerHealth);

        killText.text = playerKills.ToString();

        if (killCombo >= 3)
        {
            StartCoroutine(playHP());
            if (playerHealth <= 4.9f)
            {
                playerHealth += .25f;
            }
            killCombo = 0;
        }

        if (playerHealth <= 0)
        {
            a.SetTrigger("dead");
            var emission = gameObject.GetComponent<ParticleSystem>().emission;
            emission.enabled = true;
            gameObject.GetComponent<playLightAttackPS>().enabled = false;
            gameObject.GetComponent<AttackHandler>().enabled = false;
            gameObject.GetComponent<TopDownMovementHandler>().enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

            StartCoroutine(GameOver());
        }
    }

    public void addKill()
    {
        if(AH.barCountdown <= 10)
        {
            AH.barCountdown += 0.2f;
        }
        playerKills += 1;
        killCombo += 1;
        if (playerKills > PlayerPrefs.GetInt("HighestKills", 0))
        {
            PlayerPrefs.SetInt("HighestKills", playerKills);

        }
    }

    IEnumerator playHP()
    {
        var clone = Instantiate(HP, transform.position, transform.rotation);
        clone.transform.position = new Vector3(transform.position.x, transform.position.y + 7, transform.position.z);
        while (clone.transform.position != transform.position)
        {
            Vector3 newPos = Vector3.MoveTowards(clone.transform.position, transform.position, 10 * Time.deltaTime);
            clone.transform.position = newPos;
            if (clone.transform.position == transform.position)
            {
                Destroy(clone);
            }
            yield return null;
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("gameover");
    }
}
