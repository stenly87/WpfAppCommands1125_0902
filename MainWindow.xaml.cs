using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppCommands
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // публичное свойство, чтобы можно было 
        // задействовать привязку
        public CustomCommand Command1 { get; }
        public CustomCommand<string> Command2 { get; }

        public string SomeText { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Command1 = new CustomCommand(() => {
                MessageBox.Show("Команда исполнена");
            });

            Command2 = new CustomCommand<string>(s => {
                // s - это значение свойства Parametr
                MessageBox.Show(s);
            },
                s => !string.IsNullOrEmpty(s) );

            // Команды в wpf
            // За реализацию поддержки команд в компонентах
            // отвечает интерфейс ICommandSource
            // В этом интерфейсе
            // Command - сам объект команды
            // CommandParameter - аргумент для вызова команды
            // на исполнение
            // CommandTarget - целевой объект для выполнения
            // команды

            // Стандартные команды содержатся в нескольких
            // классах:
            // ApplicationCommands - стандартные команды* 
            // ComponentCommands - команды для курсора и тп
            // MediaCommands - команды для медиа плеера
            // NavigationCommands - команды для страниц (навигация)
            // SystemCommands - команды для окон*
            // EditingCommands - команды для потоковых документов
            // * - что-то полезное

            // Для ряда команд нет стандартной реализации, поэтому
            // ее нужно указывать вручную. Для этого существует
            // коллекция CommandBindings, которую можно задать на уровне
            // компонента (кнопка, компоновщик, окно, app.xaml)
            // В коллекции для каждой команды создается объект CommandBinding
            // в нем можно указать обработчики событий Executed и CanExecuted
            // в CanExecuted нужно задать значение для свойства e.CanExecute
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var source = e.OriginalSource as TextBox;
            File.WriteAllText(source.Name + ".txt", source.Text);
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var source = (string)e.Parameter;
            e.CanExecute = source != null && !string.IsNullOrEmpty(source);
        }

        private void CommandBindingHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // этот метод можно вызвать на F1
            MessageBox.Show("Бог поможет");
        }
    }
}
