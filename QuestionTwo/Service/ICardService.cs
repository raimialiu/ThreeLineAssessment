using QuestionTwo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuestionTwo.Service
{
    public interface ICardService
    {
        CardResponse VerifyCard(string CardPan, CardDTO cardDTO);
        CardHitCountResponse GetCardHitCount(int start=1, int limit=3);
    }

    public class CardService : ICardService
    {
        private Dictionary<string, int> HitCount = new Dictionary<string, int>();

        private Dictionary<string, CardDTO> _memoizer = new Dictionary<string, CardDTO>();
        public CardHitCountResponse GetCardHitCount(int start = 1, int limit = 3)
        {
            if(HitCount.Count == 0)
            {
                return new CardHitCountResponse()
                {
                    limit = 1,
                    start = start,
                    payload = null,
                    success = true,
                    size = HitCount.Count
                };
            }

            var index = (start - 1) * limit;
            var skippedData = HitCount.AsQueryable().Skip(index).Take(limit);

            var buildUpPayload = from c in HitCount
                                 select new
                                 {
                                     CardPan = c.Key,
                                     CardPanHitCount = c.Value
                                 };
            //StringBuilder bb = new StringBuilder();
            return new CardHitCountResponse()
            {
                success = true,
                payload = buildUpPayload,
                start = start,
                limit = limit
                
            };
            
        }

        private bool StringContainsThis(string CardPan, string input)
        {
            //string result = CardPan.FirstOrDefault(x => input.Any(y => y.ToString() == x.ToString())).ToString();
            foreach(char c in input)
            {
                string current = c.ToString();

                if(CardPan.Contains(current))
                {
                    return false;
                }
            }
            return true;
        }

        public CardResponse VerifyCard(string CardPan, CardDTO d)
        {
            bool validScheme = d.Scheme.ToUpper() == Scheme.AMERICANEXPRESS
                                || d.Scheme.ToUpper() == Scheme.AMEX ||
                                d.Scheme.ToUpper() == Scheme.MASTER ||
                                d.Scheme.ToUpper() == Scheme.VISA;

            if (!validScheme)
            {
                return new CardResponse()
                {
                    success = false,
                    message = "invalid card scheme"
                };
            }

            bool validType = d.type.ToUpper() == CardType.CREDIT
                            || d.type.ToUpper() == CardType.DEBIT
                            || d.type.ToUpper() == CardType.DOLLARCARD;

            if(!validType)
            {
                return new CardResponse()
                {
                    success = false,
                    message = "invalid card type"
                };
            }

            if (HitCount.ContainsKey(CardPan))
            {
                int before = 0;
                if(HitCount.TryGetValue(CardPan, out before))
                {
                    HitCount[CardPan] = before + 1;
                }
                else
                {
                    HitCount.Add(CardPan, before + 1);
                }
               ;
            }

            if (_memoizer.ContainsKey(CardPan))
            {
                return new CardResponse()
                {
                    success = true,
                    payload = _memoizer[CardPan],
                    message = "valid"
                };
            }

            string specCharacter = "~`@!#$%^&*()_+-=:;\'\"?/,<.>" ;
            string az = "abcdefghijklmnopqrstuvwxyz" ;
            string AZ = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" ;
            //if(CardPan.Contains())

            if(!StringContainsThis(CardPan, specCharacter) || !StringContainsThis(CardPan, az)
                    || !StringContainsThis(CardPan, AZ))
            {
                return new CardResponse()
                {
                    success = false,
                    message = "invalid card pan"
                };
            }
           

            if(!HitCount.ContainsKey(CardPan))
            {
                HitCount.Add(CardPan, 1);
            }

           

            Thread.Sleep(2000);
            _memoizer.Add(CardPan, d);
            return new CardResponse()
            {
                success = true,
                payload = d
            };
        }
    }

}
