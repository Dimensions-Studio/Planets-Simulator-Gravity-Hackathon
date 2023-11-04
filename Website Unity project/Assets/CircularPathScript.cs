using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CircularPathScript : MonoBehaviour
{
    public Transform sun;

    public float speed = 1;
    public float radius = 25;
    public float size = 1;
    public float mass = 0.5f;
    public float angle = 0;

    float x, z;

    private void Awake()
    {
        changeRaduis();
        changeSpeed();
    }
    void Update()
    {
        angle += speed * Time.deltaTime;

        x = Mathf.Cos(angle) * radius;
        z = Mathf.Sin(angle) * radius;

        transform.position = sun.position + new Vector3(x, 0, z);
    }

    public Slider raduisSlider;
    public Slider speedSlider;
    public Slider massSlider;
    public Slider sizeSlider;

    public TMP_Text raduisText;
    public TMP_Text speedText;
    public TMP_Text massText;
    public TMP_Text sizeText;

    
    public void changeSpeed()
    {
        speed = speedSlider.value;
        speedText.text = "Planet Rotation Speed: " + speed;
    }
    
    public void changeRaduis()
    {
        radius = raduisSlider.value;
        raduisText.text = "Planet Rotation Raduis: " + radius;
    }
    public void changeSize()
    {
        size = sizeSlider.value;
        sizeText.text = "Planet Size: " + size;
        transform.localScale = new Vector3(size + 5, size + 5, size + 5);
    }
    
    public void changeMass()
    {
        mass = massSlider.value;
        massText.text = "Planet Mass: " + mass;
    }

    public float starSize = 1;
    public float starAge = 1;

    public Slider starSizeSlider;
    public Slider starAgeSlider;

    public TMP_Text starSizeText;
    public TMP_Text starAgeText;
    public void changeStarSize()
    {
        starSize = starSizeSlider.value;
        starSizeText.text = "Star Size: " + starSize;
        sun.localScale = new Vector3(starSize, starSize, starSize);
    }

    public void changeStarAge()
    {
        starAge = starAgeSlider.value;
        starAgeText.text = "Star Age: " + starAge + "M";
    }

    class planetData
    {
        public string pl_orbper = "4", pl_mass, pl_rad, pl_orbeccen = "0", st_teff = "0",
            st_rad, st_mass, st_logg = "8", st_age;

    }
    planetData pl;
    public string getData()
    {
        pl = new planetData();
        pl.pl_mass = mass.ToString();
        pl.pl_rad = radius.ToString();
        pl.st_rad = starSize.ToString();
        pl.st_age = starAge.ToString();
        pl.pl_orbeccen = speed.ToString();

        return JsonUtility.ToJson(pl);
    }
}
