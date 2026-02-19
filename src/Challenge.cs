#:property PublishAot=false

// DESAFIO: Sistema de Notificações Multi-Canal
// PROBLEMA: Uma aplicação de e-commerce precisa enviar notificações por diferentes canais
// (Email, SMS, Push, WhatsApp) dependendo da preferência do cliente e tipo de notificação
// O código atual viola o Open/Closed Principle ao usar condicionais para criar notificações

using System;

namespace DesignPatternChallenge
{
    public interface INotification
    {
        void Send(string recipient, string title, string message);
    }

    public abstract class NotificationCreator
    {
        protected abstract INotification CreateNotification();

        public void SendOrderConfirmation(string recipient, string orderNumber)
        {
            var notification = CreateNotification();
            notification.Send(recipient, "Confirmação de Pedido", $"Seu pedido {orderNumber} foi confirmado!");
        }

        public void SendShippingUpdate(string recipient, string trackingCode)
        {
            var notification = CreateNotification();
            notification.Send(recipient, "Pedido Enviado", $"Seu pedido foi enviado! Código de rastreamento: {trackingCode}");
        }

        public void SendPaymentReminder(string recipient, decimal amount)
        {
            var notification = CreateNotification();
            notification.Send(recipient, "Lembrete de Pagamento", $"Você tem um pagamento pendente de R$ {amount:N2}");
        }
    }

    public class EmailNotification : INotification
    {
        public void Send(string recipient, string title, string message)
        {
            Console.WriteLine($"📧 Enviando Email para {recipient}");
            Console.WriteLine($"   Assunto: {title}");
            Console.WriteLine($"   Mensagem: {message}");
        }
    }

    public class SmsNotification : INotification
    {
        public void Send(string recipient, string title, string message)
        {
            Console.WriteLine($"📱 Enviando SMS para {recipient}");
            Console.WriteLine($"   Mensagem: {message}");
        }
    }

    public class PushNotification : INotification
    {
        public void Send(string recipient, string title, string message)
        {
            Console.WriteLine($"🔔 Enviando Push para dispositivo {recipient}");
            Console.WriteLine($"   Título: {title}");
            Console.WriteLine($"   Mensagem: {message}");
        }
    }

    public class WhatsAppNotification : INotification
    {
        public void Send(string recipient, string title, string message)
        {
            Console.WriteLine($"💬 Enviando WhatsApp para {recipient}");
            Console.WriteLine($"   Título: {title}");
            Console.WriteLine($"   Mensagem: {message}");
            Console.WriteLine("   Template: True");
        }
    }

    public class EmailNotificationCreator : NotificationCreator
    {
        protected override INotification CreateNotification() => new EmailNotification();
    }

    public class SmsNotificationCreator : NotificationCreator
    {
        protected override INotification CreateNotification() => new SmsNotification();
    }

    public class PushNotificationCreator : NotificationCreator
    {
        protected override INotification CreateNotification() => new PushNotification();
    }

    public class WhatsAppNotificationCreator : NotificationCreator
    {
        protected override INotification CreateNotification() => new WhatsAppNotification();
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Notificações ===\n");

            NotificationCreator emailCreator = new EmailNotificationCreator();
            NotificationCreator smsCreator = new SmsNotificationCreator();
            NotificationCreator pushCreator = new PushNotificationCreator();
            NotificationCreator whatsAppCreator = new WhatsAppNotificationCreator();

            emailCreator.SendOrderConfirmation("cliente@email.com", "12345");
            Console.WriteLine();

            smsCreator.SendOrderConfirmation("+5511999999999", "12346");
            Console.WriteLine();

            pushCreator.SendShippingUpdate("device-token-abc123", "BR123456789");
            Console.WriteLine();

            whatsAppCreator.SendPaymentReminder("+5511888888888", 150.00m);
        }
    }
}
