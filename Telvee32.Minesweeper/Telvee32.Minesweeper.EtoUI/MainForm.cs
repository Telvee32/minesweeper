using System;
using System.Runtime.Remoting.Channels;
using Eto.Forms;
using Eto.Drawing;
using Telvee32.Minesweeper.Common;

namespace Telvee32.Minesweeper.EtoUI
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Title = "C# Mines";
            ClientSize = new Size(800, 600);

            Content = new StackLayout
            {
                Padding = 10,
                Items =
                {
                    "Mines",
                    // add more controls here
                }
            };

            // create a few commands that can be used for the menu and toolbar
            var clickMe = new Command { MenuText = "Click Me!", ToolBarText = "Click Me!" };
            clickMe.Executed += (sender, e) => MessageBox.Show(this, "I was clicked!");

            var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            quitCommand.Executed += (sender, e) => Application.Instance.Quit();

            var newDialogCommand = new Command
            {
                MenuText = "New Game...",
                Shortcut = Application.Instance.CommonModifier | Keys.N
            };
            newDialogCommand.Executed += (sender, e) =>
            {
                var dlg = new Dialog
                {
                    Content = new Label { Text = "New game label" },
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

            // create toolbar			
            ToolBar = new ToolBar { Items = { clickMe } };

            var state = new GameState();
        }
    }
}