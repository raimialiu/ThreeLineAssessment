using QuestionTwo.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThreeLineAssessment.Test
{
    public class BaseTestClass
    {
        protected ICardService _sut;
        public BaseTestClass()
        {
            _sut = new CardService();
        }
    }
}
