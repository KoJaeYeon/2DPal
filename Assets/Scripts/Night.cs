using UnityEngine;

public class Night : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public enum DayTime { Day, Evening, Midnight, Twilight }
    public DayTime dayTime;

    public float time = 0;
    public int day = 1;
    public float timeMul = 1;

    private void Update()
    {
        time += Time.deltaTime;
        if (time < 120 * timeMul)
        {
            if (dayTime == DayTime.Twilight)
            {
                ToDay();
            }
            dayTime = DayTime.Day;
        }
        else if (time < 150 * timeMul)
        {
            if (dayTime == DayTime.Day)
            {
                ToEvening();
            }
            dayTime = DayTime.Evening;
        }
        else if (time < 210 * timeMul)
        {
            if (dayTime == DayTime.Evening)
            {
                ToMidnight();
                foreach (StrawPalBed strawPalBed in PalManager.Instance.palBedBuilding)
                {
                    Debug.Log(strawPalBed);
                    strawPalBed.Sleep();
                }
            }
            dayTime = DayTime.Midnight;
        }
        else if (time < 240 * timeMul)
        {
            if (dayTime == DayTime.Midnight)
            {
                ToTwilight();
                foreach (StrawPalBed strawPalBed in PalManager.Instance.palBedBuilding)
                {
                    strawPalBed.Done();
                }
            }
            dayTime = DayTime.Twilight;
        }
        else
        {
            day++;
            time = 0;
        }
    }

    public void ToMidnight()
    {
        animator.Play("ToMidnight");
    }
    public void ToEvening()
    {
        animator.Play("ToEvening");
    }
    public void ToTwilight()
    {
        animator.Play("ToTwilight");
    }
    public void ToDay()
    {
        animator.Play("ToDay");
    }

    public bool IsMidnight()
    {
        if(dayTime == DayTime.Midnight) return true;
        else return false;
    }

    public void SetTime(int day, float time)
    {

    }
}
