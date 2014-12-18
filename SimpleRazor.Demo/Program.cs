using System;
using System.Linq;

namespace SimpleRazor.Demo
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var anon = new
            {
                Message = "World"
            };

            var person = new Person
            {
                Name = "Joe",
                Message = "Hello World",
                Inner = new
                {
                    Name = "Inner"
                }.ToExpando()
            };

            var template =
                @"<html>
    <head>
        <title>From @Model.Name (@Model.Inner.Name)</title>
    </head>
    <body>
        Message: <em>@Model.Message</em>
    </body>
</html>";

            var complex = new
            {
                Test = new
                {
                    Names = new[]
                    {
                        "Test 1",
                        "Test 2"
                    },
                    Join = new Func<string[], string>(items =>
                    {
                        return items.Aggregate((x, y) => string.Format("{0}, {1}", x, y));
                    })
                }
            };

            Console.WriteLine(Razor.Render("Hello @Model!", "World"));
            Console.WriteLine(Razor.Render("Hello @Model.Message!", anon));
            Console.WriteLine(Razor.Render("@Model.SayHello()!", person));
            Console.WriteLine(Razor.Render(template, person));
            Console.WriteLine(Razor.Render("@string.Join(\", \", Model.Test.Names)!", complex));
            Console.WriteLine(Razor.Render("@Model.Test.Join(Model.Test.Names)!", complex));

            Console.ReadKey();
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public dynamic Inner { get; set; }

        public string SayHello()
        {
            return string.Format("{0} says hello", this.Name);
        }
    }
}
