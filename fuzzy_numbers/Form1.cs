using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using fuzzy_numbers_dll;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;

namespace fuzzy_numbers
{
    public partial class Form1 : Form
    {
        BindingList<Fuzzy_numbers<Double>> Mass = new BindingList<Fuzzy_numbers<Double>>();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Преобразует строковое представление в объект типа Fuzzy_numbers если это возможно
        /// </summary>
        /// <param name="s">Строковое представление объекта типа Fuzzy_numbers</param>
        /// <returns>Восстановленный из строки объект типа Fuzzy_numbers</returns>
        public Fuzzy_numbers<Double> ParseDouble(String s)
        {
            Fuzzy_numbers<Double> _Temp;
            String[] Name_F = s.Split('=');//отделяем название от описания
            try
            {
                //задаём имя числу
                if (Name_F.Length >= 2)
                {
                    _Temp = new Fuzzy_numbers<Double>(Name_F[0]);
                    s = Name_F[1];
                }
                else
                    _Temp = new Fuzzy_numbers<Double>();
                //заменяем знаки в соответствии с форматом и режем на элементы
                String[] _Split_s = s.Replace(" ", "").Replace(".", ",").Split('+');
                //бежим по элементам и добавляем в число
                foreach (String _s in _Split_s)
                {
                    String[] _Split_Slash = _s.Replace("\\", "/").Split('/');
                    if (_Split_Slash.Length == 2)
                        _Temp.Add(new Element_Fuzzy_numbers<Double>(Double.Parse(_Split_Slash[1]), Double.Parse(_Split_Slash[0])));
                }
            }
            catch (System.Exception ex)
            {
                throw new FormatException("Input string was invalid.\n" + ex.Message + "\n" + ex.Data);
            }
            return _Temp;
        }



        /// <summary>
        /// Считываем и распознаём множества из строкового представления
        /// </summary>
        /// <param name="text">Входная строка</param>
        public void read(String text)
        {
            Mass.Clear();//очищаем предыдущее чтение
            Error_Log_textBox.Text += "[info] Начали распознавание чисел. ";//логи
            String[] _Temp = text.Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace(".", ",").Split(';');//режем по строчкам множеств
            Error_Log_textBox.Text += "Предположительное количество: " + _Temp.Length + "\r\n";//логи
            foreach (String s in _Temp)//пробегаем
            {
                try//отлов исключений и ошибок, {самая забагованная часть кода :D}
                {
                    Fuzzy_numbers<Double> _F_Temp = ParseDouble(s);//преобразуем строку в нечёткое число 
                    if (_F_Temp == null)
                        break;
                    Mass.Add(new Fuzzy_numbers<Double>(_F_Temp, _F_Temp.Name));
                    Error_Log_textBox.Text += "[info] Число: \"" + _F_Temp.Name + "\" распознано \r\n";//логи
                }
                catch (System.Exception ex)
                {
                    Error_Log_textBox.Text += "[Ошибка] " + ex.Message + " произошла на \"" + s + "\" \r\n";//логи
                }
            }
            Error_Log_textBox.Text += "[info] Распознано: " + Mass.Count + " из " + _Temp.Length + "\r\n";//логи
        }

        private void toolStripButton_Click(object sender, EventArgs e)
        {
            switch ((sender as ToolStripButton).Text)
            {
                case "+": Instructions_textBox.Paste("+"); break;
                case "-": Instructions_textBox.Paste("-"); break;
                case "*": Instructions_textBox.Paste("*"); break;
                case "/": Instructions_textBox.Paste("/"); break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Version_label.Text += Assembly.GetExecutingAssembly().GetName().Version.ToString(); ;
            Readed_listBox.DataSource = Mass;
        }

        private void считатьМножестваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            read(Input_textBox.Text.Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace(".", ","));
        }

        private void выполнитьОписанныеНижеДействияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            read(Input_textBox.Text);
            SplitCalculate(Instructions_textBox.Text);
        }


