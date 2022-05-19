using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public partial class Form2 : Form
    {

        bool[] your_flags = new bool[5] { false, false, false, false, false }; //какие кости выбраны для переброса у игрока
        bool[] enemy_flags = new bool[5] { false, false, false, false, false }; //какие кости выбраны для переброса у противника

        int[] enemy_cubes = new int[5] { 0, 0, 0, 0, 0 }; //массивы игрового поля
        int[] your_cubes = new int[5] { 0, 0, 0, 0, 0 };

        int[] check_enemy_cubes = new int[5] { 0, 0, 0, 0, 0 }; //отсортированные массивы игрового поля
        int[] check_your_cubes = new int[5] { 0, 0, 0, 0, 0 };

        int your_sum = 0; //суммы костей. используются в случае, если комбинации и их номинал совпали
        int enemy_sum = 0;

        bool flag = false; //флаг для разограничения комбинации "ПАРА"

        Random random = new Random();

        byte coin_enemy = 255; //численное значение выпавшей комбинации
        byte coin_your = 255;

        int value_combination_enemy = 0; //численное значение комбинации одной кости
        int value_combination_your = 0;

        int enemy_val_for_TWO_PARE = 0; //для расчетов
        int enemy_val_for_FULL_HOUSE = 0;

        bool PVE_mod = false;

        void start_settings() //стартовые настройки, вызываются перед каждым началом игры
        {
            for (int i = 0; i < your_cubes.Length; i++)
            {
                your_cubes[i] = 0;
                enemy_cubes[i] = 0;
                check_your_cubes[i] = 0;
                check_enemy_cubes[i] = 0;
                your_flags[i] = false;
                enemy_flags[i] = false;

            }

            enemy_cube_1.Image = Image.FromFile("photos\\1.png");
            enemy_cube_2.Image = Image.FromFile("photos\\1.png");
            enemy_cube_3.Image = Image.FromFile("photos\\1.png");
            enemy_cube_4.Image = Image.FromFile("photos\\1.png");
            enemy_cube_5.Image = Image.FromFile("photos\\1.png");

            your_cube_1.Image = Image.FromFile("photos\\1.png");
            your_cube_2.Image = Image.FromFile("photos\\1.png");
            your_cube_3.Image = Image.FromFile("photos\\1.png");
            your_cube_4.Image = Image.FromFile("photos\\1.png");
            your_cube_5.Image = Image.FromFile("photos\\1.png");

            /*picture_choose_1.Visible = false;
            picture_choose_2.Visible = false;
            picture_choose_3.Visible = false;
            picture_choose_4.Visible = false;
            picture_choose_5.Visible = false;*/



            enemy_choose_1.BackColor = Color.White;
            enemy_choose_2.BackColor = Color.White;
            enemy_choose_3.BackColor = Color.White;
            enemy_choose_4.BackColor = Color.White;
            enemy_choose_5.BackColor = Color.White;

            choose_1.BackColor = Color.White;
            choose_2.BackColor = Color.White;
            choose_3.BackColor = Color.White;
            choose_4.BackColor = Color.White;
            choose_5.BackColor = Color.White;

            your_cube_1.Enabled = false;
            your_cube_2.Enabled = false;
            your_cube_3.Enabled = false;
            your_cube_4.Enabled = false;
            your_cube_5.Enabled = false;

            enemy_cube_1.Enabled = false;
            enemy_cube_2.Enabled = false;
            enemy_cube_3.Enabled = false;
            enemy_cube_4.Enabled = false;
            enemy_cube_5.Enabled = false;

            flag = false;

            coin_enemy = 255;
            coin_your = 255;

            value_combination_enemy = 0;
            value_combination_your = 0;

            enemy_val_for_TWO_PARE = 0;
            enemy_val_for_FULL_HOUSE = 0;

            your_sum = 0;
            enemy_sum = 0;

            button1.Text = "БРОСИТЬ КОСТИ";

            label_enemy_combination.Text = "";
            label_your_combination.Text = "";


        }

        void enemy_generation()
        {

            for (int i = 0; i < enemy_cubes.Length; i++)
            {
                //Random random = new Random();
                int value = random.Next(1, 7);
                enemy_cubes[i] = value;
                string value_string;

                switch (i)
                {
                    case 0:
                        value_string = Convert.ToString(value);
                        enemy_cube_1.Image = Image.FromFile("photos\\" + value_string + ".png");
                        break;
                    case 1:
                        value_string = Convert.ToString(value);
                        enemy_cube_2.Image = Image.FromFile("photos\\" + value_string + ".png");
                        break;
                    case 2:
                        value_string = Convert.ToString(value);
                        enemy_cube_3.Image = Image.FromFile("photos\\" + value_string + ".png");
                        break;
                    case 3:
                        value_string = Convert.ToString(value);
                        enemy_cube_4.Image = Image.FromFile("photos\\" + value_string + ".png");
                        break;
                    case 4:
                        value_string = Convert.ToString(value);
                        enemy_cube_5.Image = Image.FromFile("photos\\" + value_string + ".png");
                        break;
                }
            }
        }   //первый бросок противника

        void enemy_think() //мозги противника перед вторым броском
        {
            string enemy_result = game_result(check_enemy_cubes, false);
            string your_result = game_result(check_your_cubes, true);

            byte val_result_enemy = switch_results(enemy_result);
            byte val_result_your = switch_results(your_result);

            if (val_result_enemy > val_result_your && enemy_result != "МАЛЫЙ СТРЕЙТ" && enemy_result != "БОЛЬШОЙ СТРЕЙТ" && enemy_result != "ФУЛ-ХАУС" && enemy_result != "ПОКЕР")
            {
                for (int i = 0; i < enemy_cubes.Length; i++)
                {
                    if (enemy_cubes[i] != value_combination_enemy)
                    {
                        enemy_flags[i] = true;
                    }
                }

            }
            else if (val_result_enemy <= val_result_your)
            {
                if (enemy_result == "ПАРА" || enemy_result == "СЕТ" || enemy_result == "КАРЕ")
                {
                    for (int i = 0; i < enemy_cubes.Length; i++)
                    {
                        if (enemy_cubes[i] != value_combination_enemy)
                        {
                            enemy_flags[i] = true;
                        }
                    }
                }

                if (enemy_result == "НИЧЕГО")
                {
                    int maximun_value = enemy_cubes.Max();

                    for (int i = 0; i < enemy_cubes.Length; i++)
                    {
                        if (enemy_cubes[i] != maximun_value)
                        {
                            enemy_flags[i] = true;
                        }
                    }
                }

                if (enemy_result == "ДВЕ ПАРЫ")
                {
                    int second_pare = value_combination_your - enemy_val_for_TWO_PARE;

                    if (second_pare > enemy_val_for_TWO_PARE)
                    {
                        for (int i = 0; i < enemy_cubes.Length; i++)
                        {
                            if (enemy_cubes[i] != second_pare)
                            {
                                enemy_flags[i] = true;
                            }
                        }
                    }
                    else if (second_pare < enemy_val_for_TWO_PARE)
                    {
                        for (int i = 0; i < enemy_cubes.Length; i++)
                        {
                            if (enemy_cubes[i] != enemy_val_for_TWO_PARE)
                            {
                                enemy_flags[i] = true;
                            }
                        }
                    }
                    else if (second_pare == enemy_val_for_TWO_PARE)
                    {
                        int count = 0;

                        for (int i = 0; i < enemy_cubes.Length; i++)
                        {

                            if (enemy_cubes[i] == enemy_val_for_TWO_PARE && count < 2)
                            {
                                count++;
                                enemy_flags[i] = true;
                            }

                            if (enemy_cubes[i] != enemy_val_for_TWO_PARE)
                            {
                                enemy_flags[i] = true;
                            }
                        }
                    }

                }

                if (enemy_result == "ФУЛ-ХАУС")
                {
                    //int first_pare = enemy_val_for_FULL_HOUSE / 3;
                    int second_pare = (value_combination_your - enemy_val_for_FULL_HOUSE) / 2;

                    for (int i = 0; i < enemy_cubes.Length; i++)
                    {
                        if (enemy_cubes[i] != second_pare)
                        {
                            enemy_flags[i] = true;
                        }
                    }
                }



            }

        }

        void your_generation()
        {
            //Random random = new Random();
            for (int i = 0; i < your_cubes.Length; i++)
            {
                int value = random.Next(1, 7);
                your_cubes[i] = value;
                string value_string;

                switch (i)
                {
                    case 0:
                        value_string = Convert.ToString(value);
                        your_cube_1.Image = Image.FromFile("photos\\" + value_string + ".png");
                        break;
                    case 1:
                        value_string = Convert.ToString(value);
                        your_cube_2.Image = Image.FromFile("photos\\" + value_string + ".png");
                        break;
                    case 2:
                        value_string = Convert.ToString(value);
                        your_cube_3.Image = Image.FromFile("photos\\" + value_string + ".png");
                        break;
                    case 3:
                        value_string = Convert.ToString(value);
                        your_cube_4.Image = Image.FromFile("photos\\" + value_string + ".png");
                        break;
                    case 4:
                        value_string = Convert.ToString(value);
                        your_cube_5.Image = Image.FromFile("photos\\" + value_string + ".png");
                        break;
                }
            }
        }   //первый бросок игрока

        void your_generation(bool[] array)
        {
            for (int i = 0; i < your_cubes.Length; i++)
            {
                if (your_flags[i])
                {
                    int value = random.Next(1, 7);
                    your_cubes[i] = value;
                    string value_string;

                    switch (i)
                    {
                        case 0:
                            value_string = Convert.ToString(value);
                            your_cube_1.Image = Image.FromFile("photos\\" + value_string + ".png");
                            break;
                        case 1:
                            value_string = Convert.ToString(value);
                            your_cube_2.Image = Image.FromFile("photos\\" + value_string + ".png");
                            break;
                        case 2:
                            value_string = Convert.ToString(value);
                            your_cube_3.Image = Image.FromFile("photos\\" + value_string + ".png");
                            break;
                        case 3:
                            value_string = Convert.ToString(value);
                            your_cube_4.Image = Image.FromFile("photos\\" + value_string + ".png");
                            break;
                        case 4:
                            value_string = Convert.ToString(value);
                            your_cube_5.Image = Image.FromFile("photos\\" + value_string + ".png");
                            break;
                    }
                }
            }
        }   //второй бросок игрока

        void enemy_generation(bool[] array)
        {
            for (int i = 0; i < enemy_cubes.Length; i++)
            {
                if (enemy_flags[i])
                {
                    int value = random.Next(1, 7);
                    enemy_cubes[i] = value;
                    string value_string;

                    switch (i)
                    {
                        case 0:
                            value_string = Convert.ToString(value);
                            enemy_cube_1.Image = Image.FromFile("photos\\" + value_string + ".png");
                            break;
                        case 1:
                            value_string = Convert.ToString(value);
                            enemy_cube_2.Image = Image.FromFile("photos\\" + value_string + ".png");
                            break;
                        case 2:
                            value_string = Convert.ToString(value);
                            enemy_cube_3.Image = Image.FromFile("photos\\" + value_string + ".png");
                            break;
                        case 3:
                            value_string = Convert.ToString(value);
                            enemy_cube_4.Image = Image.FromFile("photos\\" + value_string + ".png");
                            break;
                        case 4:
                            value_string = Convert.ToString(value);
                            enemy_cube_5.Image = Image.FromFile("photos\\" + value_string + ".png");
                            break;
                    }
                }
            }
        }   //второй бросок противника

        void bubble_sort()  //сортировка "костей" противника и игрока
        {
            for (int i = 0; i < enemy_cubes.Length; i++)
            {
                check_enemy_cubes[i] = enemy_cubes[i];
                check_your_cubes[i] = your_cubes[i];
            }

            int temp;
            for (int i = 0; i < check_enemy_cubes.Length; i++)  //сортировка вражеского массива
            {
                for (int j = i + 1; j < check_enemy_cubes.Length; j++)
                {
                    if (check_enemy_cubes[i] > check_enemy_cubes[j])
                    {
                        temp = check_enemy_cubes[i];
                        check_enemy_cubes[i] = check_enemy_cubes[j];
                        check_enemy_cubes[j] = temp;
                    }
                }
            }



            for (int i = 0; i < check_your_cubes.Length; i++)  //сортировка своего массива
            {
                for (int j = i + 1; j < check_your_cubes.Length; j++)
                {
                    if (check_your_cubes[i] > check_your_cubes[j])
                    {
                        temp = check_your_cubes[i];
                        check_your_cubes[i] = check_your_cubes[j];
                        check_your_cubes[j] = temp;
                    }
                }
            }

        }

        string game_result(int[] array, bool who)    //анализирует что выпало и у кого
        {
            string res = "НИЧЕГО";
            int[] check = new int[6];

            for (int i = 1; i <= 6; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[j] == i)
                    {
                        check[i - 1]++;
                    }
                }
            }

            for (int i = 0; i < check.Length; i++)
            {
                if (check[i] == 5)
                {
                    if (who) value_combination_your = i + 1; else value_combination_enemy = i + 1;
                    res = "ПОКЕР";
                    break;
                }


                if (check[i] == 4)
                {
                    if (who) value_combination_your = i + 1; else value_combination_enemy = i + 1;
                    res = "КАРЕ";
                    break;
                }


                if (check[0] == 1 && check[1] == 1 && check[2] == 1 && check[3] == 1 && check[4] == 1)
                {
                    if (who) value_combination_your = 15; else value_combination_enemy = 15;
                    res = "МАЛЫЙ СТРЕЙТ";
                    break;
                }

                if (check[1] == 1 && check[2] == 1 && check[3] == 1 && check[4] == 1 && check[5] == 1)
                {
                    if (who) value_combination_your = 20; else value_combination_enemy = 20;
                    res = "БОЛЬШОЙ СТРЕЙТ";
                    break;
                }


                if (check[i] == 3)
                {

                    for (int j = 0; j < check.Length; j++)
                    {
                        if (check[j] == 2)
                        {
                            flag = true;
                            if (who) value_combination_your = 3 * (i + 1) + 2 * (j + 1); else value_combination_enemy = 3 * (i + 1) + 2 * (j + 1);
                            enemy_val_for_FULL_HOUSE = 3 * (i + 1);
                            res = "ФУЛ-ХАУС";
                            break;
                        }
                    }

                    if (flag)
                    {
                        flag = false;
                        break;
                    }
                    else { if (who) value_combination_your = i + 1; else value_combination_enemy = i + 1; res = "СЕТ"; }

                }


                if (check[i] == 2)
                {
                    if (i == 5)
                    {
                        if (who) value_combination_your = i + 1; else value_combination_enemy = i + 1;
                        res = "ПАРА";
                        break;
                    }

                    for (int j = 0; j < check.Length; j++)
                    {
                        if (j == i) continue;

                        if (check[j] == 2)
                        {
                            if (who) value_combination_your = (i + 1) + (j + 1); else value_combination_enemy = (i + 1) + (j + 1);
                            enemy_val_for_TWO_PARE = i + 1;
                            flag = true;
                            res = "ДВЕ ПАРЫ";
                            break;
                        }

                        if (check[j] == 3)
                        {
                            if (who) value_combination_your = 3 * (i + 1) + 2 * (j + 1); else value_combination_enemy = 3 * (i + 1) + 2 * (j + 1);
                            enemy_val_for_FULL_HOUSE = 3 * (i + 1);
                            flag = true;
                            res = "ФУЛ-ХАУС";
                            break;
                        }

                    }
                    if (flag)
                    {
                        flag = false;
                        break;
                    }
                    else
                    {
                        if (who) value_combination_your = i + 1; else value_combination_enemy = i + 1;
                        res = "ПАРА";
                        break;
                    }
                }

            }

            if (who)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    your_sum += array[i];
                }
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    enemy_sum += array[i];
                }
            }

            if (res == "НИЧЕГО") if (who) value_combination_your = 255; else value_combination_enemy = 255;

            return res;
        }

        public Form2(bool PVE)
        {
            InitializeComponent();

            PVE_mod = PVE;

            /*picture_choose_1.Image = Image.FromFile("photos\\effect.gif");
            picture_choose_2.Image = Image.FromFile("photos\\effect.gif");
            picture_choose_3.Image = Image.FromFile("photos\\effect.gif");
            picture_choose_4.Image = Image.FromFile("photos\\effect.gif");
            picture_choose_5.Image = Image.FromFile("photos\\effect.gif");*/

            start_settings();
        }

        byte switch_results(string coins)   //конвертирует слова в числа
        {
            byte res = 255;
            switch (coins)
            {
                case "ПОКЕР": { res = 8; break; }
                case "КАРЕ": { res = 7; break; }
                case "ФУЛ-ХАУС": { res = 6; break; }
                case "БОЛЬШОЙ СТРЕЙТ": { res = 5; break; }
                case "МАЛЫЙ СТРЕЙТ": { res = 4; break; }
                case "СЕТ": { res = 3; break; }
                case "ДВЕ ПАРЫ": { res = 2; break; }
                case "ПАРА": { res = 1; break; }
                case "НИЧЕГО": { res = 0; break; }
            }

            return res;
        }

        private void button1_Click(object sender, EventArgs e)  //нажатие на кнопку
        {

            if (button1.Text == "ПЕРЕБРОСИТЬ")
            {
                if (PVE_mod)
                {
                    enemy_think();
                }


                enemy_generation(enemy_flags);

                your_generation(your_flags);

                bubble_sort();

                /*picture_choose_1.Visible = false;
                picture_choose_2.Visible = false;
                picture_choose_3.Visible = false;
                picture_choose_4.Visible = false;
                picture_choose_5.Visible = false;*/

                string enemy_result = game_result(check_enemy_cubes, false);
                string your_result = game_result(check_your_cubes, true);

                coin_enemy = switch_results(enemy_result);
                coin_your = switch_results(your_result);

                int enemy_sc = Convert.ToInt32(enemy_score.Text);
                int your_sc = Convert.ToInt32(your_score.Text);

                label_enemy_combination.Text = enemy_result;
                label_your_combination.Text = your_result;

                if (coin_enemy > coin_your) //если комбинация больше у противника
                {
                    enemy_sc++;
                    enemy_score.Text = Convert.ToString(enemy_sc);
                    MessageBox.Show("Победил противник");
                }

                else if (coin_enemy < coin_your) //если комбинация больше у игрока
                {
                    your_sc++;
                    your_score.Text = Convert.ToString(your_sc);
                    MessageBox.Show("Вы победили");
                }

                else if (coin_enemy == coin_your) //если комбинации равны
                {
                    if (value_combination_enemy > value_combination_your) //сравниваем сумму номиналов комбинаций. если у противника больше
                    {
                        enemy_sc++;
                        enemy_score.Text = Convert.ToString(enemy_sc);
                        MessageBox.Show("Победил противник");
                    }
                    else if (value_combination_enemy < value_combination_your)    //если у игрока больше
                    {
                        your_sc++;
                        your_score.Text = Convert.ToString(your_sc);
                        MessageBox.Show("Вы победили");
                    }
                    else if (value_combination_enemy == value_combination_your)   //если равны, то идем смотреть сумму всех костей
                    {
                        if (your_sum > enemy_sum) //если больше у игрока
                        {
                            your_sc++;
                            your_score.Text = Convert.ToString(your_sc);
                            MessageBox.Show("Вы победили");
                        }
                        else if (your_sum < enemy_sum)  //если больше у противника
                        {
                            enemy_sc++;
                            enemy_score.Text = Convert.ToString(enemy_sc);
                            MessageBox.Show("Победил противник");
                        }
                        else if (your_sum == enemy_sum) MessageBox.Show("НИЧЬЯ");   //если равны
                    }



                }

                button1.Text = "НАЧАТЬ СНАЧАЛА";
            }

            else if (button1.Text == "БРОСИТЬ КОСТИ")
            {
                your_generation();
                enemy_generation();

                your_cube_1.Enabled = true;
                your_cube_2.Enabled = true;
                your_cube_3.Enabled = true;
                your_cube_4.Enabled = true;
                your_cube_5.Enabled = true;

                if (!PVE_mod)
                {
                    enemy_cube_1.Enabled = true;
                    enemy_cube_2.Enabled = true;
                    enemy_cube_3.Enabled = true;
                    enemy_cube_4.Enabled = true;
                    enemy_cube_5.Enabled = true;
                }

                bubble_sort();

                button1.Text = "ПЕРЕБРОСИТЬ";
            }

            else if (button1.Text == "НАЧАТЬ СНАЧАЛА")
            {
                start_settings();
            }

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)   //закрытие программы при закрытии окна
        {


            Application.Exit();

        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e) //нажание на меня "Справка"
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void your_cube_1_Click(object sender, EventArgs e)  //ниже обрабатываются нажатия на кубики
        {
            if (your_flags[0])
            {
                your_flags[0] = false;
                //picture_choose_1.Visible = false;
                choose_1.BackColor = Color.White;
            }
            else
            {
                your_flags[0] = true;
                //picture_choose_1.Visible = true;
                choose_1.BackColor = Color.Blue;
            }
        }

        private void your_cube_2_Click(object sender, EventArgs e)
        {
            if (your_flags[1])
            {
                your_flags[1] = false;
                //picture_choose_2.Visible = false;
                choose_2.BackColor = Color.White;
            }
            else
            {
                your_flags[1] = true;
                // picture_choose_2.Visible = true;
                choose_2.BackColor = Color.Blue;
            }
        }

        private void your_cube_3_Click(object sender, EventArgs e)
        {
            if (your_flags[2])
            {
                your_flags[2] = false;
                //picture_choose_3.Visible = false;
                choose_3.BackColor = Color.White;
            }
            else
            {
                your_flags[2] = true;
                //picture_choose_3.Visible = true;
                choose_3.BackColor = Color.Blue;
            }
        }

        private void your_cube_4_Click(object sender, EventArgs e)
        {
            if (your_flags[3])
            {
                your_flags[3] = false;
                //picture_choose_4.Visible = false;
                choose_4.BackColor = Color.White;
            }
            else
            {
                your_flags[3] = true;
                //picture_choose_4.Visible = true;
                choose_4.BackColor = Color.Blue;
            }
        }

        private void your_cube_5_Click(object sender, EventArgs e)
        {
            if (your_flags[4])
            {
                your_flags[4] = false;
                //picture_choose_5.Visible = false;
                choose_5.BackColor = Color.White;
            }
            else
            {
                your_flags[4] = true;
                //picture_choose_5.Visible = true;
                choose_5.BackColor = Color.Blue;
            }
        }

        private void enemy_cube_1_Click(object sender, EventArgs e)
        {
            if (!PVE_mod)
            {
                if (enemy_flags[0])
                {
                    enemy_flags[0] = false;
                    //picture_choose_1.Visible = false;
                    enemy_choose_1.BackColor = Color.White;
                }
                else
                {
                    enemy_flags[0] = true;
                    //picture_choose_1.Visible = true;
                    enemy_choose_1.BackColor = Color.Red;
                }
            }

        }

        private void enemy_cube_2_Click(object sender, EventArgs e)
        {
            if (!PVE_mod)
            {
                if (enemy_flags[1])
                {
                    enemy_flags[1] = false;
                    //picture_choose_1.Visible = false;
                    enemy_choose_2.BackColor = Color.White;
                }
                else
                {
                    enemy_flags[1] = true;
                    //picture_choose_1.Visible = true;
                    enemy_choose_2.BackColor = Color.Red;
                }
            }

        }

        private void enemy_cube_3_Click(object sender, EventArgs e)
        {
            if (!PVE_mod)
            {
                if (enemy_flags[2])
                {
                    enemy_flags[2] = false;
                    //picture_choose_1.Visible = false;
                    enemy_choose_3.BackColor = Color.White;
                }
                else
                {
                    enemy_flags[2] = true;
                    //picture_choose_1.Visible = true;
                    enemy_choose_3.BackColor = Color.Red;
                }
            }

        }

        private void enemy_cube_4_Click(object sender, EventArgs e)
        {
            if (!PVE_mod)
            {
                if (enemy_flags[3])
                {
                    enemy_flags[3] = false;
                    //picture_choose_1.Visible = false;
                    enemy_choose_4.BackColor = Color.White;
                }
                else
                {
                    enemy_flags[3] = true;
                    //picture_choose_1.Visible = true;
                    enemy_choose_4.BackColor = Color.Red;
                }
            }

        }

        private void enemy_cube_5_Click(object sender, EventArgs e)
        {
            if (!PVE_mod)
            {
                if (enemy_flags[4])
                {
                    enemy_flags[4] = false;
                    //picture_choose_1.Visible = false;
                    enemy_choose_5.BackColor = Color.White;
                }
                else
                {
                    enemy_flags[4] = true;
                    //picture_choose_1.Visible = true;
                    enemy_choose_5.BackColor = Color.Red;
                }
            }


        }
    }
}