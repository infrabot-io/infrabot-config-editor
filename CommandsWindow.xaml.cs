using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TelegramBotWpf
{
    /// <summary>
    /// Interaction logic for CommandsWindow.xaml
    /// </summary>
    public partial class CommandsWindow : Window
    {
        public int selectedCommand = 0;
        Config config;
        public CommandsWindow()
        {
            InitializeComponent();
            config = ((MainWindow)Application.Current.MainWindow).config;
            if (config != null && config.telegram_commands.Count > 0)
            {
                MainDataForm.IsEnabled = true;
                foreach (Command command in config.telegram_commands)
                {
                    telegram_commands_list.Items.Add(new Command
                    {
                        command_starts_with = command.command_starts_with,
                        command_data_id = command.command_data_id,
                        command_execute_file = command.command_execute_file,
                        command_help_manual = command.command_help_manual,
                        command_help_short = command.command_help_short,
                        command_default_error = command.command_default_error,
                        command_execute_type = command.command_execute_type,
                        command_allowed_users_id = command.command_allowed_users_id,
                        command_allowed_chats_id = command.command_allowed_chats_id,
                        command_show_in_get_commands_list = command.command_show_in_get_commands_list,
                        command_execute_results = command.command_execute_results
                    });
                }
            }
        }

        private void ShowInfo_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ManualViewer.ShowInfo(button);
        }

        private void DeleteItemFromList(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                telegram_commands_list.Items.Remove((Command)btn.DataContext);
                EmptyDataInForm();
            }
            catch { }
        }

        private void CommandsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCommand = telegram_commands_list.SelectedIndex;
            try
            {
                if (telegram_commands_list.SelectedItem != null)
                {
                    Command cm = (telegram_commands_list.SelectedItem as Command);
                    command_starts_with.Text = cm.command_starts_with;

                    command_data_id_add_list.Items.Clear();
                    foreach (int DataIDs in cm.command_data_id)
                    {
                        command_data_id_add_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(DataIDs) });
                    }

                    command_execute_file.Text = cm.command_execute_file;
                    command_help_manual.Text = cm.command_help_manual;
                    command_help_short.Text = cm.command_help_short;
                    command_default_error.Text = cm.command_default_error;
                    command_execute_type.SelectedIndex = cm.command_execute_type - 1;

                    command_allowed_users_id_add_list.Items.Clear();
                    foreach (int DataIDs in cm.command_allowed_users_id)
                    {
                        command_allowed_users_id_add_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(DataIDs) });
                    }

                    command_allowed_chats_id_add_list.Items.Clear();
                    foreach (long DataIDs in cm.command_allowed_chats_id)
                    {
                        command_allowed_chats_id_add_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(DataIDs) });
                    }

                    command_show_in_get_commands_list.IsChecked = cm.command_show_in_get_commands_list;
                    if (cm.command_show_in_get_commands_list)
                    {
                        command_show_in_get_commands_list.Content = "True";
                    }
                    else
                    {
                        command_show_in_get_commands_list.Content = "False";
                    }
                }
            }
            catch { }
        }

        private void AddItemToList(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                if (button.Name == "command_data_id_add_button")
                    command_data_id_add_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(command_data_id_add_text.Text) });
                if (button.Name == "command_allowed_users_id_add_button")
                    command_allowed_users_id_add_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(command_allowed_users_id_add_text.Text) });
                if (button.Name == "command_allowed_chats_id_add_button")
                    command_allowed_chats_id_add_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(command_allowed_chats_id_add_text.Text) });
            }
            catch { }
        }

        private void DeleteItemList(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn.Name == "command_data_id_delete_button")
                    command_data_id_add_list.Items.Remove((AllowedIDs)btn.DataContext);
                if (btn.Name == "command_allowed_users_id_delete_button")
                    command_allowed_users_id_add_list.Items.Remove((AllowedIDs)btn.DataContext);
                if (btn.Name == "command_allowed_chats_id_delete_button")
                    command_allowed_chats_id_add_list.Items.Remove((AllowedIDs)btn.DataContext);

            }
            catch { }
        }

        private void AddCommand(object sender, RoutedEventArgs e)
        {
            try
            {
                MainDataForm.IsEnabled = true;
                telegram_commands_list.Items.Add(new Command
                {
                    command_starts_with = "",
                    command_data_id = new List<int> { },
                    command_execute_file = "",
                    command_help_manual = "",
                    command_help_short = "",
                    command_default_error = "",
                    command_execute_type = 1,
                    command_allowed_users_id = new List<int> { },
                    command_allowed_chats_id = new List<long> { },
                    command_show_in_get_commands_list = false,
                    command_execute_results = new List<ExecuteResult> { new ExecuteResult { result_checktype = 1, result_output = "0", result_value = "Some info" } }
                });
                telegram_commands_list.SelectedItem = telegram_commands_list.Items.Count - 1;
                selectedCommand = telegram_commands_list.Items.Count - 1;
                SaveCommands(null, null);
            }
            catch { }
        }

        private void EmptyDataInForm()
        {
            command_starts_with.Text = "";
            command_data_id_add_list.Items.Clear();
            command_execute_file.Text = "";
            command_help_manual.Text = "";
            command_help_short.Text = "";
            command_default_error.Text = "";
            command_execute_type.SelectedIndex = 0;
            command_allowed_users_id_add_list.Items.Clear();
            command_allowed_users_id_add_text.Text = "";
            command_allowed_chats_id_add_list.Items.Clear();
            command_allowed_chats_id_add_text.Text = "";
            command_show_in_get_commands_list.IsChecked = false;
        }

        private void ChangeCheckItemText(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.IsChecked == false)
            {
                cb.Content = "False";
            }
            else
            {
                cb.Content = "True";
            }
        }

        private void ChangeCheckItemClick(object sender, RoutedEventArgs e)
        {

        }

        private void ShowExecuteResultsWindow(object sender, RoutedEventArgs e)
        {
            ExecuteResultsWindow erw = new ExecuteResultsWindow(selectedCommand);
            erw.ShowDialog();
        }

        private void SaveCommands(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Command> comms = new List<Command> { };
                for (int i = 0; i < telegram_commands_list.Items.Count; i++)
                {
                    Command comm = telegram_commands_list.Items[i] as Command;
                    comms.Add(comm);
                }

                if (sender != null && e != null)
                {
                    for (int i = 0; i < comms.Count; i++)
                    {
                        if (i == selectedCommand)
                        {
                            comms[i].command_starts_with = command_starts_with.Text;

                            List<int> command_data_id_array = new List<int> { };
                            foreach (AllowedIDs id in command_data_id_add_list.Items)
                            {
                                command_data_id_array.Add(id.ID);
                            }
                            comms[i].command_data_id = command_data_id_array;

                            comms[i].command_execute_file = command_execute_file.Text;
                            comms[i].command_help_manual = command_help_manual.Text;
                            comms[i].command_help_short = command_help_short.Text;
                            comms[i].command_default_error = command_default_error.Text;
                            comms[i].command_execute_type = command_execute_type.SelectedIndex + 1;

                            List<int> command_allowed_users_id_array = new List<int> { };
                            foreach (AllowedIDs id in command_allowed_users_id_add_list.Items)
                            {
                                command_allowed_users_id_array.Add(id.ID);
                            }
                            comms[i].command_allowed_users_id = command_allowed_users_id_array;

                            List<long> command_allowed_chats_id_array = new List<long> { };
                            foreach (AllowedIDs id in command_allowed_chats_id_add_list.Items)
                            {
                                command_allowed_chats_id_array.Add(id.ID);
                            }
                            comms[i].command_allowed_chats_id = command_allowed_chats_id_array;

                            comms[i].command_show_in_get_commands_list = (bool)command_show_in_get_commands_list.IsChecked;
                        }
                    }
                }
                config.telegram_commands = comms;
                telegram_commands_list.Items.Clear();
                if (config != null)
                {
                    foreach (Command command in config.telegram_commands)
                    {
                        telegram_commands_list.Items.Add(new Command
                        {
                            command_starts_with = command.command_starts_with,
                            command_data_id = command.command_data_id,
                            command_execute_file = command.command_execute_file,
                            command_help_manual = command.command_help_manual,
                            command_help_short = command.command_help_short,
                            command_default_error = command.command_default_error,
                            command_execute_type = command.command_execute_type,
                            command_allowed_users_id = command.command_allowed_users_id,
                            command_allowed_chats_id = command.command_allowed_chats_id,
                            command_show_in_get_commands_list = command.command_show_in_get_commands_list,
                            command_execute_results = command.command_execute_results
                        });
                    }
                }
                //(telegram_commands_list.SelectedItem as Command).command_starts_with = command_starts_with.Text;

                //((Command)telegram_commands_list.SelectedItem).command_starts_with = command_starts_with.Text;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
