using System;

interface IButton
{
    void Render();
    void OnClick();
}

class WindowsButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Render Windows Button");
    }

    public void OnClick()
    {
        Console.WriteLine("Click Windows Button");
    }
}

class HtmlButton : IButton
{
    public void Render()
    {
        Console.WriteLine("Render HTML Button");
    }

    public void OnClick()
    {
        Console.WriteLine("Click HTML Button");
    }
}

abstract class Dialog
{
    public abstract IButton CreateButton();

    public void Render()
    {
        IButton okButton = CreateButton();
        okButton.OnClick();
        okButton.Render();
    }
}

class WindowsDialog : Dialog
{
    public override IButton CreateButton()
    {
        return new WindowsButton();
    }
}

class WebDialog : Dialog
{
    public override IButton CreateButton()
    {
        return new HtmlButton();
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter platform (win / web) :: ");
        string type = Console.ReadLine();

        Dialog dialog;

        if (type == "win")
        {
            dialog = new WindowsDialog();
        }
        else
        {
            dialog = new WebDialog();
        }

        dialog.Render();
    }
}