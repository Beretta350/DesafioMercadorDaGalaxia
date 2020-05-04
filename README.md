<h2> Como executar? </h2>
Basta ir no seu prompt de comando, acessar a pasta do executavel e escrever DesafioSelecaoMercadorGalaxia.exe <"caminho_do_arquivo.txt">

<h2>Definição do problema:</h2>

Você decidiu abandonar o planeta Terra de vez, após o último colapso ecológico do planeta. Com os recursos que você possui, você pode comprar uma nave espacial, deixar a Terra e voar por toda a galáxia para vender metais de vários tipos.Comprar e vender por toda a galáxia exige que você converta números e unidades, logo você decidiu escrever um programa para ajudá-lo. Os números usados nas transações intergalácticas seguem convenção similar a dos numerais romanos, e devem ser traduzidos para que as transações possam se realizar.

<h3>Sobre os numerais romanos:</h3>
Numerais romanos são baseados em sete símbolos.
<br>
<table>
  <thead>
    <th>Símbolo</th>
    <th>Valor</th>
  </thead>
  <tbody>
    <tr>
      <td>I</td>
      <td>1</td>
    </tr>
    <tr>
      <td>V</td>
      <td>5</td>
    </tr>
    <tr>
      <td>X</td>
      <td>10</td>
    </tr>
    <tr>
      <td>L</td>
      <td>50</td>
    </tr>
    <tr>
      <td>C</td>
      <td>100</td>
    </tr>
    <tr>
      <td>D</td>
      <td>500</td>
    </tr>
    <tr>
      <td>M</td>
      <td>1000</td>
    </tr>
  </tbody>
</table>
<br>
Números são formados combinando-se símbolos e adicionando-se valores. Por exemplo, MMVI é 1000+1000+5+1=2006. Geralmente, símbolos são colocados em ordem de valor, começando com os valores maiores. Quando um valor menor precede um valor maior, os valores menores são subtraídos dos valores maiores, e o resultado é adicionado ao total. Por exemplo, MCMXLIV = 1000 + (1000 – 100) + (50 – 10) + (5 – 1) = 1944.Os símbolos “I”, “X”, “C”, e “M” podem ser repetidos, no máximo, 3 vezes em sucessão (não mais do que 3. Eles podem aparecer mais vezes se o terceiro e quarto são separados por um valor menor, como em XXXIX). “D”, “L” e “V” nunca podem ser repetidos. “I” pode ser subtraído somente do “V” e do “X”. “X” pode ser subtraído somente do “L” e do “C”. “C” pode ser subtraído do “D” e do “M” somente. “V”, “L” e “D” não podem ser subtraídos de nenhum símbolo. Somente um símbolo de valor menor pode ser subtraído de qualquer símbolo de valor maior válido. Um número escrito em numerais arábicos (nossos números) podem ser quebrados em dígitos. Por exemplo, 1903 é composto de 1, 9, 0 e 3. Para escrever o numeral romano, cada um dos dígitos diferentes de zero deve ser tratado separadamente. No exemplo acima, 1000 = M, 900 = CM, e 3 = III. Assim, 1903 = MCMIII (Fonte: Wikipedia, http://en.wikipedia.org/wiki/Roman_numerals).

<h3>Entradas e saidas do programa:</h3>
As entradas para o seu programa consistem de linhas de texto em um arquivo texto detalhando as suas anotações sobre a conversão entre unidades intergalácticas e numerais romanos. Você deve lidar com anotações inválidas de forma apropriada e gerar a saída na tela.Como se pode ver abaixo, as entradas podem ter até 7 linhas iniciais indicando a notação intergaláctica dos símbolos romanos, seguida de 0 ou mais linhas indicando o valor em créditos do número de unidades (expresso em intergaláctico) de metal sendo vendido. Na sequência, linhas com perguntas “quanto vale” / ”quantos créditos são”. Na última linha, um exemplo do que deve acontecer com uma anotação inválida. Desenvolva o programa que processa estas entradas e gera estas saídas.

<h4>Exemplos:</h4>
<br>
<h5>Entradas (de Teste)</h5>
<br>
glob is I<br>
prok is V<br>
pish is X<br>
tegj is L<br>
glob glob Silver is 34 Credits<br>
glob prok Gold is 57800 Credits<br>
pish pish Iron is 3910 Credits<br>
quanto vale pish tegj glob glob ?<br>
quantos créditos são glob prok Silver ?<br>
quantos créditos são glob prok Gold ?<br>
quantos créditos são glob prok Iron ?<br>
quanto vale wood could woodchuck mood ?<br>

<h5>Saídas (do Teste)</h5>
<br>
pish tegj glob glob is 42<br>
glob prok Silver is 68 Credits<br>
glob prok Gold is 57800 Credits<br>
glob prok Iron is 782 Credits<br>
I have no idea what you are talking about<br>
