using System;
using System.Text.RegularExpressions;

public class CardChecker
{

    public string GetCreditCardVendor(string card_input)
    {
        string card = VerifyAndNormalizeInput(card_input);
        if (!LuhnCheckPasses(card_input)) return "Unknown"; // return Unknown if card number is not valid
        //check American Express
        if (card.Length==15&&(card.StartsWith("34") || card.StartsWith("37")))
        {
            return "American Express";
        }
        //check Visa
        if ((card.Length == 13 || card.Length == 16 || card.Length == 19) && card.StartsWith("4"))
        {
            return "Visa";
        }
        //check Maestro
        int firstDigits = int.Parse(card.Substring(0,2));
        if ((firstDigits == 50 || (firstDigits < 70 && firstDigits > 55)))
        {
            return "Maestro";
        }
        //check MasterCard
        if ((firstDigits > 50 && firstDigits < 56) && card.Length == 16)
        {
            return "MasterCard";
        }
        //check JCB
        firstDigits = int.Parse(card.Substring(0, 4));
        if (card.Length == 16 && (firstDigits > 3527 && firstDigits < 3590))
        {
            return "JCB";
        }

        return "Unknown"; // temp
    }

    public bool LuhnCheckPasses(string card_input)
    {
        string card = VerifyAndNormalizeInput(card_input);
        int cardNumberLength = card.Length;
        int currentDigit;
        int sumLuhn = 0;
        int digitIndex = 0;

        for (int i = cardNumberLength - 1; i >= 0; i--)
        {
            currentDigit = int.Parse(card.Substring(i, 1));

            if (digitIndex % 2 != 0)
            {
                if ((currentDigit *= 2) > 9)
                {
                    currentDigit -= 9;
                }
            }
            digitIndex++;

            sumLuhn += currentDigit;
        }

        return (sumLuhn % 10 == 0);
    }

    public bool IsCreditCardNumberValid(string card_input)
    {
        if (GetCreditCardVendor(card_input) != "Unknown")
        {
            return LuhnCheckPasses(card_input);
        }
        else
        {
            return false;
        }
    }

    public string GenerateNextCreditCardNumber(string card_input)
    {
        string card = VerifyAndNormalizeInput(card_input);
        string newCard = "";
        int IIN = int.Parse(card.Substring(0, 6));
        long PAN = long.Parse(card.Substring(6, card.Length - 7));
        if (IsCreditCardNumberValid(card))
        {
            PAN++;
        }
        newCard = IIN.ToString() + PAN.ToString();

        int cardNumberLength = newCard.Length;
        int currentDigit;
        int sumLuhn = 0;
        int digitIndex = 1;

        for (int i = cardNumberLength - 1; i >= 0; i--)
        {
            currentDigit = int.Parse(newCard.Substring(i, 1));

            if (digitIndex % 2 != 0)
            {
                if ((currentDigit *= 2) > 9)
                {
                    currentDigit -= 9;
                }
            }
            digitIndex++;

            sumLuhn += currentDigit;
        }

        return newCard + sumLuhn*9%10;
    }

    public string VerifyAndNormalizeInput(string inputstring)
    {
        string result = "";
        Regex rgx = new Regex("([0-9])");
        MatchCollection matches = rgx.Matches(inputstring);
        foreach (Match match in matches)
        {
            result += match.Value;
        }

        if (result.Length < 12 || result.Length>19)
        {
            return "";
        }
        else return result; 
    }

}
