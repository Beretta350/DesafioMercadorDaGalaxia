using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DesafioSelecaoMercadorGalaxia
{   
    /// <summary>
    /// Classe responsavel por toda a tradução do arquivo de texto.
    /// </summary>
    class FileTranslator
    {   
        private String fileName;
        private Dictionary<String, String> measureUnitDictionary;
        private Dictionary<String, Double> itemCreditDictionary;

        public FileTranslator(String fileName){
            this.fileName = fileName;
        }

        /// <summary>
        /// Traduz todo o arquivo e retorna as respostas.
        /// </summary>
        /// <returns> Retorna as questões do arquivo respondidas </returns>
        public String Translate()
        {
            try
            {   
                //Le todo o arquivo para posteriormente separar as partes que desejadas. 
                String fileText = System.IO.File.ReadAllText(fileName);
                String answers;

                #region Validações Iniciais

                //Valida se existem linhas de setar, pegando todas as linhas em que aparece a string "is".
                if (!(Regex.Match(fileText.Trim(), @".*\bis\b.*").Success))
                {
                    throw new Exception("Linhas para setar valores não foram adicionadas.");
                }

                //Valida se existem linhas de perguntas, pegando todas as linhas em que aparecem as strings "vale" e "são".
                if (!Regex.Match(fileText.Trim(), @".*\bvale\b.*").Success
                    && !Regex.Match(fileText.Trim(), @".*\bsão\b.*").Success)
                {
                    throw new Exception("Não existem linhas de perguntas.");
                }

                #endregion

                SetDictionaries(fileText);
                answers = AnswerTheQuestions(fileText);

                return answers;
            }
            catch(Exception err)
            {
                return err.Message;
            }
        }

        /// <summary>
        /// Trata com toda a parte das questões que são feitas no arquivo, respondendo-as.
        /// </summary>
        /// <param name="fileText"> String contendo os dados do arquivo de texto </param>
        /// <returns> String contendo todas as respostas para as questões de forma já estruturada </returns>
        private String AnswerTheQuestions(String fileText) 
        {   
            MatchCollection matchLines = Regex.Matches(fileText, @"quantos? (créditos são|vale)? .* ?");
            RomanNumeralConverter romanConverter = new RomanNumeralConverter();
            StringBuilder answersStringBuilder = new StringBuilder();
            MatchCollection questionInfo;
            
            String completeQuestionInfo;
            String errorMessage;
            String info;
            String romanNumeral;
            String line;

            Double totalResult;
            Double valuePerUnit = 0;
            Decimal convertedNumeral;
            Decimal quantityResult;

            //Variavel para parada de execução da linha, para printar "I have no idea what you are talking about".
            Boolean haveNoIdea;
            //Percorre todas as linhas de perguntas.
            for(int i = 0; i< matchLines.Count; i++)
            {
                line = matchLines[i].Value.Trim();

                quantityResult = 0;
                completeQuestionInfo = "";
                romanNumeral = "";
                haveNoIdea = false;

                errorMessage = IsAQuestionSeterLine(line);
                if(errorMessage == String.Empty)
                {   
                    //Verificação de qual tipo de questão é, de unidades ou de valor de creditos.
                    if (line.ToLower().Contains(" vale "))
                    {   
                        //Variavel que guarda as informações de unidades e recursos;
                        questionInfo = Regex.Matches(line, @"\b(?!(vale|quanto))\b\S+ ");

                        //Percorre cada uma das informações.
                        for(int j = 0; j<questionInfo.Count; j++)
                        {
                            info = questionInfo[j].Value.Trim();

                            //Se se encontra no dicionario de unidades, é uma unidade.
                            if (measureUnitDictionary.ContainsKey(info.Trim()))
                            {
                                romanNumeral += measureUnitDictionary.GetValueOrDefault(info.Trim());
                                //Constroi a string de informações da questão.
                                completeQuestionInfo += info + " ";
                            }
                            else
                            {
                                //Se não está no dicionario de unidades então "I have no idea what you are talking about".
                                haveNoIdea = true;
                                break;
                            }
                        }

                        if (haveNoIdea)
                        {
                            answersStringBuilder.AppendLine("I have no idea what you are talking about");
                        }
                        else
                        {
                            convertedNumeral = romanConverter.RomanToDecimal(romanNumeral);
                            quantityResult += convertedNumeral;
                            
                            //Constroi o resultado nesse StringBuilder.
                            answersStringBuilder.AppendLine(completeQuestionInfo + "is " + Convert.ToString(quantityResult));
                        }
                    }
                    else if (line.ToLower().Contains(" créditos são "))
                    {
                        questionInfo = Regex.Matches(line, @"\b(?!(quantos|créditos|são))\b\S+");

                        for (int k = 0; k<questionInfo.Count; k++)
                        {
                            info = questionInfo[k].Value.Trim();

                            if (measureUnitDictionary.ContainsKey(info.Trim()))
                            {
                                romanNumeral += measureUnitDictionary.GetValueOrDefault(info.Trim());
                                completeQuestionInfo += info + " ";
                            }
                            else if (itemCreditDictionary.ContainsKey(info.Trim()))
                            {   
                                //Pega no dicionario de creditos, o valor da unidade do item.
                                valuePerUnit = itemCreditDictionary.GetValueOrDefault(info.Trim());
                                completeQuestionInfo += info + " ";
                            }
                            else
                            {
                                //Se não é nem item e nem unidade então "I have no idea what you are talking about".
                                haveNoIdea = true;
                                break;
                            }
                        }

                        if (haveNoIdea)
                        {
                            answersStringBuilder.AppendLine("I have no idea what you are talking about");
                        }
                        else
                        {
                            convertedNumeral = romanConverter.RomanToDecimal(romanNumeral);
                            quantityResult += convertedNumeral;

                            totalResult = valuePerUnit * Convert.ToDouble(quantityResult);

                            answersStringBuilder.AppendLine(completeQuestionInfo + "is " + Convert.ToString(totalResult) + " Credits");
                        }
                    }
                    else
                    {   
                        //Caso de algum erro que não foi tratado.
                        throw new Exception("Erro inesperado...");
                    }

                }
                else
                {
                    throw new NotSupportedException(errorMessage);
                }
                
            }

            return answersStringBuilder.ToString();
        }

        /// <summary>
        /// Inicializa os dados dos dicionarios necessarios para a tradução.
        /// </summary>
        /// <param name="fileText"> String contendo os dados do arquivo de texto </param>
        public void SetDictionaries(String fileText)
        {   
            if(measureUnitDictionary == null || measureUnitDictionary.Count <= 0)
            {
                SetMeasureUnitDictionary(fileText);
            }

            if(itemCreditDictionary == null || itemCreditDictionary.Count <= 0)
            {
                SetItemCreditDictionary(fileText);
            }
        }

        /// <summary>
        /// Inicializa os dados do dicionario de unidades de medida <(palvara), (numero romano)>
        /// </summary>
        /// <param name="fileText"> String contendo os dados do arquivo de texto </param>
        public void SetMeasureUnitDictionary(String fileText)
        {
            this.measureUnitDictionary = new Dictionary<String, String>();
            MatchCollection matchLines = Regex.Matches(fileText, @"(.*) is *([IVXLCDM].*)");
            if (matchLines.Count < 1)
            {
                throw new NotSupportedException("Não existem linhas corretamente construidas para setar unidades de medida.");
            }

            String line;
            String errorMessage;

            String[] splitedLine;

            for (int i = 0; i < matchLines.Count; i++)
            {
                line = matchLines[i].Value;
                errorMessage = IsAMeasureUnitSeterLine(line);

                if (errorMessage == String.Empty)
                {
                    splitedLine = line.ToLower().Trim().Split(" is ");

                    //Adiciona no dicionario a palavra correspondente a cada numero romano.
                    this.measureUnitDictionary.Add(splitedLine[0].Trim(), splitedLine[1].ToUpper().Trim());
                }
                else
                {
                    throw new NotSupportedException(errorMessage);
                }
            }
        }

        /// <summary>
        /// Inicializa os dados do dicionario de credito por unidade de cada recurso <(nome do recurso), (valor por unidade)>
        /// </summary>
        /// <param name="fileText"> String contendo os dados do arquivo de texto </param>
        public void SetItemCreditDictionary(String fileText)
        {
            this.itemCreditDictionary = new Dictionary<String, Double>();
            MatchCollection matchLines = Regex.Matches(fileText, @"([a-zA-Z ]*) is (\d+) (Credits)");
            if(matchLines.Count < 1)
            {
                throw new NotSupportedException("Não existem linhas corretamente construidas para setar os creditos dos recursos.");
            }

            RomanNumeralConverter romanConverter = new RomanNumeralConverter();

            String line;
            String item;
            String romanNumeral;
            String errorMessage;

            String[] splitedItem;

            Double quantity;
            Double valuePerUnit;
            Double totalGivenValue;

            Decimal convertedValue = 0;

            for (int i = 0; i < matchLines.Count; i++)
            {
                line = matchLines[i].Value.Trim();
                romanNumeral = "";

                //Verifica se realmente é uma linha que armazena informações de credito dos recursos.
                errorMessage = IsACreditSeterLine(line);
                if (errorMessage == String.Empty)
                {
                    //Setando quantidade para zero para iniciar novo calculo de linha.
                    quantity = 0;

                    //Expressão regular para pegar exatamente o valor total passado entre as strings "is" e "credits".
                    totalGivenValue = Convert.ToDouble(Regex.Match(line.ToLower(), @"(?<=is\s).*(?=\s+credits)").Value);

                    // Separando os itens antes da string "is", ou seja, pagando nome das unidades de medida e do recurso.
                    splitedItem = Regex.Match(line, @".*(?=\s+is)").Value.Trim().Split(' ');

                    //Percorrendo cada item, verificando quais palavras são as unidades de medida e quais são recursos.
                    for (int j = 0; j < splitedItem.Length; j++)
                    {
                        item = splitedItem[j].Trim();

                        if (measureUnitDictionary.ContainsKey(item.Trim()))
                        {
                            romanNumeral += measureUnitDictionary.GetValueOrDefault(item.Trim());
                        }
                        else
                        {   
                            convertedValue = romanConverter.RomanToDecimal(romanNumeral);
                            quantity += Convert.ToDouble(convertedValue);

                            valuePerUnit = totalGivenValue / quantity;

                            //Adiciona no dicionario o nome do recurso e seus creditos por unidade.
                            this.itemCreditDictionary.Add(item, valuePerUnit);

                            //Se não for o ultimo item, significa que foi passado alguma unidade inválida
                            if (j != (splitedItem.Length - 1))
                            {
                                throw new NotSupportedException(String.Format("Linha {0} de informação para os creditos é inválida, pois existem informações desorganizadas.", i+1));
                            }
                        }
                    }

                    //Se nada foi convertido significa que todos dados passados são unidades, e não foi passado o nome de um recurso/metal.
                    if(convertedValue == 0)
                    {
                        throw new NotSupportedException(String.Format("Linha {0} de informação para os creditos é inválida, pois todos os dados são unidades.", i+1));
                    }

                }
                else
                {
                    throw new NotSupportedException(errorMessage);
                }
            }

        }
        
        /// <summary>
        /// Verifica se a respectiva linha passada como parametro é uma 
        /// linha onde são setados valores de unidades de medidas.
        /// </summary>
        /// <param name="line">String contendo apenas a linha do arquivo de texto a ser analisada</param>
        /// <returns> String contendo a mensagem de erro (caso ocorra algum erro) </returns>
        private String IsAMeasureUnitSeterLine(String line)
        {
            String errorMessage = String.Empty;
            String regexValue;
            RomanNumeralConverter numeralConverter = new RomanNumeralConverter();

            //Verifica se existe um espaço entre dois valores romanos, se sim é inválido.
            regexValue = Regex.Match(line.ToLower().Trim(), @"(?<=is\s).*").Value.Trim();
            if (regexValue.Contains(" "))
            {
                errorMessage = String.Format("Linha de unidade de medida inválida, pois existe mais de um valor romano.\nTrecho inválido: {0}", regexValue);
            }

            /* Devem existir apenas uma ocorrencia da string "is" para cada linha.
             * caso exista mais de uma ocorrecia não é uma linha que seta unidades.*/
            if (Regex.Matches(line.ToLower().Trim(), " is ").Count != 1)
            {
                errorMessage = "Linha de unidade de medida inválida. Linha com mais de uma ocorrecia da palavra \"is\".";
            }

            //Verifica se o numero romano depois da string "is" é um numero romano válido.
            regexValue = Regex.Match(line.ToLower().Trim(), @"(?<=is\s).*").Value.Trim();
            if (!numeralConverter.IsARomanNumeral(regexValue))
            {
                errorMessage = String.Format("Linha de unidade de medida inválida.\nNumero romano passado inválido {0}.", regexValue);
            }

            return errorMessage;
        }

        /// <summary>
        /// Verifica se a respectiva linha passada como parametro é uma 
        /// linha onde são setados valores de creditos dos recursos.
        /// </summary>
        /// <param name="line">String contendo apenas a linha do arquivo de texto a ser analisada</param>
        /// <returns> String contendo a mensagem de erro (caso ocorra algum erro) </returns>
        private String IsACreditSeterLine(String line)
        {
            String errorMessage = String.Empty;
            String regexValue;

            /* Se não existir um valor e uma ocorrencia da string "credits" depois da 
             * string "is" na linha não é uma linha que seta os creditos. */
            if (!Regex.Match(line.ToLower().Trim(), @"(\d+) (credits)").Success)
            {
                errorMessage = "Linha de informação para os creditos inválida.\nA Linha deve seguir seguinte estrutura: \"<UNIDADES> <RECURSO> is <VALOR> Credits.\"";
            }

            /* Devem existir apenas uma ocorrencia da string "is" para cada linha.
             * caso exista mais de uma ocorrecia não é uma linha que seta creditos.*/
            if (Regex.Matches(line.ToLower().Trim(), " is ").Count != 1)
            {
                errorMessage = "A Linha de informação para os creditos inválida.\nLinha com mais de uma ocorrecia da palavra \"is\".";
            }

            //Caso existam mais de um numero separado por espaço na linha entre o "is" e o "Credits".
            regexValue = Regex.Match(line.ToLower().Trim(), @"(?<=is\s).*(?=\s+credits)").Value.Trim();
            if (regexValue.Contains(" "))
            {
                errorMessage = String.Format("A Linha de informação para os creditos é inválida, pois existem mais de um valor total.\n Trecho inválido: {0}", regexValue);
            }

            return errorMessage;
        }

        /// <summary>
        /// Verifica se a respectiva linha passada como parametro é uma linha 
        /// onde são setados dados para as questões passadas no arquivo de texto.
        /// </summary>
        /// <param name="line">String contendo apenas a linha do arquivo de texto a ser analisada</param>
        /// <returns> String contendo a mensagem de erro (caso ocorra algum erro) </returns>
        private String IsAQuestionSeterLine(String line)
        {
            String errorMessage = String.Empty;
            String regexValue;

            // Dados depois do ponto de interrogação.
            regexValue = Regex.Match(line.ToLower().Trim(), @"(?<=\?\s).*").Value;
            if ((Regex.Match(line.ToLower().Trim(), @"(?<=\?\s).*").Value.Length > 0)
                    && (!Regex.Match(line.ToLower().Trim(), @"(?<=\?\s).*").Value.Contains(" ")
                    && !Regex.Match(line.ToLower().Trim(), @"(?<=\?\s).*").Value.Contains("\n")))
            {
                errorMessage = String.Format("Questão inválida pois existem dados depois do ponto de interrogação. Trecho inválido: {0}", regexValue);
            }

            //Se não houve erro até agora segue as verificações.
            if(errorMessage == String.Empty)
            {   
                //Valida conforme o tipo de pergunta.
                if (line.ToLower().Contains(" vale "))
                {
                    if (line.ToLower().Contains(" créditos são "))
                    {
                        errorMessage = "Questão inválida, tipo de questão mal definido.\nEstrutura de questão: \"<quanto/quantos créditos> <vale/são> <UNIDADES> <RECURSO>\".";
                    }

                    if (Regex.Matches(line.ToLower().Trim(), " vale ").Count != 1)
                    {
                        errorMessage = "Questão inválida numero de strings \"vale\" maior que um.";
                    }

                }
                else if (line.ToLower().Contains(" créditos são "))
                {
                    if (line.ToLower().Contains(" vale "))
                    {
                        errorMessage = "Questão inválida, tipo de questão mal definido.\nEstrutura de questão: \"<quanto/quantos créditos> <vale/são> <UNIDADES> <RECURSO>\".";
                    }

                    if (Regex.Matches(line.ToLower().Trim(), " créditos são ").Count != 1)
                    {
                        errorMessage = "Questão inválida numero de strings \"créditos são\" maior que um.";
                    }

                }
                else
                {
                    errorMessage = "Tipo inválido de questão.";
                }
            }
            
            return errorMessage;
        }

    }
}
