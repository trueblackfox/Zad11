using System;

namespace UCH_PRCT_11
{

    public class Program
    {
        static void Main(string[] args)
        {
            // Ввод кол-ва элементов в шифрующем наборе
            int k;
            k = CheckInt("Введите k");
            // Создание массива, который хранит шифрующий набор
            int[] transp = new int[k];
            transp = CreateTransposition(k, transp);
            // Вывод шифрующего набора
            Console.Write("Шифр: ");
            for (int i = 0; i < transp.Length; i++)
            {
                Console.Write(transp[i] + " ");
            }
            Console.WriteLine();
            // Считывание текста, введённого пользователем
            Console.WriteLine("Введите текст для шифрования");
            char[] symbols = (Console.ReadLine()).ToCharArray();
            Console.WriteLine();
            // Генерация и вывод зашифрованного текста
            Console.WriteLine("а) Зашифрованный текст:");
            symbols = GetCipheredText(k, transp, symbols);
            for (int i = 0; i < symbols.Length; i++)
            {
                Console.Write(symbols[i]);
            }
            Console.WriteLine();
            Console.WriteLine();
            // Расшифровка и вывод текста
            Console.WriteLine("б) Расшифрованный текст:");
            symbols = GetRecipheredText(k, transp, symbols);
            for (int i = 0; i < symbols.Length; i++)
            {
                Console.Write(symbols[i]);
            }
            Console.WriteLine();
        }
        // Проверка ввода целого числа
        public static int CheckInt(string s)
        {
            // Проверка ввода
            Console.WriteLine(s);
            bool ok; int n;
            do
            {
                ok = int.TryParse(Console.ReadLine(), out n);
                if (!ok || n <= 0) Console.WriteLine("Необходимо ввести натуральное число");
            } while (!ok || n <= 0);
            return n;
        }
        // Создание шифрующего набора - порядка перестановки символов
        public static int[] CreateTransposition(int k, int[] transp)
        {
            Random rnd = new Random();
            int tmp = 0;
            // Пока количество чисел в шифрующем наборе меньше заданного
            for (int i = 0; i < k; i++)
            {
                // Генерация случайного числа от 1 до k включительно
                transp[i] = rnd.Next(1, k + 1);
                // Задать переменной tmp то же значение
                tmp = transp[i];
                // Перебор всех предыдущих элементов при их наличии
                for (int j = 0; j < i; j++)
                {
                    // Пока элемент совпадает с одним из предыдущих
                    while (transp[i] == transp[j])
                    {
                        // Замена текущего элемента на случайное число от 1 до k включительно
                        transp[i] = rnd.Next(1, k + 1);
                        // Обновление значений j и tmp
                        // j = 0, чтобы повторно сравнить со всеми предыдущими элементами
                        j = 0;
                        tmp = transp[i];
                    }
                    tmp = transp[i];
                }
            }
            // Возвращение массива с заданным порядком перестановки k символов
            return transp;
        }
        // Шифрование текста
        public static char[] GetCipheredText(int k, int[] transp, char[] symbols)
        {
            // Символьный массив для хранения символов из одного из рассматриваемовых наборов по k символов
            char[] tmp = new char[k];
            // Длина введённого текста
            int length = symbols.Length;
            // Если количество символов не делится нацело на k
            // То есть количество наборов по k символов не будет целым (последний набор будет заполнен не до конца)
            if (length % k != 0)
            {
                // Создание вспомогательного символьного массива такого размера, чтобы теперь длина делилась нацело на k (все наборы были заполнены полностью)
                char[] newSymbols = new char[symbols.Length + (k - (length % k))];
                // Заполнение вспомогательного массива введённым текстом
                symbols.CopyTo(newSymbols, 0);
                // Увеличение размера исходного массива, содержащего введённый текст
                symbols = new char[newSymbols.Length];
                // Заполнение исходного массива элементами, внесёнными во вспомогательный массив (= введённый текст)
                newSymbols.CopyTo(symbols, 0); //выделение памяти под недостающие элементы
                for (int i = length; i < symbols.Length; i++)
                {
                    // Заполнение пустых элементов пробелами
                    symbols[i] = ' ';
                }
            }
            // Количество наборов по k символов в исходном тексте
            int count = symbols.Length / k;
            for (int i = 0; i < count; i++)
            {
                // За каждый проход
                for (int j = 0; j < k; j++)
                {
                    // Заполнение массива tmp символами из введенной строки
                    tmp[j] = symbols[j + k * i];
                }

                for (int j = 0; j < k; j++)
                {
                    // Перестановка элементов в рассматриваемом наборе элементами (используя массив tmp) в нужном порядке (соответственно порядку из массива transp)
                    symbols[j + k * i] = tmp[transp[j] - 1];
                }
            }
            // Возвращение зашифрованного текста
            return symbols;
        }
        // Расшифровка текста
        public static char[] GetRecipheredText(int k, int[] transp, char[] symbols)
        {
            // Символьный массив для хранения символов из одного из рассматриваемовых наборов по k символов
            char[] tmp = new char[k];
            // Количество наборов по k символов в зашифрованном тексте
            // Зашифрованный текст уже дополнен до такого размера, чтобы все наборы были полными
            int count = symbols.Length / k;
            for (int i = 0; i < count; i++)
            {
                // За каждый проход
                for (int j = 0; j < k; j++)
                {
                    // Заполнение массива tmp символами из введенной строки
                    tmp[j] = symbols[j + k * i];
                }

                for (int j = 0; j < k; j++)
                {
                    // Перестановка символов внутри набора в соответствии с порядком, хранящимся в transp
                    symbols[j + k * i] = tmp[Array.IndexOf(transp, (j + 1))];
                }
            }
            return symbols;
        }
    }
}