        /// <summary>
        /// Производит обработку разбиение вычислений
        /// </summary>
        /// <param name="text">Список функций и вычислений</param>
        public void SplitCalculate(String text)
        {
            try
            {
                Result_listBox.Items.Clear();
                Error_Log_textBox.Text += "[info] Начали распознавание действий над числами. ";//логи
                String[] _Temp = text.Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace(".", ",").Split(';');//режем по строчкам множеств
                Error_Log_textBox.Text += "Предположительное число инструкций: " + _Temp.Length + "\r\n";//логи

                List<String> Poliz = new List<String>();
                Error_Log_textBox.Text += "[info] Начали преобразование выражений \r\n";//логи
                for (Int32 i = 0; i < _Temp.Length; i++)
                {
                    try
                    {
                        Poliz.Add(MyRevers(_Temp[i]));
                        Error_Log_textBox.Text += Poliz[i] + "\r\n";
                    }
                    catch { };
                }
                Error_Log_textBox.Text += "[info] Преобразования завершены \r\n[info] Начинаем расчёт\r\n";//логи
                for (int i = 0; i < Poliz.Count; i++)
                {
                    Result_listBox.Items.Add(Calculate(Poliz[i], _Temp[i]));//считаем выражение и пишем ответ
                    Result_listBox.SelectedIndex = i;
                }
            }
            catch (System.Exception ex)
            {
                Error_Log_textBox.Text += "[Предупреждение] " + ex.Message + "\r\n";//логи
            }

        }

        /// <summary>
        /// Считает результаты 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public String Calculate(String text, String Name = "")
        {
            Stack<Fuzzy_numbers<Double>> stack = new Stack<Fuzzy_numbers<Double>>();
            String[] Split_POLIZ = text.Split(' ');//режем текст по пробелам
            String str = "";
            foreach (String st in Split_POLIZ)
            {
                str = st.Replace(" ", "");//На всякий случай грязный хак
                if (str != "")
                    if (str != "+" && str != "-" && str != "*" && str != "/")
                    {
                        foreach (var fuz in Mass)//бежим по массиву чисел и сверяем есть ли там такое
                            if (fuz.Name == str)
                            {
                                stack.Push(fuz); break;
                            }
                    }
                    else
                    {
                        switch (str)
                        {
                            case "+": stack.Push(stack.Pop() + stack.Pop()); break;
                            case "-": stack.Push(stack.Pop() - stack.Pop()); break;
                            case "*": stack.Push(stack.Pop() * stack.Pop()); break;
                            case "/": stack.Push(stack.Pop() / stack.Pop()); break;
                        }
                    }
            }
            if (stack.Count == 1)
                return stack.Pop().ToString();
            else
                return Name + " = " + "Неверно заданы операции";
        }


        /// <summary>
        /// Парсер строки в ПОЛИЗ
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Результат строка представляющая запись вида ПОЛИЗ</returns>
        public String MyRevers(string input)
        {
            String myoutputString = " ";
            Int32 sL = 0;
            Int32 sW = 0;

            for (Int32 i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    sL = sL + 1;
                }
                if (input[i] == ')')
                {
                    sW = sW + 1;
                }
            }
            if (sL != sW)
            {
                MessageBox.Show("Input error");
                return " ";

            }
            Regex rx = new Regex(@"\(|\)|\+|\-|\*|\/|([a-z][A-Z]*|[А-Я][а-я]*)");
            MatchCollection mc = rx.Matches(input);

            Regex id = new Regex(@"[a-z][A-Z]*|[А-Я][а-я]*"); // идентификаторы
            Regex skobki = new Regex(@"\(|\)"); // скобки
            string[] operators = { "(", ")", "*", "/", "+", "-"}; // операторы по приоритету

            Regex opers = new Regex(@"\(|\)|\+|\-|\*|\/"); // операторы

