using System;
using System.Runtime.Remoting.Channels;
using Eto;
using Eto.Forms;
using Eto.Drawing;
using Telvee32.Minesweeper.Common;

namespace Telvee32.Minesweeper.EtoUI
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Title = $@"C# Mines ({Platform.ID}, {(EtoEnvironment.Platform.IsMono ? "Mono" : ".NET")}, 
				{(EtoEnvironment.Platform.IsWindows ? EtoEnvironment.Platform.IsWinRT
	            ? "WinRT" : "Windows" : EtoEnvironment.Platform.IsMac
	            ? "Mac" : EtoEnvironment.Platform.IsLinux
				? "Linux" : EtoEnvironment.Platform.IsUnix
				? "Unix" : "Unknown")})";
            MinimumSize = new Size(800, 600);
			

            Content = new StackLayout
            {
                Padding = 10,
                Items =
                {
                    "Mines",
                    // add more controls here
                }
            };

            var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            quitCommand.Executed += (sender, e) => Application.Instance.Quit();

            var newDialogCommand = new Command
            {
                MenuText = "New Game...",
                Shortcut = Application.Instance.CommonModifier | Keys.N
            };
            newDialogCommand.Executed += (sender, e) =>
            {
	            var layout = new TableLayout
	            {
		            Spacing = new Size(5, 5),
		            Padding = new Padding(10, 10, 10, 10),
					Rows =
					{
						new TableRow(
							new TableCell(new Label { Text = "First Column" }, true),
							new TableCell(new Label { Text = "Second Column" }, true),
							new Label { Text = "Third Column" }
						),
						new TableRow(
							new TextBox { Text = "Some Text" },
							new DropDown { Items = { "Item 1", "Item 2", "Item 3" } },
							new CheckBox { Text = "A checkbox" }
						),
						null
					}
	            };

				var dlg = new Dialog
                {
                    Content = layout,
                    ClientSize = new Size(400, 300)
                };

				dlg.DefaultButton = new Button { Text = "OK" };
				dlg.PositiveButtons.Add(dlg.DefaultButton);
	            dlg.DefaultButton.Click += (dsender, de) =>
	            {
		            MessageBox.Show("New");
	            };

				dlg.AbortButton = new Button { Text = "Cancel" };
				dlg.NegativeButtons.Add(dlg.AbortButton);
	            dlg.AbortButton.Click += (dsender, de) =>
	            {
		            dlg.Close();
	            };

				dlg.ShowModal(this);
            };

            var aboutCommand = new Command { MenuText = "About..." };
            aboutCommand.Executed += (sender, e) => MessageBox.Show(this, "About my app...");

            // create menu
            Menu = new MenuBar
            {
                Items =
                {
					// File submenu
					new ButtonMenuItem { Text = "&File", Items = { newDialogCommand } },
					// new ButtonMenuItem { Text = "&Edit", Items = { /* commands/items */ } },
					// new ButtonMenuItem { Text = "&View", Items = { /* commands/items */ } },
				},
                ApplicationItems =
                {
					// application (OS X) or file menu (others)
					new ButtonMenuItem { Text = "&Preferences..." },
                },
                QuitItem = quitCommand,
                AboutItem = aboutCommand
            };

            var state = new GameState();
        }
    }
}