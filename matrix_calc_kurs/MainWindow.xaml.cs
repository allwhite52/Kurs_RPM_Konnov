using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace matrix_calc_kurs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Width += 20;
            Height += 20;
        }

        public class MatrixRow
        {
            public List<double?> Values { get; set; } // Используем nullable, чтобы значения могли быть null

            public MatrixRow(int columnCount)
            {
                Values = new List<double?>(new double?[columnCount]); // Инициализируем пустые значения
            }
        }

        private bool IsMatrixValid(List<MatrixRow> matrix)
        {
            // Проверяем, есть ли хотя бы одна строка и все значения не null
            if (matrix == null || matrix.Count == 0)
                return false;

            foreach (var row in matrix)
            {
                foreach (var value in row.Values)
                {
                    if (!value.HasValue) // Если значение null, возвращаем false
                        return false;
                }
            }
            return true; // Все значения корректные
        }

        private void info_Click(object sender, RoutedEventArgs e)
        {
            string message = "Курсовой проект по модулю МДК 01.01 РПМ\n" +
                     "Разработчик: Коннов Вадим Сергеевич\n" +
                     "Группа: ИСП-44\n" +
                     "Тема: Калькулятор матриц\n" +
                     "Преподаватель: Мачнева Екатерина Александровна";

            MessageBox.Show(message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void create1_Click(object sender, RoutedEventArgs e)
        {
            dg1.Columns.Clear();
            if (int.TryParse(rows1.Text, out int rows) && int.TryParse(columns1.Text, out int columns))
            {
                var matrix = new List<MatrixRow>();

                for (int i = 0; i < rows; i++)
                {
                    matrix.Add(new MatrixRow(columns));
                }

                dg1.ItemsSource = matrix;
                dg1.Visibility = Visibility.Visible;

                // Создаем колонки динамически
                for (int j = 0; j < columns; j++)
                {
                    DataGridTextColumn column = new DataGridTextColumn();
                    column.Header = $"Column {j + 1}";
                    column.Binding = new Binding($"Values[{j}]");
                    dg1.Columns.Add(column);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректные числа для строк и столбцов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void create2_Click(object sender, RoutedEventArgs e)
        {
            dg2.Columns.Clear();
            if (int.TryParse(rows2.Text, out int rows) && int.TryParse(columns2.Text, out int columns))
            {
                var secondMatrix = new List<MatrixRow>();

                for (int i = 0; i < rows; i++)
                {
                    secondMatrix.Add(new MatrixRow(columns));
                }

                dg2.ItemsSource = secondMatrix;
                dg2.Visibility = Visibility.Visible;

                // Создаем колонки динамически
                dg2.Columns.Clear(); // Очищаем предыдущие колонки
                for (int j = 0; j < columns; j++)
                {
                    DataGridTextColumn column = new DataGridTextColumn();
                    column.Header = $"Column {j + 1}";
                    column.Binding = new Binding($"Values[{j}]");
                    dg2.Columns.Add(column);
                }

            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректные числа для строк и столбцов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void transposition1_Click(object sender, RoutedEventArgs e)
        {
            if (dg1.ItemsSource is List<MatrixRow> matrix && matrix.Count > 0 && IsMatrixValid(matrix))
            {
                int rows = matrix.Count;
                int columns = matrix[0].Values.Count;

                // Создаем новую матрицу для транспонирования
                var transposedMatrix = new List<MatrixRow>();

                for (int j = 0; j < columns; j++)
                {
                    var newRow = new MatrixRow(rows);
                    for (int i = 0; i < rows; i++)
                    {
                        newRow.Values[i] = matrix[i].Values[j]; // Транспонируем
                    }
                    transposedMatrix.Add(newRow);
                }

                // Обновляем DataGrid с транспонированной матрицей
                dg3.ItemsSource = transposedMatrix;
                dg3.Visibility = Visibility.Visible;

                // Создаем колонки динамически для новой матрицы
                dg3.Columns.Clear(); // Очищаем предыдущие колонки
                for (int i = 0; i < rows; i++)
                {
                    DataGridTextColumn column = new DataGridTextColumn();
                    column.Header = $"Column {i + 1}";
                    column.Binding = new Binding($"Values[{i}]");
                    dg3.Columns.Add(column);
                }
            }
            else
            {
                MessageBox.Show("Сначала создайте матрицу или убедитесь, что в ней есть корректные значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void transposition2_Click(object sender, RoutedEventArgs e)
        {
            if (dg2.ItemsSource is List<MatrixRow> matrix && matrix.Count > 0 && IsMatrixValid(matrix))
            {
                int rows = matrix.Count;
                int columns = matrix[0].Values.Count;

                // Создаем новую матрицу для транспонирования
                var transposedMatrix = new List<MatrixRow>();

                for (int j = 0; j < columns; j++)
                {
                    var newRow = new MatrixRow(rows);
                    for (int i = 0; i < rows; i++)
                    {
                        newRow.Values[i] = matrix[i].Values[j]; // Транспонируем
                    }
                    transposedMatrix.Add(newRow);
                }

                // Обновляем DataGrid с транспонированной матрицей
                dg3.ItemsSource = transposedMatrix;
                dg3.Visibility = Visibility.Visible;

                // Создаем колонки динамически для новой матрицы
                dg3.Columns.Clear(); // Очищаем предыдущие колонки
                for (int i = 0; i < rows; i++)
                {
                    DataGridTextColumn column = new DataGridTextColumn();
                    column.Header = $"Column {i + 1}";
                    column.Binding = new Binding($"Values[{i}]");
                    dg3.Columns.Add(column);
                }
            }
            else
            {
                MessageBox.Show("Сначала создайте матрицу или убедитесь, что в ней есть корректные значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_multiply_number1_Click(object sender, RoutedEventArgs e)
        {
            if (dg1.ItemsSource is List<MatrixRow> matrix && matrix.Count > 0 && IsMatrixValid(matrix))
            {
                if (double.TryParse(multiply_number1.Text, out double multiplier))
                {
                    var resultMatrix = new List<MatrixRow>();

                    foreach (var row in matrix)
                    {
                        var newRow = new MatrixRow(row.Values.Count);
                        for (int j = 0; j < row.Values.Count; j++)
                        {
                            if (row.Values[j].HasValue) // Проверяем, есть ли значение
                            {
                                newRow.Values[j] = row.Values[j] * multiplier; // Умножаем на число
                            }
                        }
                        resultMatrix.Add(newRow);
                    }

                    dg3.ItemsSource = resultMatrix;
                    dg3.Visibility = Visibility.Visible;

                    // Обновляем колонки
                    dg3.Columns.Clear();
                    for (int i = 0; i < resultMatrix[0].Values.Count; i++)
                    {
                        DataGridTextColumn column = new DataGridTextColumn();
                        column.Header = $"Column {i + 1}";
                        column.Binding = new Binding($"Values[{i}]");
                        dg3.Columns.Add(column);
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Сначала создайте матрицу или убедитесь, что в ней есть корректные значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_multiply_number2_Click(object sender, RoutedEventArgs e)
        {
            if (dg2.ItemsSource is List<MatrixRow> matrix && matrix.Count > 0 && IsMatrixValid(matrix))
            {
                if (double.TryParse(multiply_number2.Text, out double multiplier))
                {
                    var resultMatrix = new List<MatrixRow>();

                    foreach (var row in matrix)
                    {
                        var newRow = new MatrixRow(row.Values.Count);
                        for (int j = 0; j < row.Values.Count; j++)
                        {
                            if (row.Values[j].HasValue) // Проверяем, есть ли значение
                            {
                                newRow.Values[j] = row.Values[j] * multiplier; // Умножаем на число
                            }
                        }
                        resultMatrix.Add(newRow);
                    }

                    dg3.ItemsSource = resultMatrix;
                    dg3.Visibility = Visibility.Visible;

                    // Обновляем колонки
                    dg3.Columns.Clear();
                    for (int i = 0; i < resultMatrix[0].Values.Count; i++)
                    {
                        DataGridTextColumn column = new DataGridTextColumn();
                        column.Header = $"Column {i + 1}";
                        column.Binding = new Binding($"Values[{i}]");
                        dg3.Columns.Add(column);
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректное число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Сначала создайте матрицу или убедитесь, что в ней есть корректные значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_rank1_Click(object sender, RoutedEventArgs e)
        {
            if (dg1.ItemsSource is List<MatrixRow> matrix && matrix.Count > 0 && IsMatrixValid(matrix))
            {
                // Создаем двумерный массив для матрицы
                int rows = matrix.Count;
                int cols = matrix[0].Values.Count;
                double[,] matrixArray = new double[rows, cols];

                // Заполняем массив значениями из DataGrid
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (matrix[i].Values[j].HasValue) // Проверяем, есть ли значение
                        {
                            matrixArray[i, j] = matrix[i].Values[j].Value;
                        }
                        else
                        {
                            matrixArray[i, j] = 0; // Присваиваем 0, если значение null
                        }
                    }
                }

                // Используем Math.NET для вычисления ранга
                var denseMatrix = DenseMatrix.OfArray(matrixArray);
                int rank = denseMatrix.Rank();

                // Отображаем результат в соответствующем TextBox
                rank_matrix1.Text = rank.ToString();
            }
            else
            {
                MessageBox.Show("Сначала создайте матрицу или убедитесь, что в ней есть корректные значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_rank2_Click(object sender, RoutedEventArgs e)
        {
            if (dg2.ItemsSource is List<MatrixRow> matrix && matrix.Count > 0 && IsMatrixValid(matrix))
            {
                // Создаем двумерный массив для матрицы
                int rows = matrix.Count;
                int cols = matrix[0].Values.Count;
                double[,] matrixArray = new double[rows, cols];

                // Заполняем массив значениями из DataGrid
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (matrix[i].Values[j].HasValue) // Проверяем, есть ли значение
                        {
                            matrixArray[i, j] = matrix[i].Values[j].Value;
                        }
                        else
                        {
                            matrixArray[i, j] = 0; // Присваиваем 0, если значение null
                        }
                    }
                }

                // Используем Math.NET для вычисления ранга
                var denseMatrix = DenseMatrix.OfArray(matrixArray);
                int rank = denseMatrix.Rank();

                // Отображаем результат в соответствующем TextBox
                rank_matrix2.Text = rank.ToString();
            }
            else
            {
                MessageBox.Show("Сначала создайте матрицу или убедитесь, что в ней есть корректные значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void sum_matrix_Click(object sender, RoutedEventArgs e)
        {
            if (dg1.ItemsSource is List<MatrixRow> matrix1 && matrix1.Count > 0 && IsMatrixValid(matrix1) &&
                dg2.ItemsSource is List<MatrixRow> matrix2 && matrix2.Count > 0 && IsMatrixValid(matrix2))
            {
                // Проверяем, что матрицы имеют одинаковые размеры
                if (matrix1.Count != matrix2.Count || matrix1[0].Values.Count != matrix2[0].Values.Count)
                {
                    MessageBox.Show("Размеры матриц должны совпадать для сложения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создаем новую матрицу для результата
                var resultMatrix = new List<MatrixRow>();

                for (int i = 0; i < matrix1.Count; i++)
                {
                    var newRow = new MatrixRow(matrix1[i].Values.Count);
                    for (int j = 0; j < matrix1[i].Values.Count; j++)
                    {
                        // Суммируем соответствующие элементы
                        newRow.Values[j] = (matrix1[i].Values[j] ?? 0) + (matrix2[i].Values[j] ?? 0);
                    }
                    resultMatrix.Add(newRow);
                }

                // Обновляем DataGrid для отображения результата
                dg3.ItemsSource = resultMatrix;
                dg3.Visibility = Visibility.Visible;

                // Обновляем колонки
                for (int i = 0; i < resultMatrix[0].Values.Count; i++)
                {
                    dg3.Columns.Add(new DataGridTextColumn
                    {
                        Header = $"Column {i + 1}",
                        Binding = new Binding($"Values[{i}]")
                    });
                }
            }
            else
            {
                MessageBox.Show("Сначала создайте обе матрицы или убедитесь, что в них есть корректные значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void subtraction_matrix_Click(object sender, RoutedEventArgs e)
        {
            if (dg1.ItemsSource is List<MatrixRow> matrix1 && matrix1.Count > 0 && IsMatrixValid(matrix1) &&
                dg2.ItemsSource is List<MatrixRow> matrix2 && matrix2.Count > 0 && IsMatrixValid(matrix2))
            {
                // Проверяем, что матрицы имеют одинаковые размеры
                if (matrix1.Count != matrix2.Count || matrix1[0].Values.Count != matrix2[0].Values.Count)
                {
                    MessageBox.Show("Размеры матриц должны совпадать для вычитания.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создаем новую матрицу для результата
                var resultMatrix = new List<MatrixRow>();

                for (int i = 0; i < matrix1.Count; i++)
                {
                    var newRow = new MatrixRow(matrix1[i].Values.Count);
                    for (int j = 0; j < matrix1[i].Values.Count; j++)
                    {
                        // Вычитаем соответствующие элементы
                        double value1 = matrix1[i].Values[j].HasValue ? matrix1[i].Values[j].Value : 0;
                        double value2 = matrix2[i].Values[j].HasValue ? matrix2[i].Values[j].Value : 0;
                        newRow.Values[j] = value1 - value2;
                    }
                    resultMatrix.Add(newRow);
                }

                // Обновляем DataGrid для отображения результата
                dg3.ItemsSource = resultMatrix;
                dg3.Visibility = Visibility.Visible;

                // Обновляем колонки
                dg3.Columns.Clear();
                for (int i = 0; i < resultMatrix[0].Values.Count; i++)
                {
                    DataGridTextColumn column = new DataGridTextColumn();
                    column.Header = $"Column {i + 1}";
                    column.Binding = new Binding($"Values[{i}]");
                    dg3.Columns.Add(column);
                }
            }
            else
            {
                MessageBox.Show("Сначала создайте обе матрицы или убедитесь, что в них есть корректные значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void multiply_matrix_Click(object sender, RoutedEventArgs e)
        {
            if (dg1.ItemsSource is List<MatrixRow> matrix1 && matrix1.Count > 0 && IsMatrixValid(matrix1) &&
                dg2.ItemsSource is List<MatrixRow> matrix2 && matrix2.Count > 0 && IsMatrixValid(matrix2))
            {
                // Проверяем, что количество столбцов первой матрицы совпадает с количеством строк второй
                if (matrix1[0].Values.Count != matrix2.Count)
                {
                    MessageBox.Show("Количество столбцов первой матрицы должно совпадать с количеством строк второй матрицы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создаем новую матрицу для результата
                var resultMatrix = new List<MatrixRow>();

                // Размеры матриц
                int rows1 = matrix1.Count;
                int cols1 = matrix1[0].Values.Count;
                int cols2 = matrix2[0].Values.Count;

                // Инициализация результирующей матрицы
                for (int i = 0; i < rows1; i++)
                {
                    var newRow = new MatrixRow(cols2);
                    resultMatrix.Add(newRow);
                }

                // Умножение матриц
                for (int i = 0; i < rows1; i++)
                {
                    for (int j = 0; j < cols2; j++)
                    {
                        double sum = 0;

                        for (int k = 0; k < cols1; k++)
                        {
                            double value1 = matrix1[i].Values[k].HasValue ? matrix1[i].Values[k].Value : 0;
                            double value2 = matrix2[k].Values[j].HasValue ? matrix2[k].Values[j].Value : 0;
                            sum += value1 * value2;
                        }

                        resultMatrix[i].Values[j] = sum;
                    }
                }

                // Обновляем DataGrid для отображения результата
                dg3.ItemsSource = resultMatrix;
                dg3.Visibility = Visibility.Visible;

                // Обновляем колонки
                dg3.Columns.Clear();
                for (int i = 0; i < resultMatrix[0].Values.Count; i++)
                {
                    DataGridTextColumn column = new DataGridTextColumn();
                    column.Header = $"Column {i + 1}";
                    column.Binding = new Binding($"Values[{i}]");
                    dg3.Columns.Add(column);
                }
            }
            else
            {
                MessageBox.Show("Сначала создайте обе матрицы или убедитесь, что в них есть корректные значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            dg1.ItemsSource = null;
            dg2.ItemsSource = null;
            dg3.ItemsSource = null;

            // Скрываем DataGrid
            dg1.Visibility = Visibility.Hidden;
            dg2.Visibility = Visibility.Hidden;
            dg3.Visibility = Visibility.Hidden;

            // Очищаем текстовые поля
            rows1.Clear();
            columns1.Clear();
            rows2.Clear();
            columns2.Clear();
            multiply_number1.Clear();
            multiply_number2.Clear();
            rank_matrix1.Clear();
            rank_matrix2.Clear();
        }
    }
}