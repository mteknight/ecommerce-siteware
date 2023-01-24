# ecommerce-siteware

A Siteware é uma empresa cujo principal produto é uma plataforma Software
as a Service (SaaS), construída seguindo o framework ASP.NET MVC, juntamente
com a abordagem Domain-Driven Design (DDD) e utilizando padrões de projetos já
difundidos pela comunidade de desenvolvimento de software. Para a camada de
infraestrutura, é utilizado um ORM para comunicação com um banco de dados
relacional. Dentro dessa plataforma, o produto é subdividido em diversos módulos.

Nesta etapa você precisa desenvolver o protótipo de uma nova
funcionalidade para o nosso módulo de e-commerce, que consiste basicamente de
uma Loja Virtual para produtos diversos. Mesmo sendo um protótipo, é desejável
que você utilize as boas práticas de desenvolvimento que você conhece e achar
necessário.

### Critérios de aceitação
- Deve existir um CRUD de produtos;
- Cada produto deve ter ao menos as seguintes informações: nome e preço;
- Duas promoções já devem existir no sistema: “Leve 2 e Pague 1” e “3 por
R$10,00”;
- No cadastro de um produto, deve ser possível associá-lo a uma promoção
existente ou a nenhuma delas;
- As informações do produto precisam ser persistidas;
- Deve ser possível adicionar, remover e editar itens do carrinho de compras;
- O valor total do carrinho de compras deve ser calculado com base nos
produtos selecionados considerando suas respectivas promoções;
- Devem estar descritas as promoções aplicadas para cada item, caso existam;

Obs: você tem total liberdade para utilizar as tecnologias que julgar
necessárias para o desenvolvimento do protótipo. Ou seja, não é necessário utilizar
as linguagens e frameworks utilizados pela​ ​Siteware​.

É necessário que seja entregue um artefato no qual seja possível validar os
itens contidos nos critérios de aceitação.

Veja esboço do resultado esperado abaixo. Este é somente um exemplo,
então sinta-se livre para propor a solução que você achar mais adequada.

| Item | Quantidade | Preço | Promoção |
|-----|-----------|-------|-----------|
| A    |     3     |  10   | 3 por 10 |
| B    |     2     |  25   | Leve 2 e Pague 1 |
| C    |     5     |  20   | - |
|-----|-----------|-------|-----------|
| Total|           |  55   |  |

## Observações
1. Conforme enunciado, o sistema de e-commerce já existe. Entretanto, por não fazer ideia da arquitetura existente, fica impossível desenvolver algo que possa ser transposto facilmente. Nesse caso, optei por elaborar uma arquitetura para DDD e clean architecture fazendo bom uso de patterns, principalmente composição e fábrica. Pela simplicidade do enunciado, parece um exagero de engenharia, entretanto, considerando o dinamismo arquitetural será muito mais simples adicionar novos objetos de domínio e suas operações de CRUD (ou de outros tipos) no modelo criado.
Ressalto que, soubesse dos detalhes arquiteturais da solução existente, aderiria a este ao invés de refazer toda a arquitetura, naturalmente.
2. Como pode ser observado nos primeiros commits (~10), o código foi feito em TDD passo a passo para demonstração. No intuito de agilizar o trabalho, os commits seguintes não demonstram mais isso. Contudo, a implementação inteira foi realizada em TDD!
3. Na persistência foi usado o DbContext do EF em memória. Normalmente, ele deve ser usado apenas para realizar testes, mas para este exercício serviu para facilitar a persistência sem banco.
4. Dados os critérios de aceitação, acredito que estejamos falando de uma Feature completa, onde é possível repartir em várias Tasks menores. Tendo em vista o tempo necessário para implementação completa da funcionalidade, não foi possível implementar tudo.
Foquei no TDD, evolução do domínio, seus serviços e a arquitetura em geral para prover uma solução principalmente com o CRUD de Produto e suas respectivas Promoções. Podemos aproveitar a próxima reunião para discutir como seria a implementação do restante do CRUD de Produtos e do Carrinho de compras.
