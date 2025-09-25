using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


var options = new ChromeOptions();

// Aponta para o executável do Brave
//options.BinaryLocation = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";

options.BinaryLocation = @"/usr/bin/brave-browser";

//options.AddArgument(@"user-data-dir=C:\Users\Administrador\AppData\Local\BraveSoftware\Brave-Browser\User Data");
options.AddArgument(@"--user-data-dir=/home/soldier/.config/BraveSoftware/Brave-Browser");
options.AddArgument("--remote-allow-origins=*");
options.AddArgument("--disable-extensions");
options.AddArgument("--disable-popup-blocking");
options.AddArgument("--start-maximized");


using var driver = new ChromeDriver(options);
driver.Navigate().GoToUrl("https://web.whatsapp.com/");

var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

Thread.Sleep(5000);

var searchBox = wait.Until(d => d.FindElement(By.XPath("//div[@contenteditable='true']")));
searchBox.SendKeys("Grupo para marcar o Gui");
Thread.Sleep(2000);
searchBox.SendKeys(Keys.Enter);

Random random = new Random();

DateTime horario = GerarHorarioFuturo(DateTime.Now, random);

Console.WriteLine($"Horário escolhido para hoje: {horario:HH:mm}");

while (true)
{

    for (int i = 0; i < 4; i++)
    {
        //var messageBox = wait.Until(d => d.FindElement(By.XPath("//*[@id=\"main\"]/footer//p[@contenteditable='true']")));
        var messageBox = wait.Until(d => d.FindElement(By.XPath("//*[@id=\"main\"]/footer/div[1]/div/span/div/div[2]/div/div[3]/div[1]/p")));
        messageBox.SendKeys("@Gui");
        Thread.Sleep(2000);
        messageBox.SendKeys(Keys.Enter);
        messageBox.SendKeys(Keys.Enter);
    }

    Console.WriteLine($"Mensagens enviadas com sucesso às {DateTime.Now:HH:mm}");

    while (DateTime.Now < horario)
    {
        var falta = horario - DateTime.Now;
        Console.WriteLine($"Faltam {falta.Hours:D2}:{falta.Minutes:D2}:{falta.Seconds:D2} para o envio...");
        Thread.Sleep(1000 * 60 * 10); // checa a cada 10 minutos
    }

    // Gera novo horário para o próximo dia
    horario = DateTime.Today.AddDays(1)
                            .AddHours(random.Next(8, 23))  // entre 8h e 22h59
                            .AddMinutes(random.Next(0, 60));

    Console.WriteLine($"Novo horário escolhido para amanhã: {horario:HH:mm}");
}

DateTime GerarHorarioFuturo(DateTime agora, Random rnd)
{
    DateTime tentativa = DateTime.Today
        .AddHours(rnd.Next(8, 23))
        .AddMinutes(rnd.Next(0, 60));

    // Se o horário sorteado for menor que o agora, pula para o próximo dia
    if (tentativa <= agora)
    {
        tentativa = tentativa.AddDays(1);
    }

    return tentativa;
}
