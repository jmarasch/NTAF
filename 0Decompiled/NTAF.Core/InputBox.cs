using System;
using System.Drawing;
using System.Windows.Forms;

public class InputBox
{
	public InputBox()
	{
	}

	public static string Show(string Prompt, string Title)
	{
		string str = InputBox.Show(Prompt, Title, "", -1, -1, false);
		return str;
	}

	public static string Show(string Prompt, string Title, bool CapturePassword)
	{
		string str = InputBox.Show(Prompt, Title, "", -1, -1, CapturePassword);
		return str;
	}

	public static string Show(string Prompt, string Title, string DefaultResponse)
	{
		string str = InputBox.Show(Prompt, Title, DefaultResponse, -1, -1, false);
		return str;
	}

	public static string Show(string Prompt, string Title, string DefaultResponse, int XPos, int YPos, bool CapturePassword)
	{
		InputBoxForm frmInputBox = new InputBoxForm()
		{
			Title = Title,
			Prompt = Prompt,
			DefaultResponse = DefaultResponse,
			CapturePassword = CapturePassword
		};
		if ((XPos < 0 ? false : YPos >= 0))
		{
			frmInputBox.StartLocation = new Point(XPos, YPos);
		}
		frmInputBox.ShowDialog();
		return frmInputBox.ReturnValue;
	}
}