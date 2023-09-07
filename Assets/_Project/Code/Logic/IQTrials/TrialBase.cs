
using UnityEngine;

namespace Code.Logic.IQTrials
{
    public enum TrialType
    {
        MathematicAdditive = 1,
        MathematicMultiplicative = 2,
        MathematicSubtractive = 3
    }
    
    public class TrialBase
    {
        private TrialType _trialType;
        public int a = 10;
        public int b = 20;
        public int trueAnswer;


        public void GetAnswer(TrialType trialType)
        {
            if (trialType is TrialType.MathematicAdditive)
            {
                
            }
            int answer = a + b;

            a = Random.Range(0, 20);
            b = Random.Range(0, 30);

            answer = a + b;
            
            int falseAnswer = a + b + b;
        }
    }
}
