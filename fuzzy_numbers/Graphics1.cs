using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using fuzzy_numbers_dll;

namespace fuzzy_numbers
{
    public partial class Graphics1 : Form
    {
        /// <summary>
        /// Список цветов
        /// </summary>
        List<Color> Color_Array = new List<Color>();
        /// <summary>
        /// Полученные множества для построения графика
        /// </summary>
        List<Fuzzy_numbers<Double>> Selecteds;
        private CheckBox checkBox1;
        private Label label1;
        private PictureBox pictureBox1;
        Double MAX = 0;//максимум для постройки графиков
        public Graphics1(List<Fuzzy_numbers<Double>> Selecteds)
        {
            this.Selecteds = Selecteds;
            InitializeComponent();

            ColorDialog ccol = new ColorDialog();

            List<Label> Label_Array = new List<Label>();

            MAX = Math.Abs(Selecteds[0][0].Element);//максимум для постройки графика
            foreach (var e in Selecteds)
            {
                if (ccol.ShowDialog() == DialogResult.OK)
                    Color_Array.Add(ccol.Color);
                for (int i = 0; i < e.Count; i++)
                    if (MAX < Math.Abs(e[i].Element)) MAX = Math.Abs(e[i].Element);

                Label _Temp = new Label()
                {
                    AutoSize = true,
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)))
                };
                Label_Array.Add(_Temp);
            }
            for (Int32 i = 0; i < Selecteds.Count; i++)
            {
                Label_Array[i].Text = Selecteds[i].Name;
                Label_Array[i].ForeColor = Color_Array[i];
                Label_Array[i].Location = new Point(3, (i * 15) + 25);
                this.Controls.Add(Label_Array[i]);
            }
            Ris();
        }

        Graphics gr;

        void inint_gr()
        {
            Int32 _Height = pictureBox1.Height, _Width = pictureBox1.Width;//размеры холста
            if (_Height > 0 && _Width > 0)
            {
                Image im = new Bitmap(_Width, _Height);
                //Пикчер боксу присваиваю изображение на котором буду рисовать
                pictureBox1.Image = im;
                //Создаю объект график
                gr = Graphics.FromImage(pictureBox1.Image);

                //Задаю новую систему координат с серединой экрана
                gr.TranslateTransform(_Width / 2, _Height);
                //Располагаю оси с право на лево
                //Снизу  в верх.
                gr.ScaleTransform(1, -1);

                //Создаю карандаш/перо
                Pen p = new Pen(Brushes.Black);

                #region ставлю надсечки для осей
                #region ставлю надсечки для OX
                for (int i = 0; i < _Width / 2; i += 25)
                {
                    gr.DrawLine(Pens.LightGray, i, 20, i, _Height + 30);
                    gr.DrawLine(Pens.LightGray, -i, 20, -i, _Height + 30);
                    gr.DrawLine(p, i, 20, i, 30);
                    gr.DrawLine(p, -i, 20, -i, 30);
                }
                #endregion
                #region ставлю надсечки для OY
                for (int i = 0; i < _Height; i += 25)
                {
                    gr.DrawLine(Pens.LightGray, -5 - _Width / 2, i, 5 + _Width / 2, i);
                    gr.DrawLine(p, -5, i, 5, i);
                }
                #endregion
                #endregion
                //Создаю массив точек
                Point[] _Point = new Point[4];
                #region ТОчки для осей
                #region ОХ
                //Первая точка OX
                _Point[0] = new Point
                {
                    X = (-_Width / 2),
                    Y = 25
                };
                //Вторя точка OX
                _Point[1] = new Point
                {
                    X = (_Width / 2),
                    Y = 25
                };
                #endregion
                #region ОY
                //Первая точка OY
                _Point[2] = new Point
                {
                    X = 0,
                    Y = -_Height
                };
                //Вторя точка OY
                _Point[3] = new Point
                {
                    X = 0,
                    Y = _Height
                };
                #endregion
                #endregion
                //рисую оси
                gr.DrawLine(p, _Point[0], _Point[1]);
                gr.DrawLine(p, _Point[2], _Point[3]);
                #region Ставлю надписи на надсечки
                //Зеркалю
                gr.ScaleTransform(1, -1);
                //OX
                for (int i = 0; i < _Width / 2; i += 25)
                {
                    gr.DrawString(Math.Round(i / ((_Width / 2d) / MAX), 2).ToString(), DefaultFont, Brushes.Black, (float)i, (float)-20);
                    gr.DrawString(Math.Round(-i / ((_Width / 2d) / MAX), 2).ToString(), DefaultFont, Brushes.Black, (float)-i, (float)-20);
                }
                //OY
                for (int i = 0; i < _Height; i += 25)
                {
                    gr.DrawString(Math.Round(i / (Double)(_Height - 25), 2).ToString(), DefaultFont, Brushes.Black, (float)4, (float)-i - 25);
                }
                //еще раз зеркалю
                gr.ScaleTransform(1, -1);
                #endregion
            }
        }

        void plot(Fuzzy_numbers<Double> e, Brush _Brush)
        {
            Int32 _Height = pictureBox1.Height, _Width = pictureBox1.Width;//размеры холста

            Point[] _p = new Point[e.Count];//линии
            //сортировка элементов по оси х
            Fuzzy_numbers<Double> _T = e.Sort_from_Element;
            for (int i = 0; i < e.Count; i++)
            {
                _p[i].X = (int)((int)(e[i].Element * ((_Width / 2d) / MAX)));//добавляем точку x для контура
                _p[i].Y = (int)((int)((e[i].Accessory_Function) * (_Height - 25) + 25));//точку y для контура
                gr.FillEllipse(_Brush, (int)(e[i].Element * ((_Width / 2d) / MAX)) - 3, (int)((e[i].Accessory_Function) * (_Height - 25) + 22), 6, 6);
            }
            if (checkBox1.Checked)
                gr.DrawLines(new Pen(_Brush), _p);
        }

        private void Graphics_Load(object sender, EventArgs e)
        {

        }

        void Ris()
        {
            inint_gr();
            for (Int32 i = 0; i < Selecteds.Count; i++)
            {
                plot(Selecteds[i], new SolidBrush(Color_Array[i]));
            }
        }

        private void Graphics1_SizeChanged(object sender, EventArgs e)
        {
            Ris();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Ris();
        }

        private void InitializeComponent()
        {
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBox1.Location = new System.Drawing.Point(59, 11);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(12, 11);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Легенда";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(73, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(568, 335);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // Graphics1
            // 
            this.ClientSize = new System.Drawing.Size(653, 356);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Graphics1";
            this.Load += new System.EventHandler(this.Graphics1_Load);
            this.SizeChanged += new System.EventHandler(this.Graphics1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Graphics1_Load(object sender, EventArgs e)
        {

        }
    }
}
