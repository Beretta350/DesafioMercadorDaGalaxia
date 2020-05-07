## Como executar?

Navegue até a pasta do executavel pelo terminal `bin\Release\netcoreapp3.1` e execute `DesafioSelecaoMercadorGalaxia.exe <"caminho_do_arquivo.txt">`.

## Definição do problema:

Você decidiu abandonar o planeta Terra de vez, após o último colapso ecológico do planeta. Com os recursos que você possui, você pode comprar uma nave espacial, deixar a Terra e voar por toda a galáxia para vender metais de vários tipos.

Comprar e vender por toda a galáxia exige que você converta números e unidades, logo você decidiu escrever um programa para ajudá-lo.

Os números usados nas transações intergalácticas seguem convenção similar a dos numerais romanos, e devem ser traduzidos para que as transações possam se realizar.

### Sobre os numerais romanos:
Numerais romanos são baseados em sete símbolos.

<p align="center">
  <table>
  <thead>
    <th align="center">Símbolo</th>
    <th align="center">Valor</th>
  </thead>
  <tbody>
    <tr>
      <td align="center">I</td>
      <td align="center">1</td>
    </tr>
    <tr>
      <td align="center">V</td>
      <td align="center">5</td>
    </tr>
    <tr>
      <td align="center">X</td>
      <td align="center">10</td>
    </tr>
    <tr>
      <td align="center">L</td>
      <td align="center">50</td>
    </tr>
    <tr>
      <td align="center">C</td>
      <td align="center">100</td>
    </tr>
    <tr>
      <td align="center">D</td>
      <td align="center">500</td>
    </tr>
    <tr>
      <td align="center">M</td>
      <td align="center">1000</td>
    </tr>
  </tbody>
</table>
</p>

Números são formados combinando-se símbolos e adicionando-se valores. Por exemplo, MMVI é 1000+1000+5+1=2006. Geralmente, símbolos são colocados em ordem de valor, começando com os valores maiores. Quando um valor menor precede um valor maior, os valores menores são subtraídos dos valores maiores, e o resultado é adicionado ao total. Por exemplo, MCMXLIV = 1000 + (1000 – 100) + (50 – 10) + (5 – 1) = 1944.

Os símbolos “I”, “X”, “C”, e “M” podem ser repetidos, no máximo, 3 vezes em sucessão (não mais do que 3. Eles podem aparecer mais vezes se o terceiro e quarto são separados por um valor menor, como em XXXIX). “D”, “L” e “V” nunca podem ser repetidos. “I” pode ser subtraído somente do “V” e do “X”. “X” pode ser subtraído somente do “L” e do “C”. “C” pode ser subtraído do “D” e do “M” somente. “V”, “L” e “D” não podem ser subtraídos de nenhum símbolo. Somente um símbolo de valor menor pode ser subtraído de qualquer símbolo de valor maior válido.

Um número escrito em numerais arábicos (nossos números) podem ser quebrados em dígitos. Por exemplo, 1903 é composto de 1, 9, 0 e 3. Para escrever o numeral romano, cada um dos dígitos diferentes de zero deve ser tratado separadamente. No exemplo acima, 1000 = M, 900 = CM, e 3 = III. Assim, 1903 = MCMIII (Fonte: Wikipedia, http://en.wikipedia.org/wiki/Roman_numerals).

### Entradas e saidas do programa:

As entradas para o seu programa consistem de linhas de texto em um arquivo texto detalhando as suas anotações sobre a conversão entre unidades intergalácticas e numerais romanos. Você deve lidar com anotações inválidas de forma apropriada e gerar a saída na tela.

Como se pode ver abaixo, as entradas podem ter até 7 linhas iniciais indicando a notação intergaláctica dos símbolos romanos, seguida de 0 ou mais linhas indicando o valor em créditos do número de unidades (expresso em intergaláctico) de metal sendo vendido. Na sequência, linhas com perguntas “quanto vale” / ”quantos créditos são”. Na última linha, um exemplo do que deve acontecer com uma anotação inválida. 

Desenvolva o programa que processa estas entradas e gera estas saídas.

#### Exemplos:
##### Entradas (de Teste)
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

##### Saídas (do Teste)
pish tegj glob glob is 42<br>
glob prok Silver is 68 Credits<br>
glob prok Gold is 57800 Credits<br>
glob prok Iron is 782 Credits<br>
I have no idea what you are talking about<br>
