using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TelegramBotWpf
{
    /// <summary>
    /// Interaction logic for ExecuteResultsWindow.xaml
    /// </summary>
    public partial class ExecuteResultsWindow : Window
    {
        public int selectedResult = 0;
        Config config;
        int SelectedItem = 0;
        public ExecuteResultsWindow(int item)
        {
            InitializeComponent();
            config = ((MainWindow)Application.Current.MainWindow).config;
            SelectedItem = item;
            if (config != null)
            {
                if (config.telegram_commands[SelectedItem].command_execute_results != null)
                {
                    foreach (ExecuteResult command in config.telegram_commands[SelectedItem].command_execute_results)
                    {
                        command_execute_results.Items.Add(new ExecuteResult
                        {
                            result_value = command.result_value,
                            result_output = command.result_output,
                            result_checktype = command.result_checktype
                        });
                    }
                }
            }
        }

        private void ExecuteResultsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedResult = command_execute_results.SelectedIndex;
            try
            {
                if (command_execute_results.SelectedItem != null)
                {
                    ExecuteResult er = (command_execute_results.SelectedItem as ExecuteResult);
                    result_checktype.SelectedIndex = er.result_checktype - 1;
                    result_value.Text = er.result_value;
                    result_output.Text = er.result_output;
                }
            }
            catch { }
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
                command_execute_results.Items.Remove((ExecuteResult)btn.DataContext);
                EmptyDataInForm();
            }
            catch { }
        }

        private void AddExecuteResult(object sender, RoutedEventArgs e)
        {
            try
            {
                command_execute_results.Items.Add(new ExecuteResult
                {
                    result_value = "0",
                    result_output = "Some result",
                    result_checktype = 1
                });
            }
            catch { }
        }

        private void SaveExecuteResults(object sender, RoutedEventArgs e)
        {
            try
            {
                List<ExecuteResult> exress = new List<ExecuteResult> { };
                for (int i = 0; i < command_execute_results.Items.Count; i++)
                {
                    ExecuteResult exres = command_execute_results.Items[i] as ExecuteResult;
                    exress.Add(exres);
                }

                for (int i = 0; i < exress.Count; i++)
                {
                    if (i == selectedResult)
                    {
                        exress[i].result_checktype = result_checktype.SelectedIndex + 1;
                        exress[i].result_output = result_output.Text;
                        exress[i].result_value = result_value.Text;
                    }
                }
                config.telegram_commands[SelectedItem].command_execute_results = exress;
                command_execute_results.Items.Clear();
                if (config != null)
                {
                    foreach (ExecuteResult executeresult in config.telegram_commands[SelectedItem].command_execute_results)
                    {
                        command_execute_results.Items.Add(new ExecuteResult
                        {
                            result_checktype = executeresult.result_checktype,
                            result_output = executeresult.result_output,
                            result_value = executeresult.result_value,
                        });
                    }
                }
                //(telegram_commands_list.SelectedItem as Command).command_starts_with = command_starts_with.Text;

                //((Command)telegram_commands_list.SelectedItem).command_starts_with = command_starts_with.Text;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void EmptyDataInForm()
        {
            result_checktype.SelectedIndex = 0;
            result_value.Text = "";
            result_output.Text = "";
        }
    }
}
