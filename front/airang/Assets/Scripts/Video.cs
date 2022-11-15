using System.Collections.Generic;

public class Video
{
    private string screen_url;
    private string self_url;
    private string title;

    public Video(string screen_url, string title)
    {
        this.screen_url = screen_url;
        this.title = title;
    }

    public Video(string screen_url, string self_url, string title)
    {
        this.screen_url = screen_url;
        this.self_url = self_url;
        this.title = title;
    }

    public string ScreenUrl
    {
        get => screen_url;
        set => screen_url = value;
    }

    public string SelfUrl
    {
        get => self_url;
        set => self_url = value;
    }

    public string Title
    {
        get => title;
        set => title = value;
    }
}
