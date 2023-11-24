using System;

namespace RC21;

public class TimerWorck
{
    public static DateTime LoginTime = new DateTime(); 
    public static DateTime TimeBlock = new DateTime(); 
    public string Timerlab()
    {
        System.TimeSpan timer = LoginTime - DateTime.Now;
        String timerText = $"{timer.Hours}:{timer.Minutes}:{timer.Seconds}";
        return timerText;
    }

    public byte TimeBlockChack()
    {
        byte resultBlock = 0;
        System.TimeSpan timer = LoginTime - DateTime.Now;
        if (timer <= new TimeSpan(0, 0, 15, 0) && timer > new TimeSpan(0, 0, 0, 0))
        {
            resultBlock = 1;
        }
        else if (timer <= new TimeSpan(0, 0, 0, 0))
        {
            resultBlock = 2;
        }
        return resultBlock;
    }
    
    public string TimerBlock()
    {
        System.TimeSpan timer = TimeBlock - DateTime.Now;
        String timerBlockText = $"{timer.Minutes}:{timer.Seconds}";
        return timerBlockText;
    }
    
    public bool TimeEndBlockChack()
    {
        bool resultBlock = false;
        System.TimeSpan timer = TimeBlock - DateTime.Now;
        if (timer <= new TimeSpan(0, 0, 0, 0))
        {
            resultBlock = true;
        }
        return resultBlock;
    }
}