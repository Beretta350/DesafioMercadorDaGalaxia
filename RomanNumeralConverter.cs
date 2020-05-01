using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DesafioSelecaoMercadorGalaxia
{   
    /// <summary>
    /// Classe responsavel por todo tipo de manipulação com os numeros romanos.
    /// </summary>
    class RomanNumeralConverter
    {   
        private Dictionary<char, Int32> numeralDictionary = new Dictionary<char, Int32>()
        {
                {'I', 1},
                {'V', 5},
                {'X', 10},
                {'L', 50},
                {'C', 100},
                {'D', 500},
                {'M', 1000}
        };

        /// <summary>
        /// Metodo que efetua a conversão de um algarismo romano para um Decimal
        /// </summary>
        /// <param name="romanNumber"> String com o numero romano a ser convertido para decimal </param>
        /// <returns> Um numero decimal que corresponde ao numero romano passado como parametro </returns>
        public Decimal RomanToDecimal(String romanNumber)
        {       
                List<Int32> values = new List<Int32>();
                
                String romanNum = romanNumber.ToUpper().Trim();

                char lastRomanNumber = ' ';

                Decimal total = 0;
                Int32 maxValue = 1000;

                Int16 arrPointer = 0;
                Int16 repeatCounter = 1;
            
                /* Verfica se existe repetição, conforme a regra ('D', 'L' e 'V' nunca podem ser repetidos.).
                 * maior que dois pois o retorno quando a string é unicamente o algarismo o split retorna tamanho 2. */
                if (romanNum.Split('V').Length > 2 || romanNum.Split('L').Length > 2 || romanNum.Split('D').Length > 2)
                {
                    throw new Exception(String.Format("Repetição invalida de algarismos romanos. Valor a ser convertido: {0}", romanNumber));
                }

                for (int i = 0; i < romanNum.Length; i++)
                {   
                    // Verifica se existem caracteres invalidos.
                    if (!"IVXLCDM".Contains(romanNum[i]))
                    {
                        throw new Exception("Algarismo romano inexistente.");
                    }

                    /* Verifica se os caracteres são repetidos mais de 3 vezes, e se existe repetição 
                     * de algarismos para subtração, para evitarmos casos como IIV ou XXL. */
                    if (romanNum[i].Equals(lastRomanNumber))
                    {
                        repeatCounter += 1;
                        //Verificação dos casos de mais de 3 algarismos repetidos.
                        if (repeatCounter == 4)
                        {
                            throw new Exception(String.Format("Repetição execiva de algarismos. Mais de 3 algarismos consecutivos. Valor a ser convertido: {0}", romanNumber));
                        }
                    }
                    else
                    {
                        repeatCounter = 1;
                        lastRomanNumber = romanNum[i];
                    }
                }

                while (arrPointer < romanNum.Length)
                {
                    char romanDigit = romanNum[arrPointer];
                    char nextRomanDigit;

                    Int32 intDigit = this.numeralDictionary.GetValueOrDefault(romanDigit);
                    Int32 nextIntDigit;


                    // Vericara problema de um valor maior que o valor maximo possivel.
                    if (intDigit > maxValue)
                    {
                        throw new Exception(String.Format("Algarismo em posição inválida, devido valor maximo possivel. Valor a ser convertido: {0}", romanNumber));
                    }


                    /*  
                     *  Problema da subtração dos valores quando colocado digitos menores anteriormente, os digitos que 
                     *  irão subtrair devem ser pelo menos um décimo do valor do maior digito e são obrigatoriamente I, 
                     *  X ou C.
                    */
                    if (arrPointer < (romanNum.Length - 1))
                    {
                        nextRomanDigit = romanNum[arrPointer + 1];
                        nextIntDigit = this.numeralDictionary.GetValueOrDefault(nextRomanDigit);

                        if (nextIntDigit > intDigit)
                        {
                            if ((!"IXC".Contains(romanDigit)) || nextIntDigit > (intDigit * 10) || romanNum.Split(romanDigit).Length > 3)
                            {
                                throw new Exception(String.Format("Algarismo em posição inválida. Valor a ser convertido: {0}", romanNumber));
                            }

                            maxValue = intDigit - 1;
                            intDigit = nextIntDigit - intDigit;
                            arrPointer++;
                        }

                    }

                    values.Add(intDigit);

                    arrPointer++;
                }

                /*  Aqui comparamos o valor dos algarismos da esquerda para direita verificando 
                 *  se esta na ordem decrescente, respeitando a regra dos algarismos subtrativos, 
                 *  ou seja, aceitando XIX, mas recusando casos XIM e IIV.
                 */
                for (int j = 0; j <= values.Count - 2; j++)
                {
                    if (values[j] < values[j + 1])
                    {
                        throw new Exception(String.Format("Algarismo romano inválido. Neste caso o algarismo não pode ser maior que o anterior, pois nao se aplica a regra de subtração.\nValor a ser convertido: {0}", romanNumber));
                    }
                }

                foreach (Int32 digit in values)
                {
                    total += Convert.ToDecimal(digit);
                }

                return total;
        }

        /// <summary>
        /// Metodo para verificar se o caractere passado como parametro é um numero romano.
        /// </summary>
        /// <param name="romanNumeral"> Caractere a ser analisado </param>
        /// <returns> Booleano indicando se o caractere é um numero romano ou não </returns>
        public Boolean IsARomanNumeral(char romanNumeral)
        {   
            return this.numeralDictionary.ContainsKey(romanNumeral);
        }

        /// <summary>
        /// Metodo para verificar se a string passada como parametro é um numero romano.
        /// </summary>
        /// <param name="romanNumeral"> String a ser analisado </param>
        /// <returns> Booleano indicando se a string é um numero romano ou não </returns>
        public Boolean IsARomanNumeral(String romanNumeral)
        {   
            try
            {
                char key;
                Decimal value = 0;

                if (romanNumeral.Length > 1)
                {
                    value = RomanToDecimal(romanNumeral.ToUpper());
                    return true;
                }
                else
                {
                    if (Char.TryParse(romanNumeral.ToUpper(), out key))
                    {
                        return IsARomanNumeral(key);
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch(Exception)
            {
                return false;
            }
            
        }

    }


}
