using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionTwo.Model
{
    public class Scheme
    {
        public const string MASTER = "MASTERCARD";
        public const string VISA = "VISA";
        public const string AMEX = "AMEX";
        public const string AMERICANEXPRESS = "AMERICANEXPRESS";
    }
    public class CardType
    {
        public const string DEBIT = "DEBIT";
        public const string CREDIT = "CREDIT";
        public const string DOLLARCARD = "DOLLARCARD";
    }
    public class CardDTO
    {
        public string Scheme { get; set; }
        public string type { get; set; }
        public string bank { get; set; }
    }

    public class CardHitCountResponse
    {
        public bool success { get; set; }
        public int start { get; set; }
        public int size { get; set; }
        public int limit { get; set; }
        public object payload { get; set; }
    }

    public class CardResponse
    {
        public bool success { get; set; }
        public CardDTO payload { get; set; }
        public string message { get; set; }
    }
}
