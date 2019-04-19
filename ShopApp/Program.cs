using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ShopContext())
            {
                User user;
                while (true)
                {
                    Console.WriteLine("У вас есть аккаунт?(введите цифру)");
                    Console.WriteLine("1)Войти \n2)Зарегистрироваться");
                    Console.WriteLine("3)Выход");
                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        if (choice == 1)
                        {
                            user = Authorization(context);
                            break;
                        }
                        else if (choice == 2)
                        {
                            user = Registration(context);
                            break;
                        }
                        else if (choice == 3)
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод");
                    }
                }
                while (true)
                {
                    Console.WriteLine("1)Добавить продукт в корзину");
                    Console.WriteLine("2)Просмотреть корзину");
                    Console.WriteLine("3)Выход");
                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        if (choice == 1)
                        {
                            int index = 1;
                            foreach (var product in context.Products)
                            {
                                Console.WriteLine($"{index}){product.Name} - {product.Cost}");
                                index++;
                            }
                            while (true)
                            {
                                Console.WriteLine("Введите номер товара");
                                if (int.TryParse(Console.ReadLine(), out int result))
                                {
                                    if (result > 0 && result < context.Products.Count())
                                    {
                                        user.Basket.Products.Add(context.Products.ElementAt(result));
                                        context.Products.ElementAt(result).Baskets.Add(user.Basket);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Товара с таким номером нет");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Некорректный ввод");
                                }
                            }
                        }
                        else if (choice == 2)
                        {
                            if (user.Basket.Products.Count > 0)
                            {
                                foreach (var product in user.Basket.Products)
                                {
                                    Console.WriteLine(product.Name + " - " + product.Cost);
                                }
                                Console.WriteLine("Нажмите Enter для продолжения");
                            }
                            else
                            {
                                Console.WriteLine("Корзина пуста");
                                Console.WriteLine("Нажмите Enter для продолжения");
                            }
                        }
                        else if (choice == 3)
                        {
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ввод");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод");
                    }
                }
                context.SaveChanges();
            }
        }

        private static User Registration(ShopContext context)
        {
            string login;
            string password;

            while (true)
            {
                Console.WriteLine("Введите логин(минимум 4 символа, максимум 20)");
                login = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(login) || login.Length < 4 || login.Length > 20)
                {
                    Console.WriteLine("Некорректный ввод");
                }
                else { break; }
            }
            while (true)
            {
                Console.WriteLine("Введите пароль(минимум 6 символов)");
                password = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                {
                    Console.WriteLine("Некорректный ввод");
                }
                else { break; }
            }
            User user = new User {
                Login = login,
                Password = password
            };
            context.Users.Add(user);
            context.Baskets.Add(user.Basket);
            context.SaveChanges();
            Console.WriteLine("Вы успешно зарегистрировались!");
            return user;
        }

        public static User Authorization(ShopContext context)
        {
            string login;
            string password;

            while (true) {
                while (true)
                {
                    Console.WriteLine("Введите логин");
                    login = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(login))
                    {
                        Console.WriteLine("Некорректный ввод");
                    }
                    else { break; }
                }
                while (true)
                {
                    Console.WriteLine("Введите пароль");
                    password = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(password))
                    {
                        Console.WriteLine("Некорректный ввод");
                    }
                    else { break; }
                }
                foreach (var user in context.Users)
                {
                    if (user.Login == login && user.Password == password)
                    {
                        Console.WriteLine("Вы успешно вошли в систему");
                        return user;
                    }
                }
                Console.WriteLine("Пользователь не найден");
            }
        }
    }
}