            Stack stOper = new Stack();
            ArrayList expr = new ArrayList();
            foreach (Match m in mc)
            {
                Match m1;
                m1 = id.Match(m.Value);
                if (m1.Success) { expr.Add(m1.Value); continue; }
                m1 = skobki.Match(m.Value);
                if (m1.Success)
                {
                    if (m1.Value == "(") { stOper.Push(m1.Value); continue; }
                    string op = stOper.Pop().ToString();
                    while (op != "(")
                    {
                        expr.Add(op);
                        op = stOper.Pop().ToString();
                    }
                    continue;
                }
                m1 = opers.Match(m.Value);
                if (m1.Success)
                {
                    try
                    {
                        while (Array.IndexOf(operators, m1.Value) > Array.IndexOf(operators, stOper.Peek()))
                        {
                            if (stOper.Peek().ToString() == "(") break;
                            expr.Add(stOper.Pop().ToString());
                        }
                    }
                    catch
                    {

                    }
                    stOper.Push(m1.Value);
                }
            }
            while (stOper.Count != 0)
            {
                expr.Add(stOper.Pop().ToString());
            }

            foreach (string s in expr)
            {

                myoutputString += s + " ";
            }
            return myoutputString.Substring(1, myoutputString.Length - 2);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "*.fuz|*.fuz";
            save.FilterIndex = 0;
            if (save.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter write = new StreamWriter(save.FileName))
                {
                    write.WriteLine(Input_textBox.Text);
                    write.WriteLine("/==============/");
                    write.Write(Instructions_textBox.Text);
                }
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "*.fuz|*.fuz";
            open.FilterIndex = 0;
            if (open.ShowDialog() == DialogResult.OK)//если нажали ок
            {
                Read_From_File(open.FileName);
            }
        }


        /// <summary>
        /// Функция загружающая инфу из файла
        /// </summary>
        /// <param name="FileName">Адрес файла</param>
        void Read_From_File(String FileName)
        {
            Input_textBox.Text = "";//очищаем поле с числами
            Instructions_textBox.Text = "";//очищаем с инструкциями
            using (StreamReader reade = new StreamReader(FileName))//открываем поток на чтение
            {
                Int32 i = 0;//обнуляем
                while (!reade.EndOfStream)//если не конец потока
                {
                    String s = reade.ReadLine();
                    if (s == "/==============/")
                    {
                        i++; s = "";
                    }
                    if (s != "")//если s не пустая
                    {
                        if (i == 0)
                            Input_textBox.Text += s + "\r\n";
                        if (i ==1)
                            Instructions_textBox.Text += s + "\r\n";
                    }
                }
            }
        }

        private void построитьГрафикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckedListBox CheckedListBoxs = (contextMenuStrip1.SourceControl as CheckedListBox);
            if (CheckedListBoxs != null)
            {
                //Список чисел для отправки на постройку
                List<Fuzzy_numbers<Double>> Selected_Items = new List<Fuzzy_numbers<Double>>();
                foreach (var it in CheckedListBoxs.CheckedItems)//пробегаемся по отмеченным
                {
                    Fuzzy_numbers<Double> _Temp =ParseDouble( it.ToString());
                    if (_Temp != null)//если преобразовалось то добавляем в список отправки
                    {
                        Selected_Items.Add(_Temp);
                    }
                }
                if (Selected_Items.Count != 0)//если количество больше 0
                    new Graphics1(Selected_Items).Show();//отправляем на построение
            }
        }

        private void вБуферToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckedListBox CheckedListBoxs = (contextMenuStrip1.SourceControl as CheckedListBox);
            if (CheckedListBoxs != null)
            {
                String s = "";
                for (Int32 i = 0; i < CheckedListBoxs.Items.Count; i++)
                    s += CheckedListBoxs.Items[i].ToString() + "\r\n";
                Clipboard.SetText(s);
            }
        }

    }
}